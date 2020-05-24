﻿// <auto-generated />
using System;
using Crypto.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Crypto.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200512084508_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Crypto.API.Models.CoinsHodle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("PortfolioID");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("coinID");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioID");

                    b.ToTable("CoinsHodle");
                });

            modelBuilder.Entity("Crypto.API.Models.Portfolio", b =>
                {
                    b.Property<int>("PortfolioID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PortfolioName");

                    b.Property<int?>("UserId");

                    b.HasKey("PortfolioID");

                    b.HasIndex("UserId");

                    b.ToTable("Portfolio");
                });

            modelBuilder.Entity("Crypto.API.Models.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AmountBuy");

                    b.Property<double>("AmountSell");

                    b.Property<int?>("CoinsHodleId");

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.HasIndex("CoinsHodleId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Crypto.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Crypto.API.Models.CoinsHodle", b =>
                {
                    b.HasOne("Crypto.API.Models.Portfolio", "Portfolio")
                        .WithMany("coinsHodle")
                        .HasForeignKey("PortfolioID");
                });

            modelBuilder.Entity("Crypto.API.Models.Portfolio", b =>
                {
                    b.HasOne("Crypto.API.Models.User", "User")
                        .WithMany("Portfolio")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Crypto.API.Models.Transactions", b =>
                {
                    b.HasOne("Crypto.API.Models.CoinsHodle", "CoinsHodle")
                        .WithMany("Transactions")
                        .HasForeignKey("CoinsHodleId");
                });
#pragma warning restore 612, 618
        }
    }
}