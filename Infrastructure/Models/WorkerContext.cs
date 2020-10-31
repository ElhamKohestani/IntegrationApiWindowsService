using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IntegrationApiSynchroniser.Infrastructure.Models
{
    public partial class WorkerContext : DbContext
    {
        public WorkerContext()
        {
        }

        public WorkerContext(DbContextOptions<WorkerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Api> Api { get; set; }
        public virtual DbSet<ApiTokenApiQuery> ApiTokenApiQuery { get; set; }
        public virtual DbSet<ApiTokens> ApiTokens { get; set; }
        public virtual DbSet<RoleClaims> RoleClaims { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Stakeholder> Stakeholder { get; set; }
        public virtual DbSet<StakeholderApis> StakeholderApis { get; set; }
        public virtual DbSet<TblApiSync> TblApiSync { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserTokens> UserTokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=Localhost\\MSS19; Database=api_integration; User ID=sa; Password=kasperskyantigeral");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Api>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.Query).IsRequired();

                entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            });

            modelBuilder.Entity<ApiTokenApiQuery>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ApiId).HasColumnName("ApiID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TokenId).HasColumnName("TokenID");
            });

            modelBuilder.Entity<ApiTokens>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.StakeholderId).HasColumnName("StakeholderID");

                entity.Property(e => e.Token).IsRequired();

                entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            });

            modelBuilder.Entity<RoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<Stakeholder>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StakeholderName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StakeholderApis>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StakeholderAPIs");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StakeholderId).HasColumnName("StakeholderID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblApiSync>(entity =>
            {
                entity.HasKey(e => e.ApiSynId);

                entity.ToTable("tbl_ApiSync");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndPoint).HasMaxLength(300);

                entity.Property(e => e.Parameter).HasMaxLength(300);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
