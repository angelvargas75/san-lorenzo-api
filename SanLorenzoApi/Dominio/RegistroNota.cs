namespace SanLorenzoApi.Dominio
{
    public class RegistroNota
    {
        public int Id { get; set; }

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int AsignaturaId { get; set; }
        public Asignatura Asignatura { get; set; } = null!;

        public int BimestreId { get; set; }
        public Bimestre Bimestre { get; set; } = null!;

        public decimal Promedio { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        public ICollection<DetalleNota> Detalles { get; set; } = new List<DetalleNota>();
    }
}