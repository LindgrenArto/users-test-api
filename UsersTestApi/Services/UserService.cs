using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTestApi.DTOModels;
using UsersTestApi.Repositories;
using UsersTestApi.Services;

namespace UsersTestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all users
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // Get a user by ID
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        // Update an existing user
        public async Task<bool> UpdateUserAsync(UserDTO userDTO)
        {
            return await _userRepository.UpdateUserAsync(userDTO);
        }

        // Delete a user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }


        //Delete multiple users
        public async Task<bool> DeleteMultipleUsersAsync(List<int> ids)
        {
            bool success = true;

            foreach (var id in ids)
            {
                var result = await _userRepository.DeleteUserAsync(id);
                if (!result)
                {
                    success = false; // If any deletion fails, return false
                }
            }

            return success;
        }
    }
}
