using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class Menuaccess
    {
        [Key]
        [DisplayName("AccessID")]
        public int AccessId { get; set; }
        [DisplayName("Menu Group Name")]
        public short MnugrpId { get; set; }
        [StringLength(100)]
        [DisplayName("Menu Name")]
        public string? BtnNme { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevdTetime { get; set; }
        [NotMapped]
        public string? MnugrpNme { get; set; } // custom
    }
}
