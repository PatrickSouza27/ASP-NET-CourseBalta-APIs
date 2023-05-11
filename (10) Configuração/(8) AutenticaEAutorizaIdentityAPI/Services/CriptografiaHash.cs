using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Models;
using _8__AutenticaEAutorizaIdentityAPI.ViewModel.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8__AutenticaEAutorizaIdentityAPI.Services
{
    public class CriptografiaHash
    {
        private readonly ApiDataContext _conn;
        public CriptografiaHash(ApiDataContext conn)
            => _conn = conn;
        public async Task<string> CriptografarSenha(string senha)
            => new PasswordHasher<object>().HashPassword(null, senha);
        public Usuario? VerificarUsuarioExiste(ViewLogin login)
        {
            var userLogin = _conn.Usuarios.AsNoTracking().Include(x => x.RolesUser).FirstOrDefault(x => x.Login == login.Login) ??
                throw new Exception("Login ou Usuario Invalido");

            return new PasswordHasher<object>().VerifyHashedPassword(null, userLogin.Password, login.Password) == PasswordVerificationResult.Success ? userLogin : null;
        }
        
    }
}
