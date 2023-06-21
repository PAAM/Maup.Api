using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Entities
{
    public class FileStore : BaseEntity
    {
        public byte[] File { get; set; }
        public string ContentType { get; set; }
        public string FileType { get; set; }
        public string Container { get; set; }
        public string FileName { get; set; }
    }
}
