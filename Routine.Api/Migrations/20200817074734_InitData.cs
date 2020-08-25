using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Intruction = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeNo = table.Column<string>(maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070212"), "YoYo Company", "Microsoft" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("7ef3c682-6163-4074-85dd-4ab0798fec86"), "Niubi de Company", "Google" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("141cf996-29a8-4119-a291-d8115ea5c758"), "Not good Company", "Yahoo" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070211"), "YoYo Company", "Microsoft1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("7ef3c682-6163-4074-85dd-4ab0798fec81"), "Niubi de Company", "Google1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("141cf996-29a8-4119-a291-d8115ea5c751"), "Not good Company", "Yahoo1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("4500eb58-25ae-4d0c-b3c2-1e1df5070212"), "YoYo Company", "Microsoft2" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("7ef3c682-6163-4024-85dd-4ab0798fec86"), "Niubi de Company", "Google2" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("141cf996-29a8-4129-a291-d8115ea5c758"), "Not good Company", "Yahoo2" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("4500eb58-23ae-4d0c-b3cf-1e1df5070212"), "YoYo Company", "Microsoft3" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("7ef3c682-6133-4074-85dd-4ab0798fec86"), "Niubi de Company", "Google3" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Intruction", "Name" },
                values: new object[] { new Guid("141cf996-23a8-4119-a291-d8115ea5c758"), "Not good Company", "Yahoo3" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("6ec4c01c-0901-4be0-adea-a18b5ae5f3a5"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070212"), new DateTime(1986, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A8L", "Lebron", 1, "James" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("91c383a5-f359-4573-a456-8bf8dd54827d"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070212"), new DateTime(1990, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No01", "赛", 1, "田" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("b9489c18-613f-4fd6-abea-c10a53a0be56"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070212"), new DateTime(1976, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "G18L", "Macheal", 1, "Jordan" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("4cd3ebea-f2d6-4595-935c-cf0e7683a6b3"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070212"), new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "GLA300", "Taylor", 2, "Swift" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("4e432f7b-778f-4524-9853-4c36f3bd79f8"), new Guid("7ef3c682-6163-4074-85dd-4ab0798fec86"), new DateTime(1993, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "C369", "Rihanna", 2, "Lol" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("30e4ca6b-afef-4df9-b0d9-442037bb7e1b"), new Guid("7ef3c682-6163-4074-85dd-4ab0798fec86"), new DateTime(1990, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No011", "赛1", 1, "田1" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("88d5dcf1-9113-4b0e-a274-489c6ccb75e7"), new Guid("7ef3c682-6163-4074-85dd-4ab0798fec86"), new DateTime(1976, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "G18L1", "Macheal1", 1, "Jordan1" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("48e060d5-69bb-4ef0-8057-b336660bf949"), new Guid("7ef3c682-6163-4074-85dd-4ab0798fec86"), new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "GLA3001", "Taylor1", 2, "Swift1" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("2ca376c1-ae8b-4ca2-a83b-fa3debb2e267"), new Guid("141cf996-29a8-4119-a291-d8115ea5c758"), new DateTime(1986, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A8L2", "Lebron2", 2, "James2" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("68b1e56a-4248-4f11-90fb-d6d8af271896"), new Guid("141cf996-29a8-4119-a291-d8115ea5c758"), new DateTime(1990, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No012", "赛2", 1, "田2" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("5da80c4e-1624-49ea-9c8e-6f6d8210965f"), new Guid("141cf996-29a8-4119-a291-d8115ea5c758"), new DateTime(1976, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "G18L2", "Macheal2", 1, "Jordan2" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("27711c50-ff3f-440c-a19e-e686b0323228"), new Guid("141cf996-29a8-4119-a291-d8115ea5c758"), new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "GLA3002", "Taylor2", 2, "Swift2" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("6ec4c01c-0901-4be0-adea-a18b5ae5f3a1"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070211"), new DateTime(1986, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A8L1", "Lebron1", 1, "James1" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("91c383a5-f359-4573-a456-8bf8dd548271"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070211"), new DateTime(1990, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No011", "赛1", 1, "田1" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("b9489c18-613f-4fd6-abea-c10a53a0be51"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070211"), new DateTime(1976, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "G18L1", "Macheal1", 1, "Jordan1" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("4cd3ebea-f2d6-4595-935c-cf0e7683a6b1"), new Guid("4500eb58-25ae-4d0c-b3cf-1e1df5070211"), new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "GLA3001", "Taylor1", 2, "Swift1" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
