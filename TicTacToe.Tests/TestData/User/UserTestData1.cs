using System;
using System.Collections;
using System.Collections.Generic;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.Tests.TestData.User
{
    public class UserTestData1 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

        private static readonly BusinessComponent.Models.User user0 = new BusinessComponent.Models.User { Name = "Bradley", Email = "b@gmail.com", Password = "123" };
        private static readonly BusinessComponent.Models.User user1 = new BusinessComponent.Models.User { Name = "Bradley", Email = "b@gmail.com", Password = "" };
        private static readonly DataComponent.Models.User user2 = new DataComponent.Models.User { Id = Id, Name = "Bradley", Email = "b@gmail.com", };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {user0, user1, user2};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
