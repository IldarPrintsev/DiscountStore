using Microsoft.EntityFrameworkCore;
using DiscountStore.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscountStore.DAL.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(p => p.Discount)
                .WithOne(t => t.Item)
                .HasForeignKey<Discount>(p => p.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
