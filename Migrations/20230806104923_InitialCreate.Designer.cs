﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiniProject_UserManagement.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230806104923_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiniProject_UserManagement.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Minors"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Legues"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Pros"
                        });
                });

            modelBuilder.Entity("MiniProject_UserManagement.Models.GroupPermission", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupPermission");
                });

            modelBuilder.Entity("MiniProject_UserManagement.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Level 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Level 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Level 3"
                        });
                });

            modelBuilder.Entity("MiniProject_UserManagement.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GroupId = 1,
                            Name = "James"
                        },
                        new
                        {
                            Id = 2,
                            GroupId = 1,
                            Name = "Susan"
                        },
                        new
                        {
                            Id = 3,
                            GroupId = 2,
                            Name = "Jan"
                        },
                        new
                        {
                            Id = 4,
                            GroupId = 2,
                            Name = "uston"
                        },
                        new
                        {
                            Id = 5,
                            GroupId = 3,
                            Name = "Sarel"
                        },
                        new
                        {
                            Id = 6,
                            GroupId = 3,
                            Name = "Franciska"
                        });
                });

            modelBuilder.Entity("MiniProject_UserManagement.Models.GroupPermission", b =>
                {
                    b.HasOne("MiniProject_UserManagement.Models.Group", "Group")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProject_UserManagement.Models.Permission", "Permission")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("MiniProject_UserManagement.Models.User", b =>
                {
                    b.HasOne("MiniProject_UserManagement.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("MiniProject_UserManagement.Models.Group", b =>
                {
                    b.Navigation("GroupPermissions");
                });

            modelBuilder.Entity("MiniProject_UserManagement.Models.Permission", b =>
                {
                    b.Navigation("GroupPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
