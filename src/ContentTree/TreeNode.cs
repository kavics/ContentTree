using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentTree
{
    internal class TreeNode
    {
        public dynamic Content { get; set; }
        public TreeNode Parent { get; private set; }
        public List<TreeNode> Children { get; } = new List<TreeNode>();
        public string Name
        {
            get { return Content.Name; }
        }

        public void SetParent(TreeNode parent)
        {
            Parent = parent;
            parent.Children.Add(this);
        }
    }
}
