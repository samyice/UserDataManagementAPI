using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserDataManagementAPI.Data;

namespace UserDataManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DboUsersContext _context;

        public UsersController(DboUsersContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetActiveUsers()
        {
            try
            {
                //returns a list of all active users
                return await _context.Users.Where(u => u.Active).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error while creating the user");
            }

        }

        // GET: api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            try
            {
                var users = await _context.Users.FindAsync(id);

                if (users == null)
                {
                    return NotFound();
                }
                //returns, if found, the user for the given id
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error while creating the user");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser([FromBody] CreateUserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is required.");
            }

            //creates a new user with an auto-increment id
            var user = new Users
            {
                Name = userDto.Name,
                Active = userDto.Active,
                Birthdate = userDto.Birthdate,
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error while creating the user");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, [FromBody] UpdateUserDTO updateUserDto)
        {
            if (updateUserDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                // updates the user's active value
                user.Active = updateUserDto.Active;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error while creating the user");
            }

        }

        // DELETE: api/Users/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            try
            {
                var users = await _context.Users.FindAsync(id);
                if (users == null)
                {
                    return NotFound();
                }

                //deletes, if found, the user for the given id
                _context.Users.Remove(users);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error while creating the user");
            }

        }
    }
}
