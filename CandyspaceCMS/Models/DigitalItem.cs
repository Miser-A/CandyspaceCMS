using System.ComponentModel.DataAnnotations;

namespace CandyspaceCMS.Models
{
    //This is BlockData
    public class DigitalItem
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> MetadataTags { get; set; } = new List<string>();

    }

    //[ContentType(DisplayName = "Digital Item", GUID = "ANOTHER-GUID-HERE", Description = "An item inside a collection")]
    //public class DigitalItemBlock : BlockData
    //{
    //    [Required]
    //    public virtual string Title { get; set; }

    //    public virtual string Type { get; set; }

    //    public virtual string FilePath { get; set; }

    //    public virtual string Description { get; set; }

    //    public virtual IList<string> MetadataTags { get; set; }
    //}
}
