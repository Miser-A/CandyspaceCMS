using CandyspaceCMS.Models.Requests;
using CandyspaceCMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandyspaceCMS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/collections")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionsController(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        [HttpPost("create")]
        public IActionResult CreateCollection([FromBody] CreateCollectionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.OwnerId))
                return BadRequest("Title and OwnerId are required.");

            var collectionItem = _collectionRepository.CreateCollection(request.Title, request.OwnerId);
            return Ok(new { CollectionId = collectionItem.ID.ToString(), Message = "Collection created successfully" });
        }

        [HttpGet]
        public IActionResult GetCollections(int page = 1, int pageSize = 10)
        {
            string ownerId = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(ownerId))
                return Unauthorized("User is not authenticated.");

            var collections = _collectionRepository.GetCollectionsByOwner(ownerId, page, pageSize);
            return Ok(collections);
        }

        [HttpPut("{id}/items")]
        public IActionResult AddItemToCollection(string id, [FromBody] AddItemRequest request)
        {
            string ownerId = User.Identity.Name;
            var collection = _collectionRepository.GetCollectionById(id);

            if (collection == null) return NotFound("Collection not found.");
            if (collection["Owner"] != ownerId) return Forbid("Access denied.");

            bool success = _collectionRepository.AddItemToCollection(id, request.Title, request.ItemType, request.ItemUrl);
            if (!success) return StatusCode(500, "Error adding item.");

            return Ok(new { Message = "Item added successfully" });
        }
    }

}
