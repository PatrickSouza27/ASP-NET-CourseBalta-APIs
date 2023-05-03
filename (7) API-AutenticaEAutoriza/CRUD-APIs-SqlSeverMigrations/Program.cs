using Blog.Data;
using CRUD_APIs_SqlSeverMigrations;
using CRUD_APIs_SqlSeverMigrations.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})



builder.Services.AddControllers();
builder.Services.AddDbContext<BlogDataContext>();


builder.Services.AddTransient<TokenService>(); //Sempre Criar um Novo TokenService
//builder.Services.AddScoped<TokenService>();    //Durar por Requisição -> new Request
//builder.Services.AddSingleton<TokenService>(); //Singleton -> 1 por App (nunca vai derrubar da aplicação) vivendo eternamente na memoria 





var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
