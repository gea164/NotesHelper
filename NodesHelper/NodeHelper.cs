using Newtonsoft.Json;
using NotesHelper.Database.Models;

namespace NotesHelper.NodesHelper
{
    internal class NodeHelper
    {

        public static string ToKey(Topic topic)
        {
            return JsonConvert.SerializeObject(new NodeData
                {
                    Id = topic.Id,
                    Text = topic.Text,
                    Type = "Topic"
                }
            );
        }

        public static string ToKey(Note note)
        {
            return JsonConvert.SerializeObject(new NodeData
                {
                    Id = note.Id,
                    Text = note.Text,
                    Type = "Note"
                }
            );
        }
        public static NodeData FromKey(string key)
        {
            return JsonConvert.DeserializeObject<NodeData>(key);
        }
    }
}
