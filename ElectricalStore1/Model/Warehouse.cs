using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Warehouse
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
