using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTestApi.Services;
using UsersTestApi.DTOModels;
using UsersTestApi.Exceptions;

namespace UsersTestApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (ApplicationException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (ApplicationException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data is null");
            }

            try
            {
                bool isCreated = await _userService.CreateUserAsync(userDTO);

                if (isCreated)
                {
                    return CreatedAtAction(nameof(GetUserById), new { id = userDTO.Id }, userDTO);
                }

                return StatusCode(500, "An error occurred while creating the user.");
            }
            catch (DuplicateUserException ex)
            {
                return Conflict(ex.Message); // Return 409 Conflict with specific error message
            }
            catch (ApplicationException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest("User ID in the URL does not match the ID in the payload.");
            }

            try
            {
                var success = await _userService.UpdateUserAsync(userDTO);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ApplicationException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var success = await _userService.DeleteUserAsync(id);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ApplicationException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("multiple")]
        public async Task<IActionResult> DeleteMultipleUsers([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("No user IDs provided.");
            }

            try
            {
                var success = await _userService.DeleteMultipleUsersAsync(ids);
                if (!success)
                {
                    return NotFound("Some users could not be deleted.");
                }

                return NoContent();
            }
            catch (ApplicationException e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}