using System;
using System.Collections.Generic;

namespace crud_interface
{
    public partial class Department
    {
        public Department()
        {
            DeptLocations = new HashSet<DeptLocation>();
            Employees = new HashSet<Employee>();
            Projects = new HashSet<Project>();
        }

        public string? Dname { get; set; }
        public int Dnumber { get; set; }
        public decimal? MgrSsn { get; set; }
        public DateTime? MgrStartDate { get; set; }
        public int? EmpCount { get; set; }

        public virtual Employee? MgrSsnNavigation { get; set; }
        public virtual ICollection<DeptLocation> DeptLocations { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
