using _8__AutenticaEAutorizaIdentityAPI;
using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigServices(builder);
ConfigMVC(builder);
ConfigurationAuthentication(builder);
var app = builder.Build();
LoadConfiguration(app);
app.MapControllers();
app.UseStaticFiles();
//nessa ordem, primeiro authentica e depois authoriza!
app.UseAuthentication();
app.UseAuthorization();
app.Run();

void LoadConfiguration(WebApplication app)
{
    Configuration.JWTKey = app.Configuration.GetValue<string>(key: "JWTKey");
    Configuration.ApiKeyName = app.Configuration.GetValue<string>(key: "ApiKeyName");
    Configuration.ApiKey = app.Configuration.GetValue<string>(key: "ApiKey");
    var Smtp = new Configuration.SmtpConfiguration();
    app.Configuration.GetSection(key: "Smtp").Bind(Smtp);
    Configuration.Smtp = Smtp;
}

void ConfigurationAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(c =>
    {
        c.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JWTKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
}

void ConfigMVC(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(x => x.SuppressModelStateInvalidFilter = true);
}

void ConfigServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApiDataContext>(); //sempre usar quando utilizar o DbContext, ele é o nativo
    builder.Services.AddTransient<TokenService>();
    builder.Services.AddTransient<EmailServices>();
}

// app.Configuration.GetSection("Smtp"); //ele vai pegar a sessão(Objeto) completa e vai fazer o Parse
/*app.Configuration.GetValue<string>("JWTKey");
 *                  GetValue<Tipagem>(string Key)
 */
//app.Configuration.GetConnectionString() Pega a sessão do banco

//Configuration.JWTKey = app.Configuration.GetValue<string>(key: "JWTKey");
//Configuration.ApiKeyName = app.Configuration.GetValue<string>(key: "ApiKeyName");
//Configuration.ApiKey = app.Configuration.GetValue<string>(key: "ApiKey");

//var Smtp = new Configuration.SmtpConfiguration();
//app.Configuration.GetSection(key: "Smtp").Bind(Smtp);
//Bind vai pegar o Json e vai popular ela e ai n precisa fazer um por um, exemplo Smtp.Name = app.configuration
//Configuration.Smtp = Smtp;