using System;
using System.Web.Http;
using System.Web.Routing;
using AHM.BusinessLayer.Interfaces;
using AHM.DependencyInjection;
using AHM.WebAPI;
using AHM.WebAPI.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace AHM.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var container = new UnityContainer();
            UnityConfiguration.Run(container);

            var unityResolver = new UnityResolver(container);
            config.DependencyResolver = unityResolver;

            ConfigureOAuth(app, unityResolver);

            WebApiConfig.Register(config);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app, UnityResolver resolver)
        {
            UnityConfiguration.ConfigureAuthentication(app);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider((IUserService)resolver.GetService(typeof(IUserService)))
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}