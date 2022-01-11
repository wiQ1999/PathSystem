﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PathSystem.Database;

namespace PathSystem.Database.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20220110203405_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PathSystem.Models.EntityModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Speed")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("PathSystem.Models.MapModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PositionX")
                        .HasColumnType("int");

                    b.Property<int>("PositionY")
                        .HasColumnType("int");

                    b.Property<bool>("Value")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Map");
                });

            modelBuilder.Entity("PathSystem.Models.PathModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Paths");
                });

            modelBuilder.Entity("PathSystem.Models.PathPointModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PathModelId")
                        .HasColumnType("int");

                    b.Property<int>("PositionX")
                        .HasColumnType("int");

                    b.Property<int>("PositionY")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PathModelId");

                    b.ToTable("PathPoints");
                });

            modelBuilder.Entity("PathSystem.Models.PathPointModel", b =>
                {
                    b.HasOne("PathSystem.Models.PathModel", null)
                        .WithMany("Points")
                        .HasForeignKey("PathModelId");
                });

            modelBuilder.Entity("PathSystem.Models.PathModel", b =>
                {
                    b.Navigation("Points");
                });
#pragma warning restore 612, 618
        }
    }
}
