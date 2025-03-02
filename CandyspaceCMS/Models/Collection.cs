using Sitecore.Web;
using System.ComponentModel.DataAnnotations;

namespace CandyspaceCMS.Models
{

    //This would be PageData
    public class Collection
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<DigitalItem> Items { get; set; } = new List<DigitalItem>();
        public List<string> MetadataTags { get; set; } = new List<string>();
        public string Owner { get; set; } = string.Empty;
    }

    //[ContentType(DisplayName = "Collection", GUID = "YOUR-GUID-HERE", Description = "A digital collection")]
    //public class CollectionPage : PageData
    //{
    //    [Required]
    //    public virtual string Title { get; set; }

    //    public virtual string Description { get; set; }

    //    public virtual IList<DigitalItemBlock> Items { get; set; }

    //    public virtual IList<string> MetadataTags { get; set; }

    //    public virtual string Owner { get; set; }
    //}
}
