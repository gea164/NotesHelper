﻿namespace NotesHelper
{
    partial class Form1
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
            treeView = new TreeView();
            contextMenuStrip = new ContextMenuStrip(components);
            addNewTopic = new ToolStripMenuItem();
            addNewSubtopic = new ToolStripMenuItem();
            addNewDocument = new ToolStripMenuItem();
            panel = new Panel();
            textBoxTitle = new TextBox();
            labelTopic = new Label();
            label3 = new Label();
            buttonClose = new Button();
            buttonSave = new Button();
            textBoxContent = new TextBox();
            label1 = new Label();
            contextMenuStrip.SuspendLayout();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView.ContextMenuStrip = contextMenuStrip;
            treeView.Location = new Point(12, 11);
            treeView.Name = "treeView";
            treeView.Size = new Size(273, 367);
            treeView.TabIndex = 0;
            treeView.AfterSelect += treeView_AfterSelect;
            treeView.KeyDown += treeView_KeyDown;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { addNewTopic, addNewSubtopic, addNewDocument });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(194, 70);
            contextMenuStrip.Opening += contextMenuStrip_Opening;
            // 
            // addNewTopic
            // 
            addNewTopic.Name = "addNewTopic";
            addNewTopic.Size = new Size(193, 22);
            addNewTopic.Text = "Add new topic";
            addNewTopic.Click += addNewTopic_Click;
            // 
            // addNewSubtopic
            // 
            addNewSubtopic.Name = "addNewSubtopic";
            addNewSubtopic.Size = new Size(193, 22);
            addNewSubtopic.Text = "Add new sub-topic";
            addNewSubtopic.Click += addNewSubtopic_Click;
            // 
            // addNewDocument
            // 
            addNewDocument.Name = "addNewDocument";
            addNewDocument.Size = new Size(193, 22);
            addNewDocument.Text = "Add new document";
            addNewDocument.Click += addNewDocument_Click;
            // 
            // panel
            // 
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel.Controls.Add(textBoxTitle);
            panel.Controls.Add(labelTopic);
            panel.Controls.Add(label3);
            panel.Controls.Add(buttonClose);
            panel.Controls.Add(buttonSave);
            panel.Controls.Add(textBoxContent);
            panel.Controls.Add(label1);
            panel.Location = new Point(291, 11);
            panel.Name = "panel";
            panel.Size = new Size(490, 367);
            panel.TabIndex = 1;
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTitle.Location = new Point(69, 39);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(410, 22);
            textBoxTitle.TabIndex = 10;
            textBoxTitle.TextChanged += textBoxTitle_TextChanged;
            // 
            // labelTopic
            // 
            labelTopic.AutoEllipsis = true;
            labelTopic.BorderStyle = BorderStyle.Fixed3D;
            labelTopic.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelTopic.ForeColor = SystemColors.HotTrack;
            labelTopic.Location = new Point(70, 10);
            labelTopic.Name = "labelTopic";
            labelTopic.Size = new Size(409, 22);
            labelTopic.TabIndex = 9;
            labelTopic.Text = "Topic:";
            labelTopic.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 14);
            label3.Name = "label3";
            label3.Size = new Size(49, 14);
            label3.TabIndex = 8;
            label3.Text = "Topic:";
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonClose.Location = new Point(8, 338);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(125, 23);
            buttonClose.TabIndex = 6;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSave.Location = new Point(355, 339);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(125, 23);
            buttonSave.TabIndex = 5;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // textBoxContent
            // 
            textBoxContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxContent.Location = new Point(8, 68);
            textBoxContent.Multiline = true;
            textBoxContent.Name = "textBoxContent";
            textBoxContent.ScrollBars = ScrollBars.Both;
            textBoxContent.Size = new Size(472, 267);
            textBoxContent.TabIndex = 4;
            textBoxContent.WordWrap = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 42);
            label1.Name = "label1";
            label1.Size = new Size(49, 14);
            label1.TabIndex = 0;
            label1.Text = "Title:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 384);
            Controls.Add(panel);
            Controls.Add(treeView);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "Form1";
            Text = "Notes Helper";
            Load += Form1_Load;
            contextMenuStrip.ResumeLayout(false);
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem addNewTopic;
        private ToolStripMenuItem addNewDocument;
        private ToolStripMenuItem addNewSubtopic;
        private Panel panel;
        private Button buttonSave;
        private TextBox textBoxContent;
        private Label labelTopic;
        private Label label1;
        private Button buttonClose;
        private Label label3;
        private TextBox textBoxTitle;
    }
}