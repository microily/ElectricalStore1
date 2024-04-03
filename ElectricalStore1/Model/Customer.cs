using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            Services = new HashSet<Service>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
