using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobHubProject2.Migrations
{
    /// <inheritdoc />
    public partial class SkillsPropertyAddedToJobModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "JobTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Skills",
                table: "JobTable");
        }
    }
}
