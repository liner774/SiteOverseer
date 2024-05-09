using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Wbs
    {
        [Key]
        [DisplayName("Wbs ID")]
        public int WbsId { get; set; }
        [StringLength(100)]
        public required string WbsCde {  get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevdTetime { get; set; }
    }
}
