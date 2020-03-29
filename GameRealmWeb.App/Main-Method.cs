using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using GameRealmClient.Controllers;
using GameRealmWeb.App.Models;
using GameRealmWeb.App.Controllers;
using GameRealm.Data.Entities;
using GameRealm.Domain.Models;

namespace GameRealm
{
    class MainMethod
    {
       
        //allows us to update the DATABASE
        private static readonly Game_RealmContext ctx = new Game_RealmContext();
        static void Main(string[] args)
        {
            var cust = new Customer();
            var loc = new Locations();

            Console.WriteLine("Welcome to Game Realm!\n");
            Thread.Sleep(1200);
            promptUser.promtUserMenu(ctx, cust);


        }
    }
}
