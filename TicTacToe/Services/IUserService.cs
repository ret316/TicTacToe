using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
{
    public interface IUserService : IAuthentication
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserModel> GetUserAsync(Guid id);
        Task CreateUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(Guid id);
    }
}
