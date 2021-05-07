using Microsoft.EntityFrameworkCore.Migrations;

namespace MSM.Model.Migrations
{
    public partial class _202104284 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("create talbe/////drop table ////修改主键");


            //migrationBuilder.AlterColumn<string>(
            //    name: "name",
            //    table: "SysConfig",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(500)",
            //    oldMaxLength: 500,
            //    oldNullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "age",
            //    table: "SysConfig",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "SysConfig");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "SysConfig",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
