using GameRealm.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameRealmWeb.ViewModels
{
    public class OrdersViewModel
    {
        public int LocationId { get; set; } 
        public int CustomerId { get; set; }
        public int ProductID { get; set; }

        public List<Orders> OrderList { get; set; }
    }
       
    
}
