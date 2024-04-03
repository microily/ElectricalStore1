using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class SalesTransaction
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
