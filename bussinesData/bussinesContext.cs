using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CarBill.bussinesData.Models;

namespace CarBill.bussinesData
{
    public partial class bussinesContext : DbContext
    {
        public bussinesContext()
        {
        }

        public bussinesContext(DbContextOptions<bussinesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<BillRow> BillRows { get; set; } = null!;
        public virtual DbSet<BillRowImage> BillRowImages { get; set; } = null!;
        public virtual DbSet<BillsPart> BillsParts { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<MaintananceType> MaintananceTypes { get; set; } = null!;
        public virtual DbSet<SparePart> SpareParts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SAKSOOOK;Database=carBill;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.DicountPrecentage).HasColumnName("dicount_precentage");

                entity.Property(e => e.LastReading).HasColumnName("last_reading");

                entity.Property(e => e.MaintancePeriod)
                    .HasMaxLength(100)
                    .HasColumnName("maintance_period");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_price");

                entity.Property(e => e.VideoUrl).HasColumnName("video_URL");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bill_car");
            });

            modelBuilder.Entity<BillRow>(entity =>
            {
                entity.ToTable("billRow");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.TotalPriceForBillRow)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_price_forBillRow");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillRows)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_billRow_bill");
            });

            modelBuilder.Entity<BillRowImage>(entity =>
            {
                entity.ToTable("billRowImages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillRowId).HasColumnName("billRow_id");

                entity.Property(e => e.ImgUrl).HasColumnName("imgURL");

                entity.HasOne(d => d.BillRow)
                    .WithMany(p => p.BillRowImages)
                    .HasForeignKey(d => d.BillRowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_billRowImages_billRow");
            });

            modelBuilder.Entity<BillsPart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillRowId).HasColumnName("billRow_id");

                entity.Property(e => e.SparePartId).HasColumnName("sparePart_id");

                entity.HasOne(d => d.BillRow)
                    .WithMany(p => p.BillsParts)
                    .HasForeignKey(d => d.BillRowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillsParts_billRow");

                entity.HasOne(d => d.SparePart)
                    .WithMany(p => p.BillsParts)
                    .HasForeignKey(d => d.SparePartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillsParts_sparePart");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .HasColumnName("brand");

                entity.Property(e => e.Color)
                    .HasMaxLength(100)
                    .HasColumnName("color");

                entity.Property(e => e.LastReading).HasColumnName("last_reading");

                entity.Property(e => e.PlateNum)
                    .HasMaxLength(100)
                    .HasColumnName("plate_num");

                entity.Property(e => e.Symbol).HasColumnName("symbol");
            });

            modelBuilder.Entity<MaintananceType>(entity =>
            {
                entity.ToTable("maintananceType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.ToTable("sparePart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImageUrl).HasColumnName("image_URL");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.PriceOfInstaling)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price_ofInstaling");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SpareParts)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sparePart_maintananceType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
