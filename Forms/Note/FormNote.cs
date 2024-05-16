using NotesHelper.Common;
using NotesHelper.Helpers.Nodes;
using NotesHelper.Helpers.Tree;

namespace NotesHelper.Forms.Note
{
    internal class FormNote
    {
        const string TAB = "    ";

        private readonly Panel panel;
        private readonly Label topic;
        private readonly Label lastSaved;
        private readonly TextBox title;
        private readonly TextBox content;
        private readonly Button buttonClose;
        private readonly Button buttonSave;
        private readonly TreeHelper treeHelper;

        private Database.Models.Note? noteToUpdate = null;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public FormNote(Panel panel, Label topic, Label lastSaved, TextBox title, 
            TextBox content, Button buttonClose, Button buttonSave, TreeHelper treeHelper
        )
        {
            this.panel = panel;
            this.topic = topic;
            this.lastSaved = lastSaved;
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
            this.content.KeyDown += Content_KeyDown;
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
            this.topic.Text = TextHelper.UnformatTopicTex(topicData.Text);
            this.title.Text = "";
            this.content.Text = "";

            buttonSave.Enabled = false;
            buttonSave.Text = "Add";
            
            this.lastSaved.Text = "";

            this.panel.Visible = true;
            title.Focus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public void ShowUpdateNote(Database.Models.Note note, string parentTopic)
        {
            this.noteToUpdate = note;
            this.topic.Text = TextHelper.UnformatTopicTex(parentTopic);
            this.title.Text = note.Title;
            this.content.Text = note.Text;

            buttonSave.Enabled = false;
            buttonSave.Text = "Save";

            this.lastSaved.Text = "Last Update: " + note.LastUpdate;

            this.panel.Visible = true;
            content.Focus();
            content.SelectionStart = content.Text.Length;
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

            SetButtonSaveEnabledStatus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void ButtonClose_Click(object? sender, EventArgs e)
        {
            Hide();
            treeHelper.Enabled = true;
            treeHelper.Focus();
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
                    var lastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    noteToUpdate = Database.DA.Notes.Insert(
                        topicId: topicData.Id, 
                        title: formattedTitle, 
                        text: content.Text,
                        lastUpdate: lastUpdate
                    );
                    
                    treeHelper.AddNote(noteToUpdate);
                    
                    lastSaved.Text = "Last Update: " + lastUpdate;

                    this.buttonSave.Text = "Save";
                }
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void UpdateNote()
        {
            var formattedTitle = title.Text.Trim();
            var noteData = treeHelper.SelectedNodeData;
            if (noteData != null && noteToUpdate != null)
            {
                var lastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                
                noteToUpdate.Text = content.Text;
                noteToUpdate.Title = formattedTitle;
                noteToUpdate.LastUpdate = lastUpdate;

                Database.DA.Notes.Update(
                    id: noteData.Id, 
                    title: noteToUpdate.Title, 
                    text: noteToUpdate.Text,
                    lastUpdate: noteToUpdate.LastUpdate
                );

                treeHelper.UpdateSelectedNoteProps(noteData.Id, formattedTitle);

                lastSaved.Text = "Last Update: " + lastUpdate;
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
        private void Content_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (buttonSave.Enabled)
                    {
                        if (AskToSaveChanges())
                        {
                            ButtonSave_Click(null, null);
                        }
                    }
                    ButtonClose_Click(null, null);
                    break;

                case Keys.S:
                    if (e.Control && buttonSave.Enabled)
                    {
                        ButtonSave_Click(null, null);
                    }
                    break;

                case Keys.Tab:
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    int pos = content.SelectionStart;

                    if (content.SelectedText.Length > 0)
                    {
                        content.Text = content.Text.Remove(content.SelectionStart, content.SelectionLength);
                    }
                    content.Text = content.Text.Insert(pos, TAB);
                    content.SelectionStart = pos + TAB.Length;
                    break;
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private static bool AskToSaveChanges()
        {
            return MessageBox.Show(
                      "Do you want to save the current changes?",
                      "Changes were not saved!",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Information
                    ) == DialogResult.Yes;
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
