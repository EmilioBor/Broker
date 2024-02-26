﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class BrokerDBContext : DbContext
{
    public BrokerDBContext(DbContextOptions<BrokerDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aceptadoestado> Aceptadoestado { get; set; }

    public virtual DbSet<Banco> Banco { get; set; }

    public virtual DbSet<Bancoestado> Bancoestado { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Registroestado> Registroestado { get; set; }

    public virtual DbSet<Tipo> Tipo { get; set; }

    public virtual DbSet<Transaccion> Transaccion { get; set; }

    public virtual DbSet<Validacionestado> Validacionestado { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aceptadoestado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aceptadoestado_pkey");

            entity.ToTable("aceptadoestado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banco_pkey");

            entity.ToTable("banco");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.RazonSocial)
                .IsRequired()
                .HasColumnName("razonSocial");

            entity.HasOne(d => d.IdEstadoBancoNavigation).WithMany(p => p.Descripcion)
                .HasForeignKey(d => d.IdEstadoBanco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idEstadoBanco");
        });

        modelBuilder.Entity<Bancoestado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bancoestado_pkey");

            entity.ToTable("bancoestado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cuenta_pkey");

            entity.ToTable("cuenta");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cbu)
                .IsRequired()
                .HasColumnName("cbu");
            entity.Property(e => e.IdBanco).HasColumnName("idBanco");
            entity.Property(e => e.Numero).HasColumnName("numero");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdBanco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idBanco");
        });

        modelBuilder.Entity<Registroestado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("registroestado_pkey");

            entity.ToTable("registroestado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FechaHora).HasColumnName("fechaHora");
            entity.Property(e => e.IdAceptadoEstado).HasColumnName("idAceptadoEstado");
            entity.Property(e => e.IdTransaccion).HasColumnName("idTransaccion");
            entity.Property(e => e.IdValidadoEstado).HasColumnName("idValidadoEstado");

            entity.HasOne(d => d.IdAceptadoEstadoNavigation).WithMany(p => p.Registroestado)
                .HasForeignKey(d => d.IdAceptadoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idAceptadoEstado");

            entity.HasOne(d => d.IdTransaccionNavigation).WithMany(p => p.Registroestado)
                .HasForeignKey(d => d.IdTransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idTransaccion");

            entity.HasOne(d => d.IdValidadoEstadoNavigation).WithMany(p => p.Registroestado)
                .HasForeignKey(d => d.IdValidadoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idValidadoEstado");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_pkey");

            entity.ToTable("tipo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaccion_pkey");

            entity.ToTable("transaccion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FechaHora).HasColumnName("fechaHora");
            entity.Property(e => e.IdAceptadoEstado).HasColumnName("idAceptadoEstado");
            entity.Property(e => e.IdCuentaDestino).HasColumnName("idCuentaDestino");
            entity.Property(e => e.IdCuentaOrigen).HasColumnName("idCuentaOrigen");
            entity.Property(e => e.IdTipo).HasColumnName("idTipo");
            entity.Property(e => e.IdValidacionEstado).HasColumnName("idValidacionEstado");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.Numero).HasColumnName("numero");

            entity.HasOne(d => d.IdAceptadoEstadoNavigation).WithMany(p => p.Transaccion)
                .HasForeignKey(d => d.IdAceptadoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idAceptadoEstado");

            entity.HasOne(d => d.IdCuentaDestinoNavigation).WithMany(p => p.TransaccionIdCuentaDestinoNavigation)
                .HasForeignKey(d => d.IdCuentaDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idCuentaDestino");

            entity.HasOne(d => d.NombreCuentaOrigenNavigation).WithMany(p => p.TransaccionIdCuentaOrigenNavigation)
                .HasForeignKey(d => d.IdCuentaOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idCuentaOrigen");

            entity.HasOne(d => d.NombreTipoNavigation).WithMany(p => p.Transaccion)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idTipo");

            entity.HasOne(d => d.NombreValidacionEstadoNavigation).WithMany(p => p.Transaccion)
                .HasForeignKey(d => d.IdValidacionEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idValidacionEstado");
        });

        modelBuilder.Entity<Validacionestado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("validacionestado_pkey");

            entity.ToTable("validacionestado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasColumnName("estado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}