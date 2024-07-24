using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class FacilityProgress
    {
        [Key]
        [DisplayName("Progress ID")]
        public long ProgId { get; set; }
        [DisplayName("Facility Task ID")]
        public int? FcilTskId { get; set; }
        [DisplayName("Progress Percent")]
        [Required(ErrorMessage = "Please select Progress Percent")]
        public short? ProgPercent { get; set; }
        [Required(ErrorMessage = "Please select Submitted Date")]
        [DisplayName("Submitted Date")]
        public DateTime? SubmitDte { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        [DisplayName("Company")]
        public short? CmpyId { get; set; }
        [DisplayName("User")]
        public int? UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime? RevDteTime { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageName { get; set; }
        public string? ImageContentType { get; set; }
        public ICollection<FacilityProgressImage> Images { get; set; } = new List<FacilityProgressImage>();
        public ICollection<FacilityProgressHistory> ProgressHistory { get; set; } = new List<FacilityProgressHistory>();


        //Custom
     
        [NotMapped]

        [Required(ErrorMessage = "Please choose Facility")]
        public int? FcilId { get; set; }
        [NotMapped]
        [DisplayName("Facility")]
        public string? FcilNme { get; set; }
        [NotMapped]
        public string? FcilCde { get; set; }

        [NotMapped]

        [Required(ErrorMessage = "Please choose WBS Detail")]
        public int? WbsdId { get; set; }
        [NotMapped]
        [DisplayName("WBS")]
        public int? WbsId { get; set; }
        [NotMapped]
        public string? WbsCde { get; set; }
        [NotMapped]
        public string? WbsdCde { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please choose Contractor")]
        public int? CntorId { get; set; }
        [NotMapped]
        [DisplayName("Contractor")]
        public string? CntorNme { get; set; }

        [NotMapped]
        [DisplayName("Work Planned Start Date")]
        public DateTime? WorkstartDte { get; set; }
        [NotMapped]
        [DisplayName("Work Expected End Date")]
        public DateTime? WorkendDte { get; set; }

        [NotMapped]

        [DisplayName("Task Complete?")]
        public bool TaskCompleteFlg { get; set; }
        [NotMapped]
        public decimal? Budget { get; set; }
        [DisplayName("Company")]
        [NotMapped]
        public string? Company { get; set; }
        [DisplayName("User")]
        [NotMapped]
        public string? User { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string? FCompany { get; set; }
        [DisplayName("User")]
        [NotMapped]
        public string? FUser { get; set; }
        [NotMapped]
        public short? FC_Id { get; set; }
        [NotMapped]
        public int? FU_Id { get; set; }
        [NotMapped]
        [DisplayName("Revised Datetime")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime RevdTetime { get; set; }
        [NotMapped]
        [DisplayName("Selection Type")]
        public string? SelectionTyp { get; set; }
        [NotMapped]
        [DisplayName("Awarded Value")]
        public decimal? AwardedValue { get; set; }
        [NotMapped]
        [DisplayName("Progress Payment")]

        [Required(ErrorMessage = "Please choose Progress Payment")]
        public int? ProgpayId { get; set; }
        [NotMapped]
        [DisplayName("Allow Submit Expense")]
        public bool AllowSubmitExpense { get; set; }
        [NotMapped]
        public string? Remark { get; set; }

    }
}
