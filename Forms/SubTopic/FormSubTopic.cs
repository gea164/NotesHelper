using NotesHelper.NodesHelper;

namespace NotesHelper.Forms.SubTopic
{
    public partial class FormSubTopic : Form
    {
        private TreeNode parentNode;

        public delegate void OnNewSubTopicEvent(Database.Models.Topic topic);
        public OnNewSubTopicEvent OnNewSubTopic;

        public FormSubTopic(TreeNode parentNode)
        {
            InitializeComponent();
            this.parentNode = parentNode;

            textBoxTopic.Text = parentNode.Text;

            buttonAccept.Enabled = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            var nodeData = NodeHelper.FromKey(parentNode.Name);

            Database.Models.Topic topic = Database.DA.Topics.Insert(new Database.Models.Topic
            {
                
                ParentId=nodeData.Id,
                Level=parentNode.Level + 1,
                Text = textBoxSubTopic.Text.Trim(),
            });
            OnNewSubTopic?.Invoke(topic);
            Close();
        }

        private void FormTopic_Shown(object sender, EventArgs e)
        {
            textBoxSubTopic.SelectAll();
            textBoxSubTopic.Focus();
        }

        private void textBoxSubTopic_TextChanged(object sender, EventArgs e)
        {
            buttonAccept.Enabled = textBoxSubTopic.Text.Trim().Length > 0;
        }

        private void FormSubTopic_Load(object sender, EventArgs e)
        {

        }
    }
}
