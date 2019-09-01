using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seguradora.API.Domain.Models;
using Seguradora.API.Domain.Repositories;
using Seguradora.API.Domain.Services;
using Seguradora.API.Infrastructure.Configuracao;
using Seguradora.API.Infrastructure.Contexts;
using Seguradora.API.Persistence.Repositories;
using Seguradora.API.Services;

namespace Seguradora.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly IHostingEnvironment HostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            HostingEnvironment = env;

            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            AppSettingsManager.ConfigureSettings(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AppDbContext>(options => {
                options.UseInMemoryDatabase("api-in-memory");
            });
            
            services.AddScoped<ICoberturaRepository, CoberturaRepository>();
            services.AddScoped<ICotacaoService, CotacaoService>();

            services.Configure<AppSettings>(Configuration.GetSection("Services"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
