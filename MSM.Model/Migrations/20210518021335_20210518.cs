using Microsoft.EntityFrameworkCore.Migrations;

namespace MSM.Model.Migrations
{
    public partial class _20210518 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"create table SysConfig
                    (
                    id int primary key identity,
                    name nvarchar(50),
                    age int
                    )";
            migrationBuilder.Sql(sql);

            migrationBuilder.AlterColumn<string>(
                name: "ParentPath",
                table: "GoodsCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "SysConfig",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParentPath",
                table: "GoodsCategory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
