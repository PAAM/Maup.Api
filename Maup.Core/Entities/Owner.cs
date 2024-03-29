﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maup.Core.Entities
{
    public partial class Owner : BaseEntity
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }

        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public int Rol { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
