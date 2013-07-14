using System;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.Temperature;
using ControlThinkTemperature = ControlThink.ZWave.Devices.Temperature;
using ControlThinkTemperatureScale = ControlThink.ZWave.Devices.TemperatureScale;
using ControlThinkSetpointType = ControlThink.ZWave.Devices.ThermostatSetpointType;
using ControlThinkThermostatMode = ControlThink.ZWave.Devices.ThermostatMode;
using ControlThinkFanMode = ControlThink.ZWave.Devices.ThermostatFanMode;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public static class ConverstionExtensions
    {
        public static ControlThinkSetpointType ToControlThinkType(this SetpointType input)
        {
            switch (input)
            {
                case SetpointType.Heat:
                    return ControlThinkSetpointType.Heating1;

                case SetpointType.Cool:
                    return ControlThinkSetpointType.Cooling1;

                default:
                    throw new ArgumentException("Could not parse type");
            }
        }

        public static ControlThinkTemperature ToControlThinkType(this ITemperature input)
        {
            decimal value;
            ControlThinkTemperatureScale scale;

            if (input is FahrenheitTemperature)
            {
                value = (int)Math.Round(input.Value);
                scale = ControlThinkTemperatureScale.Fahrenheit;
            }
            else
            {
                value = (int)(Math.Round(input.Celsius.Value));
                scale = ControlThinkTemperatureScale.Celsius;
            }

            var result = new ControlThinkTemperature(value, scale);

            return result;
        }
        
        public static ITemperature ToRoomieType(this ControlThinkTemperature input)
        {
            ITemperature result;

            var value = Convert.ToDouble(input.Value);

            switch (input.Scale)
            {
                case ControlThinkTemperatureScale.Celsius:
                    result = new CelsiusTemperature(value);
                    break;

                case ControlThinkTemperatureScale.Fahrenheit:
                    result = new FahrenheitTemperature(value);
                    break;
                    
                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
        }

        public static ControlThinkThermostatMode ToControlThinkType(this ThermostatMode input)
        {
            ControlThinkThermostatMode result;

            switch (input)
            {
                case ThermostatMode.Off:
                    result = ControlThinkThermostatMode.Off;
                    break;

                case ThermostatMode.Heat:
                    result = ControlThinkThermostatMode.Heat;
                    break;

                case ThermostatMode.Cool:
                    result = ControlThinkThermostatMode.Cool;
                    break;

                case ThermostatMode.FanOnly:
                    result = ControlThinkThermostatMode.FanOnly;
                    break;

                case ThermostatMode.Auto:
                    result = ControlThinkThermostatMode.Auto;
                    break;

                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
        }

        public static ControlThinkFanMode ToControlThinkType(this FanMode fanMode)
        {
            switch (fanMode)
            {
                case FanMode.Auto:
                    return ControlThinkFanMode.AutoLow;

                case FanMode.On:
                    return ControlThinkFanMode.OnLow;

                default:
                    throw new ArgumentException("Could not parse type");

            }
        }
    }
}
