using SiteOverseer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class FacilityProgressHistory
{
    [Key]
    public long HistoryId { get; set; }
    public long FacilityProgressId { get; set; }
    public short ProgPercent { get; set; }
    public DateTime UpdatedAt { get; set; }

    public byte[]? ImageData { get; set; }
    public string? ImageName { get; set; }
    public string? ImageContentType { get; set; }

    [ForeignKey("FacilityProgressId")]
    public FacilityProgress FacilityProgress { get; set; }
}
