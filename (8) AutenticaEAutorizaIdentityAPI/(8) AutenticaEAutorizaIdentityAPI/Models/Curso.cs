using _8__AutenticaEAutorizaIdentityAPI.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace _8__AutenticaEAutorizaIdentityAPI.Models
{
    public class Curso
    {
        public int IdCurso { get; set; }
        public string Name { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }


        public Curso() { }
        public Curso(string name, Categoria categoria)
            => SetCurso(name, categoria);

        private void SetCurso(string name, Categoria categoria)
        {
            Name = name;
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void EditarCurso(string name, Categoria categoria)
          => SetCurso(name, categoria);
    }
}
