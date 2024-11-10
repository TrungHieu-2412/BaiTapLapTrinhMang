using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class User
    {
        public string Username { get; private set; }

        public User(string username)
        {
            this.Username = username;
        }
    }
}
