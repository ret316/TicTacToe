using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.BusinessComponent.Config;
using TicTacToe.BusinessComponent.Models;

namespace TicTacToe.BusinessComponent.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DataComponent.Services.IUserService _userService;
        private readonly string _appSettings;
        private readonly IMapper _mapper;

        public UserService(DataComponent.Services.IUserService userService, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            this._userService = userService;
            this._appSettings = appSettings.Value.Secret;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return null;
            }

            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            return users.Select(u => _mapper.Map<User>(u));
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await _userService.GetUserAsync(id);

            return _mapper.Map<User>(user);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return false;
            }
            else if (await _userService.GetUserAsync(user.Email) != null)
            {
                return false;
            }

            var password = GetPassHash(user.Password);
            try
            {
                await _userService.CreateUserAsync(new DataComponent.Models.User
                {
                    Id = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    Password = password.hash,
                    PasswordSalt = password.salt
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (!user.Id.HasValue)
            {
                return false;
            }

            var userDB = await _userService.GetUserAsync(user.Id.Value);
            var password = GetPassHash(user.Password);

            try
            {
                if (userDB != null)
                {
                    userDB.Name = user.Name;
                    userDB.Email = user.Email;
                    userDB.Password = password.hash;
                    userDB.PasswordSalt = password.salt;
                        
                    await _userService.UpdateUserAsync(userDB);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            //var result = true;
            try
            {
                var user = await _userService.GetUserAsync(id);
                await _userService.DeleteUserAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<AuthUser> Authenticate(string email, string password)
        {
            var user = await _userService.GetUserAsync(email);

            if (user is null)
            {
                return null;
            }

            if (!ComparePasswords(password, user))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(descriptor);
            var str = handler.WriteToken(token);

            return new AuthUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = str
            };
        }

        private (byte[] hash, byte[] salt) GetPassHash(string password)
        {
            using (var sha = new HMACSHA512())
            {
                return (sha.ComputeHash(Encoding.UTF8.GetBytes(password)), sha.Key);
            }
        }

        private bool ComparePasswords(string password, DataComponent.Models.User user)
        {
            using (var sha = new HMACSHA512(user.PasswordSalt))
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != user.Password[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
