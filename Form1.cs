using NotesHelper.NodesHelper;
using System.Runtime.Serialization.Formatters.Binary;

namespace NotesHelper
{
    public partial class Form1 : Form
    {
        private TreeNode? selectedNode = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void addNewTopic_Click(object sender, EventArgs e)
        {
            var form = new Forms.Topic.FormTopic();
            form.OnNewTopic += (topic =>
            {
                treeView.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text);
            });
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTopics();
            panel.Visible = false;
        }

        private void LoadTopics()
        {
            treeView.Nodes.Clear();

            Database.DA.Topics.GetTopics(-1)
                .ForEach(topic =>
                {
                    var x = treeView.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text);
                    AddSubNodes(x, topic.Id);
                });

            treeView.ExpandAll();
        }

        private void AddSubNodes(TreeNode parent, long parentId)
        {
            Database.DA.Topics.GetTopics(parentId)
                .ForEach(topic =>
                {
                    var x = parent.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text);
                    AddSubNodes(x, topic.Id);
                });
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedNode = e.Node;
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            addNewSubtopic.Visible = selectedNode != null;
        }

        private void addNewSubtopic_Click(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                var form = new Forms.SubTopic.FormSubTopic(selectedNode);
                form.OnNewSubTopic += (topic =>
                {
                    selectedNode.Nodes.Add(key: NodeHelper.ToKey(topic), text: topic.Text);
                    selectedNode.ExpandAll();
                });
                form.ShowDialog();
            }
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedNode != null)
            {
                var text = selectedNode.Text;
                var childs = GetIds(selectedNode);

                if (MessageBox.Show($"Delete '{text}'?\nChilds: {childs.Count - 1}", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    Database.DA.Topics.Delete(childs);
                    LoadTopics();
                }
            }
        }

        private List<string> GetIds(TreeNode node)
        {
            List<String> ids = new List<string>();
            if (node != null)
            {
                GetChildsId(node, ids);
            }
            return ids;
        }

        private void GetChildsId(TreeNode node, List<string> list)
        {
            if (node != null)
            {
                list.Add(NodeHelper.FromKey(node.Name).Id.ToString());
                foreach (TreeNode child in node.Nodes)
                {
                    GetChildsId(child, list);
                }
            }
        }

        private void addNewDocument_Click(object sender, EventArgs e)
        {
            panel.Visible = true;
            treeView.Enabled = false;

            buttonSave.Enabled = false;
            buttonSave.Text = "Add";

            labelTopic.Text = selectedNode.Text;
            textBoxContent.Text = "";
            textBoxTitle.Text = "";

            textBoxTitle.Focus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (buttonSave.Text == "Add")
            {
                AddNewNote();
            }
            else
            {
                UpdateNote();
            }
        }

        private void AddNewNote()
        {
            if (ExistsTitle(textBoxTitle.Text.Trim()))
            {
                MessageBox.Show(
                    "Duplicated title for this topic.",
                    "Duplicated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                var title = textBoxTitle.Text.Trim();
                var parentData = NodeHelper.FromKey(selectedNode.Name);

                var note = Database.DA.Notes.Insert(parentId: parentData.Id, text: textBoxContent.Text, title: title);
                
                selectedNode.Nodes.Add(key: NodeHelper.ToKey(note), text: title);
                selectedNode.ExpandAll();

                buttonClose_Click(null, null);
            }
        }
    
        private bool ExistsTitle(string title)
        {
            foreach (TreeNode node in selectedNode.Nodes)
            {
                if (node.Text.ToLower() == title.ToLower())
                {
                    return true;
                }
            }
            return false;
        }


        private void UpdateNote()
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            treeView.Enabled = true;
            panel.Visible = false;
        }

        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = textBoxTitle.Text.Trim().Length > 0;
        }
    }
}