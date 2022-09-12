using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Entities.Models;
using SocialNetwork.IdentityTokenServer;
using SocialNetworks.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTokenServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), x => x.MigrationsAssembly("SocialNetwork")));
        
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(ResourceManager.Apis)
            .AddInMemoryClients(ClientManager.Clients)
            .AddInMemoryApiScopes(ScopeManager.ApiScopes)
            .AddAspNetIdentity<User>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:5000", "https://localhost:5001",
                    "http://localhost:5050", "https://localhost:5051")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseIdentityServer();
        }
    }
}
