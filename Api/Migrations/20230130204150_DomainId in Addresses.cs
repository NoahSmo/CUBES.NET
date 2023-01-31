using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class DomainIdinAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainAddresses");

            migrationBuilder.AddColumn<int>(
                name: "DomainId",
                table: "Addresses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DomainId",
                table: "Addresses",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Domains_DomainId",
                table: "Addresses",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Domains_DomainId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_DomainId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "Addresses");

            migrationBuilder.CreateTable(
                name: "DomainAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DomainId = table.Column<int>(type: "integer", nullable: true),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ZipCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainAddresses_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainAddresses_DomainId",
                table: "DomainAddresses",
                column: "DomainId");
        }
    }
}
