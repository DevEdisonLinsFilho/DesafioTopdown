using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable MEN005 // File is too long
#pragma warning disable MEN003 // Method is too long
#pragma warning disable CA1062 // Validar argumentos de métodos públicos
#pragma warning disable IDE0300 // Simplificar a inicialização de coleção
#pragma warning disable CA1861 // Evite matrizes constantes como argumentos
#pragma warning disable MEN010 // Avoid magic numbers
#pragma warning disable MEN002 // Line is too long
namespace ProjetoTopdown.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductAndCustomerData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "products",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "products",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "customers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "customers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Id", "CreatedAt", "Document", "Email", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "111.111.111-11", "cliente1@email.com", "Cliente de Teste 1" },
                    { 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "222.222.222-22", "cliente2@email.com", "Cliente de Teste 2" },
                    { 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "333.333.333-33", "cliente3@email.com", "Cliente de Teste 3" },
                    { 4, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "444.444.444-44", "cliente4@email.com", "Cliente de Teste 4" },
                    { 5, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "555.555.555-55", "cliente5@email.com", "Cliente de Teste 5" },
                    { 6, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "666.666.666-66", "cliente6@email.com", "Cliente de Teste 6" },
                    { 7, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "777.777.777-77", "cliente7@email.com", "Cliente de Teste 7" },
                    { 8, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "888.888.888-88", "cliente8@email.com", "Cliente de Teste 8" },
                    { 9, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "999.999.999-99", "cliente9@email.com", "Cliente de Teste 9" },
                    { 10, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "101010.101010.101010-1010", "cliente10@email.com", "Cliente de Teste 10" },
                    { 11, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "111111.111111.111111-1111", "cliente11@email.com", "Cliente de Teste 11" },
                    { 12, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "121212.121212.121212-1212", "cliente12@email.com", "Cliente de Teste 12" },
                    { 13, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "131313.131313.131313-1313", "cliente13@email.com", "Cliente de Teste 13" },
                    { 14, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "141414.141414.141414-1414", "cliente14@email.com", "Cliente de Teste 14" },
                    { 15, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "151515.151515.151515-1515", "cliente15@email.com", "Cliente de Teste 15" },
                    { 16, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "161616.161616.161616-1616", "cliente16@email.com", "Cliente de Teste 16" },
                    { 17, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "171717.171717.171717-1717", "cliente17@email.com", "Cliente de Teste 17" },
                    { 18, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "181818.181818.181818-1818", "cliente18@email.com", "Cliente de Teste 18" },
                    { 19, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "191919.191919.191919-1919", "cliente19@email.com", "Cliente de Teste 19" },
                    { 20, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "202020.202020.202020-2020", "cliente20@email.com", "Cliente de Teste 20" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "Price", "Sku", "StockQty" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 1", 30.5m, "SKU-1001", 5 },
                    { 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 2", 41m, "SKU-1002", 10 },
                    { 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 3", 51.5m, "SKU-1003", 15 },
                    { 4, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 4", 62m, "SKU-1004", 20 },
                    { 5, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 5", 72.5m, "SKU-1005", 25 },
                    { 6, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 6", 83m, "SKU-1006", 30 },
                    { 7, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 7", 93.5m, "SKU-1007", 35 },
                    { 8, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 8", 104m, "SKU-1008", 40 },
                    { 9, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 9", 114.5m, "SKU-1009", 45 },
                    { 10, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 10", 125m, "SKU-1010", 50 },
                    { 11, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 11", 135.5m, "SKU-1011", 55 },
                    { 12, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 12", 146m, "SKU-1012", 60 },
                    { 13, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 13", 156.5m, "SKU-1013", 65 },
                    { 14, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 14", 167m, "SKU-1014", 70 },
                    { 15, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 15", 177.5m, "SKU-1015", 75 },
                    { 16, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 16", 188m, "SKU-1016", 80 },
                    { 17, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 17", 198.5m, "SKU-1017", 85 },
                    { 18, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 18", 209m, "SKU-1018", 90 },
                    { 19, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 19", 219.5m, "SKU-1019", 95 },
                    { 20, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 20", 230m, "SKU-1020", 100 },
                    { 21, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 21", 240.5m, "SKU-1021", 105 },
                    { 22, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 22", 251m, "SKU-1022", 110 },
                    { 23, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 23", 261.5m, "SKU-1023", 115 },
                    { 24, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 24", 272m, "SKU-1024", 120 },
                    { 25, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 25", 282.5m, "SKU-1025", 125 },
                    { 26, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 26", 293m, "SKU-1026", 130 },
                    { 27, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 27", 303.5m, "SKU-1027", 135 },
                    { 28, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 28", 314m, "SKU-1028", 140 },
                    { 29, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 29", 324.5m, "SKU-1029", 145 },
                    { 30, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 30", 335m, "SKU-1030", 150 },
                    { 31, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 31", 345.5m, "SKU-1031", 155 },
                    { 32, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 32", 356m, "SKU-1032", 160 },
                    { 33, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 33", 366.5m, "SKU-1033", 165 },
                    { 34, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 34", 377m, "SKU-1034", 170 },
                    { 35, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 35", 387.5m, "SKU-1035", 175 },
                    { 36, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 36", 398m, "SKU-1036", 180 },
                    { 37, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 37", 408.5m, "SKU-1037", 185 },
                    { 38, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 38", 419m, "SKU-1038", 190 },
                    { 39, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 39", 429.5m, "SKU-1039", 195 },
                    { 40, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 40", 440m, "SKU-1040", 200 },
                    { 41, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 41", 450.5m, "SKU-1041", 205 },
                    { 42, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 42", 461m, "SKU-1042", 210 },
                    { 43, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 43", 471.5m, "SKU-1043", 215 },
                    { 44, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 44", 482m, "SKU-1044", 220 },
                    { 45, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 45", 492.5m, "SKU-1045", 225 },
                    { 46, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 46", 503m, "SKU-1046", 230 },
                    { 47, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 47", 513.5m, "SKU-1047", 235 },
                    { 48, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 48", 524m, "SKU-1048", 240 },
                    { 49, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 49", 534.5m, "SKU-1049", 245 },
                    { 50, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Produto de Teste 50", 545m, "SKU-1050", 250 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_Sku",
                table: "products",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_Email",
                table: "customers",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "products",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_Sku",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_Email",
                table: "customers");

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
