namespace SanLorenzoApi.Modulos.Notas.DTOs
{
    public class DetalleNotaDto
    {
        public int NumeroNota { get; set; }
        public decimal? Valor { get; set; }
    }

    public class AlumnoNotaDto
    {
        public int AlumnoId { get; set; }
        public string AlumnoNombre { get; set; } = string.Empty;
        public int? RegistroNotaId { get; set; }   // null si aún no se ha creado el registro
        public List<DetalleNotaDto> Detalles { get; set; } = new();
        public decimal? Promedio { get; set; }
    }
}