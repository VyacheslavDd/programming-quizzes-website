using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgQuizWebsite.Migrations
{
    /// <inheritdoc />
    public partial class userclassdecomposition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "UserInfo_Surname");

            migrationBuilder.RenameColumn(
                name: "ReceiveNotifications",
                table: "Users",
                newName: "UserNotificationsInfo_ReceiveNotifications");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "UserInfo_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "NewNotificationsCount",
                table: "Users",
                newName: "UserNotificationsInfo_NewNotificationsCount");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "UserInfo_Name");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "UserInfo_Login");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "UserInfo_Email");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Users",
                newName: "UserInfo_BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserNotificationsInfo_ReceiveNotifications",
                table: "Users",
                newName: "ReceiveNotifications");

            migrationBuilder.RenameColumn(
                name: "UserNotificationsInfo_NewNotificationsCount",
                table: "Users",
                newName: "NewNotificationsCount");

            migrationBuilder.RenameColumn(
                name: "UserInfo_Surname",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "UserInfo_PhoneNumber",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "UserInfo_Name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserInfo_Login",
                table: "Users",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "UserInfo_Email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "UserInfo_BirthDate",
                table: "Users",
                newName: "BirthDate");
        }
    }
}
