using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class FacilityProgressImage
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long FacilityProgressId { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        [MaxLength(256)]
        public string FileName { get; set; }

        [ForeignKey(nameof(FacilityProgressId))]
        public FacilityProgress FacilityProgress { get; set; }
    }
}
