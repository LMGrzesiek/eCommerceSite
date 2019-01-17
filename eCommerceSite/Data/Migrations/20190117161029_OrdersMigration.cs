using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceSite.Data.Migrations
{
    public partial class OrdersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Orders",
                newName: "TrackingNumber");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlacementDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ShipDate",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Shipping",
                table: "Orders",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ShippingCity",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingInstructions",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingPostalCode",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingRecipient",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingState",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingStreet1",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingStreet2",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Orders",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "Orders",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CardDescription",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardName",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LineTotal",
                table: "OrderItems",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "OrderItems",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PlacementDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingInstructions",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingPostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingRecipient",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingState",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingStreet1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingStreet2",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardDescription",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CardName",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "LineTotal",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "TrackingNumber",
                table: "Orders",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Orders",
                newName: "OrderId");
        }
    }
}
