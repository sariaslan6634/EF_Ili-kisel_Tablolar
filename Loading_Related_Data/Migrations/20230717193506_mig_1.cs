﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Loading_Related_Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ankara" },
                    { 2, "Yozgat" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Name", "RegionId", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, "Gençay", 1, 1500, "Yıldız" },
                    { 2, "Mahmut", 2, 1500, "Salar" },
                    { 3, "Tuncay", 1, 1500, "Kahraman" },
                    { 4, "Orhan", 2, 1500, "Cakmak" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "EmployeeId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9010) },
                    { 2, 1, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9018) },
                    { 3, 2, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9019) },
                    { 4, 2, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9020) },
                    { 5, 3, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9021) },
                    { 6, 3, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9022) },
                    { 7, 3, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9023) },
                    { 8, 4, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9024) },
                    { 9, 4, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9025) },
                    { 10, 1, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9026) },
                    { 11, 2, new DateTime(2023, 7, 17, 22, 35, 6, 78, DateTimeKind.Local).AddTicks(9026) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RegionId",
                table: "Employees",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
