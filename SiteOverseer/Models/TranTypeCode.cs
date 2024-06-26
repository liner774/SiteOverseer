﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class TranTypeCode
    {
        [Key]
        [DisplayName("Tran Type ID")]
        public short TrantypId { get; set; }
        [StringLength(50)]
        [DisplayName("Tran Type Code")]
        public required string TrantypCde { get; set; }
        [StringLength(1)]
        [DisplayName("Tran Nature")]
        [Required(ErrorMessage = "Please choose Tran Nature")]
        public string? TranNature { get; set; }
        [DisplayName("Is Contractor?")]
        public required Boolean ContractorFlg { get; set; }
        [DisplayName("Require Claim?")]
        public required Boolean RequireClaim { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDtetime { get; set; }

        //custom
        [DisplayName("Company")]
        [NotMapped]
        public string? Company { get; set; }
        [DisplayName("User")]
        [NotMapped]
        public string? User { get; set; }
    }
}
