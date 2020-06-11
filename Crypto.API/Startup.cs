using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Crypto.API.Data;
using Crypto.API.Helpers;
using Crypto.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Crypto.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

     

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {

                   var server =Configuration["DBServer"] ?? API_KEY_COINMCAP.serverName;     
                   var port = Configuration["DBPort"] ?? API_KEY_COINMCAP.port;
                   var user = Configuration["DBUser"] ?? API_KEY_COINMCAP.user;
                   var password = Configuration["DBPassword"] ?? API_KEY_COINMCAP.password;
                   var database = Configuration["Database"] ?? API_KEY_COINMCAP.database;

                 services.AddDbContext<DataContext>(options => options.UseSqlServer($"Server={server},{port};Database={database};User ID={user};Password={password};MultipleActiveResultSets=true"));


            //   services.AddDbContext<DataContext>(x => {
            //        // x.UseLazyLoadingProxies();
            //         x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            //   });

            // services.AddDbContext<DataContext>(x => {
            //     x.UseSqlServer(Configuration.GetConnectionString("CryfolioConnection"));    
            // });

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {

                   var server =Configuration["DBServer"] ?? API_KEY_COINMCAP.serverName; 
                   var port = Configuration["DBPort"] ?? API_KEY_COINMCAP.port;
                   var user = Configuration["DBUser"] ?? API_KEY_COINMCAP.user;
                   var password = Configuration["DBPassword"] ?? API_KEY_COINMCAP.password;
                   var database = Configuration["Database"] ?? API_KEY_COINMCAP.database;

                   services.AddDbContext<DataContext>(options => options.UseSqlServer($"Server={server},{port};Database={database};User ID={user};Password={password};MultipleActiveResultSets=true"));



            //   services.AddDbContext<DataContext>(x => {
            //        // x.UseLazyLoadingProxies();
            //         x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //   });

              ConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opt => {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddCors();
            services.AddAutoMapper(typeof(CryptoRepository).Assembly);
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddTransient<ICryptoRepository, CryptoRepository>();
            services.AddScoped<CoinList>();
            services.AddScoped<GetCoinsInterval>();
            // services.AddSingleton<IConfiguration>(Configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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
            app.UseDeveloperExceptionPage(); // gets errors in browser from azure // comment out if working

            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new {controller = "Fallback", action = "Index"} 
                );
            });
        }
    }
}
