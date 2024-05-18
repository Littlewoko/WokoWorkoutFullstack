using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout_API.Migrations
{
    /// <inheritdoc />
    public partial class simplifyFKConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId_UserEmail",
                table: "Workouts");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId_UserEmail",
                table: "Workouts",
                columns: new[] { "UserId", "UserEmail" },
                principalTable: "Users",
                principalColumns: new[] { "Id", "Email" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId_UserEmail",
                table: "Workouts");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId_UserEmail",
                table: "Workouts",
                columns: new[] { "UserId", "UserEmail" },
                principalTable: "Users",
                principalColumns: new[] { "Id", "Email" });
        }
    }
}
