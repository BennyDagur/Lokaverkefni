namespace Lokaverkefni.Models
{
    public class UserUpdateDTO
    {

        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public IFormFile? Image { get; set; }
    }
}
