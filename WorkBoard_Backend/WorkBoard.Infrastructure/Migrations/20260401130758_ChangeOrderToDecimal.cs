using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Projects_ProjectId",
                table: "Columns");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Columns_ColumnId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects");

            migrationBuilder.AlterColumn<decimal>(
                name: "Order",
                table: "Issues",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Order",
                table: "Columns",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Columns",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Projects_ProjectId",
                table: "Columns",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Columns_ColumnId",
                table: "Issues",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Projects_ProjectId",
                table: "Columns");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Columns_ColumnId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Issues",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Columns",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Columns",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Projects_ProjectId",
                table: "Columns",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Columns_ColumnId",
                table: "Issues",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }
    }
}
