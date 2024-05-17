using System.CodeDom;

namespace NotesHelper.Forms.Encryption
{
    public partial class FormSetPassword : Form
    {
        const int MIN_PASSWORD_LENGTH = 8;

        const char PASSWORD_CHAR = '*';
        const char NO_PASSWORD_CHAR = '\0';

        private static readonly Color ERROR_COLOR = Color.FromArgb(255, 192, 192);
        private static readonly Color INFO_COLOR = Color.FromArgb(224, 224, 224);
        private static readonly Color SUCCESS_COLOR = Color.FromArgb(192, 255, 192);

        public delegate void OnEnteredPasswordEvent(string password);
        public event OnEnteredPasswordEvent? OnEnteredPassword;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public FormSetPassword()
        {
            InitializeComponent();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void FormSetPassword_Load(object sender, EventArgs e)
        {
            SetButtonSaveStatus();
            SetMessage("Enter a password");
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void password_TextChanged(object sender, EventArgs e)
        {
            if (password.Text.Length < MIN_PASSWORD_LENGTH)
            {
                SetError($"Min password length is {MIN_PASSWORD_LENGTH}");
            }
            else
            {
                SetMessage("You must repeat the password");
            }
            SetButtonSaveStatus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void repeat_TextChanged(object sender, EventArgs e)
        {
            if (password.Text != repeat.Text)
            {
                SetError("Passwords mismatch");
            }
            else
            {
                SetSuccess("You can save the password");
            }
            SetButtonSaveStatus();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void show_CheckedChanged(object sender, EventArgs e)
        {
            if (show.Checked)
            {
                password.PasswordChar = NO_PASSWORD_CHAR;
                repeat.PasswordChar = NO_PASSWORD_CHAR;
            }
            else
            {
                password.PasswordChar = PASSWORD_CHAR;
                repeat.PasswordChar = PASSWORD_CHAR;
            }

        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void buttonSave_Click(object sender, EventArgs e)
        {
            OnEnteredPassword?.Invoke(password.Text);
            Close();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void SetButtonSaveStatus()
        {
            buttonSave.Enabled = password.Text.Length >= MIN_PASSWORD_LENGTH && password.Text == repeat.Text;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        private void SetMessage(string message)
        {
            this.info.Text = message;
            this.info.BackColor = INFO_COLOR;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void SetError(string message)
        {
            this.info.Text = message;
            this.info.BackColor = ERROR_COLOR;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void SetSuccess(string message)
        {
            this.info.Text = message;
            this.info.BackColor = SUCCESS_COLOR;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                repeat.Focus();
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void repeat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (buttonSave.Enabled)
                {
                    buttonSave_Click(null, null);
                }
            }
        }
    }
}
