using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.User
{
    public class UserTestData4 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

        UserDL user0 = new UserDL { Id = Id, Name = "Bradley", Email = "b@gmail.com" };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Id, user0 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
