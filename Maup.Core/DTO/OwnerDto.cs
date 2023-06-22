using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.DTO
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public IFormFile? Photo { get; set; }
        public DateTime? Birthday { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public int Rol { get; set; }
        public bool Enabled { get; set; }
    }
}
