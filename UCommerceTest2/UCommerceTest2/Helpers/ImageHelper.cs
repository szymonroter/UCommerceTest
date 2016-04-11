using ServiceStack.Common;
using Umbraco.Web;

namespace UCommerceTest2.Helpers
{
    public static class ImageHelper
    {
        public static string GetProductImageUrl(string id)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            return id.IsNullOrEmpty() ? umbracoHelper.TypedMedia(2060).Url : umbracoHelper.TypedMedia(id).Url; //hardcoded image placeholder id
        }
    }
}