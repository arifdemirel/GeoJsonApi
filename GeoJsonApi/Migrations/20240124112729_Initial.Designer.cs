﻿// <auto-generated />
using System;
using GeoJsonApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeoJsonApi.Migrations
{
    [DbContext(typeof(GeoJsonDbContext))]
    [Migration("20240124112729_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GeoJsonApi.Models.Coordinate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int?>("PointId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PointId");

                    b.ToTable("Coordinate");
                });

            modelBuilder.Entity("GeoJsonApi.Models.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("GeoJsonApi.Models.Coordinate", b =>
                {
                    b.HasOne("GeoJsonApi.Models.Point", null)
                        .WithMany("Coordinates")
                        .HasForeignKey("PointId");
                });

            modelBuilder.Entity("GeoJsonApi.Models.Point", b =>
                {
                    b.Navigation("Coordinates");
                });
#pragma warning restore 612, 618
        }
    }
}
