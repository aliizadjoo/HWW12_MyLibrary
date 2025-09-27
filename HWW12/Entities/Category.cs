using HWW12.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public List<Book> Books { get; set; } = [];

        public override string ToString()
        {
            return $"{Genre}";
        }

    }
}
