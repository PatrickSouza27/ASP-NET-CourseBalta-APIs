using _8__AutenticaEAutorizaIdentityAPI.ViewModel;

namespace _8__AutenticaEAutorizaIdentityAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Curso> Cursos { get; set; }

        public Categoria(string name)
            => Name = name;

        public void Editar(ViewCategoria categoriaUpdate)
            => Name = categoriaUpdate.Name;

    }
}
