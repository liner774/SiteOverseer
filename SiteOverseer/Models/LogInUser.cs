using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SiteOverseer.Models
{
    public class LogInUser
    {
        public string UserCde { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
    }
}
