using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class UpdateDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Companies",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("141cf996-23a8-4119-a291-d8115ea5c758"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "Russian.21", "JinZhengEn", "Zhangchengze" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("141cf996-29a8-4119-a291-d8115ea5c751"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "2Japan.21", "SCapcom4", "AQQ1114" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("141cf996-29a8-4119-a291-d8115ea5c758"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "dChina2", "aQibolin2", "hPS42" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("141cf996-29a8-4129-a291-d8115ea5c758"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "AA2Japan.21", "THFHSCapcom4", "AWDAAQQ1114" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("4500eb58-23ae-4d0c-b3cf-1e1df5070212"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "America.21", "kltSCapcom4", "adawdAQQ1114" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("4500eb58-25ae-4d0c-b3c2-1e1df5070212"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "S2Japan.21", "HSCapcom4", "QAQQ1114" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070211"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "Japan.1", "Capcom", "QQ111" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070212"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "2China", "Qibolin", "PS4" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("7ef3c682-6133-4074-85dd-4ab0798fec86"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "America.21", "R.Star", "LOL" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("7ef3c682-6163-4024-85dd-4ab0798fec86"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "I2Japan.21", "OSCapcom4", "PAQQ1114" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("7ef3c682-6163-4074-85dd-4ab0798fec81"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "Japan.21", "Capcom4", "QQ1114" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("7ef3c682-6163-4074-85dd-4ab0798fec86"),
                columns: new[] { "Country", "Industry", "Product" },
                values: new object[] { "sChinaNo.1", "Qibolin1", "PS41" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "Companies");
        }
    }
}
