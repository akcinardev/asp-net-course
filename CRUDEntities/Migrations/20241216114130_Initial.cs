using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRUDEntities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { new Guid("15976c2a-c6db-4612-9b85-81a274560cb7"), "Belgium" },
                    { new Guid("16bb418f-1e1d-4e99-8e3a-0ed53ba8c133"), "Germany" },
                    { new Guid("2eab411e-72c0-4efc-93d9-7d44bc45c16d"), "France" },
                    { new Guid("8f466413-5b58-49c7-ac7e-952c9a1bb947"), "Netherlands" },
                    { new Guid("d7374a6e-f929-4cfd-b42e-f6a43aff61b2"), "Turkey" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonID", "Address", "CountryID", "DateOfBirth", "Email", "Gender", "PersonName", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("15a217de-9dc9-47bc-9206-7bb639abead5"), "10 Beechwood Circle, Ghent", new Guid("15976c2a-c6db-4612-9b85-81a274560cb7"), new DateTime(1982, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jamesanderson@example.com", "Male", "James Anderson", false },
                    { new Guid("34a719b9-207d-42f9-9b5d-8f3a8b6da2d6"), "789 Birch Road, Paris", new Guid("2eab411e-72c0-4efc-93d9-7d44bc45c16d"), new DateTime(1978, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "davidwilson@example.com", "Male", "David Wilson", true },
                    { new Guid("3a4ed762-5f1b-4c5a-af49-1e282b892c5e"), "21 Cypress Court, Rotterdam", new Guid("8f466413-5b58-49c7-ac7e-952c9a1bb947"), new DateTime(1990, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "christay@example.com", "Male", "Christopher Taylor", true },
                    { new Guid("7b3e1942-ccad-4a3c-b9a4-d412b2a54d1e"), "101 Walnut Drive, Hamburg", new Guid("16bb418f-1e1d-4e99-8e3a-0ed53ba8c133"), new DateTime(1987, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "sophiamartinez@example.com", "Female", "Sophia Martinez", false },
                    { new Guid("9e17e962-b74e-4621-9b2e-15d2312a5b6f"), "12 Pine Avenue, Amsterdam", new Guid("8f466413-5b58-49c7-ac7e-952c9a1bb947"), new DateTime(1980, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "michaelbrown@example.com", "Male", "Michael Brown", true },
                    { new Guid("a29d1f74-1dff-4907-ae13-3a95c2f8a4b1"), "123 Elm Street, Istanbul", new Guid("d7374a6e-f929-4cfd-b42e-f6a43aff61b2"), new DateTime(1985, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", "Male", "John Doe", true },
                    { new Guid("dd8a74c9-f7a1-4f48-9e38-479b27b21e5d"), "37 Spruce Avenue, Lyon", new Guid("2eab411e-72c0-4efc-93d9-7d44bc45c16d"), new DateTime(1993, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "avathompson@example.com", "Female", "Ava Thompson", true },
                    { new Guid("e7b8c3c2-5fae-462d-8d19-36cdba7f6474"), "45 Oak Street, Berlin", new Guid("16bb418f-1e1d-4e99-8e3a-0ed53ba8c133"), new DateTime(1992, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "janesmith@example.com", "Female", "Jane Smith", false },
                    { new Guid("ec7f23a1-ff9d-4e98-8662-3b9e321adf46"), "67 Chestnut Boulevard, Ankara", new Guid("d7374a6e-f929-4cfd-b42e-f6a43aff61b2"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "oliviajohnson@example.com", "Female", "Olivia Johnson", true },
                    { new Guid("f4c1a6b2-91b4-45f2-b97d-a7ee4f2c4471"), "56 Maple Lane, Brussels", new Guid("15976c2a-c6db-4612-9b85-81a274560cb7"), new DateTime(1995, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "emilydavis@example.com", "Female", "Emily Davis", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
