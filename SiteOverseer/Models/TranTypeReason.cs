using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class TranTypeReason
    {
        [Key]
        public short TranReasonId { get; set; }
        [StringLength(50)]
        public required string TranReasonDesc { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDtetime { get; set; }
    }
}
