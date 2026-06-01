using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTeams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCleanSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Groups_GroupEntityId",
                schema: "teams",
                table: "Members");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "teams");

            migrationBuilder.DropIndex(
                name: "IX_Members_GroupEntityId",
                schema: "teams",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "GroupEntityId",
                schema: "teams",
                table: "Members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupEntityId",
                schema: "teams",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "teams",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_GroupEntityId",
                schema: "teams",
                table: "Members",
                column: "GroupEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Groups_GroupEntityId",
                schema: "teams",
                table: "Members",
                column: "GroupEntityId",
                principalSchema: "teams",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
