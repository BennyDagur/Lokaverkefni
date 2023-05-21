using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lokaverkefni.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "https://localhost:7060/images/Default.jpg"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shares = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Likes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFollows",
                columns: table => new
                {
                    FollowerId = table.Column<int>(type: "int", nullable: false),
                    FollowingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollows", x => new { x.FollowerId, x.FollowingId });
                    table.ForeignKey(
                        name: "FK_UserFollows_User_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFollows_User_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "DateCreated", "Email", "Name", "Password", "PhoneNumber", "ProfilePicture" },
                values: new object[] { 1, new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5068), "The@Email.com", "Taurus", "Realpassword", 11111, "https://localhost:7060/images/Ghost.png" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "DateCreated", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 2, new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5081), "The@TrueEmail.com", "JimmyJames", "Notpassword", 22222 },
                    { 3, new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5082), "The@NotEmail.com", "GimmyGames", "Password", 33333 }
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "DateCreated", "Image", "Text", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5641), "https://localhost:7060/images/Ghost.png", "Taurus", 1 },
                    { 2, new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5643), "https://localhost:7060/images/Default.jpg", "Why Is this the default Profile Picture??", 3 },
                    { 3, new DateTime(2023, 5, 18, 13, 1, 46, 348, DateTimeKind.Local).AddTicks(5644), null, "I don't like posting Images", 2 }
                });

            migrationBuilder.InsertData(
                table: "UserFollows",
                columns: new[] { "FollowerId", "FollowingId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_FollowingId",
                table: "UserFollows",
                column: "FollowingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "UserFollows");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
