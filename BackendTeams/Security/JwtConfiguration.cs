/*using BackendTeams.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
namespace BackendTeams.Security;


////////////////////////////////////////////////////////////////////////////////////////////
///////////////TILLFÄLLIGT BORTKOMMENTERAD TILLS GABRIEL FIXAT SIN DEL. ////////////////////
////////////////////////////////////////////////////////////////////////////////////////////







// DHar även delar kopplade till denna i program.cs OCH user secrets. Skriver detta i egen del för att inte lägga allt i program.cs.
// Den hanterar Gabriels inloggningsdel samt när användaren i min modul 1 klickar in på sin länk och kommer till logga in sidan,
// så behöver jag denna för att ancändaren ska bekräftas 
public static class JwtConfiguration
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Systemet går in i min "user secret" och hämtar mina tre olika koder flr issuer, audience och signinkey och sparar dom
        var settings = configuration.GetSection("Jwt");
        var issuer = settings["Issuer"];
        var audience = settings["Audience"];
        var signinKey = settings["SigninKey"];



        // 2. TESTAR så allt passerar kontrollen och specifikt är ifyllt och inte tomt.
        if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience) || string.IsNullOrWhiteSpace(signinKey))
            throw new InvalidOperationException("Jwt is missing!");


        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey)),
                };
            });

        return services;
    }
}
*/