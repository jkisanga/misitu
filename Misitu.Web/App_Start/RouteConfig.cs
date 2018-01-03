using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;


namespace Misitu.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           
           
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;


            //ASP.NET Web API Route Config
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            routes.MapRoute(
                "Client_default",
                "Client/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional}
                  
            ).DataTokens.Add("area", "Client");



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                
            );

            routes.MapRoute(
              name: "Account",
              url: "Account/{action}/{id}",
              defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }            
          );



        }
    }
}
