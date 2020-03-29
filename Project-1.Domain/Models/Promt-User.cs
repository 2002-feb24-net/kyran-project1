using System;
using GameRealm.Data.Entities;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace GameRealm.Domain.Models
{

    public static class promptUser
    {

        public static Customer promtUserMenu(Game_RealmContext ctx, Customer cust)
        {
            
            var loc = new Locations();

            Console.WriteLine("What would you like to do? \n");
            Thread.Sleep(800);

            Console.WriteLine("\t1) New Customer\n" + "\t2) Search for Customer\n" + "\t3) Show Game Store Locations\n" + "\t4) Place An Order\n" + "\t5) List Of All Games\n" + "\t6) Show all Store Inventory\n" + "\t7) Quit?");

            int uChoice = 0;

            try
            {
               uChoice = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("Invalid entry, You must choose an option!");
            }
                switch (uChoice)
                {
                case 1:
                    Console.WriteLine("\n");
                    StoreCustomer.addCustomer(ctx);
                    Thread.Sleep(600);
                    Console.WriteLine("\n\n");
                    promtUserMenu(ctx, cust);
                    break;

                    case 2:
                    Console.WriteLine("\n");
                    CustLookUp.customerSearch(ctx, cust);
                    Thread.Sleep(600);
                    Console.WriteLine("\n\n");
                    promtUserMenu(ctx, cust);
                    break;

                    case 3:
                    Console.WriteLine("\n");
                    storeLocation.showLocation(ctx);
                    Thread.Sleep(600);
                    Console.WriteLine("\n\n");
                    promtUserMenu(ctx, cust);
                    break;

                    case 4:
                    Console.WriteLine("\n");
                    Console.WriteLine("Are you a registered customer? (y/n)");
                    placeOrders.buyGame(ctx, cust, loc);
                    Thread.Sleep(600);
                    Console.WriteLine("\n\n");
                    promtUserMenu(ctx, cust);
                    break;

                    case 5:
                    var listOfGames = new gameList();
                    Console.WriteLine("\n");
                    listOfGames.gameInventory();
                    Console.WriteLine("\n\n");
                    promtUserMenu(ctx, cust);
                    break;

                case 6:
                    ProductInventory.storeInventory(ctx);
                    Thread.Sleep(900);
                    promptUser.promtUserMenu(ctx, cust);
                    break;

                case 7:
                        System.Environment.Exit(0);
                        break;

                    default:
                    Console.WriteLine("\n");
                        promtUserMenu(ctx, cust);
                        break;
                }
            return cust;
        }

    }
}
