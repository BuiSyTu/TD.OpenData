using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using TD.Core.Api.Mvc;
using TD.Core.Api.Mvc.Integration;
using TD.OPDT.Data.DataContext;
using TD.OPDT.Data.Repositories;
using Unity;

namespace TD.OPDT.Api.Integration
{
    public sealed class ApiModule : IApiModule
    {
        public string GetPrefix()
        {
            return "opdtapi";
        }

        public ICollection<Assembly> GetAssemblies()
        {
            return new Assembly[] { typeof(ApiModule).Assembly };
        }

        public void RegisterApiConfig(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "opdtapi",
                routeTemplate: "opdtapi/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );
        }

        public void RegisterDIConfig(UnityContainer container)
        {
            container
                // DbContext
                .RegisterType<OpenDataContext>()
                // Repositories
                .RegisterType<IFieldRepository, FieldRepository>()
                // Another
                .RegisterFactory<ICoreServicesProvider>(c => new DefaultContextCoreServicesProvider());
        }
    }
}
