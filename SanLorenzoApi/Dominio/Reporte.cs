namespace SanLorenzoApi.Dominio
{
    public class Reporte
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string? FiltrosJson { get; set; }   // filtros usados, guardados como JSON

        public int GeneradoPorId { get; set; }
        public Usuario GeneradoPor { get; set; } = null!;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}