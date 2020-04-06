using Microsoft.EntityFrameworkCore;
using GameRealm.DataAccess.Model;
using GameRealm.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using GameRealm.Domain.Model;

namespace GameRealm.DataAccess
{
    public class LocationsDAL : ILocations
    //customer data access library
    {
        public List<Inventory> GetInventory(int id)
        {
            throw new NotImplementedException();
        }

        public List<Locations> GetList()
        {
            throw new NotImplementedException();
        }

        public int GetQty(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateInventory(int id, int qty)
        {
            throw new NotImplementedException();
        }
    }
}
