using Backend.DTOs.Categories;
using Microsoft.AspNetCore.Http;

namespace Backend.Interfaces
{
    public interface ICategoriesServices
    {
        // ✅ Read operations
        Task<List<GroupDto>> GetAllGroups(HttpRequest request);

        Task<List<SubOneDto>> GetAllSubOnesByGroupId(string groupId, HttpRequest request);

        Task<List<SubTwoDto>> GetAllSubTwosByGroupAndSubOne(string groupId, string subOneId, HttpRequest request);

        Task<List<SubThreeDto>> GetAllSubThreesByFullPath(string groupId, string subOneId, string subTwoId, HttpRequest request);

        // ✅ Upload image to category
        Task UploadGroupImageAsync(string id, IFormFile file);
        Task UploadSubOneImageAsync(string id, IFormFile file);
        Task UploadSubTwoImageAsync(string id, IFormFile file);
        Task UploadSubThreeImageAsync(string id, IFormFile file);

        // ✅ Delete image from category
        Task<bool> DeleteGroupImageAsync(string id, string imageUrl);
        Task<bool> DeleteSubOneImageAsync(string id, string imageUrl);
        Task<bool> DeleteSubTwoImageAsync(string id, string imageUrl);
        Task<bool> DeleteSubThreeImageAsync(string id, string imageUrl);
    }
}
