
namespace Lokaverkefni.Models
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string? Text { get; set; }
        public string? Image { get; set; }
        public int? Shares { get; set; } = 0;
        

        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
