using System.ComponentModel.DataAnnotations;

namespace Lokaverkefni.Models
{
    public class User
    {

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public List<User> Followers { get; set; } = new List<User>(); 
        public List<User> Following { get; set; } = new List<User>();
        public List<Post> Post { get; set; } = new List<Post>();

    }
}
