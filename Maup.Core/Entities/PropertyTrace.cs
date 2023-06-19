using System;
using System.Collections.Generic;

namespace Maup.Core.Entities
{
    public partial class PropertyTrace : BaseEntity
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }

        public virtual Property IdPropertyNavigation { get; set; } = null!;
    }
}
