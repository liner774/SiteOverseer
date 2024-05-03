using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class WbsDetail
    {
        [Key]
        public int WbsdId { get; set; }
        public int WbsId { get; set; }
        [StringLength(100)]
        public required string WbsdCde { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevdTetime { get; set; }
    }
}
