using System;
using System.Linq;
using System.Text;
using GameRealm.Data.Entities;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace GameRealm.Domain.Models
{

    class placeOrders : IGameList
    {
        public static Customer buyGame(Game_RealmContext ctx, Customer cust, Locations loc)
        {
           
           
            var choice = "";

            try
            {
                choice = Console.ReadLine();
            }
            catch (Exception)
            {

                Console.WriteLine("");
            }

            if (choice.ToUpper() == "Y")
            {
                cust = CustStorage.custLogin(ctx, cust);

            }

            else if (choice.ToUpper() == "N")
            {
                Console.WriteLine("You must be a user to place orders!\n");
                Thread.Sleep(800);
                Console.WriteLine("Please Login or Create a new account!\n");
                    //Dont allow purchase until confirmation that they are registered with us.
                    promptUser.promtUserMenu(ctx, cust);
            }
            else if(choice == "")
            {
                Console.WriteLine("You Must Enter something !\n");
                Thread.Sleep(800);
                Console.WriteLine("Please Login or Create a new account!\n");
                //Dont allow purchase until confirmation that they are registered with us.
                promptUser.promtUserMenu(ctx, cust);
            }
            else
            {
                Console.WriteLine("Invalid Entry!\n");
                Thread.Sleep(800);
                Console.WriteLine("Please Login or Create a new account!\n");
                //Dont allow purchase until confirmation that they are registered with us.
                promptUser.promtUserMenu(ctx, cust);
            }

            
            

            Console.WriteLine("Which store location would you like to purchase your game from?\n");
            Thread.Sleep(800);
            Console.WriteLine("Choose by StoreID: ");
            List<Locations> locations_list = ctx.Locations.ToList();


            foreach (var item in locations_list)
            {
                Console.WriteLine("StoreID: " + item.StoreId + " Store Name: " + item.StoreName + "\nStreet: " + item.Street + "\nCity: " + item.City + "\nState: " + item.State + "\n\n");
              
            }




            int uInput = 0;
            while (true)
            {
                try
                {
                   uInput = int.Parse(Console.ReadLine());
                    if(ctx.Locations.Find(uInput) != null)
                    break; 
                    else
                    {
                        Console.WriteLine("You Must Enter the correct Store ID: ");
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("incorrect input, enter valid location ID!");


                }
            }

            Orders newOrder = new Orders();
            newOrder.Checkout = 0;
            newOrder.StoreId = uInput;
            newOrder.CustomerId = cust.CustomerId;
            newOrder.Time = DateTime.Now;
            ctx.Orders.Add(newOrder);
            ctx.SaveChanges();
            ctx.Entry(newOrder).Reload();


            int nID = newOrder.OrderId;
            


            /*var storeID = ctx.Locations.FirstOrDefault(sid => sid.StoreId == uInput);*/
            loc = ctx.Locations.SingleOrDefault(c => c.StoreId == uInput);

            
                switch (uInput)
                {
                    case 1:
                        Console.WriteLine("\n");
                        Console.WriteLine("Your store: Long Beach, Ca\n");
                        Thread.Sleep(700);
                        break;

                    case 4:
                        Console.WriteLine("\n");
                        Console.WriteLine("Your store: Miami, Fl\n");
                        Thread.Sleep(700);
                        break;

                    case 5:
                        Console.WriteLine("\n");
                        Console.WriteLine("Your store: New York, Ny\n");
                        Thread.Sleep(700);
                        break;

                    case 6:
                        Console.WriteLine("\n");
                        Console.WriteLine("Your store: Dallas, Tx\n");
                        Thread.Sleep(700);
                        break;

                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("Incorrect input, you must choose a store location!\n\n");
                        Thread.Sleep(700);
                        Console.WriteLine("Are you a registered customer? (y/n)");
                        buyGame(ctx, cust, loc);
                        break;
                }
            
           

            List<Games> orderTotal = new List<Games>();
            List<Orders> finalCost = new List<Orders>();
            

            var showInventory = new placeOrders();
            Console.WriteLine("Choose a game that you would you like to buy, based on GameID: \n\n");
            Thread.Sleep(900);
            showInventory.gameInventory();
            Thread.Sleep(800);
            Console.WriteLine("Choose GameID: ");
            int gameChoice = 0;
            Console.WriteLine("\n");

            try
            {
                gameChoice = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("You Must Enter Some Data!");
            }

         

            /*var orderHist = ctx.Orderline*/
            var gameID = ctx.Games.FirstOrDefault(gChoice => gChoice.ProductId == gameChoice);
            


            if (gameID != null)
            {
                /*string uInput = "";*/


                Console.WriteLine($"You have chosen:\nGame Title: {gameID.Title} \nPrice: ${gameID.Price}\n");
               
                orderTotal.Add(gameID);
                
                Console.WriteLine("add another game to your cart? (y/n)");
                string answer = "";

                try
                {
                    answer = Console.ReadLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("You Must Enter Some Data!");
                }

                Orderline tCost = new Orderline();

                tCost.ProductId = gameID.ProductId;
                tCost.OrderId = nID;
                tCost.Quantity = 1;
                ctx.Orderline.Add(tCost);
                ctx.SaveChanges();



              
               
                while (answer.ToUpper() == "Y")
                {
                    Orderline totalCost = new Orderline();

                    int count = 0;
                    
                    Console.WriteLine("Choose GameID: ");
                    int gameChoice2 = 0;

                    try
                    {
                        gameChoice2 = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("You Must Enter Some Data!");
                    }

                    Console.WriteLine("\n");
                    var gameID2 = ctx.Games.FirstOrDefault(gChoice => gChoice.ProductId == gameChoice2);
                    orderTotal.Add(gameID2);

                    foreach (var item in orderTotal)
                    {
                        count++;
                    }
                    Thread.Sleep(1000);
                    Console.WriteLine($"You have chosen:\nGame Title: {gameID2.Title} \nPrice: ${gameID2.Price}\n");
                    Console.WriteLine(count + " Games in your cart!\n");

                     totalCost.ProductId = gameID2.ProductId;
                    totalCost.OrderId = nID;
                    totalCost.Quantity = 1;
                    ctx.Orderline.Add(totalCost);
                    ctx.SaveChanges();

                    Console.WriteLine("\nadd another game to your cart? (y/n)");
                    string answer2 = Console.ReadLine();
                    try
                    {

                    }
                    catch (Exception)
                    {

                        Console.WriteLine("You must choose something!");
                    }
                    if (answer2.ToUpper() == "N")
                    {

                        break;
                    }
                    
                   

                }



               
                
                    
                


                Console.WriteLine("Would you like to confirm your Purchase? (y/n)");
                var uChoice = Console.ReadLine();




                if (uChoice.ToUpper() == "Y")
                {
                    /*DateTime orederTime = DateTime.Now;
                    Orders order = new Orders()
                    {

                        
                        StoreId = loc.StoreId,
                        CustomerId = cust.CustomerId,
                        Checkout = gameID.Price,
                        Time = DateTime.Now
                    };
                    ctx.Orders.Add(order);
                    ctx.SaveChanges();*/

                    Console.WriteLine("Thank you for your purchase!\n");
                    Thread.Sleep(1000);

                    Console.WriteLine("Here is your order summary: \n");
                    foreach(var item in orderTotal)
                    {
                        Console.WriteLine($"Game: {item.Title}\nPrice: ${item.Price} \n");
                    }

                    ctx.Entry(newOrder).Reload();
                    Console.Write("Your total comes out to: $" + newOrder.Checkout);

                    

                }




                else if (uChoice.ToUpper() == "N")
                {
                    

                    Console.WriteLine("Ok, Well what would you like to do?");
                    Thread.Sleep(800);
                    Console.WriteLine("1) Main Menu\t\t2) Compete Purchase\t\t");
                    int userChoice = int.Parse(Console.ReadLine());

                    switch (userChoice)
                    {
                        case 1:
                            Console.WriteLine("\n");
                            promptUser.promtUserMenu(ctx, cust);
                            Thread.Sleep(600);
                            break;

                        case 2:
                            Console.WriteLine("\n");

                            Console.WriteLine("Thank you for your purchase!\n");
                            Thread.Sleep(1000);

                            Console.WriteLine("Here is your order summary: \n");
                            foreach (var item in orderTotal)
                            {
                                Console.WriteLine($"Game: {item.Title}\nPrice: ${item.Price} \n");
                            }

                            ctx.Entry(newOrder).Reload();
                            Console.Write("Your total comes out to: " + newOrder.Checkout);
                            Thread.Sleep(600);
                            Console.WriteLine("\n");
                            promptUser.promtUserMenu(ctx, cust);
                            break;
                        default:
                            Console.WriteLine($"{cust.FirstName} {cust.LastName} You must choose an option.");
                            Thread.Sleep(600);

                            break;
                    }

                }

            }


            else
            {
                Console.WriteLine("Invalid GameID! Please choose The proper GameID from the list of Games Shown.");
                Console.WriteLine("Are you a registered customer? (y/n)");
                placeOrders.buyGame(ctx, cust, loc);
            }


            return cust;


        }

        public void gameInventory()
        {
            var ctx = new Game_RealmContext();
            List<Games> listOfGames = ctx.Games.ToList();
            foreach (var item in listOfGames)
            {
                Console.WriteLine("GameID: " + item.ProductId + "\nTitle: " + item.Title + "\nGenre: " + item.Genre + "\nRelease Date: " + item.Release + "\nPrice: " + "$" + item.Price + "\n\n");
            }
        }

    }
}
