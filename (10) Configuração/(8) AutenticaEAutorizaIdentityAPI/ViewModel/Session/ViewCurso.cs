using _8__AutenticaEAutorizaIdentityAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace _8__AutenticaEAutorizaIdentityAPI.ViewModel.Session
{
    public class ViewCurso
    {
        [Required(ErrorMessage = "Nome Obrigatorio!")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Deve conter de 3 a 40 caracteres!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Categoria Obrigatorio!")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Digite o nome ou o Id da Categoria existente")]
        public string IdentificadorCategoria { get; set; }

        public ViewCurso(string name, string identificadorCategoria)
        {
            Name = name;
            IdentificadorCategoria = identificadorCategoria;
        }
    }
}
