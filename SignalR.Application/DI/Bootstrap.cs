using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Application.Features.Auth;
using SignalR.Application.Features.System;
using SignalR.Application.Features.System.Validation;
using SignalR.Application.Validation;
using System.Reflection;

namespace SignalR.Application.DI
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplicationStrapping(this IServiceCollection services)
        {

            #region Services

            //  System // 
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddScoped<IOTPService, OTPService>();


            // Auth // 
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            #endregion


            #region Validators

            // System // 
            services.AddScoped<IValidator<ConfirmMailOTPDto>, ConfirmEmailOTPValidation>();
            
            // Auth // 
            services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordValidation>();
            services.AddScoped<IValidator<LoginDto>, LoginValidation>();
            services.AddScoped<IValidator<RegisterDto>, RegisterValidation>();


            #endregion


            #region Mappster

            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);

            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
            
            services.AddScoped<IMapper, ServiceMapper>();

            #endregion


            return services;
        }
    }
}
