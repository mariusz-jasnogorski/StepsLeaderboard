using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Repositories;
using StepsLeaderboard.Services;
using StepsLeaderboard.WebAPI.Authentication;
using StepsLeaderboard.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add configuration settings (like a secret key). Loaded from configuration or environment variables in real scenarios.
var jwtSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<AuthSettings>(jwtSection);
var authSettings = jwtSection.Get<AuthSettings>() ?? new AuthSettings();

// Register the authentication scheme with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add repositories, services, controllers
builder.Services.AddSingleton<ITeamRepository, InMemoryTeamRepository>();
builder.Services.AddSingleton<ICounterRepository, InMemoryCounterRepository>();
builder.Services.AddSingleton<ITeamService, TeamService>();
builder.Services.AddSingleton<ICounterService, CounterService>();

// Add a TokenService to handle token generation
builder.Services.AddSingleton<TokenService>();

// Add global exception filter and controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

// Add endpoints and swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Dev pipeline for swagger
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
