using Microsoft.OpenApi.Models;

namespace Quantum_Vision.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Define application variables
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var environment = builder.Environment;
            var services = builder.Services;

            // Add services to the container.

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Add Swagger generation with options
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FlightOS API",
                    Version = "v1",
                    Description = "API for FlightOS application",
                    Contact = new OpenApiContact
                    {
                        Name = "Luc Joosten",
                        Email = "lhajoosten@outlook.com",
                        Url = new Uri("https://quantum-vision.com")
                    }
                });

                // Include XML comments (optional)
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Add CORS Middleware
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Correct middleware order
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightOS API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
