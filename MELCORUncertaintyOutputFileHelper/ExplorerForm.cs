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
    public partial class ExplorerForm : DockContent
    {
        private MainForm frmMain;
        private static string targetStr = "_PCOUT.txt";

        public ExplorerForm(MainForm frmMain)
        {
            InitializeComponent();

            this.frmMain = frmMain;
        }

        public void AddPCOUTFiles(List<PCOUTFIle> files)
        {
            foreach (var file in files)
            {
                var pureFileName = file.name;
                var fileNodeText = pureFileName.Replace(targetStr, "");
                var fileNode = new TreeNode(fileNodeText);
                fileNode.Nodes.Add(file.path, file.name);
                this.tvwFiles.Nodes.Add(fileNode);
            }
        }

        private void TvwFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void TvwFiles_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<PCOUTFIle> pcoutFiles = new List<PCOUTFIle>();

            foreach (var file in files)
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
            this.AddPCOUTFiles(pcoutFiles);
        }

        private void TvwFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                //this.frmMain.ViewSelectedFile(e.Node.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
