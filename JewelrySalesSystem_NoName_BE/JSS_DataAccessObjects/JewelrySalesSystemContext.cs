﻿using System;
using System.Collections.Generic;
using JSS_BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace JSS_DataAccessObjects;

public partial class JewelrySalesSystemContext : DbContext
{
    public JewelrySalesSystemContext()
    {
    }

    public JewelrySalesSystemContext(DbContextOptions<JewelrySalesSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ConditionWarranty> ConditionWarranties { get; set; }

    public virtual DbSet<Diamond> Diamonds { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<GoldRate> GoldRates { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MemberType> MemberTypes { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<ProcessPrice> ProcessPrices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductConditionGroup> ProductConditionGroups { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<PurchasePrice> PurchasePrices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SilverRate> SilverRates { get; set; }

    public virtual DbSet<Stall> Stalls { get; set; }

    public virtual DbSet<SubProduct> SubProducts { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    public virtual DbSet<WarrantyMappingCondition> WarrantyMappingConditions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.ToTable("Account");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ConditionWarranty>(entity =>
        {
            entity.ToTable("ConditionWarranty");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Diamond>(entity =>
        {
            entity.ToTable("Diamond");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Clarity).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Cut).HasMaxLength(50);
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.UpsDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("Discount");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Membership).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.MembershipId)
                .HasConstraintName("FK_Discount_Membership");
        });

        modelBuilder.Entity<GoldRate>(entity =>
        {
            entity.ToTable("GoldRate");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UpsDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("Material");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.MaterialName).HasMaxLength(100);
        });

        modelBuilder.Entity<MemberType>(entity =>
        {
            entity.ToTable("MemberType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.ToTable("Membership");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.MemberType).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.MemberTypeId)
                .HasConstraintName("FK_Membership_MemberType");

            entity.HasOne(d => d.User).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Membership_Account");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");

            entity.HasOne(d => d.Discount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_Order_Discount");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");

            entity.HasOne(d => d.Promotion).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK_OrderDetail_Promotion");
        });

        modelBuilder.Entity<ProcessPrice>(entity =>
        {
            entity.ToTable("ProcessPrice");

            entity.Property(e => e.ProcesspriceId).ValueGeneratedNever();
            entity.Property(e => e.ProcessPrice1).HasColumnName("ProcessPrice");
            entity.Property(e => e.UpsDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.UpsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Material).WithMany(p => p.Products)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK_Product_Material");

            entity.HasOne(d => d.Sub).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubId)
                .HasConstraintName("FK_Product_SubProducts");
        });

        modelBuilder.Entity<ProductConditionGroup>(entity =>
        {
            entity.ToTable("ProductConditionGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductConditionGroups)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductConditionGroup_Product");

            entity.HasOne(d => d.Promotion).WithMany(p => p.ProductConditionGroups)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductConditionGroup_Promotion");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.ToTable("Program");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Membership).WithMany(p => p.Programs)
                .HasForeignKey(d => d.MembershipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Membership");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Programs)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Promotion");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.ToTable("Promotion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");
            entity.Property(e => e.InsDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpsDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PurchasePrice>(entity =>
        {
            entity.ToTable("PurchasePrice");

            entity.Property(e => e.PurchasePriceId).ValueGeneratedNever();
            entity.Property(e => e.PurchasePrice1)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("PurchasePrice");
            entity.Property(e => e.UpsDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SilverRate>(entity =>
        {
            entity.ToTable("SilverRate");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UpsDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Stall>(entity =>
        {
            entity.ToTable("Stall");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Staff).WithMany(p => p.Stalls)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stall_User");
        });

        modelBuilder.Entity<SubProduct>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TitleProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.InsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Order");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.ToTable("Warranty");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CodeWarranty)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.DateOfPurchase).HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.OrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warranty_Order");
        });

        modelBuilder.Entity<WarrantyMappingCondition>(entity =>
        {
            entity.ToTable("WarrantyMappingCondition");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsDate).HasColumnType("datetime");

            entity.HasOne(d => d.ConditionWarranty).WithMany(p => p.WarrantyMappingConditions)
                .HasForeignKey(d => d.ConditionWarrantyId)
                .HasConstraintName("FK_WarrantyMappingCondition_ConditionWarranty");

            entity.HasOne(d => d.Warranty).WithMany(p => p.WarrantyMappingConditions)
                .HasForeignKey(d => d.WarrantyId)
                .HasConstraintName("FK_WarrantyMappingCondition_Warranty");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

