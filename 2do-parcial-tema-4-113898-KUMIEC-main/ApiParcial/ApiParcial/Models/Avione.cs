using System;
using System.Collections.Generic;

namespace ApiParcial.Models;

public partial class Avione
{
    public int Id { get; set; }

    public int IdFabricante { get; set; }

    public int CantidadAsientos { get; set; }

    public string Modelo { get; set; } = null!;

    public int CantidadMotores { get; set; }

    public string? DatosVarios { get; set; }

    public virtual Fabricante IdFabricanteNavigation { get; set; } = null!;
}
