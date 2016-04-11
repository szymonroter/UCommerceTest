using System.Collections.Generic;
using System.Linq;
using UCommerce.Api;
using UCommerce.EntitiesV2;

namespace UCommerceTest2.Products
{
    public static class Products
    {
        public static List<Product> GetAllProducts()
        {
            var catalogs = CatalogLibrary.GetAllCatalogs();
            var productList = new List<Product>();

            foreach (var catalog in catalogs)
            {
                foreach (var category in catalog.Categories)
                {
                    productList.AddRange(category.Products);
                }
            }

            return productList;
        }

        public static List<Product> GetThreeLastProducts()
        {
            return GetAllProducts().OrderByDescending(p => p.CreatedOn).Take(3).ToList();
        }
    }
}