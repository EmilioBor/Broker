﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data.Models;

public partial class Banco
{
    public int Id { get; set; }

    public string RazonSocial { get; set; }

    public int IdEstadoBanco { get; set; }

    public int Numero { get; set; }
    [JsonIgnore]
    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
    [JsonIgnore]
    public virtual Bancoestado IdEstadoBancoNavigation { get; set; }
}