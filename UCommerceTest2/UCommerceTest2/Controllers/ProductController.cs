
using System.Web.Mvc;
using UCommerce.Api;
using Umbraco.Web.Mvc;

namespace UCommerceTest2.Controllers
{
    public class ProductController : SurfaceController
    {
        public ActionResult ShowDetails(int id)
        {
            var product = CatalogLibrary.GetProduct(id);
            
            return View("~/Views/Product.cshtml", product);
        }
    }
}