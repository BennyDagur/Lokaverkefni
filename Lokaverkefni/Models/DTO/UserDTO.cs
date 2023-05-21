namespace Lokaverkefni.Models
{
    public class UserDTO
    {

        public string Name { get; set; }
        public string? ProfilePicture { get; set; }
        public List<int> FollowerId { get; set; } = new List<int>();
        public List<int> FollowingId { get; set; } = new List<int>();
        public List<Post> Post { get; set; }

    }
}
