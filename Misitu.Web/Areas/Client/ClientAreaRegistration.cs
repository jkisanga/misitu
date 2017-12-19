using System.Web.Mvc;
using System.Web.Routing;

namespace Misitu.Web.Areas.Client
{
    public class ClientAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Client";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
                   
        }
    }
}