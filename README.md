# ASP NET WebAPIs REST / RESTful
#### Desenvolvimento de APIs utilizando ASP.NET e autenticação/autorização JWT (JSON Web Tokens) Com ele, fui capaz de criar APIs RESTful de forma eficiente, utilizando boas práticas e padrões recomendados. Essas APIs se mostraram essenciais para o fornecimento de serviços

###### ORM
``` Microsoft.EntityFrameworkCore.SqlServer ```
``` Microsoft.EntityFrameworkCore.Design ```
###### Authenticação / Authorização / Criptografia
``` Microsoft.AspNetCore.Authentication.JwtBearer```
``` Microsoft.Extensions.Identity.Core```
###### Swagger
``` Swashbuckle.AspNetCore```

##### Condifuração Services ApiKey / TokenServices ```appsettings.json```
```Json

"JWTKey": "NjgxNTY2NTctMjljNy00OWM2LTg3ZjItYTk5NDViODAzMjEw",
"Gerei um GUID e Encryptei em Base 64 (Encode)",
  "ApiKeyName": "api_key",
  "ApiKey ": "123456"
```
### Carregando Classe com o Json configurado
```C#
 Configuration.JWTKey = app.Configuration.GetValue<string>(key: "JWTKey");
Configuration.ApiKeyName = app.Configuration.GetValue<string>(key: "ApiKeyName");
Configuration.ApiKey = app.Configuration.GetValue<string>(key: "ApiKey");
```
### Função Program Authentication / Authorization
```C#
 void ConfigurationAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(c =>
    {
        c.RequireHttpsMetadata = false;
        c.SaveToken = true;
        c.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JWTKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
}
void ConfigServices(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(x=> x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira Token JWT",
        Name = "Authorização",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    }));
    builder.Services.AddSwaggerGen(s =>
    s.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }));
    builder.Services.AddTransient<TokenServices>();
    builder.Services.AddControllers();
}
```
### Program
```C#
void ConfigServices(WebApplicationBuilder builder)
{
var builder = WebApplication.CreateBuilder(args);
ConfigServices(builder);
ConfigurationAuthentication(builder);
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
}
```
### Certificado 

![Captura de tela 2023-02-21 130017](https://github.com/PatrickSouza27/ASP-NET-CourseBalta-APIs/assets/77933748/e6bc5feb-f189-4a91-a40c-d5354918b561)



