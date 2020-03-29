using System;
using System.Linq;
using System.Threading;
using GameRealm.Data.Entities;
using System.Collections.Generic;
using System.Text;

namespace GameRealm.Domain.Models
{

    public static class CustStorage
    {
        private static readonly int userInput = int.Parse(Console.ReadLine());

        public static Customer custLogin(Game_RealmContext ctx, Customer cust)
        {

             cust = null;

            while (cust == null)
            {

                Console.WriteLine("Please Enter your credentials\n\n");
                Console.Write("Username: ");
                var userName = "";
                try
                {
                    userName = Console.ReadLine();
                }
                catch (Exception)
                {

                    Console.WriteLine("You must Enter in a Username!");
                }
                Console.Write("Password: ");
                var custPass = "";
                try
                {
                    custPass = Console.ReadLine();
                }
                catch (Exception)
                {

                    Console.WriteLine("You must enter in a Password!");
                }
                Thread.Sleep(400);



                var customer = from sales in ctx.Customer
                               where sales.UserName == userName && sales.Password == custPass
                               select sales;

                cust = ctx.Customer.Where(c => c.UserName == userName && c.Password == custPass).SingleOrDefault();
            }


            /* Customer custID = ctx.Customer.Where(cid => cid.CustomerId == customerID).SingleOrDefault();*/
            if (cust.UserName.ToUpper() != null)
            {
                if (cust.Password != null)
                {
                    Console.WriteLine("\nWelcome back: " + cust.FirstName + " " + cust.LastName + "!\n");
                }
            }

            else if (cust.UserName == null)
            {
                Console.WriteLine("Username or Password is incorrect\n");
                Thread.Sleep(900);
                Console.WriteLine("Would you like to try again? (y/n)");
                var answer = Console.ReadLine();

                if (answer.ToUpper() == "Y")
                {
                    custLogin(ctx, cust);
                }
                else
                {
                    promptUser.promtUserMenu(ctx, cust);
                }
            }

            else if (cust.Password == null)
            {
                Console.WriteLine("Username or Password is incorrect\n");
                Thread.Sleep(900);
                Console.WriteLine("Would you like to try again? (y/n)");
                var answer = Console.ReadLine();

                if (answer.ToUpper() == "Y")
                {
                    custLogin(ctx, cust);
                }
                else
                {
                    promptUser.promtUserMenu(ctx, cust);
                }
            }

            else
            {
                Console.WriteLine("You Must Enter Something!");
                Thread.Sleep(900);
                promptUser.promtUserMenu(ctx, cust);
            }

            return cust;
        }
    }
}
