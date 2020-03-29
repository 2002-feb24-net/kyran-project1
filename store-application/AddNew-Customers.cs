using System;
using System.Collections.Generic;

namespace Store {
    class StoreCustomer {
        private string name { get; set; }

        private string address { get; set; }
        private string phoneNumber { get; set; }

        public StoreCustomer (string cName, string cAddress, string cPhoneNumber) 
        {
            name = cName;

            address = cAddress;

            phoneNumber = cPhoneNumber;
            

        }

    }

}