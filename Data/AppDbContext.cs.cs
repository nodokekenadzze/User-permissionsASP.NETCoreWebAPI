using Microsoft.EntityFrameworkCore;
using UserPermissionsApi.Models;

namespace UserPermissionsApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPermission>()
                .HasKey(up => new { up.UserId, up.PermissionId });

            modelBuilder.Entity<UserPermission>()
                .HasOne(u => u.User)
                .WithMany(up => up.UserPermissions)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserPermission>()
                .HasOne(p => p.Permission)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.PermissionId);
        }

    }
}