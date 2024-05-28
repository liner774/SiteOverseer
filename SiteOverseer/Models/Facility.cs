using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(ErrorMessage = "Please Choose City")]
        public int? CityId { get; set; }
        [DisplayName("Facility Type")]
        [Required(ErrorMessage = "Please Choose Facility Type")]
        public int? FciltypId { get; set; }
        [DisplayName("Contract Value")]
        public decimal? Contractval { get; set; }
        [DisplayName("Approved Date")]
        [Required(ErrorMessage = "Approved Date is required")]
        public DateTime? ApproveDte { get; set; }
        [DisplayName("Facility Start Date")]
        [Required(ErrorMessage = "Facility Start Date is required")]
        public DateTime? FcilstartDte { get; set; }
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
        [NotMapped]
        [StringLength(30)]
        public string? CityName { get; set; }
        [DisplayName("Facility Type")]
        [NotMapped]
        public string? FciltypCde { get; internal set; }


    }
}
