using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public List<Review> Reviews { get; set; } = [];
        public List<BorrowedBook> BorrowedBooks { get; set; } = [];
        public List<Wishlist> Wishlist { get; set; } = [];
        public override string ToString()
        {
            return $"{Id}.{Title} , {Category}";
        }
    }
}
