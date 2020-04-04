﻿using GameRealm.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameRealm.Interface
{
    public interface ILocations
    {
        /// <summary>
        /// Updates inventory quantity in database
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="qty">quantity to decrease by</param>
        void UpdateInventory(int id, int qty);
        /// <summary>
        /// Retrieves inventory quantity of item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetQty(int id);
        /// <summary>
        /// Retrieves inventory of a location
        /// </summary>
        /// <param name="id">location id or name</param>
        /// <returns>list of items</returns>
        List<Inventory> GetInventory(int id);
        /// <summary>
        /// Returns all locations
        /// </summary>
        /// <returns></returns>
        List<Locations> GetList();
    }
}
