﻿// <auto-generated />
using System;
using Lokaverkefni.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lokaverkefni.Migrations
{
    [DbContext(typeof(TwitterDbContext))]
    partial class TwitterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lokaverkefni.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Shares")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Text")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            DateCreated = new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5641),
                            Image = "https://localhost:7060/images/Ghost.png",
                            Likes = 0,
                            Shares = 0,
                            Text = "Taurus",
                            UserId = 1
                        },
                        new
                        {
                            PostId = 2,
                            DateCreated = new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5643),
                            Image = "https://localhost:7060/images/Default.jpg",
                            Likes = 0,
                            Shares = 0,
                            Text = "Why Is this the default Profile Picture??",
                            UserId = 3
                        },
                        new
                        {
                            PostId = 3,
                            DateCreated = new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5644),
                            Likes = 0,
                            Shares = 0,
                            Text = "I don't like posting Images",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Lokaverkefni.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePicture")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://localhost:7060/images/Default.jpg");

                    b.HasKey("UserId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            DateCreated = new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5068),
                            Email = "The@Email.com",
                            Name = "Taurus",
                            Password = "Realpassword",
                            PhoneNumber = 11111,
                            ProfilePicture = "https://localhost:7060/images/Ghost.png"
                        },
                        new
                        {
                            UserId = 2,
                            DateCreated = new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5081),
                            Email = "The@TrueEmail.com",
                            Name = "JimmyJames",
                            Password = "Notpassword",
                            PhoneNumber = 22222
                        },
                        new
                        {
                            UserId = 3,
                            DateCreated = new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5082),
                            Email = "The@NotEmail.com",
                            Name = "GimmyGames",
                            Password = "Password",
                            PhoneNumber = 33333
                        });
                });

            modelBuilder.Entity("UserFollows", b =>
                {
                    b.Property<int>("FollowerId")
                        .HasColumnType("int");

                    b.Property<int>("FollowingId")
                        .HasColumnType("int");

                    b.HasKey("FollowerId", "FollowingId");

                    b.HasIndex("FollowingId");

                    b.ToTable("UserFollows");

                    b.HasData(
                        new
                        {
                            FollowerId = 1,
                            FollowingId = 2
                        },
                        new
                        {
                            FollowerId = 1,
                            FollowingId = 3
                        },
                        new
                        {
                            FollowerId = 3,
                            FollowingId = 1
                        });
                });

            modelBuilder.Entity("Lokaverkefni.Models.Post", b =>
                {
                    b.HasOne("Lokaverkefni.Models.User", "User")
                        .WithMany("Post")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserFollows", b =>
                {
                    b.HasOne("Lokaverkefni.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Lokaverkefni.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Lokaverkefni.Models.User", b =>
                {
                    b.Navigation("Post");
                });
#pragma warning restore 612, 618
        }
    }
}