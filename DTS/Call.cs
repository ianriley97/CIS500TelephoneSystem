using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    [Serializable()]
    public class Call
    {
        private string timeConnected;

        private string timeDisconnected; //He mentioned something about changing these but I can't remember what it was 

        private string phoneNumber;

        private string areaCode;

        public Call(string paramPhoneNumber, string paramAreaCode)
        {
            phoneNumber = paramPhoneNumber;
            areaCode = paramAreaCode;
            timeConnected = DateTime.Now.ToString();
        }

        public void Disconnected()
        {
            timeDisconnected = DateTime.Now.ToString();
        }

        public override string ToString()
        {
            return (phoneNumber + " " + timeConnected + " " + timeDisconnected);
        }
    }
}
