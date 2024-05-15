using NotesHelper.Forms.Note;
using NotesHelper.Helpers.Tree;
using NotesHelper.Helpers.TreeMenuHandler;

namespace NotesHelper
{
    public partial class Main : Form
    {
        private TreeHelper tree;
        private TreeMenuHandler treeMenu;
        private FormNote formNote;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public Main()
        {
            InitializeComponent();

            this.tree = new TreeHelper(treeView);

            this.formNote = new FormNote(
                panel: splitContainer.Panel2,
                topic: labelTopic,
                title: textBoxTitle,
                content: textBoxContent,
                buttonClose: buttonCancel,
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

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}