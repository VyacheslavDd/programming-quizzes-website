using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgQuizWebsite.Migrations
{
    /// <inheritdoc />
    public partial class countnewusernotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewNotificationsCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewNotificationsCount",
                table: "Users");
        }
    }
}
