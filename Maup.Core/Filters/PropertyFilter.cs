using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Filters
{
    public class PropertyFilter
    {
        public int? IdProperty { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int? IdOwner { get; set; }
        public int PageSize { get; set;}
        public int PageNumber { get; set; }
    }
}
