using System.Web.Mvc;

namespace KnowledgeStore.Areas.MerchantArea
{
    public class MerchantAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MerchantArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MerchantArea_default",
                "MerchantArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}