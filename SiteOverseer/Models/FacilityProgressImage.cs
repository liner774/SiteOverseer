using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class FacilityProgressImage
    {
        [Key]
        public long ImageId { get; set; }
        public long FacilityProgressId { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageName { get; set; }
        public string? ImageContentType { get; set; }

        [ForeignKey("FacilityProgressId")]
        public FacilityProgress FacilityProgress { get; set; }
    }

}
