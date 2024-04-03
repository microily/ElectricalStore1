using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Order
    {
        public Order()
        {
            SalesTransactions = new HashSet<SalesTransaction>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public string Status { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; }
    }
}
