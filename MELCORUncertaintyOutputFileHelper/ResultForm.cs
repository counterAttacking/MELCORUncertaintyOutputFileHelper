﻿using System;
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
    public partial class ResultForm : DockContent
    {
        private List<string> colValues;

        public ResultForm()
        {
            InitializeComponent();

            this.colValues = new List<string>();
            this.ColValuesSetting();
        }

        private void ReusltForm_Load(object sender, EventArgs e)
        {
            this.dgvResult.ColumnCount = this.colValues.Count;
            for (var i = 0; i < this.colValues.Count; i++)
            {
                this.dgvResult.Columns[i].Name = this.colValues[i];
            }
        }

        private void ColValuesSetting()
        {
            this.colValues.Add("Case");
            this.colValues.Add("SAMG");
            this.colValues.Add("FP Release");
            this.colValues.Add("소개여유시간");
            this.colValues.Add("24hr Class 1");
            this.colValues.Add("24hr Class 2");
            this.colValues.Add("24hr Class 3");
            this.colValues.Add("24hr Class 4");
            this.colValues.Add("24hr Class 5");
            this.colValues.Add("24hr Class 6");
            this.colValues.Add("24hr Class 7");
            this.colValues.Add("24hr Class 8");
            this.colValues.Add("24hr Class 9");
            this.colValues.Add("72hr Class 1");
            this.colValues.Add("72hr Class 2");
            this.colValues.Add("72hr Class 3");
            this.colValues.Add("72hr Class 4");
            this.colValues.Add("72hr Class 5");
            this.colValues.Add("72hr Class 6");
            this.colValues.Add("72hr Class 7");
            this.colValues.Add("72hr Class 8");
            this.colValues.Add("72hr Class 9");
        }

        public void PrintAnalysis(Analysis analysis)
        {
            this.dgvResult.Rows.Add(analysis.name, analysis.samg, analysis.fpRelease, analysis.availTime,
                analysis.fraction24.xe, analysis.fraction24.cs, analysis.fraction24.ba, analysis.fraction24.i2, analysis.fraction24.te,
                analysis.fraction24.ru, analysis.fraction24.mo, analysis.fraction24.ce, analysis.fraction24.la,
                analysis.fraction72.xe, analysis.fraction72.cs, analysis.fraction72.ba, analysis.fraction72.i2, analysis.fraction72.te,
                analysis.fraction72.ru, analysis.fraction72.mo, analysis.fraction72.ce, analysis.fraction72.la);
        }

    }
}
