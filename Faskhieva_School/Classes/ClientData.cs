﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faskhieva_School
{
    public partial class Client
    {
        public string FIO
        {
            get
            {
                return LastName + " " + FirstName + " " + Patronymic;
            }
        }
    }
}
