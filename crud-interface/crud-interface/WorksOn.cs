using System;
using System.Collections.Generic;

namespace crud_interface
{
    public partial class WorksOn
    {
        public decimal Essn { get; set; }
        public int Pno { get; set; }
        public double? Hours { get; set; }

        public virtual Employee EssnNavigation { get; set; } = null!;
        public virtual Project PnoNavigation { get; set; } = null!;
    }
}
