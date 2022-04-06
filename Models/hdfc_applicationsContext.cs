using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hdfc_loan2_app.Models
{
    public partial class hdfc_applicationsContext : DbContext
    {
        public hdfc_applicationsContext()
        {
        }

        public hdfc_applicationsContext(DbContextOptions<hdfc_applicationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Loan2DocumentsTest> Loan2DocumentsTest { get; set; }
        public virtual DbSet<Loan2DocumentsTestLog> Loan2DocumentsTestLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-VTBLP8D;Database=hdfc_applications;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan2DocumentsTest>(entity =>
            {
                entity.ToTable("loan2_documents_test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Appid)
                    .HasColumnName("appid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Filepath)
                    .HasColumnName("filepath")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Loan2DocumentsTestLog>(entity =>
            {
                entity.ToTable("loan2_documents_test_log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Appid)
                    .HasColumnName("appid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationPath)
                    .HasColumnName("destination_path")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Filepath)
                    .HasColumnName("filepath")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UploadDate)
                    .HasColumnName("upload_date")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
