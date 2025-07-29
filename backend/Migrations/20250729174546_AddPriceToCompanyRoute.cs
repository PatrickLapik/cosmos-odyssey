using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmosOdyssey.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToCompanyRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "CompanyRoutes",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CompanyRoutes");
        }
    }
}
