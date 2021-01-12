using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TicTacToe.WebApi.Models;

namespace TicTacToe.Tests.TestDataI.User
{
    public class UserTestData1 : IEnumerable<object[]>
    {
        UserModel user = new UserModel
        {
            Id = null,
            Name = "Alex",
            Email = "a@ss.com",
            Password = "123456"
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            yield return new object[] {user, content};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
