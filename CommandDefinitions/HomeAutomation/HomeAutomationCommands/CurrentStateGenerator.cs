using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.Measurements.Power;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class CurrentStateGenerator
    {
        private readonly List<Tuple<IPower, string>> _steps;

        public CurrentStateGenerator()
        {
            _steps = new List<Tuple<IPower, string>>();
        }

        public string Generate(BinarySwitchPower? power, IPower value)
        {
            if (power != BinarySwitchPower.On || value == null)
            {
                return null;
            }

            foreach (var step in _steps)
            {
                //TODO: add comparison to IPower interface
                if (value.Value <= step.Item1.Value)
                {
                    return step.Item2;
                }
            }

            return null;
        }

        public void AddStep(IPower value, string name)
        {
            _steps.Add(new Tuple<IPower, string>(value, name));

            _steps.Sort((x, y) => x.Item1.Value.CompareTo(y.Item1.Value));
        }
    }
}
