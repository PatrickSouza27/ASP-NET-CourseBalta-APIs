using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace _8__AutenticaEAutorizaIdentityAPI.ViewModel.Accounts
{
    public class ViewUsuario
    {
        [Required(ErrorMessage = "Nome Obrigatorio!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Deve conter de 3 a 40 caracteres!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login Obrigatorio!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Deve conter de 3 a 40 caracteres!")]
        public string Login { get; set; }

        //[Required(ErrorMessage = "E-mail Obrigatorio")]
        //[EmailAddress(ErrorMessage =  "O E-mail é Invalido")]
        //public string Email

        [Required(ErrorMessage = "Password Obrigatorio!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Deve conter de 3 a 40 caracteres!")]
        public string Password { get; set; }
        public ViewUsuario(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }

    }
}
