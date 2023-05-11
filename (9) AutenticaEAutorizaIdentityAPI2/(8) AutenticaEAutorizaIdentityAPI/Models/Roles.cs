namespace _8__AutenticaEAutorizaIdentityAPI.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Usuario> Usuarios { get; set; }
    }
}
