using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserServiceBL _userServiceBL;
        private readonly IMapper _mapper;
        public UserService(IUserServiceBL userServiceBL, IMapper mapper)
        {
            this._userServiceBL = userServiceBL;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userServiceBL.GetAllUsersAsync(pageNumber, pageSize);

            return users.Select(u => _mapper.Map<UserModel>(u));
        }

        public async Task<UserModel> GetUserAsync(Guid id)
        {
            var user = await _userServiceBL.GetUserAsync(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> CreateUserAsync(UserModel user)
        {
            try
            {
                await _userServiceBL.CreateUserAsync(_mapper.Map<UserBL>(user));
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
                await _userServiceBL.UpdateUserAsync(_mapper.Map<UserBL>(user));
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

            return _mapper.Map<AuthUserModel>(userAuth);
        }
    }
}
