using System.Web;
using System.Web.Http;
using DryIoc;
using DryIoc.WebApi;
using JessicasAquariumMonitor.Helpers.DependencyInjection;

namespace JessicasAquariumMonitor.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SetupDependencyInjection(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private static void SetupDependencyInjection(HttpConfiguration httpConfiguration)
        {
            var container = new Container()
                .WithWebApi(httpConfiguration, throwIfUnresolved: type => type.IsController());

            container.RegisterAll(WebModule.Instance);
        }
    }
}