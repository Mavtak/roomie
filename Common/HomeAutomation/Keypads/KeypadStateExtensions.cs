﻿using System.Linq;
using System.Text;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.Common.HomeAutomation.Keypads
{
    public static class KeypadStateExtensions
    {
        public static IKeypadState Copy(this IKeypadState state)
        {
            return ReadOnlyKeypadState.CopyFrom(state);
        }

        public static string Describe(this IKeypadState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            var buttons = state.Buttons.Describe();
            if (buttons != null)
            {
                result.Append(buttons);
            }

            return result.ToString();
        }

        public static XElement ToXElement(this IKeypadState state, string nodeName = "Keypad")
        {
            var result = new XElement(nodeName);

            if (state.Buttons != null)
            {
                var nodes = state.Buttons.Select(x => x.ToXElement());
                
                foreach (var node in nodes)
                {
                    if (node.Attributes().Any() || node.Ancestors().Any())
                    {
                        result.Add(node);
                    }
                }
            }
            
            return result;
        }

        public static IKeypadState ToKeypad(this XElement element)
        {
            return ReadOnlyKeypadState.FromXElement(element);
        }

        public static IKeypadState Changes(this IKeypadState newState, IKeypadState oldState)
        {
            var buttons = newState.Buttons.Changes(oldState.Buttons);

            var result = new ReadOnlyKeypadState(buttons);

            return result;
        }
    }
}
