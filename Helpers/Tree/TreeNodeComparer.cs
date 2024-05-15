using System.Collections;
using static NotesHelper.Helpers.Nodes.NodeData;

namespace NotesHelper.Helpers.Tree
{
    internal class TreeNodeComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x != null && y != null)
            {
                var xData = Nodes.NodeHelper.NodeDataFromTreeNode((TreeNode)x);
                var yData = Nodes.NodeHelper.NodeDataFromTreeNode((TreeNode)y);

                if (xData != null && yData != null)
                {
                    if(xData.Type == yData.Type)
                    {
                        return string.Compare(xData.Text, yData.Text);
                    }
                    return xData.Type == NodeTye.TOPIC ? -1 : 1;
                }
            }
            return 0;
        }
    }
}
