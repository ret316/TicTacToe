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
using TicTacToe.BL.Config;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;

namespace TicTacToe.BL.Services.Implementation
{
    public class UserServiceBL : IUserServiceBL
    {
        private readonly IUserServiceDL _userServiceDL;
        private readonly string _appSettings;
        private readonly IMapper _mapper;
        public UserServiceBL(IUserServiceDL userServiceDL, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            this._userServiceDL = userServiceDL;
            this._appSettings = appSettings.Value.Secret;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<UserBL>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return null;
            }

            var users = await _userServiceDL.GetAllUsersAsync(pageNumber, pageSize);

            return users.Select(u => _mapper.Map<UserBL>(u));
        }

        public async Task<UserBL> GetUserAsync(Guid id)
        {
            var user = await _userServiceDL.GetUserAsync(id);

            return _mapper.Map<UserBL>(user);
        }

        public async Task<bool> CreateUserAsync(UserBL user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return false;
            }
            else if (await _userServiceDL.GetUserAsync(user.Email) != null)
            {
                return false;
            }

            var password = GetPassHash(user.Password);
            try
            {
                await _userServiceDL.CreateUserAsync(new UserDL
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

        public async Task<bool> UpdateUserAsync(UserBL user)
        {
            if (!user.Id.HasValue)
            {
                return false;
            }

            var userDB = await _userServiceDL.GetUserAsync(user.Id.Value);
            var password = GetPassHash(user.Password);

            try
            {
                if (userDB != null)
                {
                    userDB.Name = user.Name;
                    userDB.Email = user.Email;
                    userDB.Password = password.hash;
                    userDB.PasswordSalt = password.salt;
                        
                    await _userServiceDL.UpdateUserAsync(userDB);
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
                await _userServiceDL.DeleteUserAsync(new UserDL
                {
                    Id = id
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<AuthUserModelBL> Authenticate(string email, string password)
        {
            var user = await _userServiceDL.GetUserAsync(email);

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
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(descriptor);
            var str = handler.WriteToken(token);

            return new AuthUserModelBL
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

        private bool ComparePasswords(string password, UserDL user)
        {
            using (var sha = new HMACSHA512(user.PasswordSalt))
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (hash.Where((t, i) => t != user.Password[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
