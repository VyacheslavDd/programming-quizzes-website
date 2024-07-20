using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgQuizWebsite.Migrations
{
    /// <inheritdoc />
    public partial class usernotificationsbooleanfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReceiveNotifications",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveNotifications",
                table: "Users");
        }
    }
}
