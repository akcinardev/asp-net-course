using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDEntities.Migrations
{
    /// <inheritdoc />
    public partial class TINColumn_CHECKTIN_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CHECK_TIN",
                table: "Persons",
                sql: "len([TaxIdentificationNumber]) = 8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHECK_TIN",
                table: "Persons");
        }
    }
}
