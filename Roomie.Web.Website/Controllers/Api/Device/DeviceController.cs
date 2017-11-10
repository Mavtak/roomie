using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Device
{
    [ApiRestrictedAccess]
    public class DeviceController : BaseController
    {
        RpcDeviceRepository _rpcDeviceRepository;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _rpcDeviceRepository = new RpcDeviceRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );
        }

        public object Post([FromBody] Request request)
        {
            return RpcRequestRouter.Route(_rpcDeviceRepository, request);
        }

        public IEnumerable<Persistence.Models.Device> Get(bool examples = false)
        {
            var result = _rpcDeviceRepository.List(examples);

            return result;
        }

        public Persistence.Models.Device Get(int id)
        {
            var result = _rpcDeviceRepository.Read(id);

            return result;
        }

        public void Put(int id, [FromBody] DeviceUpdateModel update)
        {
            _rpcDeviceRepository.Update(id, update?.Location, update?.Name, update?.Type);
        }

        public void Post(int id, string action, [FromBody] DeviceActionOptions options,
            [FromUri] string color = null,
            [FromUri] string mode = null,
            [FromUri] int? power = null,
            [FromUri] string temperature = null,
            [FromUri] string type = null
            )
        {
            options = options ?? new DeviceActionOptions();
            options.Color = options.Color ?? color;
            options.Mode = options.Mode ?? mode;
            options.Power = options.Power ?? power;
            options.Temperature = options.Temperature ?? temperature;
            options.Type = options.Type ?? type;

            switch (action)
            {
                case "Dim":
                    _rpcDeviceRepository.MultilevelSwitchSetPower(id, options.Power.Value);
                    break;

                case "PollBinarySensor":
                    _rpcDeviceRepository.BinarySensorPoll(id);
                    break;

                case "PollDevice":
                    _rpcDeviceRepository.Poll(id);
                    break;

                case "PollHumiditySensor":
                    _rpcDeviceRepository.HumiditySensorPoll(id);
                    break;

                case "PollIlluminanceSensor":
                    _rpcDeviceRepository.IlluminanceSensorPoll(id);
                    break;

                case "PollPowerSensor":
                    _rpcDeviceRepository.PowerSensorPoll(id);
                    break;

                case "PollTemperatureSensor":
                    _rpcDeviceRepository.TemperatureSensorPoll(id);
                    break;

                case "PowerOn":
                    _rpcDeviceRepository.BinarySwitchSetPower(id, BinarySwitchPower.On.ToString());
                    break;

                case "PowerOff":
                    _rpcDeviceRepository.BinarySwitchSetPower(id, BinarySwitchPower.Off.ToString());
                    break;

                case "SetColor":
                    _rpcDeviceRepository.ColorSwitchSetValue(id, options.Color);
                    break;

                case "SetThermostatFanMode":
                    _rpcDeviceRepository.ThermostatFanSetMode(id, options.Mode);
                    break;

                case "SetThermostatMode":
                    _rpcDeviceRepository.ThermostatCoreSetMode(id, options.Mode);
                    break;

                case "SetThermostatSetpoint":
                    _rpcDeviceRepository.ThermostatSetpointsSetSetpoint(id, options.Type, options.Temperature);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        
    }
}
