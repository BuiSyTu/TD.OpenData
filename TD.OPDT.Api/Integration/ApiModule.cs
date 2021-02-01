using System.Collections.Generic;
using TD.Core.Api.Mvc;
using TD.Core.Api.Mvc.Integration;
using Unity;
using System.Web.Http;
using System.Reflection;

namespace TD.Example.WebApi.Integration
{
    public sealed class ApiModule : IApiModule
    {
        public string GetPrefix()
        {
            return "webapi";
        }

        public ICollection<Assembly> GetAssemblies()
        {
            return new Assembly[] { typeof(ApiModule).Assembly };
        }

        public void RegisterApiConfig(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "qlUsers",
                routeTemplate: "webapi/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );
        }


        public void RegisterDIConfig(UnityContainer container)
        {
            container.RegisterFactory<ICoreServicesProvider>(c => new DefaultContextCoreServicesProvider());
        }
    }
}
