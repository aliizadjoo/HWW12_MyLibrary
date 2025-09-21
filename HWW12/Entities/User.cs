using HWW12.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Entities
{
    public class User (string username , string password , RoleEnum role )
    {
        public User() : this("default", "default", RoleEnum.User) { }
        public int Id { get;  set; }
        public string UserName { get;  set; }=username;
        private string Password {  get; set; }=password;
        public RoleEnum Role { get; set; }=role;

        public List<BorrowedBook> BorrowedBooks { get;  set; } = [];

        public bool CheckPass(string password) 
        {
            return Password == password;                         
        }
    }
}
