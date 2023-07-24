using Blogger.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Models.Accounts
{
    public class User
    {
        [Key]
        public int Id { get; set; } 

        public string Username { get; set; }

        public string Email { get; set; }

        public long? Mobile { get; set; }

        public  string Password { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public bool IsActive { get; set; }

    }
}
