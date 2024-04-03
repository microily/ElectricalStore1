using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Supply
    {
        public int SupplyId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
        public DateOnly SupplyDate { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
    }
}
