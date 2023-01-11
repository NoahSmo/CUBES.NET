using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class NoMoreManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOrder_Articles_ArticlesId",
                table: "ArticleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOrder_Orders_OrdersId",
                table: "ArticleOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleOrder",
                table: "ArticleOrder");

            migrationBuilder.DropIndex(
                name: "IX_ArticleOrder_OrdersId",
                table: "ArticleOrder");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "ArticleOrder",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ArticlesId",
                table: "ArticleOrder",
                newName: "OrderId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ArticleOrder",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "ArticleOrder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleOrder",
                table: "ArticleOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOrder_ArticleId",
                table: "ArticleOrder",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOrder_OrderId",
                table: "ArticleOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOrder_Articles_ArticleId",
                table: "ArticleOrder",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOrder_Orders_OrderId",
                table: "ArticleOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOrder_Articles_ArticleId",
                table: "ArticleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOrder_Orders_OrderId",
                table: "ArticleOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleOrder",
                table: "ArticleOrder");

            migrationBuilder.DropIndex(
                name: "IX_ArticleOrder_ArticleId",
                table: "ArticleOrder");

            migrationBuilder.DropIndex(
                name: "IX_ArticleOrder_OrderId",
                table: "ArticleOrder");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ArticleOrder");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "ArticleOrder");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ArticleOrder",
                newName: "OrdersId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ArticleOrder",
                newName: "ArticlesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleOrder",
                table: "ArticleOrder",
                columns: new[] { "ArticlesId", "OrdersId" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOrder_OrdersId",
                table: "ArticleOrder",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOrder_Articles_ArticlesId",
                table: "ArticleOrder",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOrder_Orders_OrdersId",
                table: "ArticleOrder",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
