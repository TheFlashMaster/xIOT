﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface.Basics
{
    public interface ISimpleWriter
    {
        bool Write(int value);

        bool Write(int value1, int value2);
    }
}
