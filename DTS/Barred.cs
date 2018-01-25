using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    [Serializable()]
    class Barred
    {
        private string phoneNumber;

        //public string PhoneNumber
        //{
        //    get
        //    {
        //        return phoneNumber;
        //    }
        //}
        public Barred(string paramPhoneNumber)
        {
            phoneNumber = paramPhoneNumber;
        }

        public override string ToString()
        {
            return (phoneNumber);
        }
    }
}
