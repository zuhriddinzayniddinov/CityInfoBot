using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationInformation",
                columns: table => new
                {
                    LocationName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationInformation", x => x.LocationName);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.TelegramId);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "TelegramId", "Name", "Role" },
                values: new object[,]
                {
                    { 501130550L, "ZUHRIDDIN", 1 },
                    { 1009772481L, "ABDUXAMIDOV", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationInformation");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
