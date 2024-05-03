using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class FacilityProgressDocument
    {
        [Key]
        public long DocId { get; set; }
        public required int ProgId { get; set; }
        public required byte[] ProgImg { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }
    }
}
