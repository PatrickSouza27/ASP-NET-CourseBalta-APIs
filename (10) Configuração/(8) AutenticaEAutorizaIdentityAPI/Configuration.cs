namespace _8__AutenticaEAutorizaIdentityAPI
{
    public static class Configuration
    {
        public static string JWTKey { get; set; } = "NjgxNTY2NTctMjljNy00OWM2LTg3ZjItYTk5NDViODAzMjEw"; //Token - JWT - Json web Token
        //Gerei um GUID e Encryptei no Base 64 (Encode)

        public static string ApiKeyName = "api_key";
        public static string ApiKey = "123456";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 587;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}