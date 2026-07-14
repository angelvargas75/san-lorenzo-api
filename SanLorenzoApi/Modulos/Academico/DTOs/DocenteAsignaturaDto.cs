namespace SanLorenzoApi.Modulos.Academico.DTOs
{
    public class DocenteAsignaturaDto
    {
        public int Id { get; set; }
        public int DocenteId { get; set; }
        public string DocenteNombre { get; set; } = string.Empty;
        public int AsignaturaId { get; set; }
        public string AsignaturaNombre { get; set; } = string.Empty;
        public int GradoSeccionId { get; set; }
        public string GradoSeccionTexto { get; set; } = string.Empty;  // ej: "5to A"
    }

    public class CrearDocenteAsignaturaDto
    {
        public int DocenteId { get; set; }
        public int AsignaturaId { get; set; }
        public int GradoSeccionId { get; set; }
    }
}