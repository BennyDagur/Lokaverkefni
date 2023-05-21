using Lokaverkefni.Models;
using Lokaverkefni.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lokaverkefni.Data
{
    public class TwitterRepository : ITwitterRepository
    {

        private TwitterDbContext _dbContext;

        public TwitterRepository()
        {
            _dbContext = new TwitterDbContext();
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.User.ToList();
        }

        public User? GetUserById(int id)
        {
            var user = _dbContext.User.Where(t => t.UserId == id).Include(x => x.Following).Include(x => x.Followers).FirstOrDefault();
            return user;
        }

        public void CreateUser(User user)
        {
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }

        public User? UpdateUser(int id, User userNew)
        {
            User? userCurrent = GetUserById(id);

            if (userCurrent == null)
            {
                return null;
            }

            if(userNew.ProfilePicture != null)
            {
                userCurrent.ProfilePicture = userNew.ProfilePicture;
            }

            if (userNew.Name != "" && userNew.Name != null)
            {
                userCurrent.Name = userNew.Name;
            }

            if (userNew.Password != "" && userNew.Password != null)
            {
                userCurrent.Password = userNew.Password;
            }
            if (userNew.Email != "" && userNew.Email != null)
            {
                userCurrent.Email = userNew.Email;
            }

            _dbContext.SaveChanges();

            return userCurrent;
        }

        public bool DeleteUser(User user)
        {
            try
            {
                user.Followers = new List<User>();
                user.Following = new List<User>();
                _dbContext.User.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserDTO? GetUserDTOById(int id)
        {
                User? user = GetUserById(id);

                if (user == null)
                {
                    throw new ArgumentException("");
                }

                UserDTO userDTO = new UserDTO
                {
                    Name = user.Name,
                    ProfilePicture = user.ProfilePicture,
                    FollowerId = user.Followers.Select(u => u.UserId).ToList(),
                    FollowingId = user.Following.Select(u => u.UserId).ToList()
                };

                return userDTO;
        }

        public UserDTO ToggleFollow(int followerId, int followingId)
        {

            var follower = GetUserById(followerId);
            var following = GetUserById(followingId);
            if ( follower == null || following == null)
            {
                throw new ArgumentException("User not found");
            } 
            if (follower.Following.Contains(following) && following.Followers.Contains(follower))
            {
                follower.Following.Remove(following);
                following.Followers.Remove(follower);
            }
            else
            {
                follower.Following.Add(following);
                following.Followers.Add(follower);
            }

            _dbContext.SaveChanges();

            return GetUserDTOById(followerId) ?? throw new ArgumentException("Something went wrong");
        }

        public List<PostDTO> GetAllPosts()
        {
            return _dbContext.Post.Select(p => new PostDTO
            {
                PostId= p.PostId,
                Text = p.Text,
                Image = p.Image,
                Shares = p.Shares,
                DateCreated = p.DateCreated,
                UserId = p.UserId,
                UserName = p.User.Name,
                ProfilePicture = p.User.ProfilePicture,
            }).ToList();
        }

        public Post? GetPostById(int id)
        {
            return _dbContext.Post.Where(t => t.PostId == id).FirstOrDefault();
        }

        public bool DeletePost(Post post)
        {
            try { 
            _dbContext.Post.Remove(post);
            _dbContext.SaveChanges();
            return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CreatePost(Post post)
        {
            _dbContext.Post.Add(post);
            _dbContext.SaveChanges();
        }
    }
}
