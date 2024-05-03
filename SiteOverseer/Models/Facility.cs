using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Facility
    {
        [Key]
        public int FcilId { get; set; }
        [StringLength(50)]
        public required string FcilCde {  get; set; }
        public required string FcilNme { get; set; }
        public string? Address { get; set; }
        [StringLength(50)]
        public string? Township { get; set; }
        public int CityId { get; set; }
        public int FciltypId { get; set; }
        public decimal? Contractval { get; set; }
        public DateTime? ApproveDte { get; set; }
        public DateTime? FcilstartDte { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevdTetime { get; set; }

    }
}
