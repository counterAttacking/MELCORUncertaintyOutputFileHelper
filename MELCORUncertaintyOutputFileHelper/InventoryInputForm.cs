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
    public partial class InventoryInputForm : DockContent
    {
        public Inventory inventory;
        public bool isSelected;
        private Dictionary<string, Inventory> inventoryList;
        private List<string> unitList;

        public InventoryInputForm()
        {
            InitializeComponent();

            this.inventory = new Inventory();
            this.isSelected = false;
            this.inventoryList = new Dictionary<string, Inventory>();
            this.unitList = new List<string>();
        }

        private void InventoryInputForm_Load(object sender, EventArgs e)
        {
            this.ReadInventoryFIle();
            this.SetUnitList();
        }

        private void ReadInventoryFIle()
        {
            var filePath = ".\\Data\\InventoryData.csv";
            var lineIdx = 0;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, false))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var lineVal = streamReader.ReadLine().Split(',');
                        if (lineIdx > 0)
                        {
                            var tmpInventory = new Inventory
                            {
                                xe = Convert.ToDouble(lineVal[1]),
                                cs = Convert.ToDouble(lineVal[2]),
                                ba = Convert.ToDouble(lineVal[3]),
                                i2 = Convert.ToDouble(lineVal[4]),
                                te = Convert.ToDouble(lineVal[5]),
                                ru = Convert.ToDouble(lineVal[6]),
                                mo = Convert.ToDouble(lineVal[7]),
                                ce = Convert.ToDouble(lineVal[8]),
                                la = Convert.ToDouble(lineVal[9])
                            };
                            this.inventoryList.Add(lineVal[0], tmpInventory);
                        }
                        lineIdx++;
                    }
                }
            }
        }

        private void SetUnitList()
        {
            this.unitList.Add("K2");
            this.unitList.Add("K34");
            this.unitList.Add("SK12");
            this.unitList.Add("SK34");

            foreach (var unit in this.unitList)
            {
                this.cmbUnitList.Items.Add(unit);
            }
        }

        private void CmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = this.cmbUnitList.SelectedItem.ToString();
            this.inventory = this.inventoryList[selectedItem];

            this.PrintInventory();
            this.isSelected = true;
        }

        private void PrintInventory()
        {
            this.txtXE.Text = string.Format("{0:0.0000E+00}", this.inventory.xe);
            this.txtCS.Text = string.Format("{0:0.0000E+00}", this.inventory.cs);
            this.txtBA.Text = string.Format("{0:0.0000E+00}", this.inventory.ba);
            this.txtI2.Text = string.Format("{0:0.0000E+00}", this.inventory.i2);
            this.txtTE.Text = string.Format("{0:0.0000E+00}", this.inventory.te);
            this.txtRU.Text = string.Format("{0:0.0000E+00}", this.inventory.ru);
            this.txtMO.Text = string.Format("{0:0.0000E+00}", this.inventory.mo);
            this.txtCE.Text = string.Format("{0:0.0000E+00}", this.inventory.ce);
            this.txtLA.Text = string.Format("{0:0.0000E+00}", this.inventory.la);
        }
    }
}
