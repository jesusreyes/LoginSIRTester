using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSIRTest.Models
{
    public class LoginCheck
    {
        public string user { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }

    public class LoginCheckResponse
    {
        public bool result { get; set; }
        public string rol { get; set; }
        public string message { get; set; }
    }

}
