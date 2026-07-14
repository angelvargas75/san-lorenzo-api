namespace SanLorenzoApi.Modulos.Notas.DTOs
{
    public class NotaIndividualDto
    {
        public int AlumnoId { get; set; }
        public int NumeroNota { get; set; }
        public decimal Valor { get; set; }
    }

    public class GuardarNotasLoteDto
    {
        public int AsignaturaId { get; set; }
        public int BimestreId { get; set; }
        public List<NotaIndividualDto> Notas { get; set; } = new();
    }
}