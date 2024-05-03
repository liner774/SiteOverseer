using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [StringLength(20)]
        public required string CityCode { get; set; }
        [StringLength(30)]
        public string? CityName { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDtetime { get; set; }

    }
}
