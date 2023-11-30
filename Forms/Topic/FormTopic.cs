using NotesHelper.Helpers.Nodes;

namespace NotesHelper.Forms.Topic
{
    public partial class FormTopic : Form
    {
        private NodeData? topicToUpdate = null;

        public delegate void OnNewTopicEvent(Database.Models.Topic topic);
        public OnNewTopicEvent? OnNewTopic;

        public delegate void OnUpdatedTopicEvent(long id, string text);
        public OnUpdatedTopicEvent? OnUpdatedTopic;

        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public FormTopic(NodeData? topicToUpdate = null)
        {
            InitializeComponent();
            this.topicToUpdate = topicToUpdate;

            this.buttonAccept.Enabled = false;
            if (topicToUpdate != null )
            {
                this.Text = "Update Topic";
                this.textBoxTopic.Text = topicToUpdate.Text;
                this.buttonAccept.Text = "Save";
            }
            else
            {
                this.Text = "Add New Topic";
                this.buttonAccept.Text = "Add";
            }
        }
        
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private void FormTopic_Shown(object sender, EventArgs e)
        {
            textBoxTopic.SelectAll();
            textBoxTopic.Focus();
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private void FormTopic_Load(object sender, EventArgs e)
        {

        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            var topic = textBoxTopic.Text.Trim();
            if (Database.DA.Topics.Exists(-1, topic) == false)
            {
                if (topicToUpdate == null)
                {
                    var insertedTopic = Database.DA.Topics.Insert(topic);
                    OnNewTopic?.Invoke(insertedTopic);
                }
                else
                {
                    Database.DA.Topics.Update(topicToUpdate.Id, topic);
                    OnUpdatedTopic?.Invoke(topicToUpdate.Id, topic);
                }                
                Close();
            }
            else
            {
                MessageBox.Show(
                    $"Topic '{topic}' already exists.",
                    "Duplicated Topic",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                textBoxTopic.SelectAll();
                textBoxTopic.Focus();
            }
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private void textBoxTopic_TextChanged(object sender, EventArgs e)
        {
            var formattedTopic = textBoxTopic.Text.Trim();
            if (topicToUpdate != null)
            {
                buttonAccept.Enabled = formattedTopic.Length > 0 &&
                    formattedTopic != topicToUpdate.Text;
            }
            else
            {
                buttonAccept.Enabled = formattedTopic.Length > 0;
            }                 
        }
    }
}
