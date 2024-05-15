namespace NotesHelper.Helpers.Nodes
{
    /**
     * This class is used to store the data needed in each Node of the TreeView.
     * Because the TreeView only accepts text on the "Name" field, this class
     * is used to convert it into a Json string to be stored on the Node.Name field.
     */
    public class NodeData
    {
        public enum NodeTye { TOPIC, NOTE, NONE };
        public long Id = -1;
        public NodeTye Type = NodeTye.NONE;
        public string Text = "";
    }
}
