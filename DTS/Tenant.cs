using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    [Serializable()]
    public class Tenant
    {
        private string accessCode;

        public string AccessCode
        {
            get
            {
                return accessCode;
            }
        }

        private string firstName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
        }

        private string lastName;

        public string LastName
        {
            get
            {
                return lastName;
            }
        }

        private List<Call> callLog;

        private List<Barred> barLog;

        public Tenant (string paramFirstName, string paramLastName, string paramAccessCode)
        {
            firstName = paramFirstName;
            lastName = paramLastName;
            accessCode = paramAccessCode;
            callLog = new List<Call>();
            barLog = new List<Barred>();
        }

        public override string ToString()
        {
            return (firstName + " " + lastName + " " + accessCode);
        }

        public void addBarred(string phoneNumber)
        {
            barLog.Add(new Barred(phoneNumber));
        }

        public Call addCall(string phoneNumber, string areaCode)
        {
            Call returner = new Call(phoneNumber, areaCode);
            callLog.Add(returner);
            return returner;
        }

        public void removeBarred(string phoneNumber)
        {
            Barred removed = barLog.Find(x => x.ToString() == phoneNumber);
            barLog.Remove(removed);
        }

        public bool CheckBarred(string phoneNumber, string areaCode)
        {
            Barred checker = barLog.Find((x => x.ToString() == phoneNumber));
            Barred checker2 = barLog.Find(x => x.ToString() == areaCode);
            if (checker != null || checker2 != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string[] AllCallsToArray()
        {
            int iterator = 0;
            string[] returner = new string[callLog.Count];
            foreach (Call i in callLog)
            {
                returner[iterator] = i.ToString();
                iterator++;
            }
            return returner;
        }

        public string[] AllBarredToArray()
        {
            int iterator = 0;
            string[] returner = new string[barLog.Count];
            foreach (Barred i in barLog)
            {
                returner[iterator] = i.ToString();
                iterator++;
            }
            return returner;
        }

        public void ClearCalls()
        {
            callLog.Clear();
        }
    }
}
