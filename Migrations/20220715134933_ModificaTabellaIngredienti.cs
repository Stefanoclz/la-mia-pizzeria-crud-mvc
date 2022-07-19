using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class ModificaTabellaIngredienti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredientePizza",
                columns: table => new
                {
                    listaIngredientiId = table.Column<int>(type: "int", nullable: false),
                    listaPizzeid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientePizza", x => new { x.listaIngredientiId, x.listaPizzeid });
                    table.ForeignKey(
                        name: "FK_IngredientePizza_Ingredienti_listaIngredientiId",
                        column: x => x.listaIngredientiId,
                        principalTable: "Ingredienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientePizza_Pizze_listaPizzeid",
                        column: x => x.listaPizzeid,
                        principalTable: "Pizze",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientePizza_listaPizzeid",
                table: "IngredientePizza",
                column: "listaPizzeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientePizza");
        }
    }
}
