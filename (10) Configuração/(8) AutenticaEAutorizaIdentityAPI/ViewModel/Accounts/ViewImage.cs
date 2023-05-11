using System.ComponentModel.DataAnnotations;

namespace _8__AutenticaEAutorizaIdentityAPI.ViewModel.Accounts
{
    public class ViewImage
    {
        [Required(ErrorMessage = "Imagem não encontrada")]
        public string Image64Base { get; set; }
    }
}
