using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;//Add this for serialization
using System.IO;//Add this for serialization

namespace DTS_Project
{

    public class TerminalController
    {
        private ITerminalDevice terminalDevice;

        private UserController apartment;

        private Tenant currUser;

        public TerminalController(ITerminalDevice terminalDevice)
        {
            this.terminalDevice = terminalDevice;
        }

        /// <summary>
        /// This sets the reference field of the apartment from the DTSInitializer class.
        /// </summary>
        /// <param name="apartmentParam">This is the apartment that is created in DTSInitializer</param>
        public void SetApartment(UserController apartmentParam)
        {
            apartment = apartmentParam;
        }

        #region activate(completed)
        public void Activate()
        {
            //verify password and if verified, show MainMenuDialog
            // if a user presses "Cancel", do nothing and just return
            string password = null;
            if (!terminalDevice.GetPassword(ref password)) return;
            // you need to verify the password
            if (apartment.checkAdminPassword(password))
            {
                terminalDevice.ShowMainMenuDialog(this);
            }
        }
        #endregion

        #region addTenant(completed)
        // handlers for MainMenuDialog
        public void AddTenant_Handler()
        {
            // Add a tenant
            // Get the name and access code of the tenant to be added
            string firstName = null;
            string lastName = null;
            string accessCode = null;
            if (!terminalDevice.GetTenantInfo(ref firstName, ref lastName, ref accessCode)) return;

            apartment.CreateUser(firstName, lastName, accessCode);

        }
        #endregion

        #region deleteTenant(completed)
        public void DeleteTenant_Handler()
        {
            // Delete a tenant
            // Get the first name and the last name of the tenant to be deleted
            string firstName = null;
            string lastName = null;
            if (!terminalDevice.GetTenantName(ref firstName, ref lastName)) return;

            apartment.DeleteUser(firstName, lastName);
        }
        #endregion

        #region workOnTenant(completed)
        public void WorkOnTenant_Handler()
        {
            // Work on a specific tenant
            // Input the first name and the last name of the tenant to work on
            string firstName = null;
            string lastName = null;
            if (!terminalDevice.GetTenantName(ref firstName, ref lastName)) return;
            //I think I may need a field for currently used user.
            currUser = apartment.GetWorkOnTenant(firstName, lastName);
            if (currUser != null)
            {
                terminalDevice.ShowTenantMenuDialog(this);
            }
        }
        #endregion

        #region DisplayTenantList(completed)
        public void DisplayTenantList_Handler()
        {
            // call "void DisplayList(object[] list)" to list Tenants
            terminalDevice.DisplayList(apartment.AllUsersToArray());
        }
        #endregion

        #region SaveHandler(completed)
        public void Save_Handler()
        {
            BinaryFormatter fo = new BinaryFormatter();
            using (FileStream f = new FileStream("DTSsavefile.svf", FileMode.Create, FileAccess.Write))
            {
                fo.Serialize(f, apartment.AllUsers);
            }
        }
        #endregion

        #region restoreHandler(completed)
        public void Restore_Handler()
        {
            BinaryFormatter fo = new BinaryFormatter();
            using (FileStream f = new FileStream("DTSsavefile.svf", FileMode.Open, FileAccess.Read))
            {
                apartment.AllUsers = (List<Tenant>)fo.Deserialize(f);
            }
        }
        #endregion

        #region changeSysAdminPassword(completed)
        public void ChangePassword_Handler()
        {
            string password = null;
            if (!terminalDevice.GetPassword(ref password)) return;
            apartment.ChangeSysAdminPassword(password);
        }
        #endregion

        #region BarAreaCode(NeedsToBeVerified)
        // ==== Handlers for TenantMenuDialog
        public void BarAreaCode_Handler()
        {
            // Bar an area code
            // Input the area code to bar
            string areaCode = null;
            if (!terminalDevice.GetAreaCode(ref areaCode)) return;
            currUser.addBarred(areaCode);
        }
        #endregion

        #region barTelephoneNumber(NeedsToBeVerified)
        public void BarTelephoneNumber_Handler()
        {
            // Bar a telephone number
            // Input the telephone number to bar
            string areaCode = null;
            string exchange = null;
            string number = null;
            if (!terminalDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
            //This presents a dialog box with an input for an area code to bar, and then we have to bar it. 
            string barNumber = areaCode + exchange + number;
            currUser.addBarred(barNumber);
        }
        #endregion

        #region unBarAreaCode(NeedsToBeVerified)
        public void UnBarAreaCode_Handler()
        {
            // Unbar an area code
            // Input the area code to unbar
            string areaCode = null;
            if (!terminalDevice.GetAreaCode(ref areaCode)) return;
            currUser.removeBarred(areaCode);
        }
        #endregion

        #region unbarTelephoneNumber(NeedsToBeVerified)
        public void UnBarTelephoneNumber_Handler()
        {
            // Unbar a telephone number
            // Input the telephone number to unbar 
            string areaCode = null;
            string exchange = null;
            string number = null;
            if (!terminalDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
            string barNumber = areaCode + exchange + number;
            currUser.removeBarred(barNumber);
        }
        #endregion

        public void DisplayCallList_Handler()
        {
            // call  "void DisplayList(object[] list)" to list Calls
            terminalDevice.DisplayList(currUser.AllCallsToArray());

        }

        public void DisplayBarList_Handler()
        {
            // call "void DisplayList(object[] list)" to list Bar Numbers
            terminalDevice.DisplayList(currUser.AllBarredToArray());
        }

        public void ClearCalls_Handler()
        {
            currUser.ClearCalls();
        }
    }
}
