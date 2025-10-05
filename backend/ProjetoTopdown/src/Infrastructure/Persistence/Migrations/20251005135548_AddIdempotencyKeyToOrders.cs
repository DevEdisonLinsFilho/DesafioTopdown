using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTopdown.Infrastructure.Persistence.Migrations
{
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable MEN005 // File is too long
#pragma warning disable MEN003 // Method is too long
#pragma warning disable CA1062 // Validar argumentos de métodos públicos
#pragma warning disable IDE0300 // Simplificar a inicialização de coleção
#pragma warning disable CA1861 // Evite matrizes constantes como argumentos
#pragma warning disable MEN010 // Avoid magic numbers
#pragma warning disable MEN002 // Line is too long
    /// <inheritdoc />
    public partial class AddIdempotencyKeyToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "orders",
                newName: "IX_orders_CustomerId");

            migrationBuilder.AddColumn<Guid>(
                name: "IdempotencyKey",
                table: "orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_IdempotencyKey",
                table: "orders",
                column: "IdempotencyKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_CustomerId",
                table: "orders",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_CustomerId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_IdempotencyKey",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "IdempotencyKey",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
