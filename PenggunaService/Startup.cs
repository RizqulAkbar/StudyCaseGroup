using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PenggunaService.Auth;
using PenggunaService.GraphQL;
using PenggunaService.Kafka;
using PenggunaService.Models;

namespace PenggunaService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                Console.WriteLine("--> Using Azure Database");
                services.AddDbContext<bootcampLearnDb5Context>(opt => opt.UseSqlServer(
                    Configuration.GetConnectionString("AzureDb")
                ));
            }
            else
            {
                Console.WriteLine("--> Using Local Database");
                services.AddDbContext<bootcampLearnDb5Context>(opt => opt.UseSqlServer(
                    Configuration.GetConnectionString("LocalConnection")
                ));
            }
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddAuthorization();
            
            services.Configure<KafkaSettings>(Configuration.GetSection("KafkaSettings"));

            services.AddHttpContextAccessor();
            services.AddControllers();

            services.Configure<TokenSettings>(Configuration.GetSection("TokenSettings"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = Configuration.GetSection("TokenSettings").GetValue<string>("Issuer"),
                       ValidateIssuer = true,
                       ValidAudience = Configuration.GetSection("TokenSettings").GetValue<string>("Audience"),
                       ValidateAudience = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("TokenSettings").GetValue<string>("Key"))),
                       ValidateIssuerSigningKey = true
                   };

               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
