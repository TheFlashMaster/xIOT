﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Portable.Components.OLED.SSD1306;
using XIOTCore.Portable.Factory;

namespace XIOTCore_Samples_Console.OLED
{
    public class OLEDExamples_FTDI
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreFactory.Create(Platforms.FTDI_USB);

        public async Task Init()
        {
            _factory.Init();
            var i2c = _factory.GetComponent<IXI2CDevice>();
            var writer = new I2CIO(i2c);
            var iTask = writer.Init(OLEDConstants.SSD1306_I2C_ADDRESS);
            iTask.Wait();
            var oled = new XIOTCore.Portable.Components.OLED.SSD1306.OLED(writer, OLEDDisplaySize.SSD1306_128_64);
            
            oled.Init();
            
            //return;
            var b = new Bitmap(128, 64);

            var g = Graphics.FromImage(b);

            g.DrawString("IOT", new Font("Consolas", 30), new SolidBrush(Color.White), 45, 15);
            g.DrawString("Y", new Font("Webdings", 30), new SolidBrush(Color.White),0, 15);

            g.Save();

            for (var i = 0; i < b.Height; i++)
            {
                for (var x = 0; x < b.Width; x++)
                {
                    var p = b.GetPixel(x, i);
                    var average = (p.R + p.G + p.G)/3;

                    if (average != 0)
                    {
                        oled.DrawPixel((ushort)x, (ushort)i, 1);
                    }
                }
            }

           oled.Display();
        }
    }
}
