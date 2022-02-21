﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceSite.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
    }
}
