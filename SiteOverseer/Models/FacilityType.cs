using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class FacilityType
    {
        [Key]
        public int FciltypId { get; set; }
        [StringLength(20)]
        public required string FciltypCde { get; set; }
        [StringLength(30)]
        public string? FciltypDesc { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevdTetime { get; set; }
    }
}
