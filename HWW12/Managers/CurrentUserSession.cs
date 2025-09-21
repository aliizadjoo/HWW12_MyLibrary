using HWW12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Managers
{
  
    public static class CurrentUserSession
    {
    
        public static User? LoggedInUser { get; private set; }
        public static void SetCurrentUser(User user)
        {
            LoggedInUser = user;
        }
        public static void Logout()
        {
            LoggedInUser = null;
        }
    }
}
