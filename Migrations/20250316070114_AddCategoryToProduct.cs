using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GourmetShopMVCApp.Migrations
{
    public partial class AddCategoryToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Categories Table
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            // Modify Product Table to include CategoryId
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: true);

            // Add foreign key to reference Categories
            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product");

            // Remove the CategoryId column from Product table
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            // Drop the Categories table
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
