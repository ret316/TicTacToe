using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Config
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            var tuple = GetPassHash("123456");
            Users.Add(new UserDL
            {
                Id = Guid.Parse("f9c0105f-13f1-47d6-805d-6c824d406c35"),
                Name = "Andrew",
                Email = "a@gmail.com",
                Password = tuple.hash,
                PasswordSalt = tuple.salt
            });
            Users.Add(new UserDL
            {
                Id = Guid.Parse("7baf27a8-a617-477b-b684-199699f0cabc"),
                Name = "Greg",
                Email = "g@gmail.com",
                Password = tuple.hash,
                PasswordSalt = tuple.salt
            });
            SaveChanges();
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

        public virtual DbSet<UserDL> Users{ get; set; }
    }
}
