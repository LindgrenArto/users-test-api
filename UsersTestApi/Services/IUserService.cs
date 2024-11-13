using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTestApi.DTOModels;

namespace UsersTestApi.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int? id);
        Task<bool> CreateUserAsync(UserDTO userDTO);
        Task<bool> UpdateUserAsync(UserDTO userDTO);
        Task<bool> DeleteUserAsync(int? id);
        Task<bool> DeleteMultipleUsersAsync(List<int> ids);
    }
}
