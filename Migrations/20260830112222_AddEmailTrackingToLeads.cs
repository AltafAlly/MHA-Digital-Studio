using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MHAWebDesign.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailTrackingToLeads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailSent",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailSentAt",
                table: "Leads",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSent",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "EmailSentAt",
                table: "Leads");
        }
    }
}
