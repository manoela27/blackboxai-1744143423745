using Microsoft.EntityFrameworkCore;
using CadastroBasico.Models;

namespace CadastroBasico.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User and UserLevel relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserLevel)
                .WithMany(ul => ul.Users)
                .HasForeignKey(u => u.UserLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed initial UserLevel data
            modelBuilder.Entity<UserLevel>().HasData(
                new UserLevel 
                { 
                    Id = 1, 
                    Name = "Administrador", 
                    Description = "Acesso total ao sistema" 
                },
                new UserLevel 
                { 
                    Id = 2, 
                    Name = "Cliente", 
                    Description = "Acesso às funcionalidades básicas do sistema" 
                },
                new UserLevel 
                { 
                    Id = 3, 
                    Name = "Funcionário", 
                    Description = "Acesso às funcionalidades operacionais do sistema" 
                }
            );
        }
    }
}
