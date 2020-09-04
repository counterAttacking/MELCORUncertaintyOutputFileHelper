﻿using System;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (this.tvwFiles.Nodes.Count <= 0)
            {
                return;
            }
            this.TraversalAllNode(this.tvwFiles);
            stopwatch.Stop();
            MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());
            MessageBox.Show("It's done.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TraversalAllNode(TreeView treeView)
        {
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode node in nodes)
            {
                foreach (TreeNode elem in node.Nodes)
                {
                    this.ReadFiles(elem.Name);
                }
            }
        }

        private void ReadFiles(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var nuclide24 = new Nuclide();
            var nuclide72 = new Nuclide();

            var samgStr = "  $$$  SAMG ENTRY : CET Exceeds 922 K (1200 F)";
            var releaseStr = "  $$$  FPs Start to Release to Environment";
            var samgPos = Array.FindIndex(lines, row => row.Contains(samgStr));
            var releasePos = Array.FindIndex(lines, row => row.Contains(releaseStr));
            if (samgPos < 0 || releasePos < 0)
            {
                return;
            }
            var samgLineVals = lines[samgPos - 3].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var releaseLineVals = lines[releasePos - 3].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var availTime = Convert.ToDouble(releaseLineVals[2]) - Convert.ToDouble(samgLineVals[2]);
            //MessageBox.Show(samgLineVals[2] + "\n" + releaseLineVals[2] + "\n" + availTime.ToString());

            var oneDayStr = "  $$$  24 hr after FP Release";
            var threeDayStr = "  $$$  72 hr after FP Release";
            var oneDayPos = Array.FindIndex(lines, row => row.Contains(oneDayStr));
            var threeDayPos = Array.FindIndex(lines, row => row.Contains(threeDayStr));
            if (oneDayPos < 0 || threeDayPos < 0)
            {
                return;
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
                                nuclide24.xe = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CS"))
                            {
                                nuclide24.cs = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BA"))
                            {
                                nuclide24.ba = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("I2"))
                            {
                                nuclide24.i2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("TE"))
                            {
                                nuclide24.te = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("RU"))
                            {
                                nuclide24.ru = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("MO"))
                            {
                                nuclide24.mo = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CE"))
                            {
                                nuclide24.ce = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("LA"))
                            {
                                nuclide24.la = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("UO2"))
                            {
                                nuclide24.uo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CD"))
                            {
                                nuclide24.cd = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("AG"))
                            {
                                nuclide24.ag = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BO2"))
                            {
                                nuclide24.bo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("H2O"))
                            {
                                nuclide24.h2o = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CON"))
                            {
                                nuclide24.con = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSI"))
                            {
                                nuclide24.csi = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSM"))
                            {
                                nuclide24.csm = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
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
                                nuclide72.xe = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CS"))
                            {
                                nuclide72.cs = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BA"))
                            {
                                nuclide72.ba = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("I2"))
                            {
                                nuclide72.i2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("TE"))
                            {
                                nuclide72.te = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("RU"))
                            {
                                nuclide72.ru = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("MO"))
                            {
                                nuclide72.mo = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CE"))
                            {
                                nuclide72.ce = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("LA"))
                            {
                                nuclide72.la = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("UO2"))
                            {
                                nuclide72.uo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CD"))
                            {
                                nuclide72.cd = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("AG"))
                            {
                                nuclide72.ag = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("BO2"))
                            {
                                nuclide72.bo2 = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("H2O"))
                            {
                                nuclide72.h2o = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CON"))
                            {
                                nuclide72.con = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSI"))
                            {
                                nuclide72.csi = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                            }
                            else if (splitedLineVal[0].Equals("CSM"))
                            {
                                nuclide72.csm = Convert.ToDouble(splitedLineVal[ctyp6Pos]);
                                is72hrFound = false;
                            }
                        }

                        lineIdx++;
                    }
                }
            }
        }

    }
}
