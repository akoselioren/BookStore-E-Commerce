using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class Update_Book_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStock",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ReadCount",
                table: "Books",
                newName: "StockCount");

            migrationBuilder.AddColumn<int>(
                name: "SellCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellCount",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "StockCount",
                table: "Books",
                newName: "ReadCount");

            migrationBuilder.AddColumn<bool>(
                name: "IsStock",
                table: "Books",
                type: "bit",
                nullable: true);
        }
    }
}
