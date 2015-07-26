﻿using System;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Portable.Components.LCD.HD44780;

namespace XIOTCore.Portable.Components.OLED.SSD1306
{
    public class I2CIO : ISimpleWriter
    {
        
        private bool _initialised;

        private IXI2CDevice _i2cDevice;

        public I2CIO(IXI2CDevice i2cDevice)
        {
            _i2cDevice = i2cDevice;
            _initialised = false;
        }

        public async Task<bool> Init(int address)
        {
            var result = await _i2cDevice.Init(address);

            if (!result)
            {
                return false;
            }

            byte[] b = new byte[1];
            _i2cDevice.Read(b);
            
            _initialised = true;

            return true;
        }

        public bool Write(int value)
        {
            if (_initialised)
            {
                var result = _i2cDevice.Write(value);
                return result;
            }

            return false;
        }

        public bool Write(int value1, int value2)
        {
            if (_initialised)
            {
                var resultControl = _i2cDevice.Write(value1);
                if (!resultControl)
                {
                    return false;
                }
                var result = _i2cDevice.Write(value2);
                return result;
            }

            return false;
        }
    }
}