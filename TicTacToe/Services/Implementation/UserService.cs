using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserServiceBL _userServiceBL;
        public UserService(IUserServiceBL userServiceBL)
        {
            this._userServiceBL = userServiceBL;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userServiceBL.GetAllUsersAsync(pageNumber, pageSize);
            return users.Select(u => new UserModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password
            });
        }

        public async Task<UserModel> GetUserAsync(Guid id)
        {
            var user = await _userServiceBL.GetUserAsync(id);

            if (!(user is null))
            {
                return new UserModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };
            }

            return null;
        }

        public async Task<bool> CreateUserAsync(UserModel user)
        {
            try
            {
                await _userServiceBL.CreateUserAsync(new UserBL
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            try
            {
                await _userServiceBL.UpdateUserAsync(new UserBL
                {
                    Id = user.Id.Value,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                await _userServiceBL.DeleteUserAsync(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<AuthUserModel> Authenticate(UserAuthModel user)
        {
            var userAuth = await _userServiceBL.Authenticate(user.Email, user.Password);

            if (userAuth is null)
            {
                return null;
            }

            return new AuthUserModel
            {
                Id = userAuth.Id,
                Name = userAuth.Name,
                Email = user.Email,
                Token = userAuth.Token
            };
        }
    }
}
