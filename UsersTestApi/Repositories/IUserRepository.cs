using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTestApi.DTOModels;

namespace UsersTestApi.Repositories
{
    public interface IUserRepository
    {
        // Get all userDTOs
        Task<List<UserDTO>> GetAllUsersAsync();

        // Get a userDTO by ID
        Task<UserDTO> GetUserByIdAsync(int id);

        // Update an existing user
        Task<bool> UpdateUserAsync(UserDTO userDTO);

        // Delete a user by ID
        Task<bool> DeleteUserAsync(int id);
    }
}
