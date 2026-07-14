namespace SanLorenzoApi.Modulos.Academico.DTOs
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}