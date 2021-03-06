﻿using System.Linq;
using Roomie.Common;
using Roomie.Common.Api.Models;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Backend.Controllers.Api.Device
{
    public class RpcDeviceRepository
    {
        private IDeviceRepository _deviceRepository;
        private IRepositoryFactory _repositoryFactory;
        private Persistence.Models.User _user;

        public RpcDeviceRepository(IRepositoryFactory repositoryFactory, Persistence.Models.User user)
        {
            _repositoryFactory = repositoryFactory;
            _user = user;

            _deviceRepository = _repositoryFactory.GetDeviceRepository();
        }

        public Response<Persistence.Models.Device[]> List(bool examples = false)
        {
            Persistence.Models.Device[] devices;

            if (examples)
            {
                devices = Persistence.Examples.Devices.ToArray();
            }
            else
            {
                var cache = new InMemoryRepositoryModelCache();
                var networkRepository = _repositoryFactory.GetNetworkRepository();
                var networks = networkRepository.Get(_user, cache);
                var unsortedDevices = networks.SelectMany((id) => _deviceRepository.Get(id, cache)).ToList();
                unsortedDevices.Sort(new DeviceSort());
                devices = unsortedDevices.ToArray();
            }

            var result = devices.Select(GetSerializableVersion).ToArray();

            return Response.Create(result);
        }

        public Response<Persistence.Models.Device> Read(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);
            var result = GetSerializableVersion(device);

            return Response.Create(result);
        }

        public void Update(int id, string location, string name, string type)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            device.Update(
                location: location == null ? device.Location : new Location(location),
                name: name ?? device.Name,
                type: type ?? device.Type
            );

            _deviceRepository.Update(device);

            var computerRepository = _repositoryFactory.GetComputerRepository();
            var scriptRepository = _repositoryFactory.GetScriptRepository();
            var taskRepository = _repositoryFactory.GetTaskRepository();
            var runScript = new Computer.Actions.RunScript(computerRepository, scriptRepository, taskRepository);

            runScript.Run(
                computer: device.Network.AttatchedComputer,
                scriptText: "HomeAutomation.SyncWithCloud Network=\"" + device.Network.Address + "\"",
                source: "RoomieBot",
                updateLastRunScript: false,
                user: _user
            );
        }

        public Response MultilevelSwitchSetPower(int id, int power)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.MultilevelSwitch.SetPower(power);

            return Response.Empty();
        }

        public Response BinarySensorPoll(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.BinarySensor.Poll();

            return Response.Empty();
        }

        public Response Poll(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.Poll();

            return Response.Empty();
        }

        public Response HumiditySensorPoll(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.HumiditySensor.Poll();

            return Response.Empty();
        }

        public Response IlluminanceSensorPoll(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.IlluminanceSensor.Poll();

            return Response.Empty();
        }

        public Response PowerSensorPoll(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.PowerSensor.Poll();

            return Response.Empty();
        }

        public Response TemperatureSensorPoll(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.TemperatureSensor.Poll();

            return Response.Empty();
        }
        public Response BinarySwitchSetPower(int id, string power)
        {
            var parsedPower = power.ToToggleSwitchPower();
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.BinarySwitch.SetPower(parsedPower);

            return Response.Empty();
        }

        public Response ColorSwitchSetValue(int id, string color)
        {
            var parsedColor = color.ToColor();
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.ColorSwitch.SetValue(parsedColor);

            return Response.Empty();
        }

        public Response ThermostatFanSetMode(int id, string mode)
        {
            var parsedMode = mode.ToThermostatFanMode();
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.Thermostat.Fan.SetMode(parsedMode);

            return Response.Empty();
        }
        public Response ThermostatCoreSetMode(int id, string mode)
        {
            var parsedMode = mode.ToThermostatMode();
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.Thermostat.Core.SetMode(parsedMode);

            return Response.Empty();
        }
        public Response ThermostatSetpointsSetSetpoint(int id, string type, string temperature)
        {
            var parsedType = type.ToSetpointType();
            var parsedTemperature = temperature.ToTemperature();
            var cache = new InMemoryRepositoryModelCache();
            var device = _deviceRepository.Get(_user, id, cache);

            if (device == null)
            {
                return RpcDeviceRepositoryHelpers.CreateNotFoundError();
            }

            device.Thermostat.Setpoints.SetSetpoint(parsedType, parsedTemperature);

            return Response.Empty();
        }

        private static Persistence.Models.Device GetSerializableVersion(Persistence.Models.Device device)
        {
            return Persistence.Models.Device.Create(device.Id, device);
        }
    }
}