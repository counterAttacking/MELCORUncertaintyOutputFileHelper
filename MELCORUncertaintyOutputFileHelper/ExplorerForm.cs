using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            if (this.tvwFiles.SelectedNode.Parent == null) // SelectedNode is parent
            {
                return;
            }
            else // SelectedNode is child
            {
                /*try
                {
                    this.frmMain.ViewSelectedFile(e.Node.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }*/
            }
        }

        public void Run()
        {
            if (this.tvwFiles.Nodes.Count <= 0)
            {
                return;
            }
            this.TraversalAllNode(this.tvwFiles);
            MessageBox.Show("It's done.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TraversalAllNode(TreeView treeView)
        {
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode node in nodes)
            {
                foreach (TreeNode elem in node.Nodes)
                {
                    var analysis = this.ReadFiles(elem.Name);
                    analysis = this.CalculateFraction(analysis);
                    this.frmMain.PrintResult(analysis);
                }
            }
        }

        private Analysis ReadFiles(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var analysis = new Analysis();
            analysis.name = Path.GetFileNameWithoutExtension(filePath);
            var nuclide24 = new Nuclide();
            var nuclide72 = new Nuclide();

            var samgStr = "  $$$  SAMG ENTRY : CET Exceeds 922 K (1200 F)";
            var releaseStr = "  $$$  FPs Start to Release to Environment";
            var samgPos = Array.FindIndex(lines, row => row.Contains(samgStr));
            var releasePos = Array.FindIndex(lines, row => row.Contains(releaseStr));
            if (samgPos < 0 || releasePos < 0)
            {
                return analysis;
            }
            var samgLineVals = lines[samgPos - 3].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var releaseLineVals = lines[releasePos - 3].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var availTime = Convert.ToDouble(releaseLineVals[2]) - Convert.ToDouble(samgLineVals[2]);

            analysis.samg = Convert.ToDouble(samgLineVals[2]);
            analysis.fpRelease = Convert.ToDouble(releaseLineVals[2]);
            analysis.availTime = availTime;

            var oneDayStr = "  $$$  24 hr after FP Release";
            var threeDayStr = "  $$$  72 hr after FP Release";
            var oneDayPos = Array.FindIndex(lines, row => row.Contains(oneDayStr));
            var threeDayPos = Array.FindIndex(lines, row => row.Contains(threeDayStr));
            if (oneDayPos < 0 || threeDayPos < 0)
            {
                return analysis;
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, false))
                {
                    var lineIdx = 0;
                    var is24hrFound = false;
                    var is72hrFound = false;
                    var ctyp6Pos = 0;
                    while (!streamReader.EndOfStream)
                    {
                        var lineVal = streamReader.ReadLine();

                        if (lineIdx > oneDayPos && lineIdx < threeDayPos && is24hrFound == false && lineVal.Contains(" RADIOACTIVE RADIONUCLIDE MASS DISTRIBUTION IN KG"))
                        {
                            is24hrFound = true;
                            lineIdx++;
                            continue;
                        }

                        if (lineIdx > threeDayPos && is72hrFound == false && lineVal.Contains(" RADIOACTIVE RADIONUCLIDE MASS DISTRIBUTION IN KG"))
                        {
                            is72hrFound = true;
                            lineIdx++;
                            continue;
                        }

                        if (is24hrFound == true)
                        {
                            if (String.IsNullOrEmpty(lineVal))
                            {
                                lineIdx++;
                                continue;
                            }

                            var splitedLineVal = lineVal.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            if (splitedLineVal[0].Equals("CLASS"))
                            {
                                var innerIdx = 0;
                                foreach (var val in splitedLineVal)
                                {
                                    if (val.Equals("CTYP-6"))
                                    {
                                        ctyp6Pos = innerIdx;
                                    }
                                    innerIdx++;
                                }
                            }
                            else if (splitedLineVal[0].Equals("XE"))
                            {
                                analysis.nuclide24.xe = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CS"))
                            {
                                analysis.nuclide24.cs = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BA"))
                            {
                                analysis.nuclide24.ba = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("I2"))
                            {
                                analysis.nuclide24.i2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("TE"))
                            {
                                analysis.nuclide24.te = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("RU"))
                            {
                                analysis.nuclide24.ru = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("MO"))
                            {
                                analysis.nuclide24.mo = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CE"))
                            {
                                analysis.nuclide24.ce = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("LA"))
                            {
                                analysis.nuclide24.la = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("UO2"))
                            {
                                analysis.nuclide24.uo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CD"))
                            {
                                analysis.nuclide24.cd = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("AG"))
                            {
                                analysis.nuclide24.ag = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BO2"))
                            {
                                analysis.nuclide24.bo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("H2O"))
                            {
                                analysis.nuclide24.h2o = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CON"))
                            {
                                analysis.nuclide24.con = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSI"))
                            {
                                analysis.nuclide24.csi = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSM"))
                            {
                                analysis.nuclide24.csm = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                                is24hrFound = false;
                            }
                        }

                        if (is72hrFound == true)
                        {
                            if (String.IsNullOrEmpty(lineVal))
                            {
                                lineIdx++;
                                continue;
                            }

                            var splitedLineVal = lineVal.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            if (splitedLineVal[0].Equals("CLASS"))
                            {
                                var innerIdx = 0;
                                foreach (var val in splitedLineVal)
                                {
                                    if (val.Equals("CTYP-6"))
                                    {
                                        ctyp6Pos = innerIdx;
                                    }
                                    innerIdx++;
                                }
                            }
                            else if (splitedLineVal[0].Equals("XE"))
                            {
                                analysis.nuclide72.xe = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CS"))
                            {
                                analysis.nuclide72.cs = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BA"))
                            {
                                analysis.nuclide72.ba = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("I2"))
                            {
                                analysis.nuclide72.i2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("TE"))
                            {
                                analysis.nuclide72.te = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("RU"))
                            {
                                analysis.nuclide72.ru = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("MO"))
                            {
                                analysis.nuclide72.mo = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CE"))
                            {
                                analysis.nuclide72.ce = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("LA"))
                            {
                                analysis.nuclide72.la = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("UO2"))
                            {
                                analysis.nuclide72.uo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CD"))
                            {
                                analysis.nuclide72.cd = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("AG"))
                            {
                                analysis.nuclide72.ag = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BO2"))
                            {
                                analysis.nuclide72.bo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("H2O"))
                            {
                                analysis.nuclide72.h2o = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CON"))
                            {
                                analysis.nuclide72.con = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSI"))
                            {
                                analysis.nuclide72.csi = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSM"))
                            {
                                analysis.nuclide72.csm = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                                is72hrFound = false;
                                break;
                            }
                        }

                        lineIdx++;
                    }
                }
            }
            return analysis;
        }

        private Analysis CalculateFraction(Analysis analysis)
        {
            var tmp = analysis;
            tmp.fraction24.xe = analysis.nuclide24.xe / 2.909E+02;
            tmp.fraction24.cs = (analysis.nuclide24.cs + analysis.nuclide24.csi * 0.511556 + analysis.nuclide24.csm * 0.73478922) / 1.621E+02;
            tmp.fraction24.ba = analysis.nuclide24.ba / 1.276E+02;
            tmp.fraction24.i2 = (analysis.nuclide24.i2 + analysis.nuclide24.csi * 0.488444) / 1.253E+01;
            tmp.fraction24.te = analysis.nuclide24.te / 2.552E+01;
            tmp.fraction24.ru = analysis.nuclide24.ru / 1.795E+02;
            tmp.fraction24.mo = (analysis.nuclide24.mo + analysis.nuclide24.csm * 0.26521078) / 2.117E+02;
            tmp.fraction24.ce = analysis.nuclide24.ce / 3.735E+02;
            tmp.fraction24.la = analysis.nuclide24.la / 3.466E+02;

            tmp.fraction72.xe = analysis.nuclide72.xe / 2.909E+02;
            tmp.fraction72.cs = (analysis.nuclide72.cs + analysis.nuclide72.csi * 0.511556 + analysis.nuclide72.csm * 0.73478922) / 1.621E+02;
            tmp.fraction72.ba = analysis.nuclide72.ba / 1.276E+02;
            tmp.fraction72.i2 = (analysis.nuclide72.i2 + analysis.nuclide72.csi * 0.488444) / 1.253E+01;
            tmp.fraction72.te = analysis.nuclide72.te / 2.552E+01;
            tmp.fraction72.ru = analysis.nuclide72.ru / 1.795E+02;
            tmp.fraction72.mo = (analysis.nuclide72.mo + analysis.nuclide72.csm * 0.26521078) / 2.117E+02;
            tmp.fraction72.ce = analysis.nuclide72.ce / 3.735E+02;
            tmp.fraction72.la = analysis.nuclide72.la / 3.466E+02;

            return tmp;
        }

    }
}
