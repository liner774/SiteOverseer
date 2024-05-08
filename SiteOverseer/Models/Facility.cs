using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Facility
    {
        [Key]
        [DisplayName("Facility ID")]
        public int FcilId { get; set; }
        [StringLength(50)]
        [DisplayName("Facility Code")]
        public required string FcilCde {  get; set; }
        [DisplayName("Facility Name")]
        public required string FcilNme { get; set; }
        public string? Address { get; set; }
        [StringLength(50)]
        public string? Township { get; set; }
        [DisplayName("City")]
        public int CityId { get; set; }
        [DisplayName("Facility Type")]
        public int FciltypId { get; set; }
        [DisplayName("Contract Value")]
        public decimal? Contractval { get; set; }
        [DisplayName("Approved Date")]
        public DateOnly? ApproveDte { get; set; }
        [DisplayName("Facility Start Date")]
        public DateOnly? FcilstartDte { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevdTetime { get; set; }

    }
}
