using Microsoft.EntityFrameworkCore;
using Lokaverkefni.Models;
using Lokaverkefni.Data.Interfaces;

namespace Lokaverkefni.Data
{
    public class MockRepository : ITwitterRepository
    {

        List<User> AllUsers = new List<User>()
        {
        new User() { UserId = 1, Name = "Johnny Generic", Password = "Password", Email = "email@example.check", ProfilePicture = "https://localhost:7060/images/Ghost.png", PhoneNumber = 11111},
        new User() { UserId = 2, Name = "JohnnyNotSoGeneric", Password = "Password", Email = "email@exampleer.check", PhoneNumber = 22222},
        new User() { UserId = 3, Name = "Johnny Jeneric", Password = "Password", Email = "email@exampleest.check", PhoneNumber = 33333}
        };

        public void CreatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            return AllUsers;
        }

        public Post GetPostById(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetUserById(int id)
        {

            foreach (User user in AllUsers)
            {
                if (user.UserId == id)
                {
                    return user;
                }
            }
            return null;
        }

        public UserDTO ToggleFollow(int followerId, int followingId)
        {
            throw new NotImplementedException();
        }

        public User? UpdateUser(int id, User userNew)
        {
            throw new NotImplementedException();
        }

        List<PostDTO> ITwitterRepository.GetAllPosts()
        {
            throw new NotImplementedException();
        }
    }
}
