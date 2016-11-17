using System.Web.Mvc;
using System.Web.Routing;

namespace MyPhotoViewer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Album",
                url: "Album/{photoAlbumId}/{action}",
                defaults: new { controller = "Album", action = "Index" },
                constraints: new { photoAlbumId = @"\d+" });

            routes.MapRoute(
                name: "AlbumPhoto",
                url: "Album/{photoAlbumId}/Photo/{photoId}",
                defaults: new { controller = "Album", action = "Photo" },
                constraints: new { photoAlbumId = @"\d+", photoId = @"\d+" });

            routes.MapRoute(
                name: "Photo",
                url: "Photo/{photoId}/{action}",
                defaults: new { controller = "Photo", photoId = UrlParameter.Optional},
                constraints: new { photoId = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
