using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Menugp
    {
        [Key]
        [DisplayName("Menu Group ID")]
        public short MnugrpId { get; set; }
        [DisplayName("Menu Group Name")]
        public required string MnugrpNme { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevdTetime { get; set; }
    }
}
