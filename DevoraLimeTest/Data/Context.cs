using DevoraLimeTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace DevoraLimeTest.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>()
                .HasOne<Models.Type>(h => h.Type)
                .WithMany()
                .HasForeignKey(h => h.TypeID);
            modelBuilder.Entity<Arena>()
                .HasMany<Hero>(a => a.Heroes)
                .WithOne()
                .HasForeignKey(h => h.ArenaID);
            modelBuilder.Entity<Arena>()
                .HasMany<History>(a => a.Histories)
                .WithOne()
                .HasForeignKey(h => h.ArenaID);
            modelBuilder.Entity<History>()
                .HasMany<Fight>(h=>h.Fights)
                .WithOne()
                .HasForeignKey(f=>f.HistoryID);
            modelBuilder.Entity<Fight>()
                .HasOne<Hero>(f => f.Attacker)
                .WithMany()
                .HasForeignKey(f => f.AttackerID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Fight>()
               .HasOne<Hero>(f => f.Deffender)
               .WithMany()
               .HasForeignKey(f => f.DeffenderID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Type>()
                .HasData(new Models.Type { Id = 1, Name = "Archer", MaxHP = 100 });
            modelBuilder.Entity<Models.Type>()
                .HasData(new Models.Type { Id = 2, Name = "Knight", MaxHP = 150 });
            modelBuilder.Entity<Models.Type>()
                .HasData(new Models.Type { Id = 3, Name = "SwordsMan", MaxHP = 120 });
        }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Arena> Arenas { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Fight> Fights { get; set; }
       
    }
}
