namespace SanLorenzoApi.Modulos.Notas.DTOs
{
    public class MiNotaDto
    {
        public string AsignaturaNombre { get; set; } = string.Empty;
        public string BimestreNombre { get; set; } = string.Empty;
        public decimal Promedio { get; set; }
        public List<DetalleNotaDto> Detalles { get; set; } = new();
    }
}