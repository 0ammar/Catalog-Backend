using Backend.DTOs.Items;
using Microsoft.AspNetCore.Http;

namespace Backend.Interfaces
{
    public interface IItemServices
    {
        Task<List<GetItemsDto>> GlobalSearchItemsAsync(string term, HttpRequest request, int page = 1, int pageSize = 30);

        // ✅ Get paginated items by category path (Group > SubOne > [SubTwo] > [SubThree])
        Task<List<GetItemsDto>> GetItemsWithPaginationAsync(
            string groupId,
            string subOneId,
            string? subTwoId,
            string? subThreeId,
            HttpRequest request,
            int page = 1,
            int pageSize = 30);

        // ✅ Search items by name or number within category path
        Task<List<GetItemsDto>> SearchItemsAsync(
            string term,
            string groupId,
            string subOneId,
            string? subTwoId,
            string? subThreeId,
            HttpRequest request,
            int page = 1,
            int pageSize = 30);

        // ✅ Get items by status
        Task<List<GetItemsDto>> GetItemsByStatusAsync(
            string statusId,
            HttpRequest request,
            int page = 1,
            int pageSize = 30);

        // ✅ Get item full details
        Task<GetItemDto?> GetItemByItemNoAsync(string itemNo);

        // ✅ Get item image filenames
        Task<List<string>> GetItemImagesAsync(string itemNo, HttpRequest request);

        // ✅ Get single image URL
        Task<string?> GetImageByItemNoAndImageIdAsync(string itemNo, string imageId);

        // ✅ Status utilities
        Task<List<ItemStatusDto>> GetAllItemStatusesAsync(HttpRequest request);
        Task<ItemStatusDto?> GetItemStatusAsync(string itemNo, HttpRequest request);

        // ✅ Manage item images
        Task AddImagesToItemAsync(string itemNo, List<IFormFile>? images);
        Task DeleteItemImagesAsync(string itemNo, List<string> imagesToDelete);

        // ✅ Update item metadata
        Task<bool> UpdateItemStatusAsync(string itemNo, string status);
        Task<bool> UpdateItemDescriptionAsync(string itemNo, string? description);
    }
}
