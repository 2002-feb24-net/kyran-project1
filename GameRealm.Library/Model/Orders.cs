using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameRealm.Domain.Model
{
    public partial class Orders
    {
        public Orders()
        {
            Orderline = new HashSet<Orderline>();
        }

        public int OrderId { get; set; }

        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "Order Placed")]
        public DateTime Time { get; set; }
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public decimal? Checkout { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Locations Store { get; set; }
        public virtual ICollection<Orderline> Orderline { get; set; }
    }
}
