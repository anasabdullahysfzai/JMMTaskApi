using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace JMMTaskApi
{
    public partial class jmmContext : DbContext
    {
        IConfiguration configuration;

        public jmmContext()
        {
        }

        public jmmContext(DbContextOptions<jmmContext> options , IConfiguration iconfig)
            : base(options)
        {
            this.configuration = iconfig;
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.configuration.GetSection("ConnectionStrings").GetValue<String>("Default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("PK__Customer__213EE774A73BA1F6");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.CAddress)
                    .IsRequired()
                    .HasColumnName("c_address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("c_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => e.OpId)
                    .HasName("PK__Order_Pr__A26AE2CEA6F14704");

                entity.ToTable("Order_Product");

                entity.Property(e => e.OpId).HasColumnName("op_id");

                entity.Property(e => e.OId).HasColumnName("o_id");

                entity.Property(e => e.PId).HasColumnName("p_id");

                entity.HasOne(d => d.O)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.OId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderProduct");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.PId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProduct_Product");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OId)
                    .HasName("PK__ORDERS__904BC20E6DAFDC73");

                entity.ToTable("ORDERS");

                entity.Property(e => e.OId).HasColumnName("o_id");

                entity.Property(e => e.CId).HasColumnName("c_id");

                entity.Property(e => e.ODate)
                    .HasColumnName("o_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OType)
                    .IsRequired()
                    .HasColumnName("o_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SId).HasColumnName("s_id");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CId)
                    .HasConstraintName("FK_ORDERS_CUSTOMER");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SId)
                    .HasConstraintName("FK_ORDERS_SUPPLIERS");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PK__Product__82E06B91AA14A403");

                entity.Property(e => e.PId).HasColumnName("p_id");

                entity.Property(e => e.PCode)
                    .IsRequired()
                    .HasColumnName("p_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PName)
                    .IsRequired()
                    .HasColumnName("p_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PStock).HasColumnName("p_stock");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SId)
                    .HasName("PK__Supplier__2F3684F43611EC1C");

                entity.Property(e => e.SId).HasColumnName("s_id");

                entity.Property(e => e.SAddress)
                    .IsRequired()
                    .HasColumnName("s_address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SName)
                    .IsRequired()
                    .HasColumnName("s_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
