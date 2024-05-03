using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class FacilityTranLog
    {
        [Key]
        public long  TrLogId { get; set; }
        public int FcilTskId { get; set; }
        public short TranTypId { get; set; }
        public short TranReasonId { get; set; }
        public required decimal Amount { get; set; }
        public string? Remark { get; set; }
        public bool ClaimFlg { get; set; }
        public DateTime? ClaimDteTime { get; set; }
        public bool DeleteFlg { get; set; }
        public DateTime? DeleteDteTime { get; set; }
        public int? DeletedBy { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }


    }
}
