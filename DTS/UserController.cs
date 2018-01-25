using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS_Project
{
    public class UserController
    {
        private List<Tenant> allUsers; //I am making all of the fields private. I will probably have to change some of these.

        public List<Tenant> AllUsers
        {
            get
            {
                return allUsers;
            }
            set
            {
                allUsers = value;
            }
        }

        private SysAdmin admin; //Pretty sure I need this.


        public UserController()
        {
            admin = new SysAdmin("ksu");
            allUsers = new List<Tenant>();
        }

        public bool checkAdminPassword(string password)
        {
            if (password == admin.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkAccessCode(string paramAccessCode, out Tenant currUser)
        {
            Tenant returner = allUsers.Find(x => x.AccessCode == paramAccessCode);
            currUser = returner;
            if (returner != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CreateUser(string firstName, string lastName, string accessCode)
        {
            Tenant created = new Tenant(firstName, lastName, accessCode);
            allUsers.Add(created);
        }

        public void DeleteUser(string firstName, string lastName)
        {
            //allUsers.Remove(Tenant.Find(x => Tenant.FirstName == firstName &&))
            Tenant remove = allUsers.Find(x => x.FirstName == firstName && x.LastName == lastName);
            allUsers.Remove(remove);
        }


        public Tenant GetWorkOnTenant(string firstName, string lastName)
        {
            Tenant worker = allUsers.Find(x => x.FirstName == firstName && x.LastName == lastName);
            return worker;
        }

        public string[] AllUsersToArray()
        {
            int iterator = 0;
            string[] returner = new string[allUsers.Count];
            foreach (Tenant i in allUsers)
            {
                returner[iterator] = i.ToString();
                iterator++;
            }
            return returner;
        }

        public void ChangeSysAdminPassword(string newPassword)
        {
            admin.Password = newPassword;
        }

        //I think this might need to be serializable. But we will see.

        //I think I need to add a method to reupdate the list of users after a serialized object is loaded.
    }
}
