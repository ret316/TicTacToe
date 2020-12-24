using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;

namespace TicTacToe.BL.Services.Implementation
{
    public class UserServiceBL : IUserServiceBL
    {
        private readonly IUserServiceDL _userServiceDL;
        public UserServiceBL(IUserServiceDL userServiceDL)
        {
            this._userServiceDL = userServiceDL;
        }

        public async Task<IEnumerable<UserBL>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return null;
            }

            var users = await _userServiceDL.GetAllUsersAsync(pageNumber, pageSize);
            return users.Select(u => new UserBL
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password
            });
        }

        public async Task<UserBL> GetUserAsync(Guid id)
        {
            var user = await _userServiceDL.GetUserAsync(id);

            if (!(user is null))
            {
                return new UserBL
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };
            }

            return null;
        }

        public async Task CreateUserAsync(UserBL user)
        {
            await _userServiceDL.CreateUserAsync(new UserDL
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });
        }

        public async Task UpdateUserAsync(UserBL user)
        {
            var userDB = _userServiceDL.GetUserAsync(user.Id);

            if (userDB != null)
            {
                await _userServiceDL.UpdateUserAsync(new UserDL
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                });
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userServiceDL.DeleteUserAsync(new UserDL
            {
                Id = id
            });
        }
    }
}
