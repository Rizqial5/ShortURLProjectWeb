using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortUrl.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAccesCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessCount",
                table: "ShortenDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessCount",
                table: "ShortenDatas");
        }
    }
}
