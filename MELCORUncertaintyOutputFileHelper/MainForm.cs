using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private static string targetStr = "_PCOUT.txt";

        public MainForm()
        {
            InitializeComponent();

            this.frmExplorer = new ExplorerForm(this);
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

            List<PCOUTFIle> pcoutFiles = new List<PCOUTFIle>();
            DirectoryInfo directoryInfo = new DirectoryInfo(openFolderDialog.FileName);
            if (directoryInfo.GetDirectories().Length > 0)
            {
                foreach (var dir in directoryInfo.GetDirectories())
                {
                    this.DirFileSearch(dir.FullName);
                }
            }
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (Path.GetFileName(file.Name).Contains(targetStr))
                {
                    try
                    {
                        var pcoutFile = new PCOUTFIle();
                        pcoutFile.name = Path.GetFileName(file.Name);
                        pcoutFile.path = file.FullName;
                        pcoutFiles.Add(pcoutFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            this.frmExplorer.AddPCOUTFiles(pcoutFiles);
        }

        private void RibbonBtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT File|*.txt";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            List<PCOUTFIle> pcoutFiles = new List<PCOUTFIle>();
            foreach (var file in openFileDialog.FileNames)
            {
                if (Path.GetFileName(file).Contains(targetStr))
                {
                    try
                    {
                        var pcoutFile = new PCOUTFIle();
                        pcoutFile.name = Path.GetFileName(file);
                        pcoutFile.path = file;
                        pcoutFiles.Add(pcoutFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            this.frmExplorer.AddPCOUTFiles(pcoutFiles);
        }

        private void DirFileSearch(string dirPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
            foreach (var dir in Directory.GetDirectories(dirPath))
            {
                this.DirFileSearch(dir);
            }

            List<PCOUTFIle> pcoutFiles = new List<PCOUTFIle>();
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (Path.GetFileName(file.Name).Contains(targetStr))
                {
                    try
                    {
                        var pcoutFile = new PCOUTFIle();
                        pcoutFile.name = Path.GetFileName(file.Name);
                        pcoutFile.path = file.FullName;
                        pcoutFiles.Add(pcoutFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            this.frmExplorer.AddPCOUTFiles(pcoutFiles);
        }

        private void RibbonBtnRun_Click(object sender, EventArgs e)
        {
            /*var frmResult = new ResultForm();
            frmResult.Show(this.dockPnlMain, DockState.Document);*/
            this.frmExplorer.Run();
        }

    }
}
