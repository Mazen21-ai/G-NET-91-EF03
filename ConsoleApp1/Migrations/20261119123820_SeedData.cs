using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "FullName", "Email", "Bio", "JoinedAt" },
                values: new object[]
                {
                    1,
                    "Ahmed Mohamed",
                    "ahmed@example.com",
                    "Programming Instructor",
                    new DateTime(2025, 1, 1)
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Description", "ParentCategoryId" },
                values: new object[]
                {
                    1,
                    "Programming",
                    "Programming Courses",
                    null
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[]
                {
                    "Id", "Title", "Description", "Price",
                    "Level", "IsPublished", "PublishedAt",
                    "InstructorId", "CategoryId"
                },
                values: new object[]
                {
                    1,
                    "C# Basics",
                    "Introduction to C#",
                    500m,
                    0,
                    true,
                    new DateTime(2025, 2, 1),
                    1,
                    1
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FullName", "Email", "DateOfBirth" },
                values: new object[,]
                {
                    { 1, "Ali Hassan", "ali@example.com", new DateTime(2003, 5, 10) },
                    { 2, "Omar Ahmed", "omar@example.com", new DateTime(2002, 8, 15) },
                    { 3, "Mona Samir", "mona@example.com", new DateTime(2004, 3, 20) }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[]
                {
                    "StudentId", "CourseId", "EnrollmentDate",
                    "Progress", "Status", "FinalGrade"
                },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 3, 1), 50m, 0, null },
                    { 2, 1, new DateTime(2025, 3, 2), 100m, 1, 95m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "StudentId", "CourseId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}