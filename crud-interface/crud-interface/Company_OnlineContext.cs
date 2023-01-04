using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace crud_interface
{
    public partial class Company_OnlineContext : DbContext
    {
        public Company_OnlineContext()
        {
        }

        public Company_OnlineContext(DbContextOptions<Company_OnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Dependent> Dependents { get; set; } = null!;
        public virtual DbSet<DeptLocation> DeptLocations { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<WorksOn> WorksOns { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Company_Online");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Dnumber);

                entity.ToTable("Department");

                entity.Property(e => e.Dnumber)
                    .ValueGeneratedNever()
                    .HasColumnName("DNumber");

                entity.Property(e => e.Dname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DName");

                entity.Property(e => e.MgrSsn)
                    .HasColumnType("numeric(9, 0)")
                    .HasColumnName("MgrSSN");

                entity.Property(e => e.MgrStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.MgrSsnNavigation)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.MgrSsn)
                    .HasConstraintName("FK_Department_Employee");
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.HasKey(e => new { e.Essn, e.DependentName });

                entity.ToTable("Dependent");

                entity.Property(e => e.Essn).HasColumnType("numeric(9, 0)");

                entity.Property(e => e.DependentName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dependent_Name");

                entity.Property(e => e.Bdate)
                    .HasColumnType("datetime")
                    .HasColumnName("BDate");

                entity.Property(e => e.Relationship)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.HasOne(d => d.EssnNavigation)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.Essn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dependent_Employee");
            });

            modelBuilder.Entity<DeptLocation>(entity =>
            {
                entity.HasKey(e => new { e.Dnumber, e.Dlocation });

                entity.ToTable("Dept_Locations");

                entity.Property(e => e.Dnumber).HasColumnName("DNUmber");

                entity.Property(e => e.Dlocation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DLocation");

                entity.HasOne(d => d.DnumberNavigation)
                    .WithMany(p => p.DeptLocations)
                    .HasForeignKey(d => d.Dnumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dept_Locations_Department");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Ssn);

                entity.ToTable("Employee");

                entity.Property(e => e.Ssn)
                    .HasColumnType("numeric(9, 0)")
                    .HasColumnName("SSN");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bdate)
                    .HasColumnType("datetime")
                    .HasColumnName("BDate");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LName");

                entity.Property(e => e.Minit)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Salary).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SuperSsn)
                    .HasColumnType("numeric(9, 0)")
                    .HasColumnName("SuperSSN");

                entity.HasOne(d => d.DnoNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Dno)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.SuperSsnNavigation)
                    .WithMany(p => p.InverseSuperSsnNavigation)
                    .HasForeignKey(d => d.SuperSsn)
                    .HasConstraintName("FK_Employee_Employee");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Pnumber);

                entity.ToTable("Project");

                entity.Property(e => e.Pnumber)
                    .ValueGeneratedNever()
                    .HasColumnName("PNumber");

                entity.Property(e => e.Dnum).HasColumnName("DNum");

                entity.Property(e => e.Plocation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PLocation");

                entity.Property(e => e.Pname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PName");

                entity.HasOne(d => d.DnumNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.Dnum)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Project_Department");
            });

            modelBuilder.Entity<WorksOn>(entity =>
            {
                entity.HasKey(e => new { e.Essn, e.Pno });

                entity.ToTable("Works_on");

                entity.Property(e => e.Essn).HasColumnType("numeric(9, 0)");

                entity.HasOne(d => d.EssnNavigation)
                    .WithMany(p => p.WorksOns)
                    .HasForeignKey(d => d.Essn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_on_Employee");

                entity.HasOne(d => d.PnoNavigation)
                    .WithMany(p => p.WorksOns)
                    .HasForeignKey(d => d.Pno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_on_Project");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
