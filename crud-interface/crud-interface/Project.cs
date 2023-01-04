using System;
using System.Collections.Generic;

namespace crud_interface
{
    public partial class Project
    {
        public Project()
        {
            WorksOns = new HashSet<WorksOn>();
        }

        public string? Pname { get; set; }
        public int Pnumber { get; set; }
        public string? Plocation { get; set; }
        public int? Dnum { get; set; }

        public virtual Department? DnumNavigation { get; set; }
        public virtual ICollection<WorksOn> WorksOns { get; set; }
    }
}
