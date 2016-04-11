using System.Collections.Generic;
using umbraco.NodeFactory;

namespace UCommerceTest2.Helpers
{
    public static class Navigation
    {
        public static List<Node> GetAllNodes()
        {
            var pageList = new List<Node>();

            GetNodes(pageList);
            return pageList;
        }

        private static void GetNodes(List<Node> pageList, Node node = null)
        {
            if (node == null)
            {
                node = new Node(-1);
            }
            foreach (Node child in node.Children)
            {
                pageList.Add(child);
                if (child.Children.Count > 0)
                    GetNodes(pageList, child);
            }
        }
    }
}