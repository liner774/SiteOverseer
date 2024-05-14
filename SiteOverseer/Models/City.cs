using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class City
    {
        [Key]
        [DisplayName("CityID")]
        public int CityId { get; set; }
        [StringLength(20)]
        public required string CityCode { get; set; }
        [StringLength(30)]
        public string? CityName { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDtetime { get; set; }
        // Custom
        [NotMapped]
        
        public string? Company { get; internal set; }
        [NotMapped]
        public string? User { get; internal set; }
    }
}
