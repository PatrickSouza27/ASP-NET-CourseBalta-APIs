namespace APIsCRUD_SQLServer.Models
{
    public class Curso
    {
        public int IdCurso { get; set; }
        public string Name { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
