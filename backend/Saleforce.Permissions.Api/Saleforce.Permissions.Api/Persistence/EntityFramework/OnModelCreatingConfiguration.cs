using Microsoft.EntityFrameworkCore;
using Saleforce.Permissions.Api.Entities;

namespace Saleforce.Permissions.Api.Persistence.EntityFramework
{
    public static class OnModelCreatingConfiguration
    {
        internal static void ConfigureRoleTypePrimaryKey(this ModelBuilder builder)
        {
            builder.Entity<Role>().HasKey(x => x.RoleType);
            builder.Entity<Role>().ToTable("Roles");
        }

        internal static void ConfigurePermissionPrimaryKey(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasKey(x => x.PermissionType);
            modelBuilder.Entity<Permission>().Property(x => x.PermissionType).IsRequired();
            modelBuilder.Entity<Permission>().ToTable("Permissions");
        }

        internal static void ConfigureRolePermissionsPrimaryKey(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermissions>()
                .ToTable("RolePermissions");
            modelBuilder.Entity<RolePermissions>()
                .HasKey(x => new { x.RoleType, x.PermissionType });
        }

        internal static void ConfigureUserPermissions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPermissions>().ToTable("UserPermissions");
            modelBuilder.Entity<UserPermissions>().Property(x => x.UserId).IsRequired();
            modelBuilder.Entity<UserPermissions>().Property(x => x.DataScope).IsRequired().HasColumnType("jsonb");
        }

        internal static void ConfigureUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<UserRoles>().Property(x => x.DataScope).IsRequired().HasColumnType("jsonb");
        }

        internal static void ConfigureUserDeliveryApproval(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryApproval>().ToTable("delivery_approvals");
            modelBuilder
                .Entity<DeliveryApproval>()
                .HasOne(x => x.UserInfo)
                .WithMany(ui => ui.DeliveryApprovals)
                .HasForeignKey(approval => approval.UserInfo);
        }

        internal static void ConfigureDeliveryApproval(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DeliveryApproval>()
                .HasOne(x => x.Delivery)
                .WithOne(delivery => delivery.DeliveryApproval)
                .HasForeignKey<Delivery>(delivery => delivery.DeliveryApprovalId);
        }
    }
}