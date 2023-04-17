using ChatAppMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatAppMVC.Context
{
    public class ChatAppContext : DbContext
    { 
        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        
    }
}
