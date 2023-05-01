



using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BasicNet6Template.Security;
using BasicNet6Template.Services.User;

public static class BuilderServices{
    

    public static void BuildService(WebApplicationBuilder builder){

        var services = builder.Services;
        var configuration = builder.Configuration;

        ConfigureJWT(services, configuration);
        ConfigureApiKey(services, configuration);

        InjectDependencies(services, configuration);

        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


    }

    private static void InjectDependencies(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtService, JwtService>();
    }

    private static void ConfigureJWT(IServiceCollection services, ConfigurationManager Configuration){
        var jwtSettings = Configuration.GetSection("Jwt");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
            };
        });
    }

    private static void ConfigureApiKey(IServiceCollection services, ConfigurationManager Configuration){
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        var section = Configuration.GetSection("ApiKeys").AsEnumerable();
        services.AddSingleton<IApiKeyService>(sp => new ApiKeyService(section));

    }

}



