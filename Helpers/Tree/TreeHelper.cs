﻿using NotesHelper.Database.Models;
using NotesHelper.Helpers.Nodes;

namespace NotesHelper.Helpers.Tree
{
    internal class TreeHelper
    {
        private class ImageIndex
        {
            public static readonly int Arrow = 0;
            public static readonly int Folder = 1;
            public static readonly int Document = 2;
        };
        
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
                selectedNode.Text = title;
                selectedNode.Name = NodeHelper.ToKey(new NodeData
                    {
                        Id = index,
                        Text = title,
                        Type = "Note"
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
                selectedNode.Text = title;
                selectedNode.Name = NodeHelper.ToKey(new NodeData
                {
                    Id = index,
                    Text = title,
                    Type = "Topic"
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
                    var node = treeView.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text, ImageIndex.Folder);
                    LoadTopicNodes(node, topic.Id);
                    LoadNotesNodes(node, topic.Id);
                });

            treeView.Sort();
            treeView.ExpandAll();
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
            treeView.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text, ImageIndex.Folder);
            treeView.Sort();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void AddSubTopic(Topic topic)
        {
            if (selectedNode != null)
            {
                selectedNode.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text, ImageIndex.Folder);
                treeView.Sort();
                selectedNode.ExpandAll();
                
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void AddNote(Note note) 
        { 
            if (selectedNode != null)
            {
                selectedNode.Nodes.Add(key: NodeHelper.ToKey(note), text: note.Title, ImageIndex.Folder);
                treeView.Sort();
                selectedNode.ExpandAll();                
            }        
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void LoadTopicNodes(TreeNode parent, long parentId)
        {
            Database.DA.Topics.GetTopics(parentId)
                .ForEach(topic =>
                {
                    var node = parent.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text, ImageIndex.Folder);
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
                    node.Nodes.Add(key: NodeHelper.ToKey(note), text: note.Title, ImageIndex.Document);
                }
            );
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void TreeView_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedNode != null)
            {
                var text = selectedNode.Text;
                var childs = GetIds(selectedNode);

                if (MessageBox.Show($"Delete '{text}'?\nChilds: {childs.Count - 1}",
                    "Delete", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    Database.DA.Topics.Delete(childs);
                    Load();
                }
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void TreeView_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (SelectedNode != null && SelectedNode.Parent != null)
            {
                if (SelectedNode.Parent != null && SelectedNodeData != null && SelectedNodeData.Type == "Note")
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

        private List<string> GetIds(TreeNode node)
        {
            List<String> ids = new List<string>();
            if (node != null)
            {
                GetChildsId(node, ids);
            }
            return ids;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void GetChildsId(TreeNode node, List<string> list)
        {
            if (node != null)
            {
                var nodeData = NodeHelper.NodeDataFromTreeNode(node);
                if (nodeData != null)
                {
                    list.Add(nodeData.Id.ToString());
                    foreach (TreeNode child in node.Nodes)
                    {
                        GetChildsId(child, list);
                    }
                }
            }
        }
    }
}
