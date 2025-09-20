using TaskerFullStack.Models;

namespace TaskerFullStack.Helpers
{
    public static class ImageHelper
    {
        public static readonly string DefaultProfilePictureUrl = "/img/undraw_developer-avatar_f6ac.svg";

        public static async Task<ImageUpload> GetImageUploadAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            byte[] data = ms.ToArray();

            if (ms.Length > 1 * 1024 * 1024)
            {
                throw new Exception("The image is too large to upload.");
            }
            else
            {
                ImageUpload imageupload = new()
                {
                    Id = Guid.NewGuid(),
                    Data = data,
                    Type = file.ContentType
                };

                return imageupload;
            }
        }
    }
}
