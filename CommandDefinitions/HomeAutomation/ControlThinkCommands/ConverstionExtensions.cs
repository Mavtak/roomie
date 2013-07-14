﻿using System;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.Temperature;
using ControlThinkTemperature = ControlThink.ZWave.Devices.Temperature;
using ControlThinkTemperatureScale = ControlThink.ZWave.Devices.TemperatureScale;
using ControlThinkSetpointType = ControlThink.ZWave.Devices.ThermostatSetpointType;
using ControlThinkThermostatMode = ControlThink.ZWave.Devices.ThermostatMode;
using ControlThinkFanMode = ControlThink.ZWave.Devices.ThermostatFanMode;
using ThermostatFanMode = Roomie.Common.HomeAutomation.Thermostats.Fans.ThermostatFanMode;
using ThermostatMode = Roomie.Common.HomeAutomation.Thermostats.ThermostatMode;
using ControlThinkThermostatCurrentAction = ControlThink.ZWave.Devices.ThermostatOperatingState;
using ControlThinkFanState = ControlThink.ZWave.Devices.ThermostatFanState;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    //TODO: unit test all of this
    //TODO: handle econ types
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

        public static SetpointType ToRoomieType(this ControlThinkSetpointType input)
        {
            SetpointType result;

            switch (input)
            {
                case ControlThinkSetpointType.Heating1:
                    result = SetpointType.Heat;
                    break;

                case ControlThinkSetpointType.Cooling1:
                    result = SetpointType.Cool;
                    break;

                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
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

        public static ThermostatMode ToRoomieType(this ControlThinkThermostatMode input)
        {
            ThermostatMode result;

            switch (input)
            {
                case ControlThinkThermostatMode.Off:
                    result = ThermostatMode.Off;
                    break;

                case ControlThinkThermostatMode.Heat:
                    result = ThermostatMode.Heat;
                    break;

                case ControlThinkThermostatMode.Cool:
                    result = ThermostatMode.Cool;
                    break;

                case ControlThinkThermostatMode.FanOnly:
                    result = ThermostatMode.FanOnly;
                    break;

                case ControlThinkThermostatMode.Auto:
                    result = ThermostatMode.Auto;
                    break;

                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
        }

        public static ControlThinkFanMode ToControlThinkType(this ThermostatFanMode fanMode)
        {
            switch (fanMode)
            {
                case ThermostatFanMode.Auto:
                    return ControlThinkFanMode.AutoLow;

                case ThermostatFanMode.On:
                    return ControlThinkFanMode.OnLow;

                default:
                    throw new ArgumentException("Could not parse type");

            }
        }

        public static ThermostatFanMode ToRoomieType(this ControlThinkFanMode input)
        {
            ThermostatFanMode result;

            switch (input)
            {
                case ControlThinkFanMode.AutoLow:
                case ControlThinkFanMode.AutoHigh:
                    result = ThermostatFanMode.Auto;
                    break;

                case ControlThinkFanMode.OnLow:
                case ControlThinkFanMode.OnHigh:
                    result = ThermostatFanMode.On;
                    break;

                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
        }

        public static ThermostatCurrentAction ToRoomieType(this ControlThinkThermostatCurrentAction input)
        {
            ThermostatCurrentAction result;

            switch (input)
            {
                case ControlThinkThermostatCurrentAction.Idle:
                    result = ThermostatCurrentAction.Idle;
                    break;

                case ControlThinkThermostatCurrentAction.Heating:
                case ControlThinkThermostatCurrentAction.PendingHeat:
                    result = ThermostatCurrentAction.Heating;
                    break;

                case ControlThinkThermostatCurrentAction.FanOnly:
                case ControlThinkThermostatCurrentAction.PendingCool:
                    result = ThermostatCurrentAction.FanOnly;
                    break;

                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
        }

        public static ThermostatFanCurrentAction ToRoomieType(this ControlThinkFanState input)
        {
            ThermostatFanCurrentAction result;

            switch (input)
            {
                case ControlThinkFanState.Idle:
                    result = ThermostatFanCurrentAction.Idle;
                    break;

                case ControlThinkFanState.RunningLow:
                case ControlThinkFanState.RunningHigh:
                    result = ThermostatFanCurrentAction.On;
                    break;

                default:
                    throw new ArgumentException("Could not parse type");
            }

            return result;
        }
    }
}
