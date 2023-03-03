using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options => 
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    //validation rules, how our server will validate the token is a good token
                    ValidateIssuerSigningKey = true, //validate our token issuer signing key. this measn our server is going to check the token signing key and make sure it is valid based upon the issuer signing key (line below)
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])), //get the issuer signing key from config
                    ValidateIssuer = false, //issuer is our api server, needs to be passed to our token, we don't have it yet so false for now
                    ValidateAudience = false //infortion not passed to our token yet, so false
                };
            });

            return services;
        }
    }
}