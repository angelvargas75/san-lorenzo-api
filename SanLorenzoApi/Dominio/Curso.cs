namespace SanLorenzoApi.Dominio
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public ICollection<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();
    }
}