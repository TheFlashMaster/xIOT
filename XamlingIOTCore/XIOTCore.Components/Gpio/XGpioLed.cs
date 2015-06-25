﻿using XIOTCore.Contract.Dto;
using XIOTCore.Contract.Interface;

namespace XIOTCore.Components.Gpio
{
    public class XGpioLed : IXLed
    {
        private readonly IXGpio _gpio;

        public XGpioLed(IXGpio gpio)
        {
            _gpio = gpio;
        }

        public void On()
        {
            _gpio.SetHigh();
        }

        public void Off()
        {
            _gpio.SetLow();
        }

        public bool State
        {
            get { return _gpio.GetValue() == XPinValue.High; }
            set
            {
                if (value)
                {
                    On();
                }
                else
                {
                    Off();
                }
            }
        }
    }
}