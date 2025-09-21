using HWW12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.DTO
{
    public class BooksAndCategoriesDTO
    {
        public List<Category> Categories { get; set; }
        public List<Book> Books { get; set; }
    }
}
