using OpenZWaveDotNet;
using ValueType = OpenZWaveDotNet.ZWValueID.ValueType;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDeviceValue
    {
        private readonly OpenZWaveNetwork _network;
        private readonly ZWValueID _value;

        public OpenZWaveDeviceValue(OpenZWaveNetwork network, ZWValueID value)
        {
            _network = network;
            _value = value;
        }

        public uint HomeId
        {
            get
            {
                return _value.GetHomeId();
            }
        }

        public byte DeviceId
        {
            get
            {
                return _value.GetNodeId();
            }
        }

        public CommandClass CommandClass
        {
            get
            {
                return (CommandClass)_value.GetCommandClassId();
            }
        }

        public ValueType Type
        {
            get
            {
                return _value.GetType();
            }
        }

        public byte Index
        {
            get
            {
                return _value.GetIndex();
            }
        }

        public byte Instance
        {
            get
            {
                return _value.GetInstance();
            }
        }

        #region get values

        public bool? BooleanValue
        {
            get
            {
                bool result;
                if (_network.Manager.GetValueAsBool(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public byte? ByteValue
        {
            get
            {
                byte result;
                if (_network.Manager.GetValueAsByte(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public decimal? DecimalValue
        {
            get
            {
                decimal result;
                if (_network.Manager.GetValueAsDecimal(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public int? IntValue
        {
            get
            {
                int result;
                if (_network.Manager.GetValueAsInt(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public string[] ListItemsValue
        {
            get
            {
                string[] result;
                if (_network.Manager.GetValueListItems(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public string Selection
        {
            get
            {
                string result;
                if (_network.Manager.GetValueListSelection(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public short? ShortValue
        {
            get
            {
                short result;
                if (_network.Manager.GetValueAsShort(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public string StringValue
        {
            get
            {
                string result;
                if (_network.Manager.GetValueAsString(_value, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public string Units
        {
            get
            {
                return _network.Manager.GetValueUnits(_value);
            }
        }

        public string Help
        {
            get
            {
                return _network.Manager.GetValueHelp(_value);
            }
        }

        public string Label
        {
            get
            {
                return _network.Manager.GetValueLabel(_value);
            }
        }

        #endregion

        public void Refresh()
        {
            _network.Manager.RefreshValue(_value);
        }

        #region update values

        public void SetValue(bool value)
        {
            _network.Manager.SetValue(_value, value);
        }

        public void SetValue(byte value)
        {
            _network.Manager.SetValue(_value, value);
        }

        public void SetValue(decimal value)
        {
            SetValue((float) value);
        }

        public void SetValue(float value)
        {
            _network.Manager.SetValue(_value, value);
        }

        public void SetValue(int value)
        {
            _network.Manager.SetValue(_value, value);
        }

        public void SetValue(short value)
        {
            _network.Manager.SetValue(_value, value);
        }

        public void SetValue(string value)
        {
            _network.Manager.SetValue(_value, value);
        }

        public void SetSelection(string value)
        {
            _network.Manager.SetValueListSelection(_value, value);
        }

        #endregion

        public override string ToString()
        {
            return this.FormatData();
        }
    }
}
