﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class LogIn
    {
        [NotMapped]
        public string? UserCde { get; set; }
        [NotMapped]
        public required byte[] Pwd { get; set; }
    }
}

