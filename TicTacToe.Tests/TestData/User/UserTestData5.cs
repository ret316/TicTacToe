using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.User
{
    public class UserTestData5 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

        static UserDL user1 = new UserDL { Id = Id, Name = "Bradley", Email = "b@gmail.com" };
        static UserBL user2 = new UserBL { Id = Id, Name = "Bradley", Email = "b@gmail.com" };
        static IEnumerable<UserDL> list = new List<UserDL> { user1 };
        static IEnumerable<UserBL> list2 = new List<UserBL> { user2 };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { list, list2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
