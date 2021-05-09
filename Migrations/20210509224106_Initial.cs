using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QA.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reputation = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Votes = table.Column<int>(type: "int", nullable: false),
                    Answers = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Votes = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    AnswerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d2eac9d3-6141-476c-9020-f42250d63e80", "b952fe20-b530-4847-ae39-a1f454ec5013", "Admin", null },
                    { "d2eac9d3-6141-476c-9020-f42250d63e81", "16d7b153-2b68-4337-b6a9-639120aea0fb", "Guest", null },
                    { "d2eac9d3-6141-476c-9020-f42250d63e82", "f26a0020-b7f8-4656-95ec-5e0b9aa95f87", "Moderator", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Reputation", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "d2eac9d3-6141-476c-9020-f42250d63e86", 0, "3b3e029c-9d49-4d25-968b-9c3a913d903c", "ff@qq.com", true, true, null, "FF@QQ.COM", "FF@QQ.COM", "AQAAAAEAACcQAAAAEMQJrTVkVJiBSAvpCuUKqC3g7BQyCNs1igCObs9zKmTySe8b1gNJ1iFMpviFa++k2w==", null, false, 0, "N4Q2NOPGJ5DYCTUU67NCDN6EELEJFO4N", false, "ff@qq.com" },
                    { "d985a7b1-d58b-4266-ab86-a0a0ff91ccc1", 0, "714d2b03-873f-479b-9249-4e7ef30866f9", "cc861010@gmail.com", true, true, null, "CC861010@GMAIL.COM", "CC861010@GMAIL.COM", "AQAAAAEAACcQAAAAEIyzWsOem/kp/V7dgVxg6cmAvcFfC+8zAc0bLT6iOLsm7qMiONewX13Nm3kihjYVvQ==", null, false, 0, "USIVDWV4UHHSCT6ZTTJDMSRXHVVU4P2D", false, "cc861010@gmail.com" },
                    { "f984066c-816d-4531-bd9a-c63256ca7000", 0, "dd5e1fdf-03ea-47f2-84c7-2873d0ae85b9", "cc@qq.com", true, true, null, "CC@QQ.COM", "CC@QQ.COM", "AQAAAAEAACcQAAAAEL3A1emCK92O5spftjyhQkRYl0VhfVQQGmkmn1QM68AWMzDNoOLXsfXgOmNcpaWprw==", null, false, 0, "BNDKY6ICQC3YUUQL5OTFI2277AJY7LGJ", false, "cc@qq.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -1, "abc", "1", "d2eac9d3-6141-476c-9020-f42250d63e80" },
                    { -2, "b", "1", "d2eac9d3-6141-476c-9020-f42250d63e81" },
                    { -3, "c", "1", "d2eac9d3-6141-476c-9020-f42250d63e82" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d2eac9d3-6141-476c-9020-f42250d63e80", "d2eac9d3-6141-476c-9020-f42250d63e86" },
                    { "d2eac9d3-6141-476c-9020-f42250d63e81", "d985a7b1-d58b-4266-ab86-a0a0ff91ccc1" },
                    { "d2eac9d3-6141-476c-9020-f42250d63e82", "f984066c-816d-4531-bd9a-c63256ca7000" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answers", "CreateDateTime", "Description", "Title", "UserId", "Votes" },
                values: new object[,]
                {
                    { -1, 1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", "Question title -1 ", "f984066c-816d-4531-bd9a-c63256ca7000", 12 },
                    { -2, 1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", "Question title -2", "f984066c-816d-4531-bd9a-c63256ca7000", 12 },
                    { -3, 1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", "Question title -3", "f984066c-816d-4531-bd9a-c63256ca7000", 12 },
                    { -4, 1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", "Question title -4", "f984066c-816d-4531-bd9a-c63256ca7000", 12 },
                    { -5, 1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", "Question title -5", "f984066c-816d-4531-bd9a-c63256ca7000", 12 },
                    { -6, 1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", "Question title -6", "f984066c-816d-4531-bd9a-c63256ca7000", 0 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "CreateDateTime", "Description", "QuestionId", "UserId", "Votes" },
                values: new object[,]
                {
                    { -1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -1, "f984066c-816d-4531-bd9a-c63256ca7000", 12 },
                    { -6, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -5, "f984066c-816d-4531-bd9a-c63256ca7000", 10 },
                    { -5, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -4, "f984066c-816d-4531-bd9a-c63256ca7000", 10 },
                    { -3, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -2, "f984066c-816d-4531-bd9a-c63256ca7000", 10 },
                    { -4, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -3, "f984066c-816d-4531-bd9a-c63256ca7000", 10 },
                    { -2, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -1, "f984066c-816d-4531-bd9a-c63256ca7000", 10 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AnswerId", "CreateDateTime", "Description", "QuestionId", "UserId" },
                values: new object[,]
                {
                    { -2, null, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -1, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -4, null, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -2, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -5, null, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -3, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -1, null, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -1, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -6, null, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -4, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -7, null, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -5, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -3, null, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", -1, "f984066c-816d-4531-bd9a-c63256ca7000" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[,]
                {
                    { -1, "iphone", -1 },
                    { -2, "apple", -2 },
                    { -5, "java", -5 },
                    { -3, "android", -3 },
                    { -4, "c#", -4 },
                    { -6, "js", -6 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AnswerId", "CreateDateTime", "Description", "QuestionId", "UserId" },
                values: new object[,]
                {
                    { -8, -1, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", null, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -9, -2, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", null, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -10, -3, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", null, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -11, -4, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", null, "f984066c-816d-4531-bd9a-c63256ca7000" },
                    { -12, -4, new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "{\"blocks\":[{\"key\":\"a7jmu\",\"text\":\"12321123123213\",\"type\":\"unstyled\",\"depth\":0,\"inlineStyleRanges\":[],\"entityRanges\":[],\"data\":{}}],\"entityMap\":{}}", null, "f984066c-816d-4531-bd9a-c63256ca7000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AnswerId",
                table: "Comments",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_QuestionId",
                table: "Comments",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_QuestionId",
                table: "Tags",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
