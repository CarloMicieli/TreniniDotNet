﻿//// <auto-generated />
//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
//using TreniniDotNet.Infrastructure.Persistence;

//namespace TreniniDotNet.Web.Migrations
//{
//    [DbContext(typeof(ApplicationDbContext))]
//    [Migration("20200301173002_CatalogItemsChanges")]
//    partial class CatalogItemsChanges
//    {
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
//                .HasAnnotation("ProductVersion", "3.1.1")
//                .HasAnnotation("Relational:MaxIdentifierLength", 63);

//            modelBuilder.Entity("TreniniDotNet.Infrastructure.Persistence.Catalog.Brands.Brand", b =>
//                {
//                    b.Property<Guid>("BrandId")
//                        .HasColumnType("uuid");

//                    b.Property<bool?>("Active")
//                        .HasColumnType("boolean");

//                    b.Property<string>("BrandKind")
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25);

//                    b.Property<string>("CompanyName")
//                        .HasColumnType("text");

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("timestamp without time zone");

//                    b.Property<string>("EmailAddress")
//                        .HasColumnType("character varying(250)")
//                        .HasMaxLength(250);

//                    b.Property<string>("Name")
//                        .IsRequired()
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25)
//                        .IsUnicode(true);

//                    b.Property<string>("Slug")
//                        .IsRequired()
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25);

//                    b.Property<int>("Version")
//                        .HasColumnType("integer");

//                    b.Property<string>("WebsiteUrl")
//                        .HasColumnType("character varying(250)")
//                        .HasMaxLength(250);

//                    b.HasKey("BrandId");

//                    b.HasIndex("Name")
//                        .IsUnique()
//                        .HasName("Idx_Brands_Name");

//                    b.HasIndex("Slug")
//                        .IsUnique()
//                        .HasName("Idx_Brands_Slug");

//                    b.ToTable("Brands");
//                });

//            modelBuilder.Entity("TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.CatalogItem", b =>
//                {
//                    b.Property<Guid>("CatalogItemId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uuid");

//                    b.Property<Guid>("BrandId")
//                        .HasColumnType("uuid");

//                    b.Property<DateTime?>("CreatedAt")
//                        .HasColumnType("timestamp without time zone");

//                    b.Property<string>("DeliveryDate")
//                        .HasColumnType("character varying(10)")
//                        .HasMaxLength(10)
//                        .IsUnicode(false);

//                    b.Property<string>("Description")
//                        .IsRequired()
//                        .HasColumnType("character varying(250)")
//                        .HasMaxLength(250)
//                        .IsUnicode(true);

//                    b.Property<string>("ItemNumber")
//                        .IsRequired()
//                        .HasColumnType("character varying(10)")
//                        .HasMaxLength(10)
//                        .IsUnicode(false);

//                    b.Property<string>("ModelDescription")
//                        .HasColumnType("character varying(2500)")
//                        .HasMaxLength(2500)
//                        .IsUnicode(true);

//                    b.Property<string>("PowerMethod")
//                        .HasColumnType("text");

//                    b.Property<string>("PrototypeDescription")
//                        .HasColumnType("character varying(2500)")
//                        .HasMaxLength(2500)
//                        .IsUnicode(true);

//                    b.Property<bool?>("Released")
//                        .HasColumnType("boolean");

//                    b.Property<Guid>("ScaleId")
//                        .HasColumnType("uuid");

//                    b.Property<string>("Slug")
//                        .IsRequired()
//                        .HasColumnType("character varying(50)")
//                        .HasMaxLength(50)
//                        .IsUnicode(false);

//                    b.HasKey("CatalogItemId");

//                    b.HasIndex("BrandId");

//                    b.HasIndex("ItemNumber")
//                        .HasName("Idx_CatalogItems_ItemNumber_BrandName");

//                    b.HasIndex("ScaleId");

//                    b.HasIndex("Slug")
//                        .IsUnique()
//                        .HasName("Idx_CatalogItems_Slug");

//                    b.ToTable("CatalogItems");
//                });

//            modelBuilder.Entity("TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.RollingStock", b =>
//                {
//                    b.Property<Guid>("RollingStockId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("uuid");

//                    b.Property<Guid>("CatalogItemId")
//                        .HasColumnType("uuid");

//                    b.Property<string>("Category")
//                        .IsRequired()
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25)
//                        .IsUnicode(false);

//                    b.Property<string>("ClassName")
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25)
//                        .IsUnicode(false);

//                    b.Property<string>("DepotName")
//                        .HasColumnType("character varying(50)")
//                        .HasMaxLength(50)
//                        .IsUnicode(true);

//                    b.Property<string>("Era")
//                        .IsRequired()
//                        .HasColumnType("character varying(5)")
//                        .HasMaxLength(5)
//                        .IsUnicode(false);

//                    b.Property<decimal?>("Length")
//                        .HasColumnType("numeric");

//                    b.Property<string>("Livery")
//                        .HasColumnType("character varying(50)")
//                        .HasMaxLength(50)
//                        .IsUnicode(true);

//                    b.Property<Guid>("RailwayId")
//                        .HasColumnType("uuid");

//                    b.Property<string>("RoadNumber")
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25)
//                        .IsUnicode(false);

//                    b.Property<string>("Series")
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25)
//                        .IsUnicode(false);

//                    b.HasKey("RollingStockId");

//                    b.HasIndex("CatalogItemId");

//                    b.HasIndex("RailwayId");

//                    b.ToTable("RollingStock");
//                });

//            modelBuilder.Entity("TreniniDotNet.Infrastructure.Persistence.Catalog.Railways.Railway", b =>
//                {
//                    b.Property<Guid>("RailwayId")
//                        .HasColumnType("uuid");

//                    b.Property<string>("CompanyName")
//                        .HasColumnType("character varying(100)")
//                        .HasMaxLength(100)
//                        .IsUnicode(true);

//                    b.Property<string>("Country")
//                        .HasColumnType("text");

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("timestamp without time zone");

//                    b.Property<string>("Name")
//                        .IsRequired()
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25)
//                        .IsUnicode(true);

//                    b.Property<DateTime?>("OperatingSince")
//                        .HasColumnType("timestamp without time zone");

//                    b.Property<DateTime?>("OperatingUntil")
//                        .HasColumnType("timestamp without time zone");

//                    b.Property<string>("Slug")
//                        .IsRequired()
//                        .HasColumnType("character varying(25)")
//                        .HasMaxLength(25);

//                    b.Property<string>("Status")
//                        .HasColumnType("character varying(10)")
//                        .HasMaxLength(10);

//                    b.Property<int>("Version")
//                        .HasColumnType("integer");

//                    b.HasKey("RailwayId");

//                    b.HasIndex("Name")
//                        .IsUnique()
//                        .HasName("Idx_Railways_Name");

//                    b.HasIndex("Slug")
//                        .IsUnique()
//                        .HasName("Idx_Railways_Slug");

//                    b.ToTable("Railways");
//                });

//            modelBuilder.Entity("TreniniDotNet.Infrastructure.Persistence.Catalog.Scales.Scale", b =>
//                {
//                    b.Property<Guid>("ScaleId")
//                        .HasColumnType("uuid");

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("timestamp without time zone");

//                    b.Property<decimal>("Gauge")
//                        .HasColumnType("numeric");

//                    b.Property<string>("Name")
//                        .IsRequired()
//                        .HasColumnType("character varying(10)")
//                        .HasMaxLength(10)
//                        .IsUnicode(true);

//                    b.Property<string>("Notes")
//                        .HasColumnType("text");

//                    b.Property<decimal>("Ratio")
//                        .HasColumnType("numeric");

//                    b.Property<string>("Slug")
//                        .IsRequired()
//                        .HasColumnType("character varying(10)")
//                        .HasMaxLength(10);

//                    b.Property<string>("TrackGauge")
//                        .HasColumnType("character varying(15)")
//                        .HasMaxLength(15);

//                    b.Property<int>("Version")
//                        .HasColumnType("integer");

//                    b.HasKey("ScaleId");

//                    b.HasIndex("Name")
//                        .IsUnique()
//                        .HasName("Idx_Scales_Name");

//                    b.HasIndex("Slug")
//                        .IsUnique()
//                        .HasName("Idx_Scales_Slug");

//                    b.ToTable("Scales");
//                });

//            modelBuilder.Entity("TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.CatalogItem", b =>
//                {
//                    b.HasOne("TreniniDotNet.Infrastructure.Persistence.Catalog.Brands.Brand", "Brand")
//                        .WithMany()
//                        .HasForeignKey("BrandId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("TreniniDotNet.Infrastructure.Persistence.Catalog.Scales.Scale", "Scale")
//                        .WithMany()
//                        .HasForeignKey("ScaleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.RollingStock", b =>
//                {
//                    b.HasOne("TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.CatalogItem", "CatalogItem")
//                        .WithMany("RollingStocks")
//                        .HasForeignKey("CatalogItemId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("TreniniDotNet.Infrastructure.Persistence.Catalog.Railways.Railway", "Railway")
//                        .WithMany()
//                        .HasForeignKey("RailwayId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}