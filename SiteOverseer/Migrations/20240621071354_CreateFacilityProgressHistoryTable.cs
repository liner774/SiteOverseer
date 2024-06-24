using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteOverseer.Migrations
{
    /// <inheritdoc />
    public partial class CreateFacilityProgressHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "PMS_Facilityprogress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "PMS_Facilityprogress",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "PMS_Facilityprogress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacilityProgressHistories",
                columns: table => new
                {
                    HistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityProgressId = table.Column<long>(type: "bigint", nullable: false),
                    ProgPercent = table.Column<short>(type: "smallint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityProgressHistories", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_FacilityProgressHistories_PMS_Facilityprogress_FacilityProgressId",
                        column: x => x.FacilityProgressId,
                        principalTable: "PMS_Facilityprogress",
                        principalColumn: "ProgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityProgressImages",
                columns: table => new
                {
                    ImageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityProgressId = table.Column<long>(type: "bigint", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityProgressImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_FacilityProgressImages_PMS_Facilityprogress_FacilityProgressId",
                        column: x => x.FacilityProgressId,
                        principalTable: "PMS_Facilityprogress",
                        principalColumn: "ProgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProgressHistories_FacilityProgressId",
                table: "FacilityProgressHistories",
                column: "FacilityProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityProgressImages_FacilityProgressId",
                table: "FacilityProgressImages",
                column: "FacilityProgressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityProgressHistories");

            migrationBuilder.DropTable(
                name: "FacilityProgressImages");

            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "PMS_Facilityprogress");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "PMS_Facilityprogress");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "PMS_Facilityprogress");
        }
    }
}
