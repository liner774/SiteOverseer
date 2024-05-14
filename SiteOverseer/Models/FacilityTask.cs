using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class FacilityTask
    {
        [Key]
        [DisplayName("Facility Task ID")]
        public int FciltskId { get; set; }
        public int FcilId { get; set; }
        public int WbsdId { get; set; }
        public required decimal Budget { get; set; }
        public int CntorId { get; set; }
        [StringLength(50)]
        [DisplayName("Selection Type")]
        public required string SelectionTyp { get; set; }
        [DisplayName("Work Start Date")]
        public DateTime WorkstartDte { get; set; }
        [DisplayName("Work End Date")]
        public DateTime? WorkendDte { get; set; }
        [DisplayName("Awarded Value")]
        public decimal? AwardedValue { get; set; }
        [DisplayName("Progress Payment")]
        public int ProgpayId { get; set; }
        [DisplayName("Allow Submit Expense")]
        public required Boolean AllowSubmitExpense { get; set; }
        [DisplayName("Task Complete")]
        public required Boolean TaskCompleteFlg { get; set; }
        public string? Remark { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevdTetime { get; set; }

        //custom
        [NotMapped]
        [DisplayName("Facility")]
        public  string? FcilNme { get; set; }

        [NotMapped]
        [DisplayName("Contractor")]
        public string? CntorNme { get; set; }

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
