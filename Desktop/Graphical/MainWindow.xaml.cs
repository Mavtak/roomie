using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;

namespace Roomie.Desktop.Graphical
{
    public partial class MainWindow
    {
        private readonly RoomieEngine _engine;
        public ObservableCollection<RoomieEvent> Events;

        public MainWindow()
        {
            InitializeComponent();
            _engine = new RoomieEngine();
            Events = new ObservableCollection<RoomieEvent>();
            _engine.ScriptMessageSent += engine_ScriptMessageSent;
            _engine.EngineStateChanged += _engine_EngineStateChanged;
            EventListing.ItemsSource = Events;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var exception = unhandledExceptionEventArgs.ExceptionObject;

            using (var log = File.AppendText("exceptions.log"))
            {
                log.WriteLine();
                log.WriteLine(DateTime.Now);
                log.WriteLine(exception);
                log.Flush();
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _engine.Start();
        }

        void _engine_EngineStateChanged(object sender, Engine.Delegates.EngineStateChangedEventArgs e)
        {
            if (e.NewState == EngineState.ShutDown && _shuttingDown)
            {
                var thread = new Thread(() =>
                    {
                        Print("Goodbye!");
                        Thread.Sleep(1000);
                        Print("I'll miss you!");
                        Thread.Sleep(500);
                        Print("Okay goodbye. :'c");
                        Thread.Sleep(250);
                        Dispatcher.InvokeAsync(Close);
                    });

                thread.Start();
            }
        }

        private bool _shuttingDown = false;
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_engine.EngineState != EngineState.ShutDown)
            {
                _shuttingDown = true;
                _engine.Shutdown();
                e.Cancel = true;

                return;
            }

            //TODO: remove need for this (hint: better thread management.)
            var task = new Thread(() =>
                {
                    Environment.Exit(0);
                });
            task.Start();
        }

        void engine_ScriptMessageSent(object sender, RoomieThreadEventArgs e)
        {
            var roomieEvent = new RoomieEvent
            {
                TimeStamp = DateTime.Now,
                Thread = e.Thread.Name,
                Message = e.Message
            };

            Action call = () =>
            {
                var nothingSelected = EventListing.SelectedIndex == -1;
                var lastItemSelected = EventListing.SelectedIndex == EventListing.Items.Count - 1;

                Events.Add(roomieEvent);

                if (nothingSelected || lastItemSelected)
                {
                    ScrollToEnd();
                }
            };

            Dispatcher.BeginInvoke(call);
        }

        private void ScrollToEnd()
        {
            //http://stackoverflow.com/questions/1027051/how-to-autoscroll-on-wpf-datagrid
            EventListing.SelectedIndex = EventListing.Items.Count - 1;
            EventListing.ScrollIntoView(EventListing.SelectedItem);
            EventListing.SelectedIndex = -1;
        }

        #region run script from input

        private void RunInput(bool clearInput = true)
        {
            var script = ScriptCommandList.FromText(Input.Text);
            if (clearInput)
            {
                Input.Clear();
            }
            _engine.Threads.AddCommands(script);
        }

        private void Input_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var enter = e.Key == Key.Return;
            var shift = e.KeyboardDevice.Modifiers == ModifierKeys.Shift;
            if (enter && !shift)
            {
                e.Handled = true;
                RunInput();
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            RunInput(false);
        }

        #endregion

        private void EventListing_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ScrollToEnd();
            }
        }

        private void Print(string text)
        {
            _engine.Threads.Print(text);
        }
        
    }

    public class RoomieEvent
    {
        public DateTime TimeStamp { get; set; }
        public string Thread { get; set; }
        public string Message { get; set; }
    }
}
