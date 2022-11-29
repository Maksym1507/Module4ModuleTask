using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module4ModuleTask.Migrations
{
    public partial class AutoGenerateValueForPropertyNumberInTableOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
