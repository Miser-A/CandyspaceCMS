namespace CandyspaceCMS.Repositories
{
    public interface ICollectionRepository
    {
        Item CreateCollection(string title, string ownerId);
        Item? GetCollectionById(string collectionId);
        List<Item> GetCollectionsByOwner(string ownerId, int page, int pageSize);
        bool AddItemToCollection(string collectionId, string itemTitle, string itemType, string itemUrl);
    }
}
