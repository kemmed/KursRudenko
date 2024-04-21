﻿// <auto-generated />
using System;
using Kurs2.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kurs2.Migrations
{
    [DbContext(typeof(Kurs2RudenkoContext))]
    partial class Kurs2RudenkoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kurs2.Models.AttributeProd", b =>
                {
                    b.Property<int>("AttributeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AttributeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttributeId"));

                    b.Property<string>("AttributeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AttributeValueType")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("AttributeId")
                        .HasName("PK_Attribute");

                    b.ToTable("AttributeProd", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.HasKey("OrderId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.OrderStock", b =>
                {
                    b.Property<int>("OrderStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderStockID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderStockId"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int>("StockId")
                        .HasColumnType("int")
                        .HasColumnName("StockID");

                    b.HasKey("OrderStockId");

                    b.HasIndex("OrderId");

                    b.HasIndex("StockId");

                    b.ToTable("OrderStock", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.ProductAssortment", b =>
                {
                    b.Property<int>("ProdAssortId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProdAssortID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdAssortId"));

                    b.Property<string>("ProdAssortDescription")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("ProdAssortName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProdAssortВrand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("ProdBasePrice")
                        .HasColumnType("decimal(7, 2)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("TypeID");

                    b.HasKey("ProdAssortId");

                    b.HasIndex("TypeId");

                    b.ToTable("ProductAssortment", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.ProductAttrib", b =>
                {
                    b.Property<int>("ProdAttribId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProdAttribID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdAttribId"));

                    b.Property<int>("AttributeId")
                        .HasColumnType("int")
                        .HasColumnName("AttributeID");

                    b.Property<string>("AttributeValue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<double>("PriceCoeff")
                        .HasColumnType("float");

                    b.Property<int>("ProdAssortId")
                        .HasColumnType("int")
                        .HasColumnName("ProdAssortID");

                    b.Property<int>("StockId")
                        .HasColumnType("int")
                        .HasColumnName("StockID");

                    b.HasKey("ProdAttribId")
                        .HasName("PK_ProductStock");

                    b.HasIndex("AttributeId");

                    b.HasIndex("ProdAssortId");

                    b.HasIndex("StockId");

                    b.ToTable("ProductAttrib", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SaleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"));

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime");

                    b.HasKey("SaleId")
                        .HasName("PK_Purchase");

                    b.ToTable("Sale", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.SaleStock", b =>
                {
                    b.Property<int>("SaleStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SaleStockID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleStockId"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("SaleId")
                        .HasColumnType("int")
                        .HasColumnName("SaleID");

                    b.Property<int>("StockId")
                        .HasColumnType("int")
                        .HasColumnName("StockID");

                    b.HasKey("SaleStockId")
                        .HasName("PK_PurchaseStrock");

                    b.HasIndex("SaleId");

                    b.HasIndex("StockId");

                    b.ToTable("SaleStock", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StockID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockId"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)");

                    b.HasKey("StockId");

                    b.ToTable("Stock", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.TypeProd", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("TypeId")
                        .HasName("PK_Type");

                    b.ToTable("TypeProd", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Kurs2.Models.OrderStock", b =>
                {
                    b.HasOne("Kurs2.Models.Order", "Order")
                        .WithMany("OrderStocks")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderStock_Order");

                    b.HasOne("Kurs2.Models.Stock", "Stock")
                        .WithMany("OrderStocks")
                        .HasForeignKey("StockId")
                        .IsRequired()
                        .HasConstraintName("FK_OrderStock_Stock");

                    b.Navigation("Order");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Kurs2.Models.ProductAssortment", b =>
                {
                    b.HasOne("Kurs2.Models.TypeProd", "Type")
                        .WithMany("ProductAssortments")
                        .HasForeignKey("TypeId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductAssortment_Type");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Kurs2.Models.ProductAttrib", b =>
                {
                    b.HasOne("Kurs2.Models.AttributeProd", "Attribute")
                        .WithMany("ProductAttribs")
                        .HasForeignKey("AttributeId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductStock_Attribute");

                    b.HasOne("Kurs2.Models.ProductAssortment", "ProdAssort")
                        .WithMany("ProductAttribs")
                        .HasForeignKey("ProdAssortId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductAttrib_ProductAssortment");

                    b.HasOne("Kurs2.Models.Stock", "Stock")
                        .WithMany("ProductAttribs")
                        .HasForeignKey("StockId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductStock_Stock");

                    b.Navigation("Attribute");

                    b.Navigation("ProdAssort");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Kurs2.Models.SaleStock", b =>
                {
                    b.HasOne("Kurs2.Models.Sale", "Sale")
                        .WithMany("SaleStocks")
                        .HasForeignKey("SaleId")
                        .IsRequired()
                        .HasConstraintName("FK_PurchaseStrock_Purchase");

                    b.HasOne("Kurs2.Models.Stock", "Stock")
                        .WithMany("SaleStocks")
                        .HasForeignKey("StockId")
                        .IsRequired()
                        .HasConstraintName("FK_PurchaseStrock_Stock");

                    b.Navigation("Sale");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Kurs2.Models.AttributeProd", b =>
                {
                    b.Navigation("ProductAttribs");
                });

            modelBuilder.Entity("Kurs2.Models.Order", b =>
                {
                    b.Navigation("OrderStocks");
                });

            modelBuilder.Entity("Kurs2.Models.ProductAssortment", b =>
                {
                    b.Navigation("ProductAttribs");
                });

            modelBuilder.Entity("Kurs2.Models.Sale", b =>
                {
                    b.Navigation("SaleStocks");
                });

            modelBuilder.Entity("Kurs2.Models.Stock", b =>
                {
                    b.Navigation("OrderStocks");

                    b.Navigation("ProductAttribs");

                    b.Navigation("SaleStocks");
                });

            modelBuilder.Entity("Kurs2.Models.TypeProd", b =>
                {
                    b.Navigation("ProductAssortments");
                });
#pragma warning restore 612, 618
        }
    }
}
