namespace ApiParcial.Comando
{
    public class PutAvionComando
    {
        public int Id { get; set; }

        public int CantidadAsientos { get; set; }

        public string Modelo { get; set; } = null!;

        public int CantidadMotores { get; set; }

        public string? DatosVarios { get; set; }

    }
}
