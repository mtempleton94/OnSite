using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnSite.Models
{
    public partial class VisitorContext : DbContext
    {
        public VisitorContext()
        {
        }

        public VisitorContext(DbContextOptions<VisitorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Organisation> Organisation { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffAreaAccess> StaffAreaAccess { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }
        public virtual DbSet<Visitor> Visitor { get; set; }
        public virtual DbSet<VisitorBadge> VisitorBadge { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Visitor;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasIndex(e => new { e.SiteId, e.Floor, e.Description })
                    .HasName("AK_AreaIdentification")
                    .IsUnique();

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Floor)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Area)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_SiteID");
            });

            modelBuilder.Entity<Organisation>(entity =>
            {
                entity.HasIndex(e => e.OrganisationId)
                    .HasName("AK_OrganisationID")
                    .IsUnique();

                entity.Property(e => e.OrganisationId).HasColumnName("OrganisationID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasIndex(e => new { e.StreetAddress, e.City, e.State, e.PostCode })
                    .HasName("AK_SiteAddress")
                    .IsUnique();

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.OrganisationId).HasColumnName("OrganisationID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Organisation)
                    .WithMany(p => p.Site)
                    .HasForeignKey(d => d.OrganisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Site_OrganisationID");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("AK_StaffEmail")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("AK_StaffPhone")
                    .IsUnique();

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StaffAreaAccess>(entity =>
            {
                entity.HasKey(e => new { e.StaffId, e.AreaId });

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.StaffAreaAccess)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffAreaAccess_AreaID");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffAreaAccess)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffAreaAccess_StaffID");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.BadgeId).HasColumnName("BadgeID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SignedInById).HasColumnName("SignedInByID");

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.Property(e => e.StaffEscortId).HasColumnName("StaffEscortID");

                entity.Property(e => e.UnescortedApprovedById).HasColumnName("UnescortedApprovedByID");

                entity.Property(e => e.VehicleId)
                    .HasColumnName("VehicleID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_Visit_AreaID");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.BadgeId)
                    .HasConstraintName("FK_Visit_BadgeID");

                entity.HasOne(d => d.SignedInBy)
                    .WithMany(p => p.VisitSignedInBy)
                    .HasForeignKey(d => d.SignedInById)
                    .HasConstraintName("FK_Visit_SignedInByID");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_SiteID");

                entity.HasOne(d => d.StaffEscort)
                    .WithMany(p => p.VisitStaffEscort)
                    .HasForeignKey(d => d.StaffEscortId)
                    .HasConstraintName("FK_Visit_StaffEscortID");

                entity.HasOne(d => d.UnescortedApprovedBy)
                    .WithMany(p => p.VisitUnescortedApprovedBy)
                    .HasForeignKey(d => d.UnescortedApprovedById)
                    .HasConstraintName("FK_Visit_UnescortedApprovedByID");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.VisitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_VisitorID");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.HasIndex(e => e.IdentificationNumber)
                    .HasName("AK_Visitor_IdentificationNumber")
                    .IsUnique();

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificationNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrganisationId).HasColumnName("OrganisationID");

                entity.HasOne(d => d.Organisation)
                    .WithMany(p => p.Visitor)
                    .HasForeignKey(d => d.OrganisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitor_OrganisationID");
            });

            modelBuilder.Entity<VisitorBadge>(entity =>
            {
                entity.HasKey(e => e.BadgeId);

                entity.Property(e => e.BadgeId).HasColumnName("BadgeID");

                entity.Property(e => e.BadgeType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.VisitorBadge)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitorBadge_SiteID");
            });
        }
    }
}
