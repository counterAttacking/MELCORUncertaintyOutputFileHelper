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
    public partial class InventoryInputForm : DockContent
    {
        private Inventory inventory;

        public InventoryInputForm()
        {
            InitializeComponent();

            this.inventory = new Inventory();
        }
    }
}
