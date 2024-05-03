using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class Company
    {
        [Key]
        public short CmpyId { get; set; }
        [StringLength(100)]
        public required string CmpyNme { get; set; }
        public string? Address { get; set; }
        public DateTime RevDtetime { get; set; }
    }
}
