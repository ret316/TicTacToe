using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using TicTacToe.BL.Config;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class UserSerivceTests
    {
        private Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

        private IUserServiceBL GetUserService(Mock<IUserServiceDL> mock)
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            IMapper mapper = new Mapper(configuration);
            var m1 = new Mock<IOptions<AppSettings>>();
            var app = new AppSettings
            {
                Secret =
                    "We are the sun and the sea and the mountain. We are the children who still can believe. We have faith, we have hope, we have answers. In our heart is where we are free"
            };
            var options = Options.Create(app);
            return new UserServiceBL(mock.Object, options, mapper);
        }

        [Fact]
        public async Task CreateUserTest()
        {
            UserBL user1 = new UserBL {Name = "Bradley", Email = "b@gmail.com", Password = "123"};
            UserBL user12 = new UserBL { Name = "Bradley", Email = "b@gmail.com", Password = "" };
            UserDL user2 = new UserDL {Id = Id, Name = "Bradley", Email = "b@gmail.com",};
            var mock = new Mock<IUserServiceDL>();
            mock.Setup(it => it.CreateUserAsync(user2)).Returns(Task.FromResult(user1));

            var userService = GetUserService(mock);
            var user = await userService.CreateUserAsync(user1);
            Assert.True(user);

            mock.Setup(it => it.CreateUserAsync(user2)).Returns(Task.FromResult(user1));
            var userService1 = GetUserService(mock);
            var user01 = await userService1.CreateUserAsync(user12);
            Assert.False(user01);

            mock.Setup(it => it.GetUserAsync(user1.Email)).Returns(Task.FromResult(user2));

            var userService12 = GetUserService(mock);
            var user02 = await userService12.CreateUserAsync(user1);
            Assert.False(user02);
        }

        [Fact]
        public async Task UpdateUserTest()
        {
            UserBL user1 = new UserBL {Id = Id, Name = "Bradley", Email = "b@gmail.com", Password = "123"};
            UserDL user2 = new UserDL {Id = Id, Name = "Bradley", Email = "b@gmail.com",};
            var mock = new Mock<IUserServiceDL>();
            mock.Setup(it => it.UpdateUserAsync(user2)).Returns(Task.FromResult(user1));

            var userService = GetUserService(mock);
            var user = await userService.UpdateUserAsync(user1);
            Assert.True(user);
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            UserDL user2 = new UserDL {Id = Id,};
            var mock = new Mock<IUserServiceDL>();
            mock.Setup(it => it.DeleteUserAsync(user2)).Returns(Task.FromResult(default(object)));

            var userService = GetUserService(mock);
            var user = await userService.DeleteUserAsync(Id);
            Assert.True(user);
        }

        [Fact]
        public async Task GetUserTest()
        {
            UserDL user1 = new UserDL {Id = Id, Name = "Bradley", Email = "b@gmail.com"};

            var mock = new Mock<IUserServiceDL>();
            mock.Setup(it => it.GetUserAsync(Id)).Returns(Task.FromResult(user1));

            var userService = GetUserService(mock);
            var user = await userService.GetUserAsync(Id);
            Assert.Equal(Id, user.Id.Value);
        }

        [Fact]
        public async Task GetUsersTest()
        {
            UserBL user2 = new UserBL {Id = Id, Name = "Bradley", Email = "b@gmail.com"};
            UserDL user1 = new UserDL {Id = Id, Name = "Bradley", Email = "b@gmail.com"};
            IEnumerable<UserDL> list = new List<UserDL> {user1};
            IEnumerable<UserBL> list2 = new List<UserBL> {user2};

            var mock = new Mock<IUserServiceDL>();
            mock.Setup(it => it.GetAllUsersAsync(1, 10)).Returns(Task.FromResult(list));

            var userService = GetUserService(mock);
            var users = await userService.GetAllUsersAsync(1, 10);
            Assert.Equal(list2.Select(x => x.Id), users.Select(x => x.Id));
        }

    }
}
