﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eGoatDDD.Persistence;

namespace eGoatDDD.Persistence.Migrations
{
    [DbContext(typeof(eGoatDDDDbContext))]
    partial class eGoatDDDDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("HomeAddress")
                        .HasMaxLength(50);

                    b.Property<string>("HomeCity")
                        .HasMaxLength(50);

                    b.Property<string>("HomeCountryCode")
                        .HasMaxLength(50);

                    b.Property<string>("HomePhone")
                        .HasMaxLength(50);

                    b.Property<string>("HomeRegion")
                        .HasMaxLength(50);

                    b.Property<int>("IsActivated");

                    b.Property<DateTime>("Joined");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Birth", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Alive");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Delivery");

                    b.Property<long>("GoatId");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("Total");

                    b.Property<Guid>("UniqeId");

                    b.HasKey("Id");

                    b.HasIndex("GoatId");

                    b.ToTable("Births");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Picture");

                    b.HasKey("Id");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Disposal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DisposedOn");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Reason");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Disposals");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Goat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("Code");

                    b.Property<int>("ColorId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<long?>("DisposalId");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<DateTime>("Modified");

                    b.Property<Guid>("UniqeId");

                    b.HasKey("Id");

                    b.HasIndex("DisposalId")
                        .IsUnique()
                        .HasFilter("[DisposalId] IS NOT NULL");

                    b.HasIndex("ColorId", "Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("Goats");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.GoatBreed", b =>
                {
                    b.Property<long>("GoatId");

                    b.Property<int>("BreedId");

                    b.Property<float>("Percentage");

                    b.HasKey("GoatId", "BreedId");

                    b.HasIndex("BreedId");

                    b.ToTable("GoatBreeds");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.GoatResource", b =>
                {
                    b.Property<long>("GoatId");

                    b.Property<Guid>("ResourceId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.HasKey("GoatId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("GoatResources");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.GoatService", b =>
                {
                    b.Property<long>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<DateTime>("End");

                    b.Property<long>("GoatId");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Type");

                    b.HasKey("ServiceId");

                    b.HasIndex("GoatId");

                    b.ToTable("GoatServices");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.History", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("date");

                    b.Property<long>("GoatId");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("date");

                    b.Property<string>("Note")
                        .HasColumnType("ntext");

                    b.Property<Guid>("UniqeId");

                    b.HasKey("Id");

                    b.HasIndex("GoatId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Parent", b =>
                {
                    b.Property<long>("GoatId");

                    b.Property<long>("ParentId");

                    b.HasKey("GoatId", "ParentId");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Resource", b =>
                {
                    b.Property<Guid>("ResourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Filename");

                    b.Property<string>("Location");

                    b.HasKey("ResourceId");

                    b.ToTable("Resources");
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
                    b.HasOne("eGoatDDD.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.ApplicationUser", null)
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

                    b.HasOne("eGoatDDD.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Birth", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.Goat", "Goat")
                        .WithMany("Births")
                        .HasForeignKey("GoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Goat", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.Color", "Color")
                        .WithMany("Goats")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eGoatDDD.Domain.Entities.Disposal", "Disposal")
                        .WithOne()
                        .HasForeignKey("eGoatDDD.Domain.Entities.Goat", "DisposalId");
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.GoatBreed", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.Breed", "Breed")
                        .WithMany("GoatBreeds")
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eGoatDDD.Domain.Entities.Goat", "Goat")
                        .WithMany("GoatBreeds")
                        .HasForeignKey("GoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.GoatResource", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.Goat", "Goat")
                        .WithMany("GoatResources")
                        .HasForeignKey("GoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eGoatDDD.Domain.Entities.Resource", "Resource")
                        .WithMany("GoatResources")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.GoatService", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.Goat", "Goat")
                        .WithMany("Services")
                        .HasForeignKey("GoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.History", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.Goat", "Goat")
                        .WithMany()
                        .HasForeignKey("GoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eGoatDDD.Domain.Entities.Parent", b =>
                {
                    b.HasOne("eGoatDDD.Domain.Entities.Goat", "Goat")
                        .WithMany("Parents")
                        .HasForeignKey("GoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
