using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesHelper.Forms.Topic
{
    public partial class FormTopic : Form
    {
        public delegate void OnNewTopicEvent(Database.Models.Topic topic);
        public OnNewTopicEvent OnNewTopic;

        public FormTopic()
        {
            InitializeComponent();
            buttonAccept.Enabled = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxTopic_TextChanged(object sender, EventArgs e)
        {
            buttonAccept.Enabled = textBoxTopic.Text.Trim().Length > 0;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            var topic = textBoxTopic.Text.Trim();
            if (Database.DA.Topics.Exists(-1, topic) == false)
            {
                var insertedTopic = Database.DA.Topics.Insert(topic);
                OnNewTopic?.Invoke(insertedTopic);
                Close();
            }
        }

        private void FormTopic_Shown(object sender, EventArgs e)
        {
            textBoxTopic.SelectAll();
            textBoxTopic.Focus();
        }

        private void FormTopic_Load(object sender, EventArgs e)
        {

        }
    }
}
