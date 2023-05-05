using _8__AutenticaEAutorizaIdentityAPI.ViewModel;
using SecureIdentity.Password;

namespace _8__AutenticaEAutorizaIdentityAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Roles> RolesUser { get; set; } = new();

        public Usuario(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }
        public void UpdateUser(ViewUsuario user)
        {
            Name = user.Name;
            Login = user.Login;
            Password = user.Password;
        }

        public void AdicionarRole(Roles role)
            => RolesUser.Add(role);

        public void Preencher()
        {
            RolesUser.AddRange(new List<Roles>{
                new Roles {Name = "user" },
                new Roles {Name = "author"}
            });
        }
        
    }
}
