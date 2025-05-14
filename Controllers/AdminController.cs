using Backend.DTOs.Items;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AlmutasaweqCatalog.Controllers
{
	[Route("api/admin")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	[Tags("Admin Controller")]
	[Authorize(Roles = "Admin")]
	public class AdminController(ICategoriesServices _categoriesServices, IItemServices _itemServices) : ControllerBase
	{

		/// <summary>
		/// Upload an image for a Group category.
		/// </summary>
		[HttpPost("group/{id}")]
		public async Task<IActionResult> UploadGroupImage(string id, IFormFile file)
		{
            if (string.IsNullOrWhiteSpace(id) || file == null) return BadRequest("Invalid input.");
            Log.Information("Uploading image for Group ID {Id}", id);

			try
			{
				await _categoriesServices.UploadGroupImageAsync(id, file);
				return Ok("Image uploaded successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}

		/// <summary>
		/// Upload an image for a SubOne category.
		/// </summary>
		[HttpPost("subone/{id}")]
		public async Task<IActionResult> UploadSubOneImage(string id, IFormFile file)
		{
            if (string.IsNullOrWhiteSpace(id) || file == null) return BadRequest("Invalid input.");
            Log.Information("Uploading image for SubOne ID {Id}", id);

			try
			{
				await _categoriesServices.UploadSubOneImageAsync(id, file);
				return Ok("Image uploaded successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}

		/// <summary>
		/// Upload an image for a SubTwo category.
		/// </summary>
		[HttpPost("subtwo/{id}")]
		public async Task<IActionResult> UploadSubTwoImage(string id, IFormFile file)
		{
            if (string.IsNullOrWhiteSpace(id) || file == null) return BadRequest("Invalid input.");
            Log.Information("Uploading image for SubTwo ID {Id}", id);

			try
			{
				await _categoriesServices.UploadSubTwoImageAsync(id, file);
				return Ok("Image uploaded successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}

		/// <summary>
		/// Upload an image for a SubThree category.
		/// </summary>
		[HttpPost("subthree/{id}")]
		public async Task<IActionResult> UploadSubThreeImage(string id, IFormFile file)
		{
            if (string.IsNullOrWhiteSpace(id) || file == null) return BadRequest("Invalid input.");
            Log.Information("Uploading image for SubThree ID {Id}", id);

			try
			{
				await _categoriesServices.UploadSubThreeImageAsync(id, file);
				return Ok("Image uploaded successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}


		/// <summary>
		/// Delete the image of a Group category.
		/// </summary>
		[HttpDelete("group/{id}")]
		public async Task<IActionResult> DeleteGroupImage(string id, [FromQuery] string imageUrl)
		{
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(imageUrl)) return BadRequest("Invalid input.");
            Log.Information("Deleting image for Group ID {Id}", id);

			try
			{
				await _categoriesServices.DeleteGroupImageAsync(id, imageUrl);
				return Ok("Image deleted successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}

		/// <summary>
		/// Delete the image of a SubOne category.
		/// </summary>
		[HttpDelete("subone/{id}")]
		public async Task<IActionResult> DeleteSubOneImage(string id, [FromQuery] string imageUrl)
		{
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(imageUrl)) return BadRequest("Invalid input.");
            Log.Information("Deleting image for SubOne ID {Id}", id);

			try
			{
				await _categoriesServices.DeleteSubOneImageAsync(id, imageUrl);
				return Ok("Image deleted successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}

		/// <summary>
		/// Delete the image of a SubTwo category.
		/// </summary>
		[HttpDelete("subtwo/{id}")]
		public async Task<IActionResult> DeleteSubTwoImage(string id, [FromQuery] string imageUrl)
		{
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(imageUrl)) return BadRequest("Invalid input.");
            Log.Information("Deleting image for SubTwo ID {Id}", id);

			try
			{
				await _categoriesServices.DeleteSubTwoImageAsync(id, imageUrl);
				return Ok("Image deleted successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}

		/// <summary>
		/// Delete the image of a SubThree category.
		/// </summary>
		[HttpDelete("subthree/{id}")]
		public async Task<IActionResult> DeleteSubThreeImage(string id, [FromQuery] string imageUrl)
		{
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(imageUrl)) return BadRequest("Invalid input.");
            Log.Information("Deleting image for SubThree ID {Id}", id);

			try
			{
				await _categoriesServices.DeleteSubThreeImageAsync(id, imageUrl);
				return Ok("Image deleted successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
		}

		/// <summary>
		/// Get all images for an item (without other details).
		/// </summary>
		[HttpGet("items/{itemNo}/images")]
		public async Task<IActionResult> GetItemImages(string itemNo)
		{
			if (string.IsNullOrWhiteSpace(itemNo))
				return BadRequest("Invalid ItemNo.");

			try
			{
				var images = await _itemServices.GetItemImagesAsync(itemNo, Request);
				return Ok(images);
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
		}

		/// <summary>
		/// Add new images and/or description to an existing item.
		/// </summary>
		[HttpPost("items/{itemNo}/images")]
		public async Task<IActionResult> AddImagesToItem(string itemNo, [FromForm] UploadImagesDto? dto)
		{
			if (string.IsNullOrWhiteSpace(itemNo)) return BadRequest("Invalid ItemNo.");

			try
			{
				await _itemServices.AddImagesToItemAsync(itemNo, dto?.NewImages);
				return Ok("Added successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
			catch (Exception ex) { return StatusCode(500, ex.InnerException?.Message ?? ex.Message); }
		}

		/// <summary>
		/// Delete multiple item images.
		/// </summary>
		[HttpDelete("items/{itemNo}/images")]
		public async Task<IActionResult> DeleteItemImages(string itemNo, [FromBody] List<string>? imageNames)
		{
			if (string.IsNullOrWhiteSpace(itemNo) || imageNames == null || imageNames.Count == 0)
				return BadRequest("Invalid request data.");

			try
			{
				await _itemServices.DeleteItemImagesAsync(itemNo, imageNames);
				return Ok("Selected images deleted successfully.");
			}
			catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
			catch (Exception ex) { return StatusCode(500, ex.InnerException?.Message ?? ex.Message); }
		}

		/// <summary>
		/// Update only the description of an item.
		/// </summary>
		[HttpPut("items/{itemNo}/description")]
		public async Task<IActionResult> UpdateItemDescription(string itemNo, [FromBody] UpdateDescriptionDto dto)
		{
			if (string.IsNullOrWhiteSpace(itemNo))
				return BadRequest("Invalid ItemNo.");

			try
			{
				var success = await _itemServices.UpdateItemDescriptionAsync(itemNo, dto.Description);
				if (!success)
					return NotFound("Item not found.");

				return Ok("Description updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
			}
		}

		/// <summary>
		/// Get all item statuses (icons, names, codes).
		/// </summary>
		[HttpGet("items/statuses")]
		public async Task<IActionResult> GetAllItemStatuses()
		{
			try
			{
				var statuses = await _itemServices.GetAllItemStatusesAsync(Request);
				return Ok(statuses);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
			}
		}

		/// <summary>
		/// Update the status of an item.
		/// </summary>
		[HttpPut("items/{itemNo}/status")]
		public async Task<IActionResult> UpdateItemStatus(string itemNo, [FromQuery] string statusId)
		{
            if (string.IsNullOrWhiteSpace(itemNo) || string.IsNullOrWhiteSpace(statusId))
                return BadRequest("Invalid request data.");

            try
			{
				var success = await _itemServices.UpdateItemStatusAsync(itemNo, statusId);
				if (!success)
					return NotFound("Item not found.");

				return Ok("Status updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
