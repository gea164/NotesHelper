namespace NotesHelper
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            contextMenuStrip = new ContextMenuStrip(components);
            editSelectedTopic = new ToolStripMenuItem();
            addNewTopic = new ToolStripMenuItem();
            addNewSubtopic = new ToolStripMenuItem();
            addNewDocument = new ToolStripMenuItem();
            treeView = new TreeView();
            splitContainer = new SplitContainer();
            labelLastSaved = new Label();
            buttonSave = new Button();
            buttonCancel = new Button();
            textBoxTitle = new TextBox();
            labelTopic = new Label();
            label3 = new Label();
            textBoxContent = new TextBox();
            label1 = new Label();
            contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { editSelectedTopic, addNewTopic, addNewSubtopic, addNewDocument });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(208, 92);
            // 
            // editSelectedTopic
            // 
            editSelectedTopic.Name = "editSelectedTopic";
            editSelectedTopic.Size = new Size(207, 22);
            editSelectedTopic.Text = "Edit selected topic";
            // 
            // addNewTopic
            // 
            addNewTopic.Name = "addNewTopic";
            addNewTopic.Size = new Size(207, 22);
            addNewTopic.Text = "Add new topic";
            // 
            // addNewSubtopic
            // 
            addNewSubtopic.Name = "addNewSubtopic";
            addNewSubtopic.Size = new Size(207, 22);
            addNewSubtopic.Text = "Add new sub-topic";
            // 
            // addNewDocument
            // 
            addNewDocument.Name = "addNewDocument";
            addNewDocument.Size = new Size(207, 22);
            addNewDocument.Text = "Add new note";
            // 
            // treeView
            // 
            treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView.ContextMenuStrip = contextMenuStrip;
            treeView.Location = new Point(7, 9);
            treeView.Name = "treeView";
            treeView.Size = new Size(290, 482);
            treeView.TabIndex = 3;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(treeView);
            splitContainer.Panel1MinSize = 300;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(labelLastSaved);
            splitContainer.Panel2.Controls.Add(buttonSave);
            splitContainer.Panel2.Controls.Add(buttonCancel);
            splitContainer.Panel2.Controls.Add(textBoxTitle);
            splitContainer.Panel2.Controls.Add(labelTopic);
            splitContainer.Panel2.Controls.Add(label3);
            splitContainer.Panel2.Controls.Add(textBoxContent);
            splitContainer.Panel2.Controls.Add(label1);
            splitContainer.Panel2.Paint += splitContainer_Panel2_Paint;
            splitContainer.Panel2MinSize = 500;
            splitContainer.Size = new Size(914, 498);
            splitContainer.SplitterDistance = 300;
            splitContainer.TabIndex = 2;
            // 
            // labelLastSaved
            // 
            labelLastSaved.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelLastSaved.AutoSize = true;
            labelLastSaved.Location = new Point(9, 472);
            labelLastSaved.Name = "labelLastSaved";
            labelLastSaved.Size = new Size(224, 14);
            labelLastSaved.TabIndex = 18;
            labelLastSaved.Text = "Last Saved: 2022-25-01 10:00:00";
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSave.Location = new Point(523, 468);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(442, 468);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTitle.Location = new Point(64, 38);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(534, 22);
            textBoxTitle.TabIndex = 1;
            // 
            // labelTopic
            // 
            labelTopic.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelTopic.AutoEllipsis = true;
            labelTopic.BorderStyle = BorderStyle.Fixed3D;
            labelTopic.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelTopic.ForeColor = SystemColors.HotTrack;
            labelTopic.Location = new Point(64, 9);
            labelTopic.Name = "labelTopic";
            labelTopic.Size = new Size(534, 22);
            labelTopic.TabIndex = 14;
            labelTopic.Text = "Topic:";
            labelTopic.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 13);
            label3.Name = "label3";
            label3.Size = new Size(49, 14);
            label3.TabIndex = 13;
            label3.Text = "Topic:";
            // 
            // textBoxContent
            // 
            textBoxContent.AcceptsTab = true;
            textBoxContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxContent.Location = new Point(9, 66);
            textBoxContent.Multiline = true;
            textBoxContent.Name = "textBoxContent";
            textBoxContent.ScrollBars = ScrollBars.Both;
            textBoxContent.Size = new Size(589, 391);
            textBoxContent.TabIndex = 2;
            textBoxContent.TabStop = false;
            textBoxContent.WordWrap = false;
            textBoxContent.KeyDown += textBoxContent_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 41);
            label1.Name = "label1";
            label1.Size = new Size(49, 14);
            label1.TabIndex = 11;
            label1.Text = "Title:";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 498);
            Controls.Add(splitContainer);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Notes Helper";
            Load += Form1_Load;
            contextMenuStrip.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem addNewTopic;
        private ToolStripMenuItem addNewDocument;
        private ToolStripMenuItem addNewSubtopic;
        private ToolStripMenuItem editSelectedTopic;
        private TreeView treeView;
        private SplitContainer splitContainer;
        private TextBox textBoxTitle;
        private Label labelTopic;
        private Label label3;
        private TextBox textBoxContent;
        private Label label1;
        private Button buttonSave;
        private Button buttonCancel;
        private Label labelLastSaved;
    }
}