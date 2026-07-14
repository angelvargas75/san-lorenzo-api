namespace SanLorenzoApi.Modulos.Academico.DTOs
{
    public class CrearAsignaturaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int CursoId { get; set; }
    }
}