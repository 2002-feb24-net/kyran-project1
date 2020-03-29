using System;
using System.Threading;
using System.Collections;
using GameRealm.Data.Entities;
using System.Collections.Generic;

namespace GameRealm.Domain.Models
{
    public static class StoreCustomer
    {
        
        public static void addCustomer(Game_RealmContext ctx)
        {
            Customer newCust = new Customer();



            Console.WriteLine("Please Enter your first name: ");
            newCust.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter your Last Name: ");
            newCust.LastName = Console.ReadLine();
            Console.WriteLine("Please enter a new User Name: ");
            newCust.UserName = Console.ReadLine();
            Console.WriteLine("Please enter a new password: ");
            newCust.Password = Console.ReadLine();


            ctx.Customer.Add(newCust);
            ctx.SaveChanges();
            Thread.Sleep(600);

                      
            Console.WriteLine("\nYou have been successfully added to the database! ");
        }

    }

}