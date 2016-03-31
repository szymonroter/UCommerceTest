using System.Linq;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using UCommerce.Xslt;
using Umbraco.Web.Mvc;


namespace UCommerceTest2.Controllers
{
    public class BasketController : SurfaceController
    {
        public ActionResult AddToBasket(Product product)
        {
            UCommerce.Api.TransactionLibrary.AddToBasket(1,product.Sku,product.VariantSku);
            var basket = TransactionLibrary.GetBasket();
            Library.ExecuteBasketPipeline();
            
            return View("~/Views/Basket.cshtml");
        }
    }
}