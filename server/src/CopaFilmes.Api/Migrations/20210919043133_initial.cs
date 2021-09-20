using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CopaFilmes.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TS_USUARIO",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DS_USUARIO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HX_SENHA = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TS_USUARIO", x => x.PK_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TS_USUARIO_DS_USUARIO",
                table: "TS_USUARIO",
                column: "DS_USUARIO",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TS_USUARIO");
        }
    }
}
