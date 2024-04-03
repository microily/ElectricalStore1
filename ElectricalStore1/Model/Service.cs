using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Service
    {
        public Service()
        {
            SalesTransactions = new HashSet<SalesTransaction>();
        }

        public int ServiceId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly ReceptionDate { get; set; }
        public string Status { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; }
    }
}
