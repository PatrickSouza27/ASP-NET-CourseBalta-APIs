using _8__AutenticaEAutorizaIdentityAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace _8__AutenticaEAutorizaIdentityAPI.ViewModel
{
    public class ViewCategoria
    {
        [Required(ErrorMessage = "Nome Obrigatorio!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Deve conter de 3 a 40 caracteres!")]
        public string Name { get; set; }
    }
}
