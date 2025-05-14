
using Backend.DTOs.Items;
using Backend.Helpers;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Backend.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Main Controller")]
    public class MainController(ICategoriesServices _categoriesServices, IItemServices _itemServices) : ControllerBase
    {
        [HttpGet("groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            Log.Information("GET /groups started");
            try
            {
                var groups = await _categoriesServices.GetAllGroups(Request);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GET /groups");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("subones/{groupId}")]
        public async Task<IActionResult> GetSubOnesByGroupId(string groupId)
        {
            Console.WriteLine($"🧪 REACHED GetSubOnesByGroupId with groupId = {groupId}");
            if (string.IsNullOrWhiteSpace(groupId)) return BadRequest("Invalid Group ID");

            Log.Information("GET /subones/{GroupId} started", groupId);
            try
            {
                var result = await _categoriesServices.GetAllSubOnesByGroupId(groupId, Request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Group not found: {GroupId}", groupId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GET /subones/{GroupId}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("subtwos")]
        public async Task<IActionResult> GetSubTwosByGroupAndSubOne([FromQuery] string groupId, [FromQuery] string subOneId)
        {
            if (string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(subOneId))
                return BadRequest("Invalid Group ID or SubOne ID");

            Log.Information("GET /subtwos?groupId={GroupId}&subOneId={SubOneId} started", groupId, subOneId);

            try
            {
                var result = await _categoriesServices.GetAllSubTwosByGroupAndSubOne(groupId, subOneId, Request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "SubOne not found under Group: {GroupId}, {SubOneId}", groupId, subOneId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GET /subtwos");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("subthrees")]
        public async Task<IActionResult> GetSubThreesByFullPath([FromQuery] string groupId, [FromQuery] string subOneId, [FromQuery] string subTwoId)
        {
            if (string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(subOneId) || string.IsNullOrWhiteSpace(subTwoId))
                return BadRequest("Invalid Group, SubOne or SubTwo ID");

            Log.Information("GET /subthrees?groupId={GroupId}&subOneId={SubOneId}&subTwoId={SubTwoId} started", groupId, subOneId, subTwoId);

            try
            {
                var result = await _categoriesServices.GetAllSubThreesByFullPath(groupId, subOneId, subTwoId, Request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "SubTwo not found under full path: {GroupId}, {SubOneId}, {SubTwoId}", groupId, subOneId, subTwoId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GET /subthrees");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("items/all")]
        public async Task<IActionResult> GlobalSearchItems(string term, int page = 1, int pageSize = 30)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term is required.");
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be positive integers.");

            Log.Information("GET /items/global-search started with term='{Term}', page={Page}, pageSize={PageSize}", term, page, pageSize);

            try
            {
                var results = await _itemServices.GlobalSearchItemsAsync(term, Request, page, pageSize);

                var resultWithUrls = results.Select(i => new GetItemsDto
                {
                    ItemNo = i.ItemNo,
                    Name = i.Name,
                    FirstImage = UrlHelper.GetItemImageUrl(i.FirstImage, Request),
                    Status = i.Status
                }).ToList();

                return Ok(resultWithUrls);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GET /items/global-search");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItemsByCategoryPath(string groupId, string subOneId, string? subTwoId, string? subThreeId, int page = 1, int pageSize = 30)
        {
            if (string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(subOneId))
                return BadRequest("GroupId and SubOneId are required.");
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be positive integers.");

            Log.Information("GET /items started with groupId={GroupId}, subOneId={SubOneId}, subTwoId={SubTwoId}, subThreeId={SubThreeId}, page={Page}, pageSize={PageSize}",
                groupId, subOneId, subTwoId, subThreeId, page, pageSize);

            try
            {
                var items = await _itemServices.GetItemsWithPaginationAsync(groupId, subOneId, subTwoId, subThreeId, Request, page, pageSize);

                var resultWithUrls = items.Select(i => new GetItemsDto
                {
                    ItemNo = i.ItemNo,
                    Name = i.Name,
                    FirstImage = UrlHelper.GetItemImageUrl(i.FirstImage, Request),
                    Status = i.Status
                }).ToList();

                return Ok(resultWithUrls);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GET /items");
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("items/search")]
        public async Task<IActionResult> SearchItemsByCategoryPath(string term, string groupId, string subOneId, string? subTwoId, string? subThreeId, int page = 1, int pageSize = 30)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term is required.");
            if (string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(subOneId))
                return BadRequest("GroupId and SubOneId are required.");
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be positive integers.");

            Log.Information("GET /items/search started with term='{Term}', groupId={GroupId}, subOneId={}, subTwoId={}, subThreeId={}, page={Page}, pageSize={PageSize}",
                term, groupId, subOneId, subTwoId, subThreeId, page, pageSize);

            try
            {
                var results = await _itemServices.SearchItemsAsync(term, groupId, subOneId, subTwoId, subThreeId, Request, page, pageSize);

                var resultWithUrls = results.Select(i => new GetItemsDto
                {
                    ItemNo = i.ItemNo,
                    Name = i.Name,
                    FirstImage = UrlHelper.GetItemImageUrl(i.FirstImage, Request),
                    Status = i.Status
                }).ToList();

                return Ok(resultWithUrls);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GET /items/search");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("items/by-status")]
        public async Task<IActionResult> GetItemsByStatus(string statusId, int page = 1, int pageSize = 30)
        {
            if (string.IsNullOrWhiteSpace(statusId))
                return BadRequest("Invalid StatusId.");
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be positive integers.");

            var items = await _itemServices.GetItemsByStatusAsync(statusId, Request, page, pageSize);
            var resultWithUrls = items.Select(i => new GetItemsDto
            {
                ItemNo = i.ItemNo,
                Name = i.Name,
                FirstImage = UrlHelper.GetItemImageUrl(i.FirstImage, Request),
                Status = i.Status
            }).ToList();

            return Ok(resultWithUrls);
        }

        [HttpGet("items/{itemNo}")]
        public async Task<IActionResult> GetItemByItemNo(string itemNo)
        {
            if (string.IsNullOrWhiteSpace(itemNo)) return BadRequest("Invalid ItemNo.");

            var item = await _itemServices.GetItemByItemNoAsync(itemNo);
            if (item == null) return NotFound($"No item found with ItemNo {itemNo}.");

            return Ok(item);
        }

        [HttpGet("items/{itemNo}/images/{imageId}")]
        public async Task<IActionResult> GetItemImage(string itemNo, string imageId)
        {
            var image = await _itemServices.GetImageByItemNoAndImageIdAsync(itemNo, imageId);
            if (image == null)
                return NotFound("Image not found for this item.");

            var imageUrl = UrlHelper.GetItemImageUrl(image, Request);
            return Ok(new { imageUrl });
        }

        [HttpGet("items/{itemNo}/status")]
        public async Task<IActionResult> GetItemStatus(string itemNo)
        {
            if (string.IsNullOrWhiteSpace(itemNo))
                return BadRequest("Invalid ItemNo.");

            var status = await _itemServices.GetItemStatusAsync(itemNo, Request);
            if (status == null)
                return NotFound("Status not found for this item.");

            return Ok(status);
        }
    }
}
