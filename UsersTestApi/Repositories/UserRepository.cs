using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserTestApi.Models;

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
            userDatabaseSettings.Value.BooksCollectionName);
    }

    // Get all users
    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _users.Find(_ => true).ToListAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Username = u.Username,
            Email = u.Email,
            Phone = u.Phone,
            Website = u.Website,
            Address = new AddressDto
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
            Company = new CompanyDto
            {
                Name = u.Company.Name,
                CatchPhrase = u.Company.CatchPhrase,
                Bs = u.Company.Bs
            }
        }).ToList();
    }

    // Get a user by ID
    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        if (user == null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone,
            Website = user.Website,
            Address = new AddressDto
            {
                Street = user.Address.Street,
                Suite = user.Address.Suite,
                City = user.Address.City,
                Zipcode = user.Address.Zipcode,
                Geo = new GeoDTO
                {
                    Lat = u.Address.Geo.Lat,
                    Lng = u.Address.Geo.Lng
                }
            },
            Company = new CompanyDto
            {
                Name = user.Company.Name,
                CatchPhrase = user.Company.CatchPhrase,
                Bs = user.Company.Bs
            }
        };
    }

    // Update an existing user
    public async Task<bool> UpdateUserAsync(UserDTO userDTO)
    {
        var user = await _users.Find(u => u.Id == userDTO.Id).FirstOrDefaultAsync();
        if (user == null)
            return false;

        // Map back from DTO to the actual model
        user.Name = userDTO.Name;
        user.Username = userDTO.Username;
        user.Email = userDTO.Email;
        user.Phone = userDTO.Phone;
        user.Website = userDTO.Website;
        user.Address = new Address
        {
            Street = userDTO.Address.Street,
            Suite = userDTO.Address.Suite,
            City = userDTOAddress.City,
            Zipcode = userDTO.Address.Zipcode
            Geo = new Geo
            {
                Lat = userDTO.Address.Geo.Lat,
                Lng = userDTO.Addresss.Geo.Lng
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

    // Delete a user by ID
    public async Task<bool> DeleteUserAsync(int id)
    {
        var result = await _users.DeleteOneAsync(user => user.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}

