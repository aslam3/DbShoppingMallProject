using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbShoppingMallProject.Data.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Designation = table.Column<string>(nullable: true),
                    Basic = table.Column<decimal>(nullable: false),
                    Medical = table.Column<decimal>(nullable: false),
                    HouseRent = table.Column<decimal>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StallManagers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    StallId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StallManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StallManagers_Stalls_StallId",
                        column: x => x.StallId,
                        principalTable: "Stalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DesigId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    TotalSalary = table.Column<decimal>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Salaries_DesigId",
                        column: x => x.DesigId,
                        principalTable: "Salaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesigId",
                table: "Employees",
                column: "DesigId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_ApplicationUserId",
                table: "Salaries",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StallManagers_StallId",
                table: "StallManagers",
                column: "StallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "StallManagers");

            migrationBuilder.DropTable(
                name: "Salaries");
        }
    }
}
