using Core.Data;
using Core.Domain.Users;
using EmailValidation;
using Microsoft.EntityFrameworkCore;
using RED.Services.ServiceBase.Services;
using RED.Services.UserServices.Interfaces;

namespace RED.Services.UserServices.Services;

public class UserService: ServiceBase<User>, IUserService
{
    public UserService(RedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await FindAll().OrderBy(u => u.FirstName).ToListAsync();
    }

    public async Task<User?> GetUserById(int userId)
    {
        return FindByCondition(u => u.Id.Equals(userId)).Include(u => u.Properties)
            .Include(u => u.UserAddresses).FirstOrDefault();
    }

    public async Task<IList<User>> GetUsersByIds(int[] userIds)
    {
        if (userIds.ToString() == null || userIds.Length == 0)
            return new List<User>();

        var users = new List<User>();
        foreach (var id in userIds)
        {
            var user = FindByCondition(u => u.Id.Equals(id)).Include(u => u.Properties)
                .Include(u => u.UserAddresses).FirstOrDefault();
            if (user != null)
                users.Add(user);
        }

        return await Task.FromResult(users);
    }

    public Task<User?> GetUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));
        
        return Task.FromResult(FindByCondition(u => u.UserEmail.Equals(email)).Include(u => u
                .Properties).Include(u => u.UserAddresses).FirstOrDefault());
    }

    public async Task<User?> GetUserWithDetails(int userId)
    {
        throw new NotImplementedException();
    }

    public Task AddUser(User user)
    {
        var users = FindAll().OrderBy(u => u.UserEmail).ToListAsync();
        
        if (!users.Result.Contains(user))
            throw new ArgumentNullException(nameof(user.Active));

        var newUser = user.UserEmail.Trim();
        EmailValidator.Validate(newUser);

        Create(user);
        return Task.CompletedTask;
    }

    public Task UpdateUser(User user)
    {
        Update(user);
        return Task.CompletedTask;
    }

    public Task DeleteUser(User user)
    {
        Delete(user);
        return Task.CompletedTask;
    }
}