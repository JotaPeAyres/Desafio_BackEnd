using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArrumandoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_RentalPlans_RentalPlanId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "RentalPlans");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentalPlanId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentalPlanId",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "License",
                table: "DeliveryMen",
                newName: "LicenseNumber");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Rentals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RentalPlan",
                table: "Rentals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCust",
                table: "Rentals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryManId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "DeliveryMen",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "OrderNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryManId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderNotifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryManId",
                table: "Orders",
                column: "DeliveryManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMen_DeliveryManId",
                table: "Orders",
                column: "DeliveryManId",
                principalTable: "DeliveryMen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMen_DeliveryManId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderNotifications");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryManId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentalPlan",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "TotalCust",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "DeliveryManId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DeliveryMen");

            migrationBuilder.RenameColumn(
                name: "LicenseNumber",
                table: "DeliveryMen",
                newName: "License");

            migrationBuilder.AddColumn<Guid>(
                name: "RentalPlanId",
                table: "Rentals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RentalPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cust = table.Column<decimal>(type: "numeric", nullable: false),
                    Days = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Penalty = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPlans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalPlanId",
                table: "Rentals",
                column: "RentalPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_RentalPlans_RentalPlanId",
                table: "Rentals",
                column: "RentalPlanId",
                principalTable: "RentalPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
