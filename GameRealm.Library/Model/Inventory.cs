using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameRealm.Domain.Model
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        [Display(Name = "Store")]
        public int StoreId { get; set; }
        [Display(Name = "Game")]
        public int ProductId { get; set; }
        [Display(Name = "Stock")]
        public int Quantity { get; set; }
        public string Title { get; set; }

        public virtual Games Product { get; set; }
        public virtual Locations Store { get; set; }
    }
}
