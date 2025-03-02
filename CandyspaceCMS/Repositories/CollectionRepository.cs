using Microsoft.AspNetCore.Mvc.RazorPages;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System.Collections.Generic;
using Glass.Mapper.Sc;

namespace CandyspaceCMS.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ISitecoreService _sitecoreService;

        private readonly ICacheService _cache;

        public CollectionRepository(ISitecoreService sitecoreService, ICacheService cacheService)
        {
            _sitecoreService = sitecoreService;

            _cache = cacheService;
        }

        public Item CreateCollection(string title, string ownerId)
        {
            using (new SecurityDisabler())
            {
                Item parentItem = _sitecoreService.GetItem("/sitecore/content/DigitalArchives/Collections");

                TemplateID collectionTemplate = new TemplateID(new ID("{YOUR-COLLECTION-TEMPLATE-ID}"));
                Item newCollection = parentItem.Add(title, collectionTemplate);

                newCollection.Editing.BeginEdit();
                newCollection["Owner"] = ownerId;
                newCollection["CreatedDate"] = DateTime.UtcNow.ToString("o");
                newCollection.Editing.EndEdit();

                return newCollection;
            }
        }

        public Item? GetCollectionById(string collectionId)
        {
            return _sitecoreService.GetItem(new ID(collectionId));
        }

        public List<Item> GetCollectionsByOwner(string ownerId, int page, int pageSize)
        {
            string cacheKey = $"collections-{ownerId}-page{page}";

            if (_cache.Contains(cacheKey))
                return _cache.Get<List<Item>>(cacheKey);

            var query = $"/sitecore/content/DigitalArchives/Collections//*[@Owner='{ownerId}']";
            var collections = Sitecore.Context.Database.SelectItems(query)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            _cache.Add(cacheKey, collections, TimeSpan.FromMinutes(10));
            return collections;
        }

        public bool AddItemToCollection(string collectionId, string itemTitle, string itemType, string itemUrl)
        {
            using (new SecurityDisabler())
            {
                Item collection = _sitecoreService.GetItem(new ID(collectionId));
                if (collection == null) return false;

                TemplateID itemTemplate = new TemplateID(new ID("{GUID goes here}"));
                Item newItem = collection.Add(itemTitle, itemTemplate);

                newItem.Editing.BeginEdit();
                newItem["ItemType"] = itemType;
                newItem["ItemUrl"] = itemUrl;
                newItem.Editing.EndEdit();

                return true;
            }
        }
    }
}
