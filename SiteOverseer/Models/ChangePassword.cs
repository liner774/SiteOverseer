using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SiteOverseer.Models
{
    public class ChangePassword
    {
        public int UserId { get; set; }
        [StringLength(24)]
        [DisplayName("User Code")]
        public required string UserCde { get; set; }
        [StringLength(100)]
        [DisplayName("User Name")]
        public required string UserNme { get; set; }
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
