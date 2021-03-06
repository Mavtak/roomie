﻿using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
using ControlThinkFanMode = ControlThink.ZWave.Devices.ThermostatFanMode;
using ControlThinkFanState = ControlThink.ZWave.Devices.ThermostatFanState;
using ControlThinkSetpointType = ControlThink.ZWave.Devices.ThermostatSetpointType;
using ControlThinkTemperature = ControlThink.ZWave.Devices.Temperature;
using ControlThinkTemperatureScale = ControlThink.ZWave.Devices.TemperatureScale;
using ControlThinkThermostatCurrentAction = ControlThink.ZWave.Devices.ThermostatOperatingState;
using ControlThinkThermostatMode = ControlThink.ZWave.Devices.ThermostatMode;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    //TODO: unit test all of this
    //TODO: handle econ types
    internal static class ConverstionExtensions
    {
        public static ControlThinkSetpointType? ToControlThinkType(this ThermostatSetpointType input)
        {
            ControlThinkSetpointType? result;

            switch (input)
            {
                case ThermostatSetpointType.Heat:
                    result = ControlThinkSetpointType.Heating1;
                    break;

                case ThermostatSetpointType.Cool:
                    result = ControlThinkSetpointType.Cooling1;
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }

        public static ThermostatSetpointType? ToRoomieType(this ControlThinkSetpointType input)
        {
            ThermostatSetpointType? result;

            //TODO: improve this (with more Roomie types?)

            switch (input)
            {
                case ControlThinkSetpointType.Heating1:
                case ControlThinkSetpointType.HeatingEcon:
                case ControlThinkSetpointType.AwayHeating:
                    result = ThermostatSetpointType.Heat;
                    break;

                case ControlThinkSetpointType.Cooling1:
                case ControlThinkSetpointType.CoolingEcon:
                    result = ThermostatSetpointType.Cool;
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }

        public static IEnumerable<ThermostatSetpointType> ToRoomieType(this IEnumerable<ControlThinkSetpointType> input)
        {
            var result = input.Select(x => x.ToRoomieType()).NotNull().Distinct();

            return result;
        }


        public static ControlThinkTemperature? ToControlThinkType(this ITemperature input)
        {
            if (input == null)
            {
                return null;
            }

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
                    result = null;
                    break;
            }

            return result;
        }

        public static ControlThinkThermostatMode? ToControlThinkType(this ThermostatMode input)
        {
            ControlThinkThermostatMode? result;

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
                    result = null;
                    break;
            }

            return result;
        }

        public static ThermostatMode? ToRoomieType(this ControlThinkThermostatMode input)
        {
            ThermostatMode? result;

            //TODO: improve this (with more Roomie types?)

            switch (input)
            {
                case ControlThinkThermostatMode.Off:
                    result = ThermostatMode.Off;
                    break;

                case ControlThinkThermostatMode.Heat:
                case ControlThinkThermostatMode.AuxiliaryOrEmergencyHeat:
                case ControlThinkThermostatMode.HeatEcon:
                    result = ThermostatMode.Heat;
                    break;

                case ControlThinkThermostatMode.Cool:
                case ControlThinkThermostatMode.CoolEcon:
                    result = ThermostatMode.Cool;
                    break;

                case ControlThinkThermostatMode.FanOnly:
                    result = ThermostatMode.FanOnly;
                    break;

                case ControlThinkThermostatMode.Auto:
                    result = ThermostatMode.Auto;
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }

        public static IEnumerable<ThermostatMode> ToRoomieType(this IEnumerable<ControlThinkThermostatMode> input)
        {
            var result = input.Select(x => x.ToRoomieType()).NotNull().Distinct();

            return result;
        }

        public static ControlThinkFanMode? ToControlThinkType(this ThermostatFanMode input)
        {
            ControlThinkFanMode? result;

            switch (input)
            {
                case ThermostatFanMode.Auto:
                    result = ControlThinkFanMode.AutoLow;
                    break;

                case ThermostatFanMode.On:
                    result = ControlThinkFanMode.OnLow;
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }

        public static ThermostatFanMode? ToRoomieType(this ControlThinkFanMode input)
        {
            ThermostatFanMode? result;

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
                    result = null;
                    break;
            }

            return result;
        }

        public static IEnumerable<ThermostatFanMode> ToRoomieType(this IEnumerable<ControlThinkFanMode> input)
        {
            var result = input.Select(x => x.ToRoomieType()).NotNull().Distinct();

            return result;
        }

        public static ThermostatCurrentAction? ToRoomieType(this ControlThinkThermostatCurrentAction input)
        {
            ThermostatCurrentAction? result;

            switch (input)
            {
                case ControlThinkThermostatCurrentAction.Idle:
                    result = ThermostatCurrentAction.Idle;
                    break;

                case ControlThinkThermostatCurrentAction.Heating:
                case ControlThinkThermostatCurrentAction.PendingHeat:
                    result = ThermostatCurrentAction.Heating;
                    break;

                case ControlThinkThermostatCurrentAction.Cooling:
                case ControlThinkThermostatCurrentAction.PendingCool:
                    result = ThermostatCurrentAction.Cooling;
                    break;

                case ControlThinkThermostatCurrentAction.FanOnly:
                    result = ThermostatCurrentAction.FanOnly;
                    break;

                default:
                    result = null;
                    break;
            }

            return result;
        }

        public static ThermostatFanCurrentAction? ToRoomieType(this ControlThinkFanState input)
        {
            ThermostatFanCurrentAction? result;

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
                    result = null;
                    break;
            }

            return result;
        }

        private static IEnumerable<T> NotNull<T>(this IEnumerable<T?> values)
            where T : struct
        {
            var result = values.Where(x => x.HasValue).Select(x => x.Value);

            return result;
        }
    }
}
