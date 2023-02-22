using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddisDeactivated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Statuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Providers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "ProviderOrders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Permissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Images",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Domains",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Articles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeactivated",
                table: "Addresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "ProviderOrders");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "isDeactivated",
                table: "Addresses");
        }
    }
}
