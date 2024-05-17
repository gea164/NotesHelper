using NotesHelper.Common;

namespace NotesHelper.Forms.Encryption
{
    public partial class FormEnterPassword : Form
    {
        const int MIN_PASSWORD_LENGTH = 8;

        const char PASSWORD_CHAR = '*';
        const char NO_PASSWORD_CHAR = '\0';

        private static readonly Color ERROR_COLOR = Color.FromArgb(255, 192, 192);
        private static readonly Color INFO_COLOR = Color.FromArgb(224, 224, 224);

        public delegate void OnEnteredPasswordEvent(string decryptedContent, string password);
        public event OnEnteredPasswordEvent? OnEnteredPassword;

        public delegate void OnCloseEvent(bool result);

        private readonly string contentToDecrypt;
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public FormEnterPassword(string contentToDecrypt)
        {
            InitializeComponent();
            this.contentToDecrypt = contentToDecrypt;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void FormSetPassword_Load(object sender, EventArgs e)
        {
            SetButtonEnterStatus();
            SetMessage("Enter the password");
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
                SetMessage("Press ENTER to continue");
            }
            SetButtonEnterStatus();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void show_CheckedChanged(object sender, EventArgs e)
        {
            if (show.Checked)
            {
                password.PasswordChar = NO_PASSWORD_CHAR;
            }
            else
            {
                password.PasswordChar = PASSWORD_CHAR;
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
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            string? decryptedContent = Crypto.Decrypt(contentToDecrypt, password.Text);
            if (decryptedContent != null)
            {
                OnEnteredPassword?.Invoke(decryptedContent: decryptedContent, password: password.Text);
                Close();
            }
            else
            {
                SetError("Invalid password!");
                password.Focus();
            }
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void SetButtonEnterStatus()
        {
            buttonEnter.Enabled = password.Text.Length >= MIN_PASSWORD_LENGTH;
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
        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                if (buttonEnter.Enabled)
                {
                    buttonEnter_Click(null, null);
                }
            }
        }
    }
}
