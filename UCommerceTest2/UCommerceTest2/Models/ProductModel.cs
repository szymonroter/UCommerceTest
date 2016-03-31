
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace UCommerceTest2.Models
{
    public class ProductModel : RenderModel
    {
        public ProductModel(IPublishedContent content) : base(content)
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}