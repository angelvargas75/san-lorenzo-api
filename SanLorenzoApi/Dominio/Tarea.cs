namespace SanLorenzoApi.Dominio
{
    public enum TipoTarea
    {
        Tarea,
        Examen
    }

    public class Tarea
    {
        public int Id { get; set; }

        public int AsignaturaId { get; set; }
        public Asignatura Asignatura { get; set; } = null!;

        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public TipoTarea Tipo { get; set; }
    }
}