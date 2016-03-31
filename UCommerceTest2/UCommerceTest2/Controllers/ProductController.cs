using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerceTest2.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace UCommerceTest2.Controllers
{
    public class ProductController : SurfaceController
    {
        public ActionResult ShowDetails(int id)
        {
            var product = CatalogLibrary.GetProduct(id);

            return View("~/Views/ProductPage.cshtml", product);
        }
    }
}