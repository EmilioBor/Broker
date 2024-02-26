using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrokerApi.Migrations
{
    /// <inheritdoc />
    public partial class NameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aceptadoestado",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("aceptadoestado_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bancoestado",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("bancoestado_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipo_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "validacionestado",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("validacionestado_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "banco",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    razonSocial = table.Column<string>(type: "text", nullable: false),
                    IdEstadoBanco = table.Column<int>(type: "integer", nullable: false),
                    numero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("banco_pkey", x => x.id);
                    table.ForeignKey(
                        name: "idEstadoBanco",
                        column: x => x.IdEstadoBanco,
                        principalTable: "bancoestado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cuenta",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    numero = table.Column<long>(type: "bigint", nullable: false),
                    cbu = table.Column<string>(type: "text", nullable: false),
                    idBanco = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cuenta_pkey", x => x.id);
                    table.ForeignKey(
                        name: "idBanco",
                        column: x => x.idBanco,
                        principalTable: "banco",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transaccion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    monto = table.Column<float>(type: "real", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    idTipo = table.Column<int>(type: "integer", nullable: false),
                    idValidacionEstado = table.Column<int>(type: "integer", nullable: false),
                    idAceptadoEstado = table.Column<int>(type: "integer", nullable: false),
                    idCuentaOrigen = table.Column<int>(type: "integer", nullable: false),
                    idCuentaDestino = table.Column<int>(type: "integer", nullable: false),
                    numero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaccion_pkey", x => x.id);
                    table.ForeignKey(
                        name: "idAceptadoEstado",
                        column: x => x.idAceptadoEstado,
                        principalTable: "aceptadoestado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idCuentaDestino",
                        column: x => x.idCuentaDestino,
                        principalTable: "cuenta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idCuentaOrigen",
                        column: x => x.idCuentaOrigen,
                        principalTable: "cuenta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idTipo",
                        column: x => x.idTipo,
                        principalTable: "tipo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idValidacionEstado",
                        column: x => x.idValidacionEstado,
                        principalTable: "validacionestado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "registroestado",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    idValidadoEstado = table.Column<int>(type: "integer", nullable: false),
                    idAceptadoEstado = table.Column<int>(type: "integer", nullable: false),
                    idTransaccion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("registroestado_pkey", x => x.id);
                    table.ForeignKey(
                        name: "idAceptadoEstado",
                        column: x => x.idAceptadoEstado,
                        principalTable: "aceptadoestado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idTransaccion",
                        column: x => x.idTransaccion,
                        principalTable: "transaccion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "idValidadoEstado",
                        column: x => x.idValidadoEstado,
                        principalTable: "validacionestado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_banco_IdEstadoBanco",
                table: "banco",
                column: "IdEstadoBanco");

            migrationBuilder.CreateIndex(
                name: "IX_cuenta_idBanco",
                table: "cuenta",
                column: "idBanco");

            migrationBuilder.CreateIndex(
                name: "IX_registroestado_idAceptadoEstado",
                table: "registroestado",
                column: "idAceptadoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_registroestado_idTransaccion",
                table: "registroestado",
                column: "idTransaccion");

            migrationBuilder.CreateIndex(
                name: "IX_registroestado_idValidadoEstado",
                table: "registroestado",
                column: "idValidadoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idAceptadoEstado",
                table: "transaccion",
                column: "idAceptadoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idCuentaDestino",
                table: "transaccion",
                column: "idCuentaDestino");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idCuentaOrigen",
                table: "transaccion",
                column: "idCuentaOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idTipo",
                table: "transaccion",
                column: "idTipo");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idValidacionEstado",
                table: "transaccion",
                column: "idValidacionEstado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registroestado");

            migrationBuilder.DropTable(
                name: "transaccion");

            migrationBuilder.DropTable(
                name: "aceptadoestado");

            migrationBuilder.DropTable(
                name: "cuenta");

            migrationBuilder.DropTable(
                name: "tipo");

            migrationBuilder.DropTable(
                name: "validacionestado");

            migrationBuilder.DropTable(
                name: "banco");

            migrationBuilder.DropTable(
                name: "bancoestado");
        }
    }
}
