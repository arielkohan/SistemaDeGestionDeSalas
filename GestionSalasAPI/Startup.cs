using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using GestionSalasAPI.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Security.OAuth;
using GestionSalasAPI.Providers;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using GestionSalasAPI.Infrastructure;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(GestionSalasAPI.Startup))]

namespace GestionSalasAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

            //ConfigureAuth(app);
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {

            

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            // Configure the db context and user manager to use a single instance per request
            //Rest of code is removed for brevity

            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost:51154")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            
            var formatters = config.Formatters; //Algunas rutas usan esta configuracion
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            //settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var formatters2 = GlobalConfiguration.Configuration.Formatters; //Otras rutas usan esta configuracion.
            var jsonFormatter2 = formatters.JsonFormatter;
            var settings2 = jsonFormatter.SerializerSettings;
            //settings2.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }


        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = "http://localhost:51154";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }


    }
}
