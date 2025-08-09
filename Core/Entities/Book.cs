using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
    }
}
