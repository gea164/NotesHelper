using NotesHelper.Forms.Note;
using NotesHelper.Helpers.Nodes;
using NotesHelper.Helpers.Tree;
using NotesHelper.Helpers.TreeMenuHandler;

namespace NotesHelper
{
    public partial class Form1 : Form
    {
        private TreeHelper tree;
        private TreeMenuHandler treeMenu;
        private FormNote formNote;
        private TreeNode? selectedNode = null;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();

            this.tree = new TreeHelper(treeView);

            this.formNote = new FormNote(
                panel: panel,
                topic: labelTopic,
                title: textBoxTitle,
                content: textBoxContent,
                buttonClose: buttonClose,
                buttonSave: buttonSave,
                treeHelper: tree
            );

            this.treeMenu = new TreeMenuHandler(
                menu: contextMenuStrip,
                addNewTopic: addNewTopic,
                addNewSubTopic: addNewSubtopic,
                addNewNote: addNewDocument,
                editTopic: editSelectedTopic,
                treeHelper: tree,
                formNote: formNote
            );

            //Events connections
            tree.OnNoteDoubleClick += formNote.ShowUpdateNote;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            tree.Load();
            formNote.Hide();
        }
    }
}