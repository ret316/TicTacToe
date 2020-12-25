using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        public UserServiceBL(IUserServiceDL userServiceDL, IOptions<AppSettings> appSettings)
        {
            this._userServiceDL = userServiceDL;
            this._appSettings = appSettings.Value.Secret;
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
                //Password = u.Password
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
                    //Password = user.Password
                };
            }

            return null;
        }

        public async Task CreateUserAsync(UserBL user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return;
            }
            else if (await _userServiceDL.GetUserAsync(user.Email) != null)
            {
                return;
            }

            var password = GetPassHash(user.Password);

            await _userServiceDL.CreateUserAsync(new UserDL
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                Password = password.hash,
                PasswordSalt = password.salt
            });
        }

        public async Task UpdateUserAsync(UserBL user)
        {
            var userDB = await _userServiceDL.GetUserAsync(user.Id);
            var password = GetPassHash(user.Password);

            if (userDB != null)
            {
                await _userServiceDL.UpdateUserAsync(new UserDL
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = password.hash,
                    PasswordSalt = password.salt
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
