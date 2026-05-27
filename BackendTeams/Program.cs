using BackendTeams.API.Swagger;
using BackendTeams.Infrastructure.Data;
using BackendTeams.Security;
using BackendTeams.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// LÅSER SÅ MAN MÅSTE SKRIVA IN SÄKERHETSNYCKEL FÖR SWAGGER
/*builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiKeyAuthFilter>();
});*/

builder.Services.AddControllers();



// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://lms-shiko.vercel.app") //BÅDE TEST-LOKALHOST OCH RIKTIGA LÄNKEN
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});





//JWT - stänger av tillfälligt tills Gabriel fixat sin del
// builder.Services.AddJwtAuthentication(builder.Configuration);



// 2 SWAGGER KEY FÖR SÄKERHET
builder.Services.Configure<ApiKeyOptions>(builder.Configuration.GetSection("ApiKeyOptions"));
builder.Services.AddScoped<ApiKeyAuthFilter>();


// DEPENDENCY INJECTION - Kopplar min service med DbContextt (kopplar här pga clean architecture och för att inte behöva röra runt i dependencys
builder.Services.AddScoped<BackendTeams.Application.Interfaces.IMemberService, BackendTeams.Application.Services.MemberService>();


// SWAGGER
builder.Services.AddSwagger();


// DATABASEN
// DÅ VI INTE HAR EN DATABAS JUST NU SÅ FÅR VI KOMMENTERA UT DENNA
/*builder.Services.AddDbContext<TeamsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLAzure")));*/


// IApplicationDbContext som just nu ger all info från TeamsDbContext till servicen
//Också utkommenterad för att allt inte ska krascha
/*builder.Services.AddScoped<BackendTeams.Application.Interfaces.IApplicationDbContext>(provider =>
    provider.GetRequiredService<TeamsDbContext>());*/



var app = builder.Build();




// dessa 2 FÖR JWT SPECIFIKT. När användaren trycker på mailet i modul 1 > behöver sen modul 2 och gabriels inloggningsdel dessa
// app.UseAuthentication();
// app.UseAuthorization();



// SWAGGER
app.MapSwagger(app.Environment);




app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
