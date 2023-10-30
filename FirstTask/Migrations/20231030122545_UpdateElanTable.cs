using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstTask.Migrations
{
    public partial class UpdateElanTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElanTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatAt = table.Column<DateTime>(nullable: true),
                    CreatBy = table.Column<string>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    ElanId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElanTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElanTags_Elans_ElanId",
                        column: x => x.ElanId,
                        principalTable: "Elans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElanTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElanTags_ElanId",
                table: "ElanTags",
                column: "ElanId");

            migrationBuilder.CreateIndex(
                name: "IX_ElanTags_TagId",
                table: "ElanTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElanTags");
        }
    }
}
