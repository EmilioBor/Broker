﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Transaccion
{
    public int Id { get; set; }

    public float Monto { get; set; }

    public DateTime FechaHora { get; set; }

    public int IdTipo { get; set; }

    public int IdValidacionEstado { get; set; }

    public int IdAceptadoEstado { get; set; }

    public int IdCuentaOrigen { get; set; }

    public int IdCuentaDestino { get; set; }

    public string Numero { get; set; }

    public virtual Aceptadoestado IdAceptadoEstadoNavigation { get; set; }

    public virtual Cuenta IdCuentaDestinoNavigation { get; set; }

    public virtual Cuenta IdCuentaOrigenNavigation { get; set; }

    public virtual Tipo IdTipoNavigation { get; set; }

    public virtual Validacionestado IdValidacionEstadoNavigation { get; set; }

    public virtual ICollection<Registroestado> Registroestado { get; set; } = new List<Registroestado>();
}