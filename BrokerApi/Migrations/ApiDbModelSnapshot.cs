﻿// <auto-generated />
using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BrokerApi.Migrations
{
    [DbContext(typeof(ApiDb))]
    partial class ApiDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.Aceptadoestado", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descripcion");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("estado");

                    b.HasKey("Id")
                        .HasName("aceptadoestado_pkey");

                    b.ToTable("aceptadoestado", (string)null);
                });

            modelBuilder.Entity("Data.Models.Banco", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int>("IdEstadoBanco")
                        .HasColumnType("integer");

                    b.Property<int>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("numero");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("razonSocial");

                    b.HasKey("Id")
                        .HasName("banco_pkey");

                    b.HasIndex("IdEstadoBanco");

                    b.ToTable("banco", (string)null);
                });

            modelBuilder.Entity("Data.Models.Bancoestado", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descripcion");

                    b.HasKey("Id")
                        .HasName("bancoestado_pkey");

                    b.ToTable("bancoestado", (string)null);
                });

            modelBuilder.Entity("Data.Models.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Cbu")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cbu");

                    b.Property<int>("IdBanco")
                        .HasColumnType("integer")
                        .HasColumnName("idBanco");

                    b.Property<long>("Numero")
                        .HasColumnType("bigint")
                        .HasColumnName("numero");

                    b.HasKey("Id")
                        .HasName("cuenta_pkey");

                    b.HasIndex("IdBanco");

                    b.ToTable("cuenta", (string)null);
                });

            modelBuilder.Entity("Data.Models.Registroestado", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fechaHora");

                    b.Property<int>("IdAceptadoEstado")
                        .HasColumnType("integer")
                        .HasColumnName("idAceptadoEstado");

                    b.Property<int>("IdTransaccion")
                        .HasColumnType("integer")
                        .HasColumnName("idTransaccion");

                    b.Property<int>("IdValidadoEstado")
                        .HasColumnType("integer")
                        .HasColumnName("idValidadoEstado");

                    b.HasKey("Id")
                        .HasName("registroestado_pkey");

                    b.HasIndex("IdAceptadoEstado");

                    b.HasIndex("IdTransaccion");

                    b.HasIndex("IdValidadoEstado");

                    b.ToTable("registroestado", (string)null);
                });

            modelBuilder.Entity("Data.Models.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descripcion");

                    b.HasKey("Id")
                        .HasName("tipo_pkey");

                    b.ToTable("tipo", (string)null);
                });

            modelBuilder.Entity("Data.Models.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fechaHora");

                    b.Property<int>("IdAceptadoEstado")
                        .HasColumnType("integer")
                        .HasColumnName("idAceptadoEstado");

                    b.Property<int>("IdCuentaDestino")
                        .HasColumnType("integer")
                        .HasColumnName("idCuentaDestino");

                    b.Property<int>("IdCuentaOrigen")
                        .HasColumnType("integer")
                        .HasColumnName("idCuentaOrigen");

                    b.Property<int>("IdTipo")
                        .HasColumnType("integer")
                        .HasColumnName("idTipo");

                    b.Property<int>("IdValidacionEstado")
                        .HasColumnType("integer")
                        .HasColumnName("idValidacionEstado");

                    b.Property<float>("Monto")
                        .HasColumnType("real")
                        .HasColumnName("monto");

                    b.Property<int>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("numero");

                    b.HasKey("Id")
                        .HasName("transaccion_pkey");

                    b.HasIndex("IdAceptadoEstado");

                    b.HasIndex("IdCuentaDestino");

                    b.HasIndex("IdCuentaOrigen");

                    b.HasIndex("IdTipo");

                    b.HasIndex("IdValidacionEstado");

                    b.ToTable("transaccion", (string)null);
                });

            modelBuilder.Entity("Data.Models.Validacionestado", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descripcion");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("estado");

                    b.HasKey("Id")
                        .HasName("validacionestado_pkey");

                    b.ToTable("validacionestado", (string)null);
                });

            modelBuilder.Entity("Data.Models.Banco", b =>
                {
                    b.HasOne("Data.Models.Bancoestado", "IdEstadoBancoNavigation")
                        .WithMany("Banco")
                        .HasForeignKey("IdEstadoBanco")
                        .IsRequired()
                        .HasConstraintName("idEstadoBanco");

                    b.Navigation("IdEstadoBancoNavigation");
                });

            modelBuilder.Entity("Data.Models.Cuenta", b =>
                {
                    b.HasOne("Data.Models.Banco", "IdBancoNavigation")
                        .WithMany("Cuenta")
                        .HasForeignKey("IdBanco")
                        .IsRequired()
                        .HasConstraintName("idBanco");

                    b.Navigation("IdBancoNavigation");
                });

            modelBuilder.Entity("Data.Models.Registroestado", b =>
                {
                    b.HasOne("Data.Models.Aceptadoestado", "IdAceptadoEstadoNavigation")
                        .WithMany("Registroestado")
                        .HasForeignKey("IdAceptadoEstado")
                        .IsRequired()
                        .HasConstraintName("idAceptadoEstado");

                    b.HasOne("Data.Models.Transaccion", "IdTransaccionNavigation")
                        .WithMany("Registroestado")
                        .HasForeignKey("IdTransaccion")
                        .IsRequired()
                        .HasConstraintName("idTransaccion");

                    b.HasOne("Data.Models.Validacionestado", "IdValidadoEstadoNavigation")
                        .WithMany("Registroestado")
                        .HasForeignKey("IdValidadoEstado")
                        .IsRequired()
                        .HasConstraintName("idValidadoEstado");

                    b.Navigation("IdAceptadoEstadoNavigation");

                    b.Navigation("IdTransaccionNavigation");

                    b.Navigation("IdValidadoEstadoNavigation");
                });

            modelBuilder.Entity("Data.Models.Transaccion", b =>
                {
                    b.HasOne("Data.Models.Aceptadoestado", "IdAceptadoEstadoNavigation")
                        .WithMany("Transaccion")
                        .HasForeignKey("IdAceptadoEstado")
                        .IsRequired()
                        .HasConstraintName("idAceptadoEstado");

                    b.HasOne("Data.Models.Cuenta", "IdCuentaDestinoNavigation")
                        .WithMany("TransaccionIdCuentaDestinoNavigation")
                        .HasForeignKey("IdCuentaDestino")
                        .IsRequired()
                        .HasConstraintName("idCuentaDestino");

                    b.HasOne("Data.Models.Cuenta", "IdCuentaOrigenNavigation")
                        .WithMany("TransaccionIdCuentaOrigenNavigation")
                        .HasForeignKey("IdCuentaOrigen")
                        .IsRequired()
                        .HasConstraintName("idCuentaOrigen");

                    b.HasOne("Data.Models.Tipo", "IdTipoNavigation")
                        .WithMany("Transaccion")
                        .HasForeignKey("IdTipo")
                        .IsRequired()
                        .HasConstraintName("idTipo");

                    b.HasOne("Data.Models.Validacionestado", "IdValidacionEstadoNavigation")
                        .WithMany("Transaccion")
                        .HasForeignKey("IdValidacionEstado")
                        .IsRequired()
                        .HasConstraintName("idValidacionEstado");

                    b.Navigation("IdAceptadoEstadoNavigation");

                    b.Navigation("IdCuentaDestinoNavigation");

                    b.Navigation("IdCuentaOrigenNavigation");

                    b.Navigation("IdTipoNavigation");

                    b.Navigation("IdValidacionEstadoNavigation");
                });

            modelBuilder.Entity("Data.Models.Aceptadoestado", b =>
                {
                    b.Navigation("Registroestado");

                    b.Navigation("Transaccion");
                });

            modelBuilder.Entity("Data.Models.Banco", b =>
                {
                    b.Navigation("Cuenta");
                });

            modelBuilder.Entity("Data.Models.Bancoestado", b =>
                {
                    b.Navigation("Banco");
                });

            modelBuilder.Entity("Data.Models.Cuenta", b =>
                {
                    b.Navigation("TransaccionIdCuentaDestinoNavigation");

                    b.Navigation("TransaccionIdCuentaOrigenNavigation");
                });

            modelBuilder.Entity("Data.Models.Tipo", b =>
                {
                    b.Navigation("Transaccion");
                });

            modelBuilder.Entity("Data.Models.Transaccion", b =>
                {
                    b.Navigation("Registroestado");
                });

            modelBuilder.Entity("Data.Models.Validacionestado", b =>
                {
                    b.Navigation("Registroestado");

                    b.Navigation("Transaccion");
                });
#pragma warning restore 612, 618
        }
    }
}
