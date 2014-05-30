using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiClarity.Models {
    public class LoginRequest {
        public string loginname { get; set; }
        public string password { get; set; }
    }
}