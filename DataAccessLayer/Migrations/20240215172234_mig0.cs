using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudentInformations_StudentLoginCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UpVotes_Entries_EntryId",
                table: "UpVotes");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudentInformations_StudentLoginCode",
                table: "AspNetUsers",
                column: "StudentLoginCode",
                principalTable: "StudentInformations",
                principalColumn: "LoginCode");

            migrationBuilder.AddForeignKey(
                name: "FK_UpVotes_Entries_EntryId",
                table: "UpVotes",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudentInformations_StudentLoginCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UpVotes_Entries_EntryId",
                table: "UpVotes");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudentInformations_StudentLoginCode",
                table: "AspNetUsers",
                column: "StudentLoginCode",
                principalTable: "StudentInformations",
                principalColumn: "LoginCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UpVotes_Entries_EntryId",
                table: "UpVotes",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
