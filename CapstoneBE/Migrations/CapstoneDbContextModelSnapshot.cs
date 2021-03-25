﻿// <auto-generated />
using System;
using CapstoneBE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CapstoneBE.Migrations
{
    [DbContext(typeof(CapstoneDbContext))]
    partial class CapstoneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CapstoneBE.Models.CapstoneBEUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FcmTokenM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FcmTokenW")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDel")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNewUser")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CapstoneBE.Models.Crack", b =>
                {
                    b.Property<int>("CrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<float>("Accuracy")
                        .HasColumnType("real");

                    b.Property<string>("AssessmentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssessmentResult")
                        .HasColumnType("int");

                    b.Property<string>("CensorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("'https://bcdsysstorage.blob.core.windows.net/crack-images/' + CAST([CrackId] AS VARCHAR) + '.png'");

                    b.Property<string>("ImageThumbnails")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("'https://bcdsysstorage.blob.core.windows.net/thumbnails/' + CAST([CrackId] AS VARCHAR) + '.jpg'");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MaintenanceOrderId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Severity")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("UpdateUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CrackId");

                    b.HasIndex("CensorId");

                    b.HasIndex("FlightId");

                    b.HasIndex("MaintenanceOrderId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("Crack");
                });

            modelBuilder.Entity("CapstoneBE.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DataCollectorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RecordDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlightId");

                    b.HasIndex("DataCollectorId");

                    b.HasIndex("LocationId");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("CapstoneBE.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDel")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("CapstoneBE.Models.LocationHistory", b =>
                {
                    b.Property<int>("LocationHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmpId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("LocationHistoryId");

                    b.HasIndex("EmpId");

                    b.HasIndex("LocationId");

                    b.ToTable("LocationHistory");
                });

            modelBuilder.Entity("CapstoneBE.Models.MaintenanceOrder", b =>
                {
                    b.Property<int>("MaintenanceOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AssessmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AssessmentResult")
                        .HasColumnType("int");

                    b.Property<string>("AssessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreateUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MaintenanceDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("MaintenanceExpense")
                        .HasColumnType("real");

                    b.Property<int?>("MaintenanceWorkerId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("UpdateUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaintenanceOrderId");

                    b.HasIndex("AssessorId");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("LocationId");

                    b.HasIndex("MaintenanceWorkerId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("MaintenanceOrder");
                });

            modelBuilder.Entity("CapstoneBE.Models.MaintenanceWorker", b =>
                {
                    b.Property<int>("MaintenanceWorkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDel")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("MaintenanceWorkerId");

                    b.ToTable("MaintenanceWorker");
                });

            modelBuilder.Entity("CapstoneBE.Models.PushNotification", b =>
                {
                    b.Property<int>("PushNotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PushNotificationId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("PushNotification");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                            ConcurrencyStamp = "68beae30-44c7-428a-aa5a-2df894db7c2e",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "3c5e154e-3b0e-446f-86af-483d54fd7210",
                            ConcurrencyStamp = "9afa4137-011d-4ec7-a0bf-c31175f11f56",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "2c3e174e-3b0e-446f-86af-483d56fd7210",
                            ConcurrencyStamp = "953cf865-c701-4397-a853-340b29a25d7b",
                            Name = "Staff",
                            NormalizedName = "STAFF"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CapstoneBE.Models.Crack", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "Censor")
                        .WithMany("CracksC")
                        .HasForeignKey("CensorId");

                    b.HasOne("CapstoneBE.Models.Flight", "Flight")
                        .WithMany("Cracks")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBE.Models.MaintenanceOrder", "MaintenanceOrder")
                        .WithMany("Cracks")
                        .HasForeignKey("MaintenanceOrderId");

                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "UpdateUser")
                        .WithMany("CracksUU")
                        .HasForeignKey("UpdateUserId");

                    b.Navigation("Censor");

                    b.Navigation("Flight");

                    b.Navigation("MaintenanceOrder");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("CapstoneBE.Models.Flight", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "DataCollector")
                        .WithMany("Flights")
                        .HasForeignKey("DataCollectorId");

                    b.HasOne("CapstoneBE.Models.Location", "Location")
                        .WithMany("Flights")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataCollector");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("CapstoneBE.Models.LocationHistory", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "Employee")
                        .WithMany("LocationHistories")
                        .HasForeignKey("EmpId");

                    b.HasOne("CapstoneBE.Models.Location", "Location")
                        .WithMany("LocationHistories")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("CapstoneBE.Models.MaintenanceOrder", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "Assessor")
                        .WithMany("MaintenanceOrdersA")
                        .HasForeignKey("AssessorId");

                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "CreateUser")
                        .WithMany("MaintenanceOrdersCU")
                        .HasForeignKey("CreateUserId");

                    b.HasOne("CapstoneBE.Models.Location", "Location")
                        .WithMany("MaintenanceOrders")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBE.Models.MaintenanceWorker", "MaintenanceWorker")
                        .WithMany("MaintenanceOrders")
                        .HasForeignKey("MaintenanceWorkerId");

                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "UpdateUser")
                        .WithMany("MaintenanceOrdersUU")
                        .HasForeignKey("UpdateUserId");

                    b.Navigation("Assessor");

                    b.Navigation("CreateUser");

                    b.Navigation("Location");

                    b.Navigation("MaintenanceWorker");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("CapstoneBE.Models.PushNotification", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "Receiver")
                        .WithMany("ReceivedNotifications")
                        .HasForeignKey("ReceiverId");

                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", "Sender")
                        .WithMany("SentNotifications")
                        .HasForeignKey("SenderId");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CapstoneBE.Models.CapstoneBEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CapstoneBE.Models.CapstoneBEUser", b =>
                {
                    b.Navigation("CracksC");

                    b.Navigation("CracksUU");

                    b.Navigation("Flights");

                    b.Navigation("LocationHistories");

                    b.Navigation("MaintenanceOrdersA");

                    b.Navigation("MaintenanceOrdersCU");

                    b.Navigation("MaintenanceOrdersUU");

                    b.Navigation("ReceivedNotifications");

                    b.Navigation("SentNotifications");
                });

            modelBuilder.Entity("CapstoneBE.Models.Flight", b =>
                {
                    b.Navigation("Cracks");
                });

            modelBuilder.Entity("CapstoneBE.Models.Location", b =>
                {
                    b.Navigation("Flights");

                    b.Navigation("LocationHistories");

                    b.Navigation("MaintenanceOrders");
                });

            modelBuilder.Entity("CapstoneBE.Models.MaintenanceOrder", b =>
                {
                    b.Navigation("Cracks");
                });

            modelBuilder.Entity("CapstoneBE.Models.MaintenanceWorker", b =>
                {
                    b.Navigation("MaintenanceOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
