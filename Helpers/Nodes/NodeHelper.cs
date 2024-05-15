using Newtonsoft.Json;
using NotesHelper.Database.Models;
using static NotesHelper.Helpers.Nodes.NodeData;

namespace NotesHelper.Helpers.Nodes
{
    /**
     * This class has helpers to allow us to convert the Node.Name string value (json)
     * into a NodeData object and viceversa.
     * The data to be store on the "Name" property of the node can be a "Topic"
     * or a "Note".
     */
    internal class NodeHelper
    {
        //---------------------------------------------------------------------  
        //---------------------------------------------------------------------  
        public static NodeData? NodeDataFromTreeNode(TreeNode? node)
        {
            if (node != null)
            {
                return JsonConvert.DeserializeObject<NodeData>(node.Name);
            }
            return null;
        }
        //---------------------------------------------------------------------  
        //---------------------------------------------------------------------  
        public static string ToKey(NodeData data)
        {
            return JsonConvert.SerializeObject(data);
        }
        //---------------------------------------------------------------------  
        //---------------------------------------------------------------------  
        public static string ToKey(Topic topic)
        {
            return ToKey(new NodeData
            {
                Id = topic.Id,
                Text = topic.Text,
                Type = NodeTye.TOPIC
            }
            );
        }
        //---------------------------------------------------------------------  
        //---------------------------------------------------------------------  
        public static string ToKey(Note note)
        {
            return ToKey(new NodeData
                {
                    Id = note.Id,
                    Text = note.Text,
                    Type = NodeTye.NOTE
            }
            );
        }
    }
}
