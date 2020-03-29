using System;
using System.Collections.Generic;

namespace Store {
    class CustomerInput 
    {
        HashSet<int> CustID = new HashSet<int>();
        Dictionary<string , string> custInfo = new Dictionary<string, string>(); 
        
        public CustomerInput()
        {
            Random rand = new Random();

            HashSet<int> CustID = rand.Next(1, 10000);

                CustID++;
        }
        
        public void addCustomerToDict () 
        {
            

            System.Console.WriteLine ("Please Enter your Name: ");
            string custName = Console.ReadLine ();

            System.Console.WriteLine ("Please enter your Address: ");
            string custAddress = Console.ReadLine ();


            var newCustomer = new StoreCustomer (custName, custAddress, custInfo);

            custInfo.Add (custName, custAddress);



        }

    }
}