using System.ComponentModel;
using System.Dynamic;
using ContentTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TreeNodeTests
    {
        [TestMethod]
        public void TreeNode_Name()
        {
            var treeNode = new TreeNode { Content = CreateContent("Node1") };
            Assert.AreEqual("Node1", treeNode.Name);
        }
        [TestMethod]
        public void TreeNode_SetParent()
        {
            var parentNode = new TreeNode { Content = CreateContent("N1") };
            var childNode = new TreeNode { Content = CreateContent("N2") };

            childNode.SetParent(parentNode);

            Assert.AreEqual(parentNode, childNode.Parent);
            Assert.AreEqual(1, parentNode.Children.Count);
            Assert.AreEqual(childNode, parentNode.Children[0]);
        }

        private dynamic  CreateContent(string name)
        {
            dynamic content = new ExpandoObject();
            content.Name = name;
            return content;
        }
    }
}
