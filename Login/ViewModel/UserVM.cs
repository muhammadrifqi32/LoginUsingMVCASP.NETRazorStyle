﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.ViewModel
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role_id { get; set; }
        public string RoleName { get; set; }
    }
}