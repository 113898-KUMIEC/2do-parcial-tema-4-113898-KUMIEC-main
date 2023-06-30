using ApiParcial.Models;

namespace ApiParcial.Resultado.ListadoAviones;

public class ListadoAviones : ResultadoBase
{
    public List<ItemAvion> ListAviones { get; set; } = new List<ItemAvion>();
}
public class ItemAvion
{
    public int Id { get; set; }

    public int CantidadAsientos { get; set; }

    public string Modelo { get; set; } = null!;

    public int CantidadMotores { get; set; }

    public string? DatosVarios { get; set; }

    public string FabricanteNavigation { get; set; } = null!;

}

