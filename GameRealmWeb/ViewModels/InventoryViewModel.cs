using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRealmWeb.ViewModels
{
    public class InventoryViewModel
    {
        public int InventoryId { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
    }
}
