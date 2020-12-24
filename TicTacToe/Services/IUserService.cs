using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.WebApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserViewModel> GetUserAsync(Guid id);
        Task CreateUserAsync(UserViewModel user);
        Task UpdateUserAsync(UserViewModel user);
        Task DeleteUserAsync(Guid id);
    }
}
