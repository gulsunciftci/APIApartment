using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class startpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "Floor", "No", "Status", "Type" },
                values: new object[] { 1, 2, 3, "Boş", "3+1" });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "Floor", "No", "Status", "Type" },
                values: new object[] { 2, 4, 6, "Dolu", "3+1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");
        }
    }
}
