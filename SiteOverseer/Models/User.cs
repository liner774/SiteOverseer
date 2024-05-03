using System.ComponentModel.DataAnnotations;

namespace SiteOverseer.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(24)]
        public required string UserCde { get; set; }
        [StringLength(100)]
        public required string UserNme { get; set; }
        [StringLength(100)]
        public required string Position { get; set; }
        [StringLength(24)]
        public required string Gender { get; set; }
        public short MnugrpId { get; set; }
        public required byte[] Pwd { get; set; }
        public short CmpyId { get; set; }
        public DateTime RevdTetime { get; set; }

    }
}
