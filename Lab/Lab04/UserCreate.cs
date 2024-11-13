using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04
{
    public class UserCreate
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Language { get; set; }
        public string Phone { get; set; }
        public bool EmailVerified { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperuser { get; set; }
    }
}