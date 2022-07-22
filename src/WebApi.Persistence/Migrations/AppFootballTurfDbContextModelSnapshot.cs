﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApi.Persistence;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    [DbContext(typeof(AppFootballTurfDbContext))]
    partial class AppFootballTurfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ScheduleIndex")
                        .HasColumnType("integer");

                    b.Property<Guid>("TurfId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TurfId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Turf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Latitude")
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Turfs");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.TurfImage", b =>
                {
                    b.Property<Guid>("TurfId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("TurfId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("TurfImages");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Schedule", b =>
                {
                    b.HasOne("WebApi.Domain.Entities.Turf", "Turf")
                        .WithMany("Schedules")
                        .HasForeignKey("TurfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turf");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Turf", b =>
                {
                    b.HasOne("WebApi.Domain.Entities.User", "Owner")
                        .WithMany("Turfs")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.TurfImage", b =>
                {
                    b.HasOne("WebApi.Domain.Entities.Image", "Image")
                        .WithMany("TurfImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Domain.Entities.Turf", "Turf")
                        .WithMany("TurfImages")
                        .HasForeignKey("TurfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Turf");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Image", b =>
                {
                    b.Navigation("TurfImages");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Turf", b =>
                {
                    b.Navigation("Schedules");

                    b.Navigation("TurfImages");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.User", b =>
                {
                    b.Navigation("Turfs");
                });
#pragma warning restore 612, 618
        }
    }
}
