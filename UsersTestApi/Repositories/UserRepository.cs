using AutoMapper;
using MongoDB.Driver;
using UsersTestApi.Models;
using UsersTestApi.DTOModels;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace UsersTestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMapper _mapper;

        public UserRepository(IOptions<UserDatabaseSettings> userDatabaseSettings, IMapper mapper)
        {
            var mongoClient = new MongoClient(userDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(userDatabaseSettings.Value.DatabaseName);
            _users = mongoDatabase.GetCollection<User>(userDatabaseSettings.Value.UserCollection);
            _mapper = mapper;
        }

        // Get all users
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _users.Find(_ => true).ToListAsync();
                return _mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving all users.", e);
            }
        }

        // Get a user by ID
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
                if (user == null) return null;

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception e)
            {
                throw new ApplicationException($"An error occurred while retrieving the user with ID {id}.", e);
            }
        }

        // Create a new user
        public async Task<bool> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                var newUser = _mapper.Map<User>(userDTO);

                await _users.InsertOneAsync(newUser);
                return true;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while creating a new user.", e);
            }
        }

        // Update an existing user
        public async Task<bool> UpdateUserAsync(UserDTO userDTO)
        {
            try
            {
                var user = await _users.Find(u => u.Id == userDTO.Id).FirstOrDefaultAsync();
                if (user == null) return false;

                user = _mapper.Map(userDTO, user);

                var result = await _users.ReplaceOneAsync(u => u.Id == userDTO.Id, user);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception e)
            {
                throw new ApplicationException($"An error occurred while updating the user with ID {userDTO.Id}.", e);
            }
        }

        // Delete a user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var result = await _users.DeleteOneAsync(user => user.Id == id);
                return result.IsAcknowledged && result.DeletedCount > 0;
            }
            catch (Exception e)
            {
                throw new ApplicationException($"An error occurred while deleting the user with ID {id}.", e);
            }
        }
    }
}
