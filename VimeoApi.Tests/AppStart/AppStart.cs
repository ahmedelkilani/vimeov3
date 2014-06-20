using Autofac;
using OAuth2.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.Mvc;
using VimeoApi.OAuth2;
using RestSharp;
using System.Web.Mvc;
using Subtext.TestLibrary;
using VimeoApi.OAuth2.Clients;
using VimeoApi.OAuth2.Clients.Impl;

namespace VimeoApi.Tests.Setup
{
    public class AppStart
    {
        public static HttpSimulator SetDependencyResolver()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder
                .RegisterAssemblyTypes(
                    Assembly.GetExecutingAssembly(),
                    Assembly.GetAssembly(typeof(OAuth2Client)),
                    Assembly.GetAssembly(typeof(RestClient)))
                .AsImplementedInterfaces().AsSelf();

            builder.RegisterType<ExtendedAuthorizationRoot>()
                .WithParameter(new NamedParameter("sectionName", "oauth2"));


            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            return new HttpSimulator().SimulateRequest();
        }

        /// <summary>
        /// Initializes and returns an instance of VimeoClient of the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InitVimeoClient<T>() where T : VimeoClient
        {
            var authorizationRoot = DependencyResolver.Current.GetService<ExtendedAuthorizationRoot>();
            var client = authorizationRoot.Clients.FirstOrDefault(p => p.Name == "VimeoClient" && p is T) as T;
            if (client is AuthenticatedViaRedirectVimeoClient)
            {
                // To retrieve these tokens navigate to /auth/display in the example website
                client.SetAccessToken(
                    AccessTokenConfig.Token, 
                    AccessTokenConfig.TokenType, 
                    AccessTokenConfig.Scop);
            }
            return client;
        }
    }
}
