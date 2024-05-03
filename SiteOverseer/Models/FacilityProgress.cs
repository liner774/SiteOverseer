using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class FacilityProgress
    {
        [Key]
        public long ProgId { get; set; }
        public required int  FlilTskId { get; set; }
        public required DateOnly SubmitDte { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set;}
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }
    }
}
