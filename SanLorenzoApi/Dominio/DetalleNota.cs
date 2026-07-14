namespace SanLorenzoApi.Dominio
{
    public class DetalleNota
    {
        public int Id { get; set; }

        public int RegistroNotaId { get; set; }
        public RegistroNota RegistroNota { get; set; } = null!;

        public int NumeroNota { get; set; }   // 1 a 5
        public decimal Valor { get; set; }    // 0 a 20
    }
}