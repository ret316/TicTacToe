using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DL.Config;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services.Implementation
{
    public class UserServiceDL : IUserServiceDL
    {
        private readonly DataBaseContext _dataBaseContext;
        public UserServiceDL(DataBaseContext dataBaseContext)
        {
            this._dataBaseContext = dataBaseContext;
        }

        public async Task<IEnumerable<UserDL>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            return await _dataBaseContext.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<UserDL> GetUserAsync(Guid id)
        {
            return await _dataBaseContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserDL> GetUserAsync(string email)
        {
            return await _dataBaseContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(UserDL user)
        {
            await _dataBaseContext.Users.AddAsync(user);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDL user)
        {
            _dataBaseContext.Entry(user).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(UserDL user)
        {
            //TODO make a check 

            _dataBaseContext.Attach(user);
            _dataBaseContext.Remove(user);

            await _dataBaseContext.SaveChangesAsync();
        }
    }
}
