using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class TranTypeReason
    {
        [Key]
        [DisplayName("Reason ID")]
        public short TranReasonId { get; set; }
        [StringLength(50)]
        [DisplayName("Transaction Reason")]
        public required string TranReasonDesc { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDtetime { get; set; }
    }
}
