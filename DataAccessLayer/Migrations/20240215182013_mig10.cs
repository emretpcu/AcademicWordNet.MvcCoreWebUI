using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudentInformations_StudentLoginCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_AspNetUsers_UserId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_User1Id",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_User2Id",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Headings_AspNetUsers_FromWhoId",
                table: "Headings");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

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
                name: "FK_Entries_AspNetUsers_UserId",
                table: "Entries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_User1Id",
                table: "Friends",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_User2Id",
                table: "Friends",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Headings_AspNetUsers_FromWhoId",
                table: "Headings",
                column: "FromWhoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UpVotes_Entries_EntryId",
                table: "UpVotes",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudentInformations_StudentLoginCode",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_AspNetUsers_UserId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_User1Id",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_User2Id",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Headings_AspNetUsers_FromWhoId",
                table: "Headings");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

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
                name: "FK_Entries_AspNetUsers_UserId",
                table: "Entries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_User1Id",
                table: "Friends",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_User2Id",
                table: "Friends",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Headings_AspNetUsers_FromWhoId",
                table: "Headings",
                column: "FromWhoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UpVotes_Entries_EntryId",
                table: "UpVotes",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id");
        }
    }
}
