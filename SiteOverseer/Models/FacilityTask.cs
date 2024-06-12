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
        [Required(ErrorMessage = "Please choose Facility")]
        public int? FcilId { get; set; }
        [DisplayName("WBS Detail")]
        [Required(ErrorMessage = "Please choose WBS Detail")]
        public int? WbsdId { get; set; }
        [Required(ErrorMessage = "Budget is required")]
        public decimal? Budget { get; set; }
        [Required(ErrorMessage = "Please choose Contractor")]
        public int? CntorId { get; set; }
        [StringLength(50)]
        [DisplayName("Selection Type")]
        [Required(ErrorMessage = "Selection Type is required")]
        public required string SelectionTyp { get; set; }
        [DisplayName("Work Start Date")]
        [Required(ErrorMessage = "Work Start Date is required")]
        public DateTime? WorkstartDte { get; set; }
        [DisplayName("Work End Date")]
        [Required(ErrorMessage = "Work End Date is required")]
        public DateTime? WorkendDte { get; set; }
        [DisplayName("Awarded Value")]
        public decimal? AwardedValue { get; set; }
        [DisplayName("Progress Payment")]
        [Required(ErrorMessage = "Progress Payment is required")]
        public int? ProgpayId { get; set; }
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
        
        [NotMapped]
        public  string? WbsdCde { get; set; }
        [NotMapped]
        public string? WbsCde { get; set; }
        [NotMapped]
        [DisplayName("WBS Code")]       
        public int? WbsId { get; set; }
        [DisplayName("Currency Code")]
        [NotMapped]
        public string? Currcde { get; set; }
    }
}
