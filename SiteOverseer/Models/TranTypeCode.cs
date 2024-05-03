using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class TranTypeCode
    {
        [Key]
        public short TrantypId { get; set; }
        [StringLength(50)]
        public required string TrantypCde { get; set; }
        [StringLength(1)]
        public required string TranNature { get; set; }
        public required Boolean ContractorFlg { get; set; }
        public required Boolean RequireClaim { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDtetime { get; set; }
    }
}
