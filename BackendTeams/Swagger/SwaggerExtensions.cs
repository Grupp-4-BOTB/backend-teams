/*using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackendTeams.API.Swagger
{
    public static class SwaggerExtensions
    {

        // 2. För app.UseSwagger
        public static IApplicationBuilder MapSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TITEL PÅ DET SOM SKA GÖRAS");
                    options.RoutePrefix = string.Empty; // Gör så Swagger laddas på rot-URLen
                });
            }

            return app;
        }
    }
}*/