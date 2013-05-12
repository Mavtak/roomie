using System.Windows.Controls;
using System.Windows.Media;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Roomie.Desktop.Graphical
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _engine.Start();
        }

        void _engine_EngineStateChanged(object sender, Engine.Delegates.EngineStateChangedEventArgs e)
        {
            if (e.NewState == EngineState.ShutDown && _shuttingDown)
            {
                Dispatcher.InvokeAsync(Close);
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
            }
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
                    bool nothingSelected = EventListing.SelectedIndex == -1;
                    bool lastItemSelected = EventListing.SelectedIndex == EventListing.Items.Count - 1;

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

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {

        }

        #region run script from input

        private void RunInput(bool clearInput = true)
        {
            var script = ScriptCommandList.FromText(Input.Text);
            if (clearInput)
            {
                Input.Clear();
            }
            _engine.RootThread.AddCommands(script);
        }

        private void Input_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var enter = e.Key == Key.Return;
            var shift = e.KeyboardDevice.Modifiers == ModifierKeys.Shift;
            if (enter && !shift)
            {
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

        
    }

    public class RoomieEvent
    {
        public DateTime TimeStamp { get; set; }
        public string Thread { get; set; }
        public string Message { get; set; }
    }
}
