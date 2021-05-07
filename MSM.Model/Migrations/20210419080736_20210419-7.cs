using Microsoft.EntityFrameworkCore.Migrations;

namespace MSM.Model.Migrations
{
    public partial class _202104197 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_GoodsCategory_GoodsCategoryCategoryID",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_GoodsCategoryCategoryID",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "GoodsCategoryCategoryID",
                table: "Goods");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_CategoryID",
                table: "Goods",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_GoodsCategory_CategoryID",
                table: "Goods",
                column: "CategoryID",
                principalTable: "GoodsCategory",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_GoodsCategory_CategoryID",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_CategoryID",
                table: "Goods");

            migrationBuilder.AddColumn<int>(
                name: "GoodsCategoryCategoryID",
                table: "Goods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_GoodsCategoryCategoryID",
                table: "Goods",
                column: "GoodsCategoryCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_GoodsCategory_GoodsCategoryCategoryID",
                table: "Goods",
                column: "GoodsCategoryCategoryID",
                principalTable: "GoodsCategory",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
