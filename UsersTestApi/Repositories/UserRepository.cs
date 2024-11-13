using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTestApi.Models;
using UsersTestApi.DTOModels;
using Microsoft.Extensions.Options;

namespace UsersTestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<UserDatabaseSettings> userDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                userDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                userDatabaseSettings.Value.DatabaseName);

            _users = mongoDatabase.GetCollection<User>(
                userDatabaseSettings.Value.UserCollection);
        }

        // Get all users
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _users.Find(_ => true).ToListAsync();
                return users.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Phone = u.Phone,
                    Website = u.Website,
                    Address = new AddressDTO
                    {
                        Street = u.Address.Street,
                        Suite = u.Address.Suite,
                        City = u.Address.City,
                        Zipcode = u.Address.Zipcode,
                        Geo = new GeoDTO
                        {
                            Lat = u.Address.Geo.Lat,
                            Lng = u.Address.Geo.Lng
                        }
                    },
                    Company = new CompanyDTO
                    {
                        Name = u.Company.Name,
                        CatchPhrase = u.Company.CatchPhrase,
                        Bs = u.Company.Bs
                    }
                }).ToList();
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
                if (user == null)
                    return null;

                return new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone,
                    Website = user.Website,
                    Address = new AddressDTO
                    {
                        Street = user.Address.Street,
                        Suite = user.Address.Suite,
                        City = user.Address.City,
                        Zipcode = user.Address.Zipcode,
                        Geo = new GeoDTO
                        {
                            Lat = user.Address.Geo.Lat,
                            Lng = user.Address.Geo.Lng
                        }
                    },
                    Company = new CompanyDTO
                    {
                        Name = user.Company.Name,
                        CatchPhrase = user.Company.CatchPhrase,
                        Bs = user.Company.Bs
                    }
                };
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
                var newUser = new User
                {
                    Id = userDTO.Id,
                    Name = userDTO.Name,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Phone = userDTO.Phone,
                    Website = userDTO.Website,
                    Address = new Address
                    {
                        Street = userDTO.Address.Street,
                        Suite = userDTO.Address.Suite,
                        City = userDTO.Address.City,
                        Zipcode = userDTO.Address.Zipcode,
                        Geo = new Geo
                        {
                            Lat = userDTO.Address.Geo.Lat,
                            Lng = userDTO.Address.Geo.Lng
                        }
                    },
                    Company = new Company
                    {
                        Name = userDTO.Company.Name,
                        CatchPhrase = userDTO.Company.CatchPhrase,
                        Bs = userDTO.Company.Bs
                    }
                };

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
                if (user == null)
                    return false;

                user.Name = userDTO.Name;
                user.Username = userDTO.Username;
                user.Email = userDTO.Email;
                user.Phone = userDTO.Phone;
                user.Website = userDTO.Website;
                user.Address = new Address
                {
                    Street = userDTO.Address.Street,
                    Suite = userDTO.Address.Suite,
                    City = userDTO.Address.City,
                    Zipcode = userDTO.Address.Zipcode,
                    Geo = new Geo
                    {
                        Lat = userDTO.Address.Geo.Lat,
                        Lng = userDTO.Address.Geo.Lng
                    }
                };
                user.Company = new Company
                {
                    Name = userDTO.Company.Name,
                    CatchPhrase = userDTO.Company.CatchPhrase,
                    Bs = userDTO.Company.Bs
                };

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
