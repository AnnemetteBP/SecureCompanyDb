using System;
using System.Collections.Generic;

namespace crud_interface
{
    public partial class DeptLocation
    {
        public int Dnumber { get; set; }
        public string Dlocation { get; set; } = null!;

        public virtual Department DnumberNavigation { get; set; } = null!;
    }
}
