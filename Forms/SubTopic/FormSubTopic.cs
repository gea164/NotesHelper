namespace NotesHelper.Forms.SubTopic
{
    public partial class FormSubTopic : Form
    {
        private readonly TreeNode parentNode;
        private readonly long parentId;

        public delegate void OnNewSubTopicEvent(Database.Models.Topic topic);
        public OnNewSubTopicEvent? OnNewSubTopic;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public FormSubTopic(TreeNode parentNode, long parentId)
        {
            InitializeComponent();
            this.parentNode = parentNode;
            this.parentId = parentId;

            textBoxTopic.Text = parentNode.Text;

            buttonAccept.Enabled = false;
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
            Database.Models.Topic topic = Database.DA.Topics.Insert(
                new Database.Models.Topic
                {
                
                    ParentId= parentId,
                    Level =parentNode.Level + 1,
                    Text = textBoxSubTopic.Text.Trim(),
                }
            );
            
            OnNewSubTopic?.Invoke(topic);
            Close();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void textBoxSubTopic_TextChanged(object sender, EventArgs e)
        {
            buttonAccept.Enabled = textBoxSubTopic.Text.Trim().Length > 0;
        }        
    }
}
