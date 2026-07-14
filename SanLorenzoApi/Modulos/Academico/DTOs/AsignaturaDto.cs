namespace SanLorenzoApi.Modulos.Academico.DTOs
{
    public class AsignaturaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public string CursoNombre { get; set; } = string.Empty;
    }
}