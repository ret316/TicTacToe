using System;
using System.Collections;
using System.Collections.Generic;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.User
{
    public class UserTestData2 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

        UserBL user0 = new UserBL { Id = Id, Name = "Bradley", Email = "b@gmail.com", Password = "123" };
        UserDL user1 = new UserDL { Id = Id, Name = "Bradley", Email = "b@gmail.com", };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { user0, user1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}