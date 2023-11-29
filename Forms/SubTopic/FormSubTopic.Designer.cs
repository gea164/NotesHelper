namespace NotesHelper.Forms.SubTopic
{
    partial class FormSubTopic
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
            textBoxSubTopic = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // buttonAccept
            // 
            buttonAccept.Location = new Point(339, 74);
            buttonAccept.Name = "buttonAccept";
            buttonAccept.Size = new Size(75, 23);
            buttonAccept.TabIndex = 0;
            buttonAccept.Text = "Accept";
            buttonAccept.UseVisualStyleBackColor = true;
            buttonAccept.Click += buttonAccept_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(258, 74);
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
            textBoxTopic.Location = new Point(96, 12);
            textBoxTopic.Name = "textBoxTopic";
            textBoxTopic.ReadOnly = true;
            textBoxTopic.Size = new Size(318, 22);
            textBoxTopic.TabIndex = 3;
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
            // textBoxSubTopic
            // 
            textBoxSubTopic.Location = new Point(95, 43);
            textBoxSubTopic.Name = "textBoxSubTopic";
            textBoxSubTopic.Size = new Size(319, 22);
            textBoxSubTopic.TabIndex = 5;
            textBoxSubTopic.TextChanged += textBoxSubTopic_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 46);
            label3.Name = "label3";
            label3.Size = new Size(77, 14);
            label3.TabIndex = 6;
            label3.Text = "Sub-Topic:";
            // 
            // FormSubTopic
            // 
            AcceptButton = buttonAccept;
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(423, 107);
            Controls.Add(label3);
            Controls.Add(textBoxSubTopic);
            Controls.Add(label2);
            Controls.Add(textBoxTopic);
            Controls.Add(label1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonAccept);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSubTopic";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add New Sub-Topic";
            Load += FormSubTopic_Load;
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
        private TextBox textBoxSubTopic;
        private Label label3;
    }
}