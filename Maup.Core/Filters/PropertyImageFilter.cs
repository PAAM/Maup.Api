using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Filters
{
    public class PropertyImageFilter
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public bool? Enabled { get; set; }
    }
}
