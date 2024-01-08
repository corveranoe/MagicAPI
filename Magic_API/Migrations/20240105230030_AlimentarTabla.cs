using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Magics",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "otros", "Desarrollo de una API y consumirla", new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8476), new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8467), "", 50, "Noe Corvera", 5, 37.0 },
                    { 2, "otros", "Creacion de una API", new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8481), new DateTime(2024, 1, 5, 17, 0, 30, 190, DateTimeKind.Local).AddTicks(8480), "", 30, "Danely Alas", 4, 20.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Magics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Magics",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
