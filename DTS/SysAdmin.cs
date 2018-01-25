using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    public class SysAdmin
    {
        private string password; //This should work just fine.

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public SysAdmin(string inputPassword)
        {
            password = inputPassword;
        }
    }
}
