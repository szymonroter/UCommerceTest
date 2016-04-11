using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.Common;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Pipelines;
using UCommerce.Xslt;
using Umbraco.Web.Mvc;


namespace UCommerceTest2.Controllers
{
    public class BasketController : SurfaceController
    {
        public ViewResult ShowBasket()
        {
            return View("~/Views/Basket.cshtml");
        }

        public ActionResult AddToBasket()
        {
            var id = int.Parse(Request.Form["id"]);

            var product = CatalogLibrary.GetProduct(id);
            var uniqueVariants = from v in product.Variants.SelectMany(p => p.ProductProperties)
                                 where v.ProductDefinitionField.DisplayOnSite
                                 group v by v.ProductDefinitionField into g
                                 select g;

            var quantity = int.Parse(Request.Form["quantity"]);
            IEnumerable<Product> variants = product.Variants;

            if (variants != null && variants.Any())
            {
                foreach (var prop in uniqueVariants)
                {
                    var propName = prop.Key.Name.ToLower();
                    var controlName = string.Format("variation-{0}", propName);
                    if (Request.Form[controlName].IsNullOrEmpty())
                        return RedirectToAction("ShowDetails", "Product", new {id = Request.Form["id"]});

                    variants = variants.Where(v => v.ProductProperties.Any(p => p.Value == Request.Form[controlName]));
                }

                var variantSku = variants.FirstOrDefault().VariantSku;
                

                TransactionLibrary.AddToBasket(quantity, Request.Form["sku"], variantSku);
            }
            else
            {
                TransactionLibrary.AddToBasket(quantity, Request.Form["sku"]);
            }

            Library.ExecuteBasketPipeline();

            return View("~/Views/Basket.cshtml");
        }

        public ActionResult Clear()
        {
            var basket = TransactionLibrary.GetBasket().PurchaseOrder;
            
            if (basket.IsBasket)
            {
                basket.Delete();
            }
            return View("~/Views/Basket.cshtml");
        }

        public ActionResult RemoveItem(int id)
        {
            var order = TransactionLibrary.GetBasket(true).PurchaseOrder;
            order.RemoveOrderLine(order.OrderLines.FirstOrDefault(o => o.Id == id));
            order.Save();
            return View("~/Views/Basket.cshtml");
        }

        public ActionResult Checkout()
        {
            return View("~/Views/AddressInformation.cshtml");
        }

        public ActionResult ContinueCheckout(OrderAddress address)
        {
            var order = TransactionLibrary.GetBasket().PurchaseOrder;
            address.PurchaseOrder = order;
            address.Country = Country.Get(8);
            order.BillingAddress = address;
            order.Save();

            //Shipment
            var shippingMethod = ShippingMethod.SingleOrDefault(x => x.Name == "Courier");
            
            var shipment = order.CreateShipment(shippingMethod, address);
            order.OrderLines.ForEach(x => shipment.AddOrderLine(x));
            shipment.Save();

            PipelineFactory.Create<PurchaseOrder>("basket").Execute(order);
            
            //Payment method
            var paymentMethod = PaymentMethod.SingleOrDefault(x => x.Name == "Account");
            
            Library.CreatePayment(paymentMethod.Id, -1, true, false);
            
            order.Delete();

            return View("~/Views/Payment.cshtml");
        }
    }
}