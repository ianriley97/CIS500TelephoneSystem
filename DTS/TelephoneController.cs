using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTS_Project
{
    public class TelephoneController
    {
        /// <summary>
        /// This is the TelephoneDevice Interface.
        /// </summary>
        private ITelephoneDevice telephoneDevice;

        /// <summary>
        /// This is the tenant holder and how we'll interact with the tenants
        /// </summary>
        private UserController apartment;

        private Tenant currUser;

        // You need to add reference and/or value fields of TelephoneController
        // You may need to add Set methods to set the initlize values of these fields
        // These set methods are called from DTSInitializer.Initialize()
   
        public TelephoneController(ITelephoneDevice telephoneDevice)
        {
            this.telephoneDevice = telephoneDevice;
        }

        public void Activate()
        {
            // Receive an access code
            string accessCode = null;
            if (!telephoneDevice.GetAccessCode(ref accessCode)) return;
            //Here I need to validate the existance of the access codes. So I need to figure out the lamda expressions. 
            if (apartment.checkAccessCode(accessCode, out currUser))
            {
                // Recieve a telephone number
                string areaCode = null;
                string exchange = null;
                string number = null;
                if (!telephoneDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
                string phoneNumber = areaCode + exchange + number;
                if (!currUser.CheckBarred(phoneNumber, areaCode))
                {
                    Call currentCall = currUser.addCall(phoneNumber, areaCode);
                    // Connect the phone
                    telephoneDevice.ConnectPhone();
                    // User has terminated the call
                    currentCall.Disconnected();
                }
            }
        }

        /// <summary>
        /// This sets the reference field of the apartment from the DTSInitializer class.
        /// </summary>
        /// <param name="apartmentParam">This is the apartment that is created in DTSInitializer</param>
        public void SetApartment(UserController apartmentParam)
        {
            apartment = apartmentParam;
        }
    }
}
