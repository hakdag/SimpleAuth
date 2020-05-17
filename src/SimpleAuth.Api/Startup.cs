using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Api.Filters;
using SimpleAuth.Api.Models;
using SimpleAuth.Api.Validators;
using SimpleAuth.Business;
using SimpleAuth.Business.PasswordReset;
using SimpleAuth.Business.Strategies;
using SimpleAuth.Common;
using SimpleAuth.Contracts.Business;
using SimpleAuth.Contracts.Business.PasswordReset;
using SimpleAuth.Contracts.Business.Strategies;
using SimpleAuth.Contracts.Data;
using SimpleAuth.Contracts.Data.PasswordReset;
using SimpleAuth.Data;
using SimpleAuth.Data.PasswordReset;
using SimpleAuth.PasswordHasherRfc2898;
using System;
using System.Text;

namespace SimpleAuth.Api
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
            services
                .AddMvc(options => options.Filters.Add<ValidateModelAttribute>())
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)
                .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<Startup>(); });
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v0.1";
                    document.Info.Title = "Simple Authentication API";
                    document.Info.Description = "A simple Dotnet Core Authentication API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Hakan Akdag",
                        Email = string.Empty,
                        Url = "https://github.com/hakdag/SimpleAuth"
                    };
                };
            });

            // configure DI for application services
            ConfigureIoC(services);
        }

        private void ConfigureIoC(IServiceCollection services)
        {
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // read settings
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            /* CONFIGURE THIS PART FOR YOUR OWN NEEDS */
            /* ---------------------------------------------------------------- */
            // create parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                AuthenticationType = "Bearer"
            };
            /* ---------------------------------------------------------------- */

            services.AddTransient<IValidator<CreateUserVM>, CreateUserValidator>();
            services.AddTransient<IValidator<UpdateUserVM>, UpdateUserValidator>();
            services.AddTransient<IValidator<CreateRoleVM>, CreateRoleValidator>();
            services.AddTransient<IValidator<UpdateRoleVM>, UpdateRoleValidator>();
            services.AddTransient<IValidator<UserRoleVM>, UserRoleValidator>();
            services.AddTransient<IValidator<ChangePasswordVM>, ChangePasswordValidator>();
            services.AddTransient<IValidator<LockAccountVM>, LockAccountValidator>();
            services.AddTransient<IValidator<GeneratePasswordResetKeyVM>, GeneratePasswordResetKeyValidator>();

            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();
            services.AddScoped<IUserRoleBusiness, UserRoleBusiness>();
            services.AddScoped<IChangePasswordBusiness, ChangePasswordBusiness>();
            services.AddScoped<IPasswordHistoryBusiness>(s => new PasswordHistoryBusiness(s.GetRequiredService<IPasswordHistoryData>(), appSettings.PasswordChangeHistoryRule));
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ILockAccountBusiness, LockAccountBusiness>();
            services.AddScoped<IAuthenticationBusiness, AuthenticationBusiness>();
            services.AddScoped<IAuthenticateAttemptBusiness>(s => new AuthenticateAttemptBusiness(appSettings.RemainingAttemptsCount, s.GetRequiredService<IAuthenticateAttemptData>()));
            services.AddScoped<ITokenGenerationBusiness, TokenGenerationBusiness>();
            services.AddScoped<IAuthorizationBusiness>(s => new AuthorizationBusiness(s.GetRequiredService<IUserData>(), tokenValidationParameters, s.GetRequiredService<ILockAccountBusiness>()));
            Enum.TryParse<PasswordChangeStrategies>(appSettings.PasswordChangeStrategy, out var passwordChangeStrategy);
            switch (passwordChangeStrategy)
            {
                case PasswordChangeStrategies.ChangePasswordWithHistory:
                    services.AddScoped<IChangePasswordStrategy, ChangePasswordWithHistoryStrategy>();
                    break;
                case PasswordChangeStrategies.Default:
                default:
                    services.AddScoped<IChangePasswordStrategy, ChangePasswordStrategy>();
                    break;
            }
            Enum.TryParse<AuthenticateAttemptStrategies>(appSettings.AuthenticateAttemptStrategy, out var authenticateAttemptStrategy);
            switch (authenticateAttemptStrategy)
            {
                case AuthenticateAttemptStrategies.UseAuthenticateAttempt:
                    services.AddScoped<IAuthenticateAttempsStrategy, AuthenticateWithRemainingAttempsStrategy>();
                    break;
                case AuthenticateAttemptStrategies.Default:
                default:
                    services.AddScoped<IAuthenticateAttempsStrategy, AuthenticateWithUnlimitedAttempsStrategy>();
                    break;
            }
            services.AddScoped<IGeneratePasswordResetKeyBusiness, GeneratePasswordResetKeyBusiness>();

            services.AddScoped<IUserData, UserData>();
            services.AddScoped<IRoleData, RoleData>();
            services.AddScoped<IUserRoleData, UserRoleData>();
            services.AddScoped<IChangePasswordData, ChangePasswordData>();
            services.AddScoped<IPasswordHistoryData, PasswordHistoryData>();
            services.AddScoped<ILockAccountData, LockAccountData>();
            services.AddScoped<IAuthenticateAttemptData, AuthenticateAttemptData>();
            services.AddScoped<IGeneratePasswordResetKeyData, GeneratePasswordResetKeyData>();

            services.AddScoped<IRepository, PGRepository>();

            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
