using System.Linq.Expressions;
using Core.Domain.Common;
using Core.Domain.Properties;
using Core.Domain.Users;
using RED.Services.Dtos;
using RED.Services.ServiceBase.Interfaces;

namespace RED.Services.UserServices.Interfaces;

public interface IUserService : IServiceBase<User>
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int userId);
    Task<IList<User>> GetUsersByIds(int[] userIds);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserWithDetails(int userId);
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
}