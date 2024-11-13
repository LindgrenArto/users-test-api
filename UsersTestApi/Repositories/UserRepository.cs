using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserTestApi.Models;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(
           IOptions<UserDatabaseSettings> userDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            userDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userDatabaseSettings.Value.DatabaseName);

        _users = mongoDatabase.GetCollection<User>(
            userDatabaseSettings.Value.BooksCollectionName);
    }

    // Get all users
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }

    // Get a user by ID
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    // Update an existing user
    public async Task<bool> UpdateUserAsync(User user)
    {
        var result = await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    // Delete a user by ID
    public async Task<bool> DeleteUserAsync(int id)
    {
        var result = await _users.DeleteOneAsync(user => user.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}
