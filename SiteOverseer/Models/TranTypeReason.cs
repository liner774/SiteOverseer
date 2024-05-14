using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class TranTypeReason
    {
        [Key]
        [DisplayName("Reason ID")]
        public short TranReasonId { get; set; }
        [StringLength(50)]
        [DisplayName("Reason")]
        public required string TranReasonDesc { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDtetime { get; set; }

        // Custom

        [DisplayName("Facility Type Code")]
        [NotMapped]
        public string? FciltypCde { get; set; }
        [DisplayName("Company")]
        [NotMapped]
        public string? Company { get; set; }
        [DisplayName("User")]
        [NotMapped]
        public string? User { get; set; }
    }
}
