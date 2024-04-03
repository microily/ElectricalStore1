using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Product
    {
        public Product()
        {
            Services = new HashSet<Service>();
            Supplies = new HashSet<Supply>();
            Warehouses = new HashSet<Warehouse>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
