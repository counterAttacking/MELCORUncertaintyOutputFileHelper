using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MELCORUncertaintyOutputFileHelper
{
    public partial class MainForm : RibbonForm
    {
        private ExplorerForm frmExplorer;

        public MainForm()
        {
            InitializeComponent();

            this.frmExplorer = new ExplorerForm();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.frmExplorer.Show(this.dockPnlMain, DockState.DockLeft);
        }

        private void RibbonBtnOpenFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog();
            openFolderDialog.IsFolderPicker = true;
            openFolderDialog.Multiselect = true;
            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Cancel)
            {
                return;
            }
        }

        private void RibbonBtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
        }

    }
}
