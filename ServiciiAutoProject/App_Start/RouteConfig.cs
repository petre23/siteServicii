using System.Web.Mvc;
using System.Web.Routing;

namespace ServiciiAutoProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Records", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
