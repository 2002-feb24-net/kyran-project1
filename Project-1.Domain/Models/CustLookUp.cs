using System;
using System.Threading;
using System.Linq;
using GameRealm.Data.Entities;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace GameRealm.Domain.Models
{
    class CustLookUp
    {
        public static void customerSearch(Game_RealmContext ctx, Customer cust)
        {
            Console.WriteLine("Who would you like you like to search for? \n");
            Thread.Sleep(800);
            string userINput = Console.ReadLine();

            var custSearch = from sales in ctx.Customer
                             where sales.FirstName == userINput
                             select sales;
            /*var custOrderHistory = ctx.Orders.Include("Orders").ToList();*/
           
            var custName = ctx.Customer.FirstOrDefault(cid => cid.FirstName == userINput);

            if (custName != null)
            {
                



                foreach (var item in custSearch)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("\t\t\t\t\tCustomer Found!");
                    Console.WriteLine("\n");
                    Console.WriteLine($"First Name: {item.FirstName}\nLast Name: {item.LastName}\n\nUser Name:  {item.UserName}");
                    var orderHist = ctx.Orders.Where(order => order.CustomerId == item.CustomerId).Include("Orderline").ToList();

                    foreach (var item2 in orderHist)
                    {
                        foreach (var item3 in item2.Orderline)
                        {
                            Console.WriteLine($"OrderID: {item3.OrderId}\tProductID: {item3.ProductId}\tQuantity: {item3.Quantity}\tPurchase Date: {item3.Order.Time}");
                        }
                    }
                }

             

            }

            else
            {
                Console.WriteLine("\ninvalid Entry! or Customer not Found!\n\n");
                Thread.Sleep(1000);
                promptUser.promtUserMenu(ctx, cust);
            }
        }

    }
}
