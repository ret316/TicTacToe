using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserServiceBL _userServiceBL;
        public UserService(IUserServiceBL userServiceBL)
        {
            this._userServiceBL = userServiceBL;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userServiceBL.GetAllUsersAsync(pageNumber, pageSize);
            return users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password
            });
        }

        public async Task<UserViewModel> GetUserAsync(Guid id)
        {
            var user = await _userServiceBL.GetUserAsync(id);

            if (!(user is null))
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };
            }

            return null;
        }

        public async Task CreateUserAsync(UserViewModel user)
        {
            await _userServiceBL.CreateUserAsync(new UserBL
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });
        }

        public async Task UpdateUserAsync(UserViewModel user)
        {
            await _userServiceBL.UpdateUserAsync(new UserBL
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userServiceBL.DeleteUserAsync(id);
        }
    }
}
