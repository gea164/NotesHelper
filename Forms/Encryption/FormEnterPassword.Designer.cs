namespace NotesHelper.Forms.Encryption
{
    partial class FormEnterPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            password = new TextBox();
            label1 = new Label();
            buttonCancel = new Button();
            buttonEnter = new Button();
            label3 = new Label();
            show = new CheckBox();
            info = new Label();
            SuspendLayout();
            // 
            // password
            // 
            password.Location = new Point(87, 37);
            password.MaxLength = 100;
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(221, 23);
            password.TabIndex = 0;
            password.TextChanged += password_TextChanged;
            password.KeyDown += password_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 40);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 1;
            label1.Text = "Password:";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(87, 94);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonEnter
            // 
            buttonEnter.Location = new Point(233, 94);
            buttonEnter.Name = "buttonEnter";
            buttonEnter.Size = new Size(75, 23);
            buttonEnter.TabIndex = 3;
            buttonEnter.Text = "Enter";
            buttonEnter.UseVisualStyleBackColor = true;
            buttonEnter.Click += buttonEnter_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 65);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 6;
            label3.Text = "Show:";
            // 
            // show
            // 
            show.AutoSize = true;
            show.Location = new Point(87, 66);
            show.Name = "show";
            show.Size = new Size(15, 14);
            show.TabIndex = 2;
            show.UseVisualStyleBackColor = true;
            show.CheckedChanged += show_CheckedChanged;
            // 
            // info
            // 
            info.BackColor = Color.FromArgb(224, 224, 224);
            info.BorderStyle = BorderStyle.Fixed3D;
            info.Location = new Point(0, 0);
            info.Name = "info";
            info.Size = new Size(320, 25);
            info.TabIndex = 8;
            info.Text = "label4";
            info.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormEnterPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(320, 124);
            Controls.Add(info);
            Controls.Add(show);
            Controls.Add(label3);
            Controls.Add(buttonEnter);
            Controls.Add(buttonCancel);
            Controls.Add(label1);
            Controls.Add(password);
            Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormEnterPassword";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Set Password";
            Load += FormSetPassword_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox password;
        private Label label1;
        private Button buttonCancel;
        private Button buttonEnter;
        private Label label3;
        private CheckBox show;
        private Label info;
    }
}