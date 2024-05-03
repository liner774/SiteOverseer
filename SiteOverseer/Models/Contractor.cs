using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Contractor
    {
        [Key]
        public int CntorId { get; set; }
        [StringLength(100)]
        public required string CntorNme { get; set; }
        public int FciltypId { get; set; }
        public int ProgpayId { get; set; }
        public DateTime? Establisheddte { get; set; }
        public Boolean BadStatus { get; set; }
        public string? Remark { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDtetime { get; set; }

    }
}
