using System;
using System.Collections.Generic;

namespace crud_interface
{
    public partial class Employee
    {
        public Employee()
        {
            Departments = new HashSet<Department>();
            Dependents = new HashSet<Dependent>();
            InverseSuperSsnNavigation = new HashSet<Employee>();
            WorksOns = new HashSet<WorksOn>();
        }

        public string? Fname { get; set; }
        public string? Minit { get; set; }
        public string? Lname { get; set; }
        public decimal Ssn { get; set; }
        public DateTime? Bdate { get; set; }
        public string? Address { get; set; }
        public string? Sex { get; set; }
        public decimal? Salary { get; set; }
        public decimal? SuperSsn { get; set; }
        public int? Dno { get; set; }

        public virtual Department? DnoNavigation { get; set; }
        public virtual Employee? SuperSsnNavigation { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Dependent> Dependents { get; set; }
        public virtual ICollection<Employee> InverseSuperSsnNavigation { get; set; }
        public virtual ICollection<WorksOn> WorksOns { get; set; }
    }
}
