using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Filters
{
    public class OwnerFilter
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public int Rol { get; set; }
        public bool Enabled { get; set; }
    }
}
