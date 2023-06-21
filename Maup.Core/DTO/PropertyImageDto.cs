using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.DTO
{
    public class PropertyImageDto
    {
        public int Id { get; set; }
        public int IdProperty { get; set; }
        public IFormFile File { get; set; }
        public bool? Enabled { get; set; }
    }
}
