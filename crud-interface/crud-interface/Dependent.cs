using System;
using System.Collections.Generic;

namespace crud_interface
{
    public partial class Dependent
    {
        public decimal Essn { get; set; }
        public string DependentName { get; set; } = null!;
        public string? Sex { get; set; }
        public DateTime? Bdate { get; set; }
        public string? Relationship { get; set; }

        public virtual Employee EssnNavigation { get; set; } = null!;
    }
}
