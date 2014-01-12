using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.Keypads.Buttons
{
    public static class KeypadButtonStateExtensions
    {
        public static IKeypadButtonState Copy(this IKeypadButtonState state)
        {
            return ReadOnlyKeypadButtonState.CopyFrom(state);
        }

        public static IEnumerable<IKeypadButtonState> Copy(this IEnumerable<IKeypadButtonState> states)
        {
            var result = states.Select(x => x.Copy());
            result = result.ToArray();

            return result;
        }

        public static string Describe(this IKeypadButtonState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            result.Append(state.Id);
            result.Append("=");

            switch (state.Pressed)
            {
                case true:
                    result.Append("Pressed");
                    break;
                    
                case false:
                    result.Append("Released");
                    break;

                default:
                    result.Append("?");
                    break;
            }

            return result.ToString();
        }

        public static string Describe(this IEnumerable<IKeypadButtonState> states)
        {
            var result = new StringBuilder();

            var descriptions = states.Select(x => x.Describe());

            foreach(var description in descriptions)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append(description);
            }

            return result.ToString();
        }

        public static XElement ToXElement(this IKeypadButtonState state, string nodeName = "Button")
        {
            var result = new XElement(nodeName);

            if (state.Id != null)
            {
                result.Add(new XAttribute("Id", state.Id));
            }

            if (state.Pressed != null)
            {
                result.Add(new XAttribute("Pressed", state.Pressed));
            }

            return result;
        }

        public static IKeypadButtonState ToButton(this XElement element)
        {
            return ReadOnlyKeypadButtonState.FromXElement(element);
        }

        public static IEnumerable<IKeypadButtonState> ToButtons(this IEnumerable<XElement> element)
        {
            var result = element.Select(ReadOnlyKeypadButtonState.FromXElement);
            result = result.ToArray();

            return result;
        }

        public static bool Changed(this IKeypadButtonState current, IKeypadButtonState previous)
        {
            if (!string.Equals(current.Id, previous.Id))
            {
                throw new ArgumentException("Button " + previous.Id + " does not match button " + current.Id);
            }

            if (previous.Pressed == null)
            {
                return current.Pressed == true;
            }

            return current.Pressed != previous.Pressed;
        }

        public static IEnumerable<IKeypadButtonState> Changes(this IEnumerable<IKeypadButtonState> newState, IEnumerable<IKeypadButtonState> oldState)
        {
            var result = newState.Where(@new =>
                {
                    var old = oldState.FirstOrDefault(x => x.Id == @new.Id);

                    if (old == null)
                    {
                        old = new ReadOnlyKeypadButtonState(@new.Id, null);
                    }

                    return @new.Changed(old);
                }).ToArray();

            return result;
        }

        public static IKeypadButtonState Get(this IEnumerable<IKeypadButtonState> buttonsStates, string id)
        {
            var result = buttonsStates.First(b => b.Id == id);

            return result;
        }
    }
}
