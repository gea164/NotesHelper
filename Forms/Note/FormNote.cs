using NotesHelper.Common;
using NotesHelper.Forms.Encryption;
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
        private readonly Button buttonEncrypt;
        private readonly Button buttonSave;
        private readonly TreeHelper treeHelper;

        private Database.Models.Note? noteToUpdate = null;
        private string? password = null;
        private string contentSaved = "";

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public FormNote(Panel panel, Label topic, Label lastSaved, TextBox title,
            TextBox content, Button buttonClose, Button buttonSave, Button buttonEncrypt, 
            TreeHelper treeHelper
        )
        {
            this.panel = panel;
            this.topic = topic;
            this.lastSaved = lastSaved;
            this.title = title;
            this.content = content;
            this.buttonClose = buttonClose;
            this.buttonSave = buttonSave;
            this.buttonEncrypt = buttonEncrypt;
            this.treeHelper = treeHelper;

            //Events
            this.buttonClose.Click += ButtonClose_Click;
            this.buttonSave.Click += ButtonSave_Click;
            this.buttonEncrypt.Click += ButtonEncrypt_Click;

            this.title.TextChanged += Title_TextChanged;
            this.title.KeyDown += Title_KeyDown;

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
        // Used to add a new note.
        //---------------------------------------------------------------------
        public void ShowNewNote(NodeData topicData)
        {
            this.topic.Text = TextHelper.UnformatTopicTex(topicData.Text);
            this.title.Text = "";
            this.content.Text = "";
            this.contentSaved = "";
            this.password = null;

            buttonSave.Enabled = false;
            buttonSave.Text = "Add";
            
            this.lastSaved.Text = "";

            this.panel.Visible = true;
            title.Focus();
        }
        //---------------------------------------------------------------------
        // Used to update a note.
        //---------------------------------------------------------------------
        public void ShowUpdateNote(Database.Models.Note note, string parentTopic, string? password)
        {
            this.noteToUpdate = note;
            this.topic.Text = TextHelper.UnformatTopicTex(parentTopic);
            this.title.Text = note.Title;
            this.content.Text = note.Text;
            this.contentSaved = note.Text;
            this.password = password;   

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
        private void ButtonEncrypt_Click(object? sender, EventArgs e)
        {
            var formPassword = new FormSetPassword();
            formPassword.OnEnteredPassword += (password) =>
            {
                this.password = password;

                ButtonSave_Click(null, null);
            };

            formPassword.ShowDialog();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void ButtonClose_Click(object? sender, EventArgs e)
        {
            password = null;

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

                    contentSaved = content.Text;

                    noteToUpdate = Database.DA.Notes.Insert(
                        topicId: topicData.Id, 
                        title: formattedTitle, 
                        text: GetContent(content.Text, this.password),
                        isEncrypted: password != null,
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
        private static string GetContent(string content, string? password)
        {
            if (password != null)
            {
                var encryptedContent = Crypto.Encrypt(content, password);
                if (encryptedContent != null)
                {
                    return encryptedContent;
                }
            }
            return content;
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

                contentSaved = content.Text;

                noteToUpdate.Text = GetContent(content.Text, this.password);
                noteToUpdate.IsEncrypted = this.password != null;
                noteToUpdate.Title = formattedTitle;
                noteToUpdate.LastUpdate = lastUpdate;

                Database.DA.Notes.Update(
                    id: noteData.Id, 
                    title: noteToUpdate.Title, 
                    text: noteToUpdate.Text,
                    lastUpdate: noteToUpdate.LastUpdate,
                    isEncrypted: noteToUpdate.IsEncrypted
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
        private void Title_KeyDown(object? sender, KeyEventArgs e)
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
            }
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
                        || content.Text != contentSaved;
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
