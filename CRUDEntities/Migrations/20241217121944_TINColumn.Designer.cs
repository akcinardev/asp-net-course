﻿// <auto-generated />
using System;
using CRUDEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRUDEntities.Migrations
{
    [DbContext(typeof(PersonsDbContext))]
    [Migration("20241217121944_TINColumn")]
    partial class TINColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRUDEntities.Country", b =>
                {
                    b.Property<Guid>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            CountryID = new Guid("d7374a6e-f929-4cfd-b42e-f6a43aff61b2"),
                            CountryName = "Turkey"
                        },
                        new
                        {
                            CountryID = new Guid("16bb418f-1e1d-4e99-8e3a-0ed53ba8c133"),
                            CountryName = "Germany"
                        },
                        new
                        {
                            CountryID = new Guid("8f466413-5b58-49c7-ac7e-952c9a1bb947"),
                            CountryName = "Netherlands"
                        },
                        new
                        {
                            CountryID = new Guid("15976c2a-c6db-4612-9b85-81a274560cb7"),
                            CountryName = "Belgium"
                        },
                        new
                        {
                            CountryID = new Guid("2eab411e-72c0-4efc-93d9-7d44bc45c16d"),
                            CountryName = "France"
                        });
                });

            modelBuilder.Entity("CRUDEntities.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Gender")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("PersonName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("ReceiveNewsLetters")
                        .HasColumnType("bit");

                    b.Property<string>("TIN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.ToTable("Persons", (string)null);

                    b.HasData(
                        new
                        {
                            PersonID = new Guid("a29d1f74-1dff-4907-ae13-3a95c2f8a4b1"),
                            Address = "123 Elm Street, Istanbul",
                            CountryID = new Guid("d7374a6e-f929-4cfd-b42e-f6a43aff61b2"),
                            DateOfBirth = new DateTime(1985, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "johndoe@example.com",
                            Gender = "Male",
                            PersonName = "John Doe",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("e7b8c3c2-5fae-462d-8d19-36cdba7f6474"),
                            Address = "45 Oak Street, Berlin",
                            CountryID = new Guid("16bb418f-1e1d-4e99-8e3a-0ed53ba8c133"),
                            DateOfBirth = new DateTime(1992, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "janesmith@example.com",
                            Gender = "Female",
                            PersonName = "Jane Smith",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("9e17e962-b74e-4621-9b2e-15d2312a5b6f"),
                            Address = "12 Pine Avenue, Amsterdam",
                            CountryID = new Guid("8f466413-5b58-49c7-ac7e-952c9a1bb947"),
                            DateOfBirth = new DateTime(1980, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "michaelbrown@example.com",
                            Gender = "Male",
                            PersonName = "Michael Brown",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("f4c1a6b2-91b4-45f2-b97d-a7ee4f2c4471"),
                            Address = "56 Maple Lane, Brussels",
                            CountryID = new Guid("15976c2a-c6db-4612-9b85-81a274560cb7"),
                            DateOfBirth = new DateTime(1995, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "emilydavis@example.com",
                            Gender = "Female",
                            PersonName = "Emily Davis",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("34a719b9-207d-42f9-9b5d-8f3a8b6da2d6"),
                            Address = "789 Birch Road, Paris",
                            CountryID = new Guid("2eab411e-72c0-4efc-93d9-7d44bc45c16d"),
                            DateOfBirth = new DateTime(1978, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "davidwilson@example.com",
                            Gender = "Male",
                            PersonName = "David Wilson",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("7b3e1942-ccad-4a3c-b9a4-d412b2a54d1e"),
                            Address = "101 Walnut Drive, Hamburg",
                            CountryID = new Guid("16bb418f-1e1d-4e99-8e3a-0ed53ba8c133"),
                            DateOfBirth = new DateTime(1987, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "sophiamartinez@example.com",
                            Gender = "Female",
                            PersonName = "Sophia Martinez",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("3a4ed762-5f1b-4c5a-af49-1e282b892c5e"),
                            Address = "21 Cypress Court, Rotterdam",
                            CountryID = new Guid("8f466413-5b58-49c7-ac7e-952c9a1bb947"),
                            DateOfBirth = new DateTime(1990, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "christay@example.com",
                            Gender = "Male",
                            PersonName = "Christopher Taylor",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("ec7f23a1-ff9d-4e98-8662-3b9e321adf46"),
                            Address = "67 Chestnut Boulevard, Ankara",
                            CountryID = new Guid("d7374a6e-f929-4cfd-b42e-f6a43aff61b2"),
                            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "oliviajohnson@example.com",
                            Gender = "Female",
                            PersonName = "Olivia Johnson",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("15a217de-9dc9-47bc-9206-7bb639abead5"),
                            Address = "10 Beechwood Circle, Ghent",
                            CountryID = new Guid("15976c2a-c6db-4612-9b85-81a274560cb7"),
                            DateOfBirth = new DateTime(1982, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jamesanderson@example.com",
                            Gender = "Male",
                            PersonName = "James Anderson",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("dd8a74c9-f7a1-4f48-9e38-479b27b21e5d"),
                            Address = "37 Spruce Avenue, Lyon",
                            CountryID = new Guid("2eab411e-72c0-4efc-93d9-7d44bc45c16d"),
                            DateOfBirth = new DateTime(1993, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "avathompson@example.com",
                            Gender = "Female",
                            PersonName = "Ava Thompson",
                            ReceiveNewsLetters = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
