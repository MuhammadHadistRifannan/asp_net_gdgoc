using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gdgoc_aspnet.Migrations
{
    /// <inheritdoc />
    public partial class UsersV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "users",
                newName: "password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "passwordHash");
        }
    }
}
