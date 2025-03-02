namespace CandyspaceCMS.Models
{
    public class Collection
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<DigitalItem> Items { get; set; } = new List<DigitalItem>();
        public List<string> MetadataTags { get; set; } = new List<string>();
        public string Owner { get; set; } = string.Empty;
    }
}
