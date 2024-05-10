using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(24)]
        [DisplayName("User Code")]
        public required string UserCde { get; set; }
        [StringLength(100)]
        [DisplayName("User Name")]
        public required string UserNme { get; set; }
        [StringLength(100)]
        public required string Position { get; set; }
        [StringLength(24)]
        public required string Gender { get; set; }
        public short MnugrpId { get; set; }
        public required byte[] Pwd { get; set; }
        public short CmpyId { get; set; }
        public DateTime RevdTetime { get; set; }

        //custom
        [NotMapped]
        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(ConfirmPassword), ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }


    }
}
