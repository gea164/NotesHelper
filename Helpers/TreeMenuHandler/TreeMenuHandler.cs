using NotesHelper.Forms.Note;
using NotesHelper.Helpers.Nodes;
using NotesHelper.Helpers.Tree;

namespace NotesHelper.Helpers.TreeMenuHandler
{
    internal class TreeMenuHandler
    {
        private readonly ContextMenuStrip menu;
        private readonly ToolStripMenuItem addNewTopic;
        private readonly ToolStripMenuItem addNewSubTopic;
        private readonly ToolStripMenuItem addNewNote;
        private readonly ToolStripMenuItem editTopic;
        private readonly TreeHelper treeHelper;
        private readonly FormNote formNote;
        

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public TreeMenuHandler(ContextMenuStrip menu,
            ToolStripMenuItem addNewTopic,
            ToolStripMenuItem addNewSubTopic,
            ToolStripMenuItem addNewNote,
            ToolStripMenuItem editTopic,
            TreeHelper treeHelper,
            FormNote formNote
        )
        {
            this.menu = menu;
            this.addNewTopic = addNewTopic;
            this.addNewSubTopic = addNewSubTopic;
            this.addNewNote = addNewNote;
            this.editTopic = editTopic;
            this.treeHelper = treeHelper;
            this.formNote = formNote;
            

            //Events
            this.menu.Opening += Menu_Opening;
            this.addNewTopic.Click += AddNewTopic_Click;
            this.addNewSubTopic.Click += AddNewSubTopic_Click;
            this.addNewNote.Click += AddNewNote_Click;
            this.editTopic.Click += EditTopic_Click;

        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void Menu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var isTopic = treeHelper.SelectedNode != null &&
                treeHelper.SelectedNodeData != null &&
                treeHelper.SelectedNodeData.Type == "Topic";
            
            editTopic.Visible = isTopic;
            addNewSubTopic.Visible = isTopic;
            addNewNote.Visible = isTopic;            
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void AddNewTopic_Click(object? sender, EventArgs e)
        {
            var form = new Forms.Topic.FormTopic();
            
            form.OnNewTopic += treeHelper.AddTopic;
            
            form.ShowDialog();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void AddNewSubTopic_Click(object? sender, EventArgs e)
        {
            if(treeHelper.SelectedNode != null && treeHelper.SelectedNodeData != null)
            {
                var form = new Forms.SubTopic.FormSubTopic(
                    treeHelper.SelectedNodeData.Id,
                    treeHelper.SelectedNode
                );
                form.OnNewSubTopic += treeHelper.AddSubTopic;
                form.ShowDialog();
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void AddNewNote_Click(object? sender, EventArgs e)
        {
            if (treeHelper.SelectedNode != null)
            {
                var topicData = NodeHelper.NodeDataFromTreeNode(treeHelper.SelectedNode);
                if (topicData != null) {
                    treeHelper.Enabled = false;
                    formNote.ShowNewNote(topicData: topicData);
                }
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void EditTopic_Click(object? sender, EventArgs e)
        {
            if (treeHelper.SelectedNode != null)
            {
                // If this is a topic
                if (treeHelper.SelectedNode.Parent == null)
                {
                    var formTopic = new Forms.Topic.FormTopic(treeHelper.SelectedNodeData);
                    formTopic.OnUpdatedTopic += treeHelper.UpdateSelectedTopicProps;
                    formTopic.ShowDialog();
                }
                // If this is a subtopic
                else if (treeHelper.SelectedNodeData != null)
                {
                    var formSubTopic = new Forms.SubTopic.FormSubTopic(
                        treeHelper.SelectedNode.Parent,
                        treeHelper.SelectedNodeData
                    );
                    formSubTopic.OnUpdatedSubTopic += treeHelper.UpdateSelectedTopicProps;
                    formSubTopic.ShowDialog();
                }                
            }
        }
    }
}
