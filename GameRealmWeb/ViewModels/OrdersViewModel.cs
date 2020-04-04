using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRealmWeb.ViewModels
{
    public class OrdersViewModel
    {
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public int StoreId { get; set; }
        public int ProductID { get; set; }

    }
}
