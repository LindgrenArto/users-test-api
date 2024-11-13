using System.Collections.Generic;
using System.Threading.Tasks;
using UserTestApi.DTOModels;

public interface IUserService
{
    Task<List<UserDTO>> GetAllUsersAsync();
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> DeleteMultipleUsersAsync(List<int> ids);
}