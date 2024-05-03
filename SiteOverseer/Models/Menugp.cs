using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Menugp
    {
        [Key]
        public short MnugrpId { get; set; }
        public required string MnugrpNme { get; set; }
        public DateTime RevdTetime { get; set; }
    }
}
