﻿namespace Backend.Helpers
{
	public static class UrlHelper
	{

        public static string GetCategoryImageUrl(string? imageFileName, HttpRequest request)
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var finalImage = string.IsNullOrEmpty(imageFileName) ? "no-image.png" : imageFileName;
            return $"{baseUrl}/CategoriesImages/{finalImage}";
        }

        public static string GetItemImageUrl(string? imageFileName, HttpRequest request)
		{
			var baseUrl = $"{request.Scheme}://{request.Host}";
			var finalImage = string.IsNullOrEmpty(imageFileName) ? "no-image.png" : imageFileName;
			return $"{baseUrl}/UploadedImages/{finalImage}";
		}
        public static string GetStatusIconUrl(string? imageFileName, HttpRequest request)
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";

            if (string.IsNullOrWhiteSpace(imageFileName))
                return $"{baseUrl}/StaticFiles/no-image.png";

            return $"{baseUrl}{imageFileName}";
        }

    }
}
