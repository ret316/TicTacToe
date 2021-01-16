﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using TicTacToe.BusinessComponent.Config;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.BusinessComponent.Services.Implementation;
using TicTacToe.DataComponent.Models;
using TicTacToe.DataComponent.Services;
using TicTacToe.Tests.TestData.User;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class UserSerivceTests
    {
        private Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

        private BusinessComponent.Services.IUserService GetUserService(Mock<DataComponent.Services.IUserService> mock)
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
            return new UserService(mock.Object, options, mapper);
        }

        [Theory]
        [ClassData(typeof(UserTestData1))]
        public async Task Test1_CreateUser(BusinessComponent.Models.User user0, BusinessComponent.Models.User user1, DataComponent.Models.User user2)
        {
            var mock = new Mock<DataComponent.Services.IUserService>();
            mock.Setup(it => it.CreateUserAsync(user2)).Returns(Task.FromResult(user0));

            var userService = GetUserService(mock);
            var user = await userService.CreateUserAsync(user0);
            Assert.True(user);

            mock.Setup(it => it.CreateUserAsync(user2)).Returns(Task.FromResult(user0));
            var userService1 = GetUserService(mock);
            var user01 = await userService1.CreateUserAsync(user1);
            Assert.False(user01);

            mock.Setup(it => it.GetUserAsync(user0.Email)).Returns(Task.FromResult(user2));
            var userService12 = GetUserService(mock);
            var user02 = await userService12.CreateUserAsync(user0);
            Assert.False(user02);
        }

        [Theory]
        [ClassData(typeof(UserTestData2))]
        public async Task Test2_UpdateUser(BusinessComponent.Models.User user1, DataComponent.Models.User user2)
        {
            var mock = new Mock<DataComponent.Services.IUserService>();
            mock.Setup(it => it.UpdateUserAsync(user2)).Returns(Task.FromResult(user1));

            var userService = GetUserService(mock);
            var user = await userService.UpdateUserAsync(user1);
            Assert.True(user);
        }

        [Theory]
        [ClassData(typeof(UserTestData3))]
        public async Task Test3_DeleteUser(Guid id, DataComponent.Models.User user0)
        {
            var mock = new Mock<DataComponent.Services.IUserService>();
            mock.Setup(it => it.DeleteUserAsync(user0)).Returns(Task.FromResult(default(object)));

            var userService = GetUserService(mock);
            var user = await userService.DeleteUserAsync(id);
            Assert.True(user);
        }

        [Theory]
        [ClassData(typeof(UserTestData4))]
        public async Task Test4_GetUser(Guid id, DataComponent.Models.User user1)
        {
            var mock = new Mock<DataComponent.Services.IUserService>();
            mock.Setup(it => it.GetUserAsync(Id)).Returns(Task.FromResult(user1));

            var userService = GetUserService(mock);
            var user = await userService.GetUserAsync(Id);
            Assert.Equal(id, user.Id.Value);
        }

        [Theory]
        [ClassData(typeof(UserTestData5))]
        public async Task Test5_GetUsers(IEnumerable<DataComponent.Models.User> list, IEnumerable<BusinessComponent.Models.User> list2)
        {
            var mock = new Mock<DataComponent.Services.IUserService>();
            mock.Setup(it => it.GetAllUsersAsync(1, 10)).Returns(Task.FromResult(list));

            var userService = GetUserService(mock);
            var users = await userService.GetAllUsersAsync(1, 10);
            Assert.Equal(list2.Select(x => x.Id), users.Select(x => x.Id));
        }

    }
}
