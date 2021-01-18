using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.DataComponent.Config
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            var u = Users.FirstOrDefault(x => x.Id == Guid.Parse("F403AA84-B314-4044-93C3-AD514D35EA4A"));
            if (u == null)
            {
                var tuple = GetPassHash("123456");
                Users.Add(new User
                {
                    Id = Guid.Parse("F403AA84-B314-4044-93C3-AD514D35EA4A"),
                    Name = "test1",
                    Email = "test1@gmail.com",
                    Password = tuple.hash,
                    PasswordSalt = tuple.salt
                });
                Users.Add(new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "bot1",
                    Email = "bot1@gmail.com",
                    Password = tuple.hash,
                    PasswordSalt = tuple.salt
                });
                SaveChanges();
            }
        }

        private (byte[] hash, byte[] salt) GetPassHash(string password)
        {
            using (var sha = new HMACSHA512())
            {
                return (sha.ComputeHash(Encoding.UTF8.GetBytes(password)), sha.Key);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameHistory> GameHistories { get; set; }
        public virtual DbSet<GameResult> GameResults { get; set; }
    }
}
