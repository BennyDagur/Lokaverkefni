using System.ComponentModel.DataAnnotations;

namespace Lokaverkefni.Models
{
    public class Post
    {

        public int PostId { get; set; }
        public string? Text { get; set; }
        public string? Image { get; set; }
        public int? Shares { get; set; } = 0;
        public int? Likes { get; set; } = 0;
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
