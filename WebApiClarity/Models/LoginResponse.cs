using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiClarity.Models {
    public class LoginResponse {
        public string token { get; set; }
        public bool ok { get; set; }
        public object data { get; set; }
        public string error { get; set; }
    }
}