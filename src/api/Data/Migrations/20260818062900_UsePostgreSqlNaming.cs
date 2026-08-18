using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.InterviewQuestion004.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsePostgreSqlNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "profiles");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "profiles",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "profiles",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Occupation",
                table: "profiles",
                newName: "occupation");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "profiles",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "profiles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProfileBase64",
                table: "profiles",
                newName: "profile_base64");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "profiles",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "profiles",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "profiles",
                newName: "created_at_utc");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "profiles",
                newName: "birth_date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_profiles",
                table: "profiles",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_profiles",
                table: "profiles");

            migrationBuilder.RenameTable(
                name: "profiles",
                newName: "Profiles");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "Profiles",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Profiles",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "occupation",
                table: "Profiles",
                newName: "Occupation");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Profiles",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Profiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "profile_base64",
                table: "Profiles",
                newName: "ProfileBase64");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Profiles",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Profiles",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at_utc",
                table: "Profiles",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Profiles",
                newName: "BirthDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");
        }
    }
}
