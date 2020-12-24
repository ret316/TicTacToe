using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    public interface IUserServiceBL 
    {
        Task<IEnumerable<UserBL>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserBL> GetUserAsync(Guid id);
        Task CreateUserAsync(UserBL user);
        Task UpdateUserAsync(UserBL user);
        Task DeleteUserAsync(Guid id);
    }
}
