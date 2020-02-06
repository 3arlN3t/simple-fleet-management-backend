using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetManagementAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chasseez",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Series = table.Column<string>(maxLength: 255, nullable: false),
                    Number = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chasseez", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChassisId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    NumberOfPassengers = table.Column<byte>(nullable: false),
                    Color = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Chasseez_ChassisId",
                        column: x => x.ChassisId,
                        principalTable: "Chasseez",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ChassisId",
                table: "Vehicles",
                column: "ChassisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Chasseez");
        }
    }
}
