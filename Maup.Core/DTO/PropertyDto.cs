using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.DTO
{

    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public int CodeInternal { get; set; }
        public int? Year { get; set; }
        public int IdOwner { get; set; }
    }
}
