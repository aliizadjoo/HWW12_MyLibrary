using HWW12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.DTO
{
    public class BookDetailsDTO
    {
        public List<Review> Reviews { get; set; } = [];
        public double AvgRating { get; set; }
    }
}
