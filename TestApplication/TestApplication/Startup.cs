using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleAuthExtensions;
using SimpleAuthExtensions.Authentication;
using SimpleAuthExtensions.Authorization;
using System.Text;

namespace TestApplication
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
            services.AddControllers();

            // configure strongly typed settings objects
            var secret = Configuration.GetSection("AppSettings:Secret");

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(secret.Value);
            services.AddAuthentication(SimpleAuthenticationDefaults.AuthenticationScheme)
                .AddSimpleAuth<SimpleAuthorizationService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmins", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin"));
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
                endpoints.MapControllers();
            });
        }
    }
}
