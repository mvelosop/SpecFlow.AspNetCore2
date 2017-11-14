﻿// <auto-generated />
using DFlow.Budget.Core.Model;
using DFlow.Budget.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DFlow.Budget.Migrations
{
    [DbContext(typeof(BudgetDbContext))]
    partial class BudgetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DFlow.Budget.Core.Model.BudgetClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("SortOrder");

                    b.Property<int>("Tenant_Id");

                    b.Property<int>("TransactionType");

                    b.HasKey("Id");

                    b.HasIndex("Tenant_Id", "Name")
                        .IsUnique();

                    b.ToTable("BudgetClasses","Budget");
                });

            modelBuilder.Entity("DFlow.Budget.Core.Model.BudgetLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int>("BudgetClass_Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Order");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("BudgetClass_Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("BudgetLines","Budget");
                });

            modelBuilder.Entity("DFlow.Budget.Core.Model.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Tenants","Tenants");
                });

            modelBuilder.Entity("DFlow.Budget.Core.Model.BudgetClass", b =>
                {
                    b.HasOne("DFlow.Budget.Core.Model.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("Tenant_Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DFlow.Budget.Core.Model.BudgetLine", b =>
                {
                    b.HasOne("DFlow.Budget.Core.Model.BudgetClass", "BudgetClass")
                        .WithMany("BudgetLines")
                        .HasForeignKey("BudgetClass_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
