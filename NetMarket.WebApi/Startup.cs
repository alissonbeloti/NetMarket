using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using NetMarket.BusinessLogic.Data;
using NetMarket.BusinessLogic.Logic;
using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;
using NetMarket.WebApi.Dto;
using NetMarket.WebApi.Middleware;

using StackExchange.Redis;

namespace NetMarket.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            var builder = services.AddIdentityCore<Usuario>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<IdentityRole>();

            builder.AddEntityFrameworkStores<SegurancaDbContext>();
            builder.AddSignInManager<SignInManager<Usuario>>();
            builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services); builder = new IdentityBuilder(builder.UserType, builder.Services);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                        ValidIssuer = Configuration["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,//Audiência pública
                        
                    };
                    //opt.RequireHttpsMetadata = false;
                    //opt.SaveToken = true;
                }); builder = new IdentityBuilder(builder.UserType, builder.Services);
            services.AddAuthorization();

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof (IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped(typeof(IGenericSegurancaRepository<>), (typeof(GenericSegurancaRepository<>)));
            services.AddDbContext<MarketDbContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<SegurancaDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("IdentitySeguranca"));
            });
            ///Configuração para o Redis
            services.AddSingleton<IConnectionMultiplexer>(c => {
                var config = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(config);
            });

            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddControllers();
            services.AddScoped<ICarrinhoCompraRepository, CarrinhoCompraRepository>();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyMethod().AllowAnyHeader().WithOrigins("*");
                });
            });
            //services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetMarket.WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Autorization header using the Bearer scheme (Example: 'Bearer 12351x3fasd454gs6gd54gd.54g6s45s4g6.64gs6dg4s6dg5g5dg4sd5gd')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

            app.UseRouting();
            app.UseCors("CorsRule");

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetMarket.WebApi v1"));
        }
    }
}
