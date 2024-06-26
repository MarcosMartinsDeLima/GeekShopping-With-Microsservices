﻿// <auto-generated />
using GeekShopping.CouponApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.CouponApi.Migrations
{
    [DbContext(typeof(MysqlContext))]
    [Migration("20240429203006_addCouponData")]
    partial class addCouponData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.CouponApi.Models.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CouponCode");

                    b.Property<decimal>("DiscountAmout")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("DiscountAmout");

                    b.HasKey("Id");

                    b.ToTable("Coupon");
                });
#pragma warning restore 612, 618
        }
    }
}
