using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class ResendSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("04f7b129-765e-4ae7-9436-6210e90172f8"), "Easy" },
                    { new Guid("1ebec00c-25ed-430c-b731-f829a03daffc"), "Hard" },
                    { new Guid("240c310f-14bf-4c80-b43e-58e246fb5054"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0c70d4ea-ba81-4fd7-a930-b5b625acc932"), "KENT", "Kent Region", "https://imgur.com/a/bVZTnjj" },
                    { new Guid("682bb6ab-3da6-4224-bf30-1b54d34920cf"), "RAVN", "Ravenna Region", "https://imgur.com/a/gCRsu1E" },
                    { new Guid("d8cacfcd-59a7-48bd-9bfa-6cfb917718f5"), "AKRO", "Akron Region", "https://imgur.com/a/cMTL2Xo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("04f7b129-765e-4ae7-9436-6210e90172f8"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1ebec00c-25ed-430c-b731-f829a03daffc"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("240c310f-14bf-4c80-b43e-58e246fb5054"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0c70d4ea-ba81-4fd7-a930-b5b625acc932"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("682bb6ab-3da6-4224-bf30-1b54d34920cf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d8cacfcd-59a7-48bd-9bfa-6cfb917718f5"));
        }
    }
}
