using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class Contractor
    {
        [Key]
        [DisplayName("Contractor ID")]
        public int CntorId { get; set; }
        [StringLength(100)]
        [DisplayName("Contractor Name")]
        public required string CntorNme { get; set; }
        [DisplayName("Faciliy Type")]
        [Required(ErrorMessage = "Please Choose Facility Type")]
        public int? FciltypId { get; set; }
        [DisplayName("Progress Payment")]
        [Required(ErrorMessage = "Please Choose Progress Payment")]
        public int? ProgpayId { get; set; }
        [DisplayName("Established Date")]
        public DateTime? Establisheddte { get; set; }
        [DisplayName("Bad Status")]
        public Boolean BadStatus { get; set; }
        public string? Remark { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDtetime { get; set; }

        //custom
        
        [DisplayName("Facility Type Code")]
        [NotMapped]
        public string? FcilityCde{ get; internal set; }
        [DisplayName("Company")]
        [NotMapped]
        public string? Company { get; internal set; }
        [DisplayName("User")]
        [NotMapped]
        public string? User { get; internal set; }
    }
}
