using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlThink.ZWave.Devices;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public static class ConverstionExtensions
    {
        public static ThermostatSetpointType ToControlThinkType(this SetpointType input)
        {
            switch (input)
            {
                case SetpointType.Heat:
                    return ThermostatSetpointType.Heating1;

                case SetpointType.Cool:
                    return ThermostatSetpointType.Cooling1;

                default:
                    throw new ArgumentException("Could not parse type");
            }
        }

        public static Temperature ToControlThinkType(this ITemperature input)
        {
            decimal value;
            TemperatureScale scale;

            if (input is FahrenheitTemperature)
            {
                value = (int)Math.Round(input.Value);
                scale = TemperatureScale.Fahrenheit;
            }
            else
            {
                value = (int)(Math.Round(input.Celsius.Value));
                scale = TemperatureScale.Celsius;
            }

            var result = new Temperature(value, scale);

            return result;
        }

        
        public static ITemperature ToRoomieType(this Temperature input)
        {
            ITemperature result;

            var value = Convert.ToDouble(input.Value);

            switch (input.Scale)
            {
                case TemperatureScale.Celsius:
                    result = new CelsiusTemperature(value);
                    break;

                case TemperatureScale.Fahrenheit:
                    result = new FahrenheitTemperature(value);
                    break;
                    
                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
        }

        public static ThermostatFanMode ToControlThinkType(this FanMode fanMode)
        {
            switch (fanMode)
            {
                case FanMode.Auto:
                    return ThermostatFanMode.AutoLow;

                case FanMode.On:
                    return ThermostatFanMode.OnLow;

                default:
                    throw new ArgumentException("Could not parse type");

            }
        }
    }
}
