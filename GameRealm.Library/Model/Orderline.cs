﻿using System;
using System.Collections.Generic;

namespace GameRealm.Domain.Model
{
    public partial class Orderline
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int OrderlineId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Games Product { get; set; }

        public virtual List<Inventory> stock { get; set; }
    }
}
