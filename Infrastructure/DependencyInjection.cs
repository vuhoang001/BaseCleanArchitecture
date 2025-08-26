using System.Text;
using Application.Data;
using Application.Dtos;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Idenitty;
using Infrastructure.Idenitty.Mapper;
using Infrastructure.Services;
using Infrastructure.Services.ApprovalLevel;
using Infrastructure.Services.CodeGeneration;
using Infrastructure.Services.Mail;
using Infrastructure.Services.Order;
using Infrastructure.Services.PurchasePlan;
using Infrastructure.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.Interfaces;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectString = configuration.GetConnectionString("Database");
        services.AddDistributedMemoryCache();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


        services.AddDbContext<ApplicationDbContext>((sb, options) =>
        {
            if (connectString is null) throw new ArgumentNullException(nameof(connectString));
            options.AddInterceptors(sb.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectString);
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IUserContextService, UserContext>();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.Decorate<IUserRepository, CachedUserRepository>();


        services.AddAuth(configuration);
        services.AddServices(configuration);
        return services;
    }

    private static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
            ?? throw new ArgumentNullException(nameof(JwtSettings));

        services.Configure<GoogleSettings>(configuration.GetSection(GoogleSettings.SectionName));
        var googleSettings = configuration.GetSection(GoogleSettings.SectionName).Get<GoogleSettings>()
            ?? throw new ArgumentNullException(nameof(GoogleSettings));

        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        services.AddAuthentication(options =>
            {
                /*
                 * JWT
                 */
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;

                /*
                 * Google OAuth Cookie scheme
                 */

                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath      = "/api/User/google-login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.Cookie.Name         = "GoogleAuth";
                options.Cookie.HttpOnly     = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite     = SameSiteMode.Lax;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidIssuer              = jwtSettings.Issuer,
                    ValidateAudience         = true,
                    ValidAudience            = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey         = new SymmetricSecurityKey(key),
                    ValidateLifetime         = true,
                    ClockSkew                = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = _ => { return Task.CompletedTask; },
                    OnTokenValidated       = _ => { return Task.CompletedTask; },
                    OnChallenge            = _ => { return Task.CompletedTask; }
                };
            })
            .AddGoogle(options =>
            {
                options.ClientId     = googleSettings.ClientId;
                options.ClientSecret = googleSettings.ClientSecret;
                options.CallbackPath = "/api/User/google-callback";

                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Events.OnRemoteFailure = context =>
                {
                    Console.WriteLine("OnRemoteFailure: " + context.Failure?.Message);
                    return Task.CompletedTask;
                };

                options.Scope.Add("email");
                options.Scope.Add("profile");
                options.SaveTokens = true;


                options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.CorrelationCookie.SameSite     = SameSiteMode.Lax;
            });

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IJwtService, JwtService>();
    }

    private static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        var _ = configuration.GetSection(EmailSettings.SectionName).Get<EmailSettings>()
            ?? throw new ArgumentNullException(nameof(EmailSettings));

        services.AddScoped<IOrderRepository, OrderRepos>();
        services.AddScoped<IPurchasePlanRepos, PurchasePlanRepos>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ICodeGeneration, CodeGeneration>();
        services.AddScoped<IApprovalLevelRepos, ApprovalLevelRepos>();
    }
}