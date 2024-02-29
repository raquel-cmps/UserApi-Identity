using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserApi_Identity.Authorization;
using UserApi_Identity.Data;
using UserApi_Identity.Models;
using UserApi_Identity.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:UserConnection"]!;

builder.Services.AddDbContext<UserDbContext>
    (opts =>
    {        
        opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IAuthorizationHandler,AgeAuthorization>();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* AUTENTICAÇÃO
 * É o processo de verificar a identidade do usuário. Em outras palavras, autenticação responde
 * à pergunta: "Quem é você?".
 * - token
 */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"]!)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

/* AUTORIZAÇÃO
 *  É o processo de determinar se o usuário autenticado tem permissão para acessar um recurso específico
 * ou executar uma determinada ação.
 * Autorização responde à pergunta: "Você tem permissão para fazer isso?". 
 */
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MinimalAge", policy =>
        policy.AddRequirements(new MinimalAge(18))
    );
});

builder.Services.AddScoped<UserService>(); 
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ProductService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

