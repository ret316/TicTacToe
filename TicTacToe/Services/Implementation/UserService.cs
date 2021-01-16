using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly BusinessComponent.Services.IUserService _userService;
        private readonly IMapper _mapper;

        public UserService(BusinessComponent.Services.IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<Models.User>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            return users.Select(u => _mapper.Map<Models.User>(u));
        }

        public async Task<Models.User> GetUserAsync(Guid id)
        {
            var user = await _userService.GetUserAsync(id);

            return _mapper.Map<Models.User>(user);
        }

        public async Task<bool> CreateUserAsync(Models.User user)
        {
            try
            {
                await _userService.CreateUserAsync(_mapper.Map<BusinessComponent.Models.User>(user));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(Models.User user)
        {
            try
            {
                await _userService.UpdateUserAsync(_mapper.Map<BusinessComponent.Models.User>(user));
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
                await _userService.DeleteUserAsync(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Models.AuthUser> Authenticate(UserAuth user)
        {
            var userAuth = await _userService.Authenticate(user.Email, user.Password);

            return _mapper.Map<Models.AuthUser>(userAuth);
        }
    }
}
