using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Example.InterviewQuestion004.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOccupationMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "occupations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occupations", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "occupations",
                columns: new[] { "id", "code", "display_order", "is_active", "name" },
                values: new object[,]
                {
                    { 1, "software-engineer", 10, true, "Software Engineer" },
                    { 2, "business-analyst", 20, true, "Business Analyst" },
                    { 3, "quality-assurance", 30, true, "Quality Assurance" },
                    { 4, "ux-ui-designer", 40, true, "UX/UI Designer" },
                    { 5, "project-manager", 50, true, "Project Manager" }
                });

            migrationBuilder.AddColumn<int>(
                name: "occupation_id",
                table: "candidate_profiles",
                type: "integer",
                nullable: true);

            migrationBuilder.Sql(
                """
                UPDATE candidate_profiles AS profile
                SET occupation_id = occupation.id
                FROM occupations AS occupation
                WHERE occupation.name = profile.occupation;
                """);

            migrationBuilder.AlterColumn<int>(
                name: "occupation_id",
                table: "candidate_profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "occupation",
                table: "candidate_profiles");

            migrationBuilder.CreateIndex(
                name: "IX_candidate_profiles_occupation_id",
                table: "candidate_profiles",
                column: "occupation_id");

            migrationBuilder.CreateIndex(
                name: "IX_occupations_code",
                table: "occupations",
                column: "code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_candidate_profiles_occupations_occupation_id",
                table: "candidate_profiles",
                column: "occupation_id",
                principalTable: "occupations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_candidate_profiles_occupations_occupation_id",
                table: "candidate_profiles");

            migrationBuilder.AddColumn<string>(
                name: "occupation",
                table: "candidate_profiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                """
                UPDATE candidate_profiles AS profile
                SET occupation = occupation.name
                FROM occupations AS occupation
                WHERE occupation.id = profile.occupation_id;
                """);

            migrationBuilder.DropIndex(
                name: "IX_candidate_profiles_occupation_id",
                table: "candidate_profiles");

            migrationBuilder.DropColumn(
                name: "occupation_id",
                table: "candidate_profiles");

            migrationBuilder.DropTable(
                name: "occupations");
        }
    }
}
