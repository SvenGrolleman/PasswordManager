﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Database.Entities
{
    public class MainPassword
    {
        public int MainId { get; set; }
        public string Password { get; set; }
        public string MainSalt { get; set; }
    }
}
