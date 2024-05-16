using NotesHelper.Common;
using NotesHelper.Database.Models;
using NotesHelper.Helpers.Nodes;
using static NotesHelper.Helpers.Nodes.NodeData;

namespace NotesHelper.Helpers.Tree
{
    internal class TreeHelper
    {
        class ListOfIdsToDelete
        {
            public List<string> listOfTopicsIds = new List<string>();
            public List<string> listOfNotesIds = new List<string>();
        }

        private readonly TreeView treeView;
        private TreeNode? selectedNode = null;

        public delegate void OnNoteDoubleClickEvent(Note note, string parentTopic);
        public OnNoteDoubleClickEvent? OnNoteDoubleClick;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public TreeHelper(TreeView treeView)
        {
            this.treeView = treeView;
            this.treeView.TreeViewNodeSorter = new TreeNodeComparer();
            
            //Events
            this.treeView.AfterSelect += (_, e) => { 
                selectedNode = e.Node;
            };

            this.treeView.KeyDown += TreeView_KeyDown;
            this.treeView.MouseDoubleClick += TreeView_MouseDoubleClick;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public TreeNode? SelectedNode { get { return selectedNode; } }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void UpdateSelectedNoteProps(long index, string title)
        {
            if (selectedNode != null)
            {
                selectedNode.Text = TextHelper.FormatNoteText(title);
                selectedNode.Name = NodeHelper.ToKey(new NodeData
                    {
                        Id = index,
                        Text = title,
                        Type = NodeTye.NOTE
                }
                );
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void UpdateSelectedTopicProps(long index, string title)
        {
            if (selectedNode != null)
            {
                selectedNode.Text = TextHelper.FormatTopicText(title);
                selectedNode.Name = NodeHelper.ToKey(new NodeData
                {
                    Id = index,
                    Text = title,
                    Type = NodeTye.TOPIC
                }
                );
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public NodeData? SelectedNodeData
        {
            get { return NodeHelper.NodeDataFromTreeNode(selectedNode); }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public bool Enabled 
        { 
            get { return treeView.Enabled; }
            set { treeView.Enabled = value; } 
        }        
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void Load()
        {
            treeView.Nodes.Clear();

            Database.DA.Topics.GetTopics(-1)
                .ForEach(topic =>
                {
                    var node = treeView.Nodes.Add(
                        key: NodeHelper.ToKey(topic), 
                        text: TextHelper.FormatTopicText(topic.Text)
                    );

                    LoadTopicNodes(node, topic.Id);
                    LoadNotesNodes(node, topic.Id);
                });

            treeView.Sort();
            treeView.ExpandAll();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void Focus()
        {
            if (selectedNode != null)
            {
                treeView.SelectedNode = selectedNode;
            }
            treeView.Focus();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public bool ExistsNodeWithText(string text)
        {
            if (selectedNode != null)
            {
                foreach (TreeNode node in selectedNode.Nodes)
                {
                    if (node.Text.ToLower() == text.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void AddTopic(Topic topic)
        {
            var newNode = treeView.Nodes.Add(
                key: NodeHelper.ToKey(topic),
                text: TextHelper.FormatTopicText(topic.Text)
            );
            treeView.Sort();
            treeView.SelectedNode = newNode;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void AddSubTopic(Topic topic)
        {
            if (selectedNode != null)
            {
                var newNode = selectedNode.Nodes.Add(
                    key: NodeHelper.ToKey(topic),
                    text: TextHelper.FormatTopicText(topic.Text)
                );
                treeView.Sort();
                selectedNode.ExpandAll();
                treeView.SelectedNode = newNode;
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void AddNote(Note note) 
        { 
            if (selectedNode != null)
            {
                var newNode = selectedNode.Nodes.Add(
                    key: NodeHelper.ToKey(note),
                    text: TextHelper.FormatNoteText(note.Title)
                );

                treeView.Sort();
                selectedNode.ExpandAll();
                treeView.SelectedNode = newNode;
            }        
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void LoadTopicNodes(TreeNode parent, long parentId)
        {
            Database.DA.Topics.GetTopics(parentId)
                .ForEach(topic =>
                {
                    var node = parent.Nodes.Add(
                        key: NodeHelper.ToKey(topic),
                        text: TextHelper.FormatTopicText(topic.Text)
                    );
                    LoadTopicNodes(node, topic.Id);
                    LoadNotesNodes(node, topic.Id);
                }
            );
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void LoadNotesNodes(TreeNode node, long topicId)
        {
            Database.DA.Notes.GetNotes(topicId)
                .ForEach(note =>
                {
                    node.Nodes.Add(
                        key: NodeHelper.ToKey(note),
                        text: TextHelper.FormatNoteText(note.Title)
                    );
                }
            );
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void TreeView_KeyDown(object? sender, KeyEventArgs e)
        {
            if (selectedNode != null && SelectedNodeData != null)
            {
                switch(e.KeyCode)
                {
                    case Keys.Delete:
                        if (SelectedNodeData.Type == NodeTye.TOPIC)
                        {
                            DeleteTopicAndSubTree();
                        }
                        else
                        {
                            DelteNote();
                        }
                        break;

                    case Keys.Enter:
                        TreeView_MouseDoubleClick(null, null);
                        break;
                }
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void DeleteTopicAndSubTree()
        {
            var text = TextHelper.UnformatTopicTex(selectedNode.Text);

            var idsToDelete = GetIds(selectedNode);

            if (MessageBox.Show($"Delete '{text}'?" +
                $"\nChilds: {idsToDelete.listOfTopicsIds.Count - 1}" +
                $"\nNotes : {idsToDelete.listOfNotesIds.Count}",
                "Delete Topic",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                selectedNode = null;

                if (idsToDelete.listOfNotesIds.Count > 0)
                {
                    Database.DA.Notes.Delete(idsToDelete.listOfNotesIds);
                }
                if (idsToDelete.listOfTopicsIds.Count > 0)
                {
                    Database.DA.Topics.Delete(idsToDelete.listOfTopicsIds);
                }

                Load();
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void DelteNote()
        {
            var text = TextHelper.UnformatNoteTex(selectedNode.Text);

            if (MessageBox.Show($"Delete note '{text}'?", "Delete Note",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                selectedNode = null;

                Database.DA.Notes.Delete(SelectedNodeData.Id);
                Load();
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void TreeView_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (SelectedNode != null && SelectedNode.Parent != null)
            {
                if (SelectedNode.Parent != null && SelectedNodeData != null && SelectedNodeData.Type == NodeTye.NOTE)
                {
                    var note = Database.DA.Notes.GetNote(SelectedNodeData.Id);
                    if (note != null)
                    {
                        Enabled = false;
                        OnNoteDoubleClick?.Invoke(note, SelectedNode.Parent.Text);
                    }
                }
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private ListOfIdsToDelete GetIds(TreeNode node)
        {
            ListOfIdsToDelete ids = new ListOfIdsToDelete();
            if (node != null)
            {
                GetChildsId(node, ids);
            }
            return ids;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void GetChildsId(TreeNode node, ListOfIdsToDelete ids)
        {
            if (node != null)
            {
                var nodeData = NodeHelper.NodeDataFromTreeNode(node);
                if (nodeData != null)
                {
                    if (nodeData.Type == NodeTye.TOPIC)
                    {
                        ids.listOfTopicsIds.Add(nodeData.Id.ToString());
                        foreach (TreeNode child in node.Nodes)
                        {
                            GetChildsId(child, ids);
                        }
                    }
                    else
                    {
                        ids.listOfNotesIds.Add(nodeData.Id.ToString());
                    }                    
                }
            }
        }
    }
}
