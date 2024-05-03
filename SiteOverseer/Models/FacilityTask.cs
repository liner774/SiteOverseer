using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class FacilityTask
    {
        [Key]
        public int FciltskId { get; set; }
        public int FcilId { get; set; }
        public int WbsdId { get; set; }
        public required decimal Budget { get; set; }
        public int CntorId { get; set; }
        [StringLength(50)]
        public required string SelectionTyp { get; set; }
        public DateTime WorkstartDte { get; set; }
        public DateTime? WorkendDte { get; set; }
        public decimal? AwardedValue { get; set; }
        public int ProgpayId { get; set; }
        public required Boolean AllowSubmitExpense { get; set; }
        public required Boolean TaskCompleteFlg { get; set; }
        public string? Remark { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevdTetime { get; set; }
    }
}
