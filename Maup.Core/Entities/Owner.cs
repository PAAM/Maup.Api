using System;
using System.Collections.Generic;

namespace Maup.Core.Entities
{
    public partial class Owner: BaseEntity
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }

        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
