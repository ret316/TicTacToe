using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DataComponent.Config;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.DataComponent.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DataBaseContext _dataBaseContext;
        public UserService(DataBaseContext dataBaseContext)
        {
            this._dataBaseContext = dataBaseContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            return await _dataBaseContext.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _dataBaseContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _dataBaseContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            await _dataBaseContext.Users.AddAsync(user);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dataBaseContext.Entry(user).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            //TODO make a check 

            _dataBaseContext.Entry(user).State = EntityState.Deleted;
            await _dataBaseContext.SaveChangesAsync();
        }
    }
}
