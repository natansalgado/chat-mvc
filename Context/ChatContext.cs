using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmvc.Context.Maps;
using Microsoft.EntityFrameworkCore;
using mvc.Context.Maps;
using mvc.Models;

namespace mvc.Context
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<MessageModel> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new MessageMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}