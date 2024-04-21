using System;
using Kurs2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kurs2.Database;

public partial class Kurs2RudenkoContext : DbContext
{
    public Kurs2RudenkoContext()
    {
    }

    public Kurs2RudenkoContext(DbContextOptions<Kurs2RudenkoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttributeProd> AttributeProds { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProd> OrderProds { get; set; }

    public virtual DbSet<ProductAssortment> ProductAssortments { get; set; }

    public virtual DbSet<ProductAttrib> ProductAttribs { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleProd> SaleProds { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<TypeProd> TypeProds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KEMMED\\SQLEXPRESS;Initial Catalog=Kurs2Rudenko;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttributeProd>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK_Attribute");

            entity.ToTable("AttributeProd");

            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AttributeValueType)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderProd>(entity =>
        {
            entity.HasKey(e => e.OrderProdId).HasName("PK_OrderStock");

            entity.ToTable("OrderProd");

            entity.Property(e => e.OrderProdId).HasColumnName("OrderProdID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProds)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderStock_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProds)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderStock_ProductAssortment");
        });

        modelBuilder.Entity<ProductAssortment>(entity =>
        {
            entity.HasKey(e => e.ProdAssortId);

            entity.ToTable("ProductAssortment");

            entity.Property(e => e.ProdAssortId).HasColumnName("ProdAssortID");
            entity.Property(e => e.ProdAssortDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProdAssortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProdBasePrice).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.ВrandId).HasColumnName("ВrandID");

            entity.HasOne(d => d.Type).WithMany(p => p.ProductAssortments)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductAssortment_Type");

            entity.HasOne(d => d.Вrand).WithMany(p => p.ProductAssortments)
                .HasForeignKey(d => d.ВrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductAssortment_Brand");
        });

        modelBuilder.Entity<ProductAttrib>(entity =>
        {
            entity.HasKey(e => e.ProdAttribId).HasName("PK_ProductStock");

            entity.ToTable("ProductAttrib");

            entity.Property(e => e.ProdAttribId).HasColumnName("ProdAttribID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeValue)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProdAssortId).HasColumnName("ProdAssortID");
            entity.Property(e => e.StockId).HasColumnName("StockID");

            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductAttribs)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStock_Attribute");

            entity.HasOne(d => d.ProdAssort).WithMany(p => p.ProductAttribs)
                .HasForeignKey(d => d.ProdAssortId)
                .HasConstraintName("FK_ProductAttrib_ProductAssortment");

            entity.HasOne(d => d.Stock).WithMany(p => p.ProductAttribs)
                .HasForeignKey(d => d.StockId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductStock_Stock");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK_Purchase");

            entity.ToTable("Sale");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SaleProd>(entity =>
        {
            entity.HasKey(e => e.SaleProdId).HasName("PK_PurchaseStrock");

            entity.ToTable("SaleProd");

            entity.Property(e => e.SaleProdId).HasColumnName("SaleProdID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SaleId).HasColumnName("SaleID");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleProds)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleStock_ProductAssortment");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleProds)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK_PurchaseStrock_Purchase");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.Property(e => e.StockId).HasColumnName("StockID");
            entity.Property(e => e.Place)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TypeProd>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK_Type");

            entity.ToTable("TypeProd");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
