using _8__AutenticaEAutorizaIdentityAPI;
using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApiDataContext>(); //sempre usar quando utilizar o DbContext, ele é o nativo


builder.Services.AddTransient<TokenService>(); //Sempre Criar um Novo DataContext
//builder.Services.AddScoped<TokenService>();    //Durar por Requisição -> new Request (ele abre um DataContext e nas proximas requisição ele reutiliza)
//builder.Services.AddSingleton<TokenService>(); //Singleton -> 1 por App (nunca vai derrubar da aplicação) vivendo eternamente na memoria



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

var app = builder.Build();

app.MapControllers();


//nessa ordem, primeiro authentica e depois authoriza!
app.UseAuthentication();
app.UseAuthorization();



app.Run();
