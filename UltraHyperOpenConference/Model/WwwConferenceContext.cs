using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UltraHyperOpenConference.Model
{
    public partial class WwwConferenceContext : DbContext
    {
        public WwwConferenceContext()
        {
        }

        public WwwConferenceContext(DbContextOptions<WwwConferenceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BanUserCapability> BanUserCapabilities { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCapability> UserCapabilities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\;Database=WwwConference;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<BanUserCapability>(entity =>
            {
                entity.ToTable("BanUserCapability");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Moderator)
                    .WithMany(p => p.BanUserCapabilityModerators)
                    .HasForeignKey(d => d.ModeratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BanUserCapability_User1");

                entity.HasOne(d => d.UserCapabilityNavigation)
                    .WithMany(p => p.BanUserCapabilities)
                    .HasForeignKey(d => d.UserCapability)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BanUserCapability_UserCapability");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BanUserCapabilityUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BanUserCapability_User");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                entity.HasOne(d => d.ParentMessage)
                    .WithMany(p => p.InverseParentMessage)
                    .HasForeignKey(d => d.ParentMessageId)
                    .HasConstraintName("FK_Message_Message");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Theme");

                entity.HasOne(d => d.UserAuthor)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserAuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.ToTable("Theme");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserCapability>(entity =>
            {
                entity.ToTable("UserCapability");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
