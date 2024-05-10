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
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevdTetime { get; set; }
    }
}
