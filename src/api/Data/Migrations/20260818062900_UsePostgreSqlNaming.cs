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
                name: "PK_CandidateProfiles",
                table: "CandidateProfiles");

            migrationBuilder.RenameTable(
                name: "CandidateProfiles",
                newName: "candidate_profiles");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "candidate_profiles",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "candidate_profiles",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Occupation",
                table: "candidate_profiles",
                newName: "occupation");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "candidate_profiles",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "candidate_profiles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProfileBase64",
                table: "candidate_profiles",
                newName: "profile_base64");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "candidate_profiles",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "candidate_profiles",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "candidate_profiles",
                newName: "created_at_utc");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "candidate_profiles",
                newName: "birth_date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_candidate_profiles",
                table: "candidate_profiles",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_candidate_profiles",
                table: "candidate_profiles");

            migrationBuilder.RenameTable(
                name: "candidate_profiles",
                newName: "CandidateProfiles");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "CandidateProfiles",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "CandidateProfiles",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "occupation",
                table: "CandidateProfiles",
                newName: "Occupation");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "CandidateProfiles",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CandidateProfiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "profile_base64",
                table: "CandidateProfiles",
                newName: "ProfileBase64");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "CandidateProfiles",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "CandidateProfiles",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at_utc",
                table: "CandidateProfiles",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "CandidateProfiles",
                newName: "BirthDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateProfiles",
                table: "CandidateProfiles",
                column: "Id");
        }
    }
}
