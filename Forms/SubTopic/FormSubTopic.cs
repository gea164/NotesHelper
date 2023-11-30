using NotesHelper.Database.Models;
using NotesHelper.Database;
using NotesHelper.Helpers.Nodes;

namespace NotesHelper.Forms.SubTopic
{
    public partial class FormSubTopic : Form
    {
        private readonly TreeNode parentNode;
        private readonly long parentId;
        private readonly NodeData? subtopicToUpdate = null;

        public delegate void OnNewSubTopicEvent(Database.Models.Topic topic);
        public OnNewSubTopicEvent? OnNewSubTopic;

        public delegate void OnUpdatedSubTopicEvent(long id, string text);
        public OnUpdatedSubTopicEvent? OnUpdatedSubTopic;
        //---------------------------------------------------------------------
        // Constructor to Add a new Sub-Topic
        //---------------------------------------------------------------------
        public FormSubTopic( TreeNode parentNode, NodeData subtopicToUpdate)
            : this(-1, parentNode, subtopicToUpdate)
        {

        }

        //---------------------------------------------------------------------
        // Constructor to Add a new Sub-Topic
        //---------------------------------------------------------------------
        public FormSubTopic(
            long parentId, 
            TreeNode parentNode,
            NodeData? subtopicToUpdate = null)
        {
            InitializeComponent();
            this.parentNode = parentNode;
            this.parentId = parentId;
            this.subtopicToUpdate = subtopicToUpdate;

            textBoxTopic.Text = parentNode.Text;
            
            buttonAccept.Enabled = false;

            if (subtopicToUpdate == null)
            {
                this.Text = "Add New Sub-Topic";
                buttonAccept.Text = "Add";
            }
            else
            {
                this.Text = "Update Sub-Topic";
                textBoxSubTopic.Text = subtopicToUpdate.Text;
                buttonAccept.Text = "Save";
            }            
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void FormTopic_Shown(object sender, EventArgs e)
        {
            textBoxSubTopic.SelectAll();
            textBoxSubTopic.Focus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void FormSubTopic_Load(object sender, EventArgs e)
        {

        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (subtopicToUpdate == null)
            {
                AddNewSubTopic();
            }
            else
            {
                UpdateSubTopic();
            }
           
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void AddNewSubTopic()
        {
            Database.Models.Topic topic = Database.DA.Topics.Insert(
               new Database.Models.Topic
               {
                   ParentId = parentId,
                   Level = parentNode.Level + 1,
                   Text = textBoxSubTopic.Text.Trim(),
               }
           );

            OnNewSubTopic?.Invoke(topic);
            Close();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void UpdateSubTopic()
        {
            Database.DA.Topics.Update(subtopicToUpdate.Id, textBoxSubTopic.Text.Trim());
            OnUpdatedSubTopic?.Invoke(subtopicToUpdate.Id, textBoxSubTopic.Text.Trim());
            Close();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void textBoxSubTopic_TextChanged(object sender, EventArgs e)
        {
            var subtopic = textBoxSubTopic.Text.Trim();
            if (subtopicToUpdate  == null)
            {
                buttonAccept.Enabled = subtopic.Length > 0;
            }
            else
            {
                buttonAccept.Enabled = subtopic.Length > 0 
                    && subtopic != subtopicToUpdate.Text;
            }            
        }        
    }
}
