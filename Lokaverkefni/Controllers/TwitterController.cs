using Lokaverkefni.Data;
using Microsoft.AspNetCore.Mvc;
using Lokaverkefni.Models;
using Lokaverkefni.Data.Interfaces;

namespace Lokaverkefni.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitterController : ControllerBase
    {

        private ITwitterRepository _repo;
        private readonly IWebHostEnvironment _env;

        public TwitterController(ITwitterRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [HttpGet]
        [Route("profile")]
        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        [HttpGet]
        [Route("profile/{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
        
            try
            {
                User? user = _repo.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                UserDTO userDTO = new UserDTO {
                    Name = user.Name,
                    ProfilePicture = user.ProfilePicture, 
                    FollowerId = user.Followers.Select(f => f.UserId).ToList(), 
                    FollowingId = user.Following.Select(f => f.UserId).ToList() };

                return userDTO;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("profile")]
        public ActionResult<User> CreateUser(User user)
        {

            try
            {
                _repo.CreateUser(user);

                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPatch]
        [Route("profile/{id}")]
        public IActionResult UpdateUser(int id, [FromForm] UserUpdateDTO userNew)
        {

            if (id != userNew.UserId)
            {
                return BadRequest();
            }

            try
            {

                User userCurrent = new User
                {
                    UserId = userNew.UserId ?? 0,
                    ProfilePicture = null,
                    Name = userNew.Name,
                    Password = userNew.Password,
                    Email = userNew.Email,
                };

                if (userNew.Image != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userNew.Image.FileName);
                    var imgPath = Path.Combine(_env.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        userNew.Image.CopyTo(stream);
                    }
                    userCurrent.ProfilePicture = $"https://localhost:7060/images/{fileName}";
                }

                User? updated = _repo.UpdateUser(id, userCurrent);

                if (updated == null)
                {
                    return NotFound();
                }

                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        [Route("profile/{id}")]
        public ActionResult<bool> DeleteUser(int id)
        {
            try
            {
                User? user = _repo.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                bool success = _repo.DeleteUser(user);

                if (!success)
                {
                    return StatusCode(500);
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("profile/{followerId}/follow/{followingId}")]
        public ActionResult<UserDTO> ToggleFollow(int followerId, int followingId)
        {
            try
            {
                var follow = _repo.ToggleFollow(followerId, followingId);
                return Ok(follow);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("post")]
        public List<PostDTO> GetAllPosts()
        {
            return _repo.GetAllPosts();
        }

        [HttpGet]
        [Route("post/{id}")]
        public ActionResult<Post> GetPostById(int id)
        {

            try
            {
                Post? post = _repo.GetPostById(id);

                if (post == null)
                {
                    return NotFound();
                }

                return post;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("post/{id}")]
        public ActionResult<bool> DeletePostById(int id)
        {
            try
            {
                Post? post = _repo.GetPostById(id);

                if (post == null)
                {
                    return NotFound();
                }

                bool success = _repo.DeletePost(post);

                if (!success)
                {
                    return StatusCode(500);
                }

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("post")]
        public ActionResult<Post> CreatePost([FromForm] Post post, IFormFile? image)
        {

            try
            {
                if(image != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var imgPath = Path.Combine(_env.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(imgPath, FileMode.Create))

                    {
                        image.CopyTo(stream);
                    }
                    post.Image = $"https://localhost:7060/images/{fileName}";
                }
                _repo.CreatePost(post);

                return CreatedAtAction(nameof(GetPostById), new { id = post.PostId }, post);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
