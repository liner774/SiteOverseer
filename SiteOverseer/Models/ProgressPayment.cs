using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOverseer.Models
{
    public class ProgressPayment
    {
        [Key]
        [DisplayName("Progress Payment Id")]
        public int Progpayid { get; set; }
        [DisplayName("Currency Code")]
        public string Currcde {  get; set; }
        [DisplayName("Currency Rate")]
        public decimal Currate { get; set; }
        [DisplayName("Company Id")]
        public short CmpyId { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }
        [DisplayName("Revised DateTime")]
        public DateTime RevdTetime { get; set; }

        [DisplayName("Company Name")]
        [NotMapped]
        public string? CmpyNme { get; set; }
        [DisplayName("User Name")]
        [NotMapped]
        public string? UserNme {  get; set; }
    }
}
