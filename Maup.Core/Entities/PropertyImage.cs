using System;
using System.Collections.Generic;

namespace Maup.Core.Entities
{
    public partial class PropertyImage : BaseEntity
    {
        public int IdProperty { get; set; }
        public string? File { get; set; }
        public bool? Enabled { get; set; }

        public virtual Property IdPropertyNavigation { get; set; } = null!;

    }
}
