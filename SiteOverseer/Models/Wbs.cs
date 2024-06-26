﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class Wbs
    {
        [Key]
        [DisplayName("WBS ID")]
        public int WbsId { get; set; }
        [StringLength(100)]
        [DisplayName("WBS Name")]
        public required string WbsCde {  get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevdTetime { get; set; }

        //custom
        [NotMapped]
        [DisplayName("WBS Codes")]
        public string? WbsdCde { get; set; }

    }
}
