using System.ComponentModel.DataAnnotations;
using TaskerFullStack.Client.Models;
using TaskerFullStack.Data;

namespace TaskerFullStack.Models
{
    public class DbTaskerItem : TaskerItem  
    {
        [Required]
        public string? UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
