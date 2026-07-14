namespace SanLorenzoApi.Dominio
{
    public class Bimestre
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;  // ej: "1er Bimestre"
        public int AnioEscolar { get; set; }                // ej: 2026

        public ICollection<RegistroNota> RegistrosNotas { get; set; } = new List<RegistroNota>();
    }
}