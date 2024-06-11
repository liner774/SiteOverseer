using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteOverseer.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilityProgressImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevDtetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Company",
                columns: table => new
                {
                    CmpyId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CmpyNme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevDtetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Company", x => x.CmpyId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Contractor",
                columns: table => new
                {
                    CntorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CntorNme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FciltypId = table.Column<int>(type: "int", nullable: false),
                    ProgpayId = table.Column<int>(type: "int", nullable: false),
                    Establisheddte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BadStatus = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevDtetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Contractor", x => x.CntorId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Facility",
                columns: table => new
                {
                    FcilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FcilCde = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FcilNme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Township = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    FciltypId = table.Column<int>(type: "int", nullable: false),
                    Contractval = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ApproveDte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FcilstartDte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Facility", x => x.FcilId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Facilitytask",
                columns: table => new
                {
                    FciltskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FcilId = table.Column<int>(type: "int", nullable: false),
                    WbsdId = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CntorId = table.Column<int>(type: "int", nullable: false),
                    SelectionTyp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkstartDte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkendDte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AwardedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProgpayId = table.Column<int>(type: "int", nullable: false),
                    AllowSubmitExpense = table.Column<bool>(type: "bit", nullable: false),
                    TaskCompleteFlg = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Facilitytask", x => x.FciltskId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Facilitytype",
                columns: table => new
                {
                    FciltypId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FciltypCde = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FciltypDesc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Facilitytype", x => x.FciltypId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Menuaccess",
                columns: table => new
                {
                    AccessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MnugrpId = table.Column<short>(type: "smallint", nullable: false),
                    BtnNme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Menuaccess", x => x.AccessId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Menugp",
                columns: table => new
                {
                    MnugrpId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MnugrpNme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Menugp", x => x.MnugrpId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Trantypecode",
                columns: table => new
                {
                    TrantypId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrantypCde = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TranNature = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ContractorFlg = table.Column<bool>(type: "bit", nullable: false),
                    RequireClaim = table.Column<bool>(type: "bit", nullable: false),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevDtetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Trantypecode", x => x.TrantypId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Trantypereason",
                columns: table => new
                {
                    TranReasonId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranReasonDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevDtetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Trantypereason", x => x.TranReasonId);
                });

            migrationBuilder.CreateTable(
                name: "MS_User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCde = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    UserNme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    MnugrpId = table.Column<short>(type: "smallint", nullable: false),
                    Pwd = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Wbs",
                columns: table => new
                {
                    WbsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WbsCde = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Wbs", x => x.WbsId);
                });

            migrationBuilder.CreateTable(
                name: "MS_Wbsdetail",
                columns: table => new
                {
                    WbsdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WbsId = table.Column<int>(type: "int", nullable: false),
                    WbsdCde = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevdTetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Wbsdetail", x => x.WbsdId);
                });

            migrationBuilder.CreateTable(
                name: "PMS_Facilityprogress",
                columns: table => new
                {
                    ProgId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FcilTskId = table.Column<int>(type: "int", nullable: false),
                    ProgPercent = table.Column<short>(type: "smallint", nullable: false),
                    SubmitDte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    CmpyId = table.Column<short>(type: "smallint", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RevDteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMS_Facilityprogress", x => x.ProgId);
                });

            migrationBuilder.CreateTable(
                name: "PMS_Facilityprogressdocument",
                columns: table => new
                {
                    DocId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgId = table.Column<int>(type: "int", nullable: false),
                    ProgImg = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevDteTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMS_Facilityprogressdocument", x => x.DocId);
                });

            migrationBuilder.CreateTable(
                name: "PMS_Facilitytranlog",
                columns: table => new
                {
                    TrLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FcilTskId = table.Column<int>(type: "int", nullable: false),
                    TranTypId = table.Column<short>(type: "smallint", nullable: false),
                    TranReasonId = table.Column<short>(type: "smallint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimFlg = table.Column<bool>(type: "bit", nullable: false),
                    ClaimDteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteFlg = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CmpyId = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RevDteTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMS_Facilitytranlog", x => x.TrLogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_City");

            migrationBuilder.DropTable(
                name: "MS_Company");

            migrationBuilder.DropTable(
                name: "MS_Contractor");

            migrationBuilder.DropTable(
                name: "MS_Facility");

            migrationBuilder.DropTable(
                name: "MS_Facilitytask");

            migrationBuilder.DropTable(
                name: "MS_Facilitytype");

            migrationBuilder.DropTable(
                name: "MS_Menuaccess");

            migrationBuilder.DropTable(
                name: "MS_Menugp");

            migrationBuilder.DropTable(
                name: "MS_Trantypecode");

            migrationBuilder.DropTable(
                name: "MS_Trantypereason");

            migrationBuilder.DropTable(
                name: "MS_User");

            migrationBuilder.DropTable(
                name: "MS_Wbs");

            migrationBuilder.DropTable(
                name: "MS_Wbsdetail");

            migrationBuilder.DropTable(
                name: "PMS_Facilityprogress");

            migrationBuilder.DropTable(
                name: "PMS_Facilityprogressdocument");

            migrationBuilder.DropTable(
                name: "PMS_Facilitytranlog");
        }
    }
}
