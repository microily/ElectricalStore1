using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Supplier
    {
        public Supplier()
        {
            Supplies = new HashSet<Supply>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Supply> Supplies { get; set; }
    }
}
