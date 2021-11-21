using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsShop.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string email { get; set; }
    }
}
