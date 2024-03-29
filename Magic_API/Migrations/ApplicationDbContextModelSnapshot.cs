﻿// <auto-generated />
using System;
using Magic_API.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Magic_API.Modelos.Magic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Magics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "otros",
                            Detalle = "Desarrollo de una API y consumirla",
                            FechaActualizacion = new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8476),
                            FechaCreacion = new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8467),
                            ImagenUrl = "",
                            MetrosCuadrados = 50,
                            Nombre = "Noe Corvera",
                            Ocupantes = 5,
                            Tarifa = 37.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "otros",
                            Detalle = "Creacion de una API",
                            FechaActualizacion = new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8481),
                            FechaCreacion = new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8480),
                            ImagenUrl = "",
                            MetrosCuadrados = 30,
                            Nombre = "Danely Alas",
                            Ocupantes = 4,
                            Tarifa = 20.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
