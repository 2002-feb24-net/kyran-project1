using Microsoft.EntityFrameworkCore;
using GameRealm.DataAccess.Model;
using GameRealm.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameRealm.DataAccess
{
    public class InventoryDAL : IInventory
    //customer data access library
    {
        public int InventoryId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? StoreId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ProductId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
