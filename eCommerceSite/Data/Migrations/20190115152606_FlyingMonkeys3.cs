using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceSite.Data.Migrations
{
    public partial class FlyingMonkeys3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "CardTypes",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CardTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CardTypes",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CardTypes",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CardTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CardTypes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CardTypes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CardTypes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CardTypes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CardTypes",
                newName: "name");
        }
    }
}
