namespace NotesHelper.Forms.Topic
{
    partial class FormTopic
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
            buttonAccept = new Button();
            buttonCancel = new Button();
            label1 = new Label();
            textBoxTopic = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // buttonAccept
            // 
            buttonAccept.Location = new Point(311, 56);
            buttonAccept.Name = "buttonAccept";
            buttonAccept.Size = new Size(75, 23);
            buttonAccept.TabIndex = 0;
            buttonAccept.Text = "Accept";
            buttonAccept.UseVisualStyleBackColor = true;
            buttonAccept.Click += buttonAccept_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(230, 55);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-122, 42);
            label1.Name = "label1";
            label1.Size = new Size(49, 14);
            label1.TabIndex = 2;
            label1.Text = "Topic:";
            // 
            // textBoxTopic
            // 
            textBoxTopic.Location = new Point(67, 12);
            textBoxTopic.Name = "textBoxTopic";
            textBoxTopic.Size = new Size(319, 22);
            textBoxTopic.TabIndex = 3;
            textBoxTopic.TextChanged += textBoxTopic_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(49, 14);
            label2.TabIndex = 4;
            label2.Text = "Topic:";
            // 
            // FormTopic
            // 
            AcceptButton = buttonAccept;
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(398, 90);
            Controls.Add(label2);
            Controls.Add(textBoxTopic);
            Controls.Add(label1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonAccept);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormTopic";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add new topic";
            Load += FormTopic_Load;
            Shown += FormTopic_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAccept;
        private Button buttonCancel;
        private Label label1;
        private TextBox textBoxTopic;
        private Label label2;
    }
}