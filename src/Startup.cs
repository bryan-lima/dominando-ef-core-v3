using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.UowRepository.Data;
using EFCore.UowRepository.Data.Repositories;
using EFCore.UowRepository.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace EFCore.UowRepository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options => 
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFCore.UowRepository", Version = "v1" });
            });

            services.AddDbContext<ApplicationContext>(optionsBuilder => optionsBuilder.UseSqlServer("Server=DESKTOP-B76722G\\SQLEXPRESS; Database=UoW; User ID=developer; Password=dev*10; Integrated Security=True;"));

            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore.UowRepository v1"));
            }

            InicializarBaseDeDados(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InicializarBaseDeDados(IApplicationBuilder app)
        {
            using ApplicationContext db = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();

            if (db.Database.EnsureCreated())
            {
                db.Departamentos.AddRange(Enumerable.Range(1, 10)
                                .Select(numeroDepartamento => new Departamento
                {
                    Descricao = $"Departamento - {numeroDepartamento}",
                    Colaboradores = Enumerable.Range(1, 10)
                                              .Select(numeroColaborador => new Colaborador 
                    {
                        Nome = $"Colaborador {numeroColaborador}/{numeroDepartamento}"
                    })
                                              .ToList()
                }));

                db.SaveChanges();
            }
        }
    }
}
