using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoclukProject.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class baseEntityChangedTimeZoneToUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(9958),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entryvote",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(1428),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entryfavorite",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 385, DateTimeKind.Utc).AddTicks(7679),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entrycommentvote",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(8267),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entrycommentfavorite",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(5045),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entrycomment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(3099),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entry",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 385, DateTimeKind.Utc).AddTicks(5425),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "emailconfirmations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 385, DateTimeKind.Utc).AddTicks(3985),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(9958));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entryvote",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(1428));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entryfavorite",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 385, DateTimeKind.Utc).AddTicks(7679));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entrycommentvote",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(8267));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entrycommentfavorite",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(5045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entrycomment",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 386, DateTimeKind.Utc).AddTicks(3099));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "entry",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 385, DateTimeKind.Utc).AddTicks(5425));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "emailconfirmations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 2, 28, 12, 21, 8, 385, DateTimeKind.Utc).AddTicks(3985));
        }
    }
}
