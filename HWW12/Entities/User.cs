using HWW12.Enums;
using HWW12.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Entities
{
    public class User 
    {
        private User()  
        { }
        public User(string username, string password, RoleEnum role)
        {
            UserName = username;
            Password = password;
            Role = role;

        }
        public int Id { get;  set; }
        public string UserName { get;  set; }
        private string Password {  get; set; }
        public RoleEnum Role { get; set; }

        public double PenaltyAmount { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; } = [];
        public List<Review> Reviews { get;  set; } = [];
        public List<Wishlist> Wishlist { get; set; } = [];
        public bool CheckPass(string password) 
        {
            return Password == password;                         
        }

        public void AddPenaltyAmount(int durationDays) 
        {
            PenaltyAmount = PenaltyAmount + ((durationDays - 7) * 10000);
        }
    }
}
