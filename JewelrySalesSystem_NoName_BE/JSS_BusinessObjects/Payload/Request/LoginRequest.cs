﻿using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class LoginRequest
    {
        public string? Phone { get; set; }

        public string Password { get; set; } = null!;
    }
}