using Lokaverkefni.Models;

namespace Lokaverkefni.Data.Interfaces
{
    public interface ITwitterRepository
    {

        List<User> GetAllUsers();
        User? GetUserById(int id);
        void CreateUser(User user);
        User? UpdateUser(int id, User userNew);
        bool DeleteUser(User user);
        UserDTO ToggleFollow(int followerId, int followingId);

        List<PostDTO> GetAllPosts();
        Post? GetPostById(int id);
        bool DeletePost(Post post);
        void CreatePost(Post post);
    }
}
