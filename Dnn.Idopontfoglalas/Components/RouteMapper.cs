using DotNetNuke.Web.Api;

namespace Dnn.Bce.Dnn.Idopontfoglalas.Components
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
                moduleFolderName: "Dnn.Idopontfoglalas",
                routeName: "default",
                url: "{controller}/{action}",
                namespaces: new[] { "Dnn.Bce.Dnn.Idopontfoglalas.Controllers" }
            );
        }
    }
}
