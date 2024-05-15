using NotesHelper.Helpers.Nodes;
using NotesHelper.Helpers.Tree;

namespace NotesHelper.Forms.Note
{
    internal class FormNote
    {
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
            this.topic.Text = UnformatText(topicData.Text);
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
            this.topic.Text = UnformatText(parentTopic);
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
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private static string UnformatText(string text)
        {
            if (text.StartsWith("[ ") && text.EndsWith(" ]"))
            {
                return text.Substring(2, text.Length - 4);
            }
            return text;
        }
    }
}
