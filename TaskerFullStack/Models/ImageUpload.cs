using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace TaskerFullStack.Models
{
    public class ImageUpload
    {
        public Guid Id { get; set; }
        [Required]
        public byte[]? Data { get; set; }
        [Required]
        public string? Type { get; set; }
        public string Url => $"/uploads/{Id}";
    }
}
