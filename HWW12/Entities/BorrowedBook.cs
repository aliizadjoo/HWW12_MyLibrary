using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Entities
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTime BorrowDate { get; set; }

        public override string ToString()
        {
           return $"{Id}.{Book},BorrowDate:{BorrowDate}";
        }
    }

}
