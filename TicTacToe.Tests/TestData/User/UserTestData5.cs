using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.Tests.TestData.User
{
    public class UserTestData5 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");

        static DataComponent.Models.User user1 = new DataComponent.Models.User { Id = Id, Name = "Bradley", Email = "b@gmail.com" };
        static BusinessComponent.Models.User user2 = new BusinessComponent.Models.User { Id = Id, Name = "Bradley", Email = "b@gmail.com" };
        static IEnumerable<DataComponent.Models.User> list = new List<DataComponent.Models.User> { user1 };
        static IEnumerable<BusinessComponent.Models.User> list2 = new List<BusinessComponent.Models.User> { user2 };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { list, list2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
