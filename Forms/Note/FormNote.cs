using NotesHelper.Helpers.Nodes;
using NotesHelper.Helpers.Tree;

namespace NotesHelper.Forms.Note
{
    internal class FormNote
    {
        private readonly Panel panel;
        private readonly Label topic;
        private readonly TextBox title;
        private readonly TextBox content;
        private readonly Button buttonClose;
        private readonly Button buttonSave;
        private readonly TreeHelper treeHelper;

        private Database.Models.Note? noteToUpdate = null;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public FormNote(Panel panel, Label topic, TextBox title, TextBox content,
            Button buttonClose, Button buttonSave, TreeHelper treeHelper
        )
        {
            this.panel = panel;
            this.topic = topic;
            this.title = title;
            this.content = content;
            this.buttonClose = buttonClose;
            this.buttonSave = buttonSave;
            this.treeHelper = treeHelper;

            //Events
            this.buttonClose.Click += ButtonClose_Click;
            this.buttonSave.Click += ButtonSave_Click;
            this.title.TextChanged += Title_TextChanged;
            this.content.TextChanged += Content_TextChanged;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void Hide()
        {
            this.panel.Visible = false; 
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void ShowNewNote(NodeData topicData)
        {
            this.topic.Text = topicData.Text;
            this.title.Text = "";
            this.content.Text = "";

            buttonSave.Enabled = false;
            buttonSave.Text = "Add";

            this.panel.Visible = true;
            title.Focus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void ShowUpdateNote(Database.Models.Note note, string parentTopic)
        {
            this.noteToUpdate = note;
            this.topic.Text = parentTopic;
            this.title.Text = note.Title;
            this.content.Text = note.Text;

            buttonSave.Enabled = false;
            buttonSave.Text = "Save";

            this.panel.Visible = true;
            title.Focus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void ButtonSave_Click(object? sender, EventArgs e)
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
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void ButtonClose_Click(object? sender, EventArgs e)
        {
            Hide();
            treeHelper.Enabled = true;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void AddNewNote()
        {
            var formattedTitle = title.Text.Trim();
            if (treeHelper.ExistsNodeWithText(formattedTitle))
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
                var topicData = treeHelper.SelectedNodeData;
                if (topicData != null)
                {
                    var note = Database.DA.Notes.Insert(topicId: topicData.Id, title: formattedTitle, text: content.Text);
                    
                    treeHelper.AddNote(note);

                    ButtonClose_Click(null, null);
                }
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void UpdateNote()
        {
            var formattedTitle = title.Text.Trim();
            var noteData = treeHelper.SelectedNodeData;
            if (noteData != null)
            {

                Database.DA.Notes.Update(noteData.Id, title: formattedTitle, text: content.Text);

                treeHelper.UpdateSelectedNoteProps(formattedTitle, noteData.Id);

                ButtonClose_Click(null, null);
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void Title_TextChanged(object? sender, EventArgs e)
        {
            SetButtonSaveEnabledStatus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void Content_TextChanged(object? sender, EventArgs e)
        {
            SetButtonSaveEnabledStatus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void SetButtonSaveEnabledStatus()
        {
            var formattedTitle = title.Text.Trim();
            if (formattedTitle.Length > 0)
            {
                if (noteToUpdate != null)
                {
                    buttonSave.Enabled = formattedTitle != noteToUpdate.Title
                        || content.Text != noteToUpdate.Text;
                }
                else
                {
                    buttonSave.Enabled = true;
                }
            }
            else
            {
                buttonSave.Enabled = false;
            }              
        }
    }
}
