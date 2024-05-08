using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public required string TranNature { get; set; }
        public required Boolean ContractorFlg { get; set; }
        public required Boolean RequireClaim { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDtetime { get; set; }
    }
}
