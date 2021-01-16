using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TicTacToe.Tests.Config
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {

    }
}
