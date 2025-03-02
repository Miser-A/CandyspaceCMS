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
        private readonly CollectionRepository _repository;

        public CollectionsController()
        {
            _repository = new CollectionRepository();
        }

        // 1️⃣ Create a new collection
        [HttpPost("create")]
        public IActionResult CreateCollection([FromBody] CreateCollectionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.OwnerId))
                return BadRequest("Title and OwnerId are required.");

            var collectionItem = _repository.CreateCollection(request.Title, request.OwnerId);
            return Ok(new { CollectionId = collectionItem.ID.ToString(), Message = "Collection created successfully" });
        }

        // 2️⃣ Retrieve all collections for a user
        [HttpGet]
        public IActionResult GetCollections(int page = 1, int pageSize = 10)
        {
            string ownerId = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(ownerId))
                return Unauthorized("User is not authenticated.");

            var collections = _repository.GetCollectionsByOwner(ownerId, page, pageSize);
            return Ok(collections);
        }

        // 3️⃣ Add an item to an existing collection
        [HttpPut("{id}/items")]
        public IActionResult AddItemToCollection(string id, [FromBody] AddItemRequest request)
        {
            string ownerId = User.Identity.Name; // Get the logged-in user’s ID
            var collection = _repository.GetCollectionById(id);

            if (collection == null) return NotFound("Collection not found.");
            if (collection["Owner"] != ownerId) return Forbid("Access denied.");

            bool success = _repository.AddItemToCollection(id, request.Title, request.ItemType, request.ItemUrl);
            if (!success) return StatusCode(500, "Error adding item.");

            return Ok(new { Message = "Item added successfully" });
        }
    }

    // DTOs (Data Transfer Objects) for request validation
    public class CreateCollectionRequest
    {
        public string Title { get; set; }
        public string OwnerId { get; set; }
    }

    public class AddItemRequest
    {
        public string Title { get; set; }
        public string ItemType { get; set; }  // e.g., "Image", "Document"
        public string ItemUrl { get; set; }
    }
}
