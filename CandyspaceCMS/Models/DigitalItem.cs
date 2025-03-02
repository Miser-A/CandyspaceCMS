namespace CandyspaceCMS.Models
{
    public class DigitalItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; 
        public string FilePath { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
        public List<string> MetadataTags { get; set; } = new List<string>();

    }
}
