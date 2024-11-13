using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTestApi.DTOModels;
using UsersTestApi.Repositories;
using UsersTestApi.Services;
using UsersTestApi.Exceptions;

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
            try
            {
                return await _userRepository.GetAllUsersAsync();
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException("An error occurred while retrieving all users in the service layer.", e);
            }
        }

        // Get a user by ID
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                return await _userRepository.GetUserByIdAsync(id);
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException($"An error occurred while retrieving the user with ID {id} in the service layer.", e);
            }
        }

        // Create a new user
        public async Task<bool> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                //Check for an existing user with the same email or ID
                var existingUserById = await _userRepository.GetUserByIdAsync(userDTO.Id);
                var existingUserByEmail = (await _userRepository.GetAllUsersAsync())
                                            .FirstOrDefault(u => u.Email == userDTO.Email);

                if (existingUserById != null)
                {
                    throw new DuplicateUserException($"A user with ID {userDTO.Id} already exists.");
                }

                if (existingUserByEmail != null)
                {
                    throw new DuplicateUserException($"A user with email {userDTO.Email} already exists.");
                }

                return await _userRepository.CreateUserAsync(userDTO);
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException("An error occurred while creating a new user in the service layer.", e);
            }
        }

        // Update an existing user
        public async Task<bool> UpdateUserAsync(UserDTO userDTO)
        {
            try
            {
                return await _userRepository.UpdateUserAsync(userDTO);
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException($"An error occurred while updating the user with ID {userDTO.Id} in the service layer.", e);
            }
        }

        // Delete a user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                return await _userRepository.DeleteUserAsync(id);
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException($"An error occurred while deleting the user with ID {id} in the service layer.", e);
            }
        }

        // Delete multiple users
        public async Task<bool> DeleteMultipleUsersAsync(List<int> ids)
        {
            bool success = true;
            try
            {
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
            catch (ApplicationException e)
            {
                throw new ApplicationException("An error occurred while deleting multiple users in the service layer.", e);
            }
        }
    }
}
