﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class FacilityType
    {
        [Key]
        [DisplayName("Facility ID")]
        public int FciltypId { get; set; }
        [StringLength(20)]
        [DisplayName("Facility Type Code")]
        public required string FciltypCde { get; set; }
        [StringLength(30)]
        [DisplayName("Facility Type Desc")]
        public string? FciltypDesc { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevdTetime { get; set; }

        //custom
        [DisplayName("Company")]
        [NotMapped]
        public string? Company { get; set; }
        [DisplayName("User")]
        [NotMapped]
        public string? User { get; set; }
    }
}
