using System;
using System.Collections.Generic;

namespace ElectricalStore1.Model
{
    public partial class Employee
    {
        public Employee()
        {
            SalesTransactions = new HashSet<SalesTransaction>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int RoleId { get; set; }
        public string? Patronymic { get; set; }
        public decimal Salary { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; }
    }
}
