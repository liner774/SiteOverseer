using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Menuaccess
    {
        [Key]
        public int AccessId { get; set; }
        public short MnugrpId { get; set; }
        [StringLength(100)]
        public string? BtnNme { get; set; }
        public DateTime RevdTetime { get; set; }
    }
}
