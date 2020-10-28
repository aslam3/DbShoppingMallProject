using Microsoft.EntityFrameworkCore.Migrations;

namespace DbShoppingMallProject.Data.Migrations
{
    public partial class m5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StallManagers_Stalls_StallId",
                table: "StallManagers");

            migrationBuilder.AlterColumn<int>(
                name: "StallId",
                table: "StallManagers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StallManagers_Stalls_StallId",
                table: "StallManagers",
                column: "StallId",
                principalTable: "Stalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StallManagers_Stalls_StallId",
                table: "StallManagers");

            migrationBuilder.AlterColumn<int>(
                name: "StallId",
                table: "StallManagers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_StallManagers_Stalls_StallId",
                table: "StallManagers",
                column: "StallId",
                principalTable: "Stalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
