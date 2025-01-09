﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TasinmazProject.DataAccess;

namespace TasinmazProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.Il", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IlAdi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Iller");
                });

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.Ilce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IlId")
                        .HasColumnType("integer");

                    b.Property<string>("IlceAdi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IlId");

                    b.ToTable("Ilceler");
                });

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.Mahalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IlceId")
                        .HasColumnType("integer");

                    b.Property<string>("MahalleAdi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IlceId");

                    b.ToTable("Mahalleler");
                });

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.Tasinmaz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Ada")
                        .HasColumnType("text");

                    b.Property<string>("Adres")
                        .HasColumnType("text");

                    b.Property<string>("Koordinat")
                        .HasColumnType("text");

                    b.Property<int>("MahalleId")
                        .HasColumnType("integer");

                    b.Property<string>("Nitelik")
                        .HasColumnType("text");

                    b.Property<string>("Parsel")
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MahalleId");

                    b.HasIndex("userId");

                    b.ToTable("Tasinmazlar");
                });

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("role")
                        .HasColumnType("text");

                    b.Property<string>("userEmail")
                        .HasColumnType("text");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.Ilce", b =>
                {
                    b.HasOne("TasinmazProject.Entities.Concrete.Il", "Il")
                        .WithMany()
                        .HasForeignKey("IlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.Mahalle", b =>
                {
                    b.HasOne("TasinmazProject.Entities.Concrete.Ilce", "Ilce")
                        .WithMany()
                        .HasForeignKey("IlceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TasinmazProject.Entities.Concrete.Tasinmaz", b =>
                {
                    b.HasOne("TasinmazProject.Entities.Concrete.Mahalle", "Mahalle")
                        .WithMany()
                        .HasForeignKey("MahalleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TasinmazProject.Entities.Concrete.User", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
