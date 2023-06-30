using System;
using System.Collections.Generic;

namespace ApiParcial.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdRol { get; set; }
}
