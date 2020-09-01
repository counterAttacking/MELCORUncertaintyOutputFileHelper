namespace MELCORUncertaintyOutputFileHelper
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonTabFile = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonBtnOpenFolder = new System.Windows.Forms.RibbonButton();
            this.ribbonBtnOpenFile = new System.Windows.Forms.RibbonButton();
            this.dockPnlMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 447);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon1.OrbVisible = false;
            // 
            // 
            // 
            this.ribbon1.QuickAccessToolbar.Visible = false;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(784, 143);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.Tabs.Add(this.ribbonTabFile);
            this.ribbon1.TabSpacing = 4;
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Halloween;
            // 
            // ribbonTabFile
            // 
            this.ribbonTabFile.Name = "ribbonTabFile";
            this.ribbonTabFile.Panels.Add(this.ribbonPanel1);
            this.ribbonTabFile.Text = "File";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreEnabled = false;
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Items.Add(this.ribbonBtnOpenFolder);
            this.ribbonPanel1.Items.Add(this.ribbonBtnOpenFile);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "Open";
            // 
            // ribbonBtnOpenFolder
            // 
            this.ribbonBtnOpenFolder.Image = global::MELCORUncertaintyOutputFileHelper.Properties.Resources.newFolder_48;
            this.ribbonBtnOpenFolder.LargeImage = global::MELCORUncertaintyOutputFileHelper.Properties.Resources.newFolder_48;
            this.ribbonBtnOpenFolder.Name = "ribbonBtnOpenFolder";
            this.ribbonBtnOpenFolder.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonBtnOpenFolder.SmallImage")));
            this.ribbonBtnOpenFolder.Text = "Folder";
            this.ribbonBtnOpenFolder.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.ribbonBtnOpenFolder.Click += new System.EventHandler(this.RibbonBtnOpenFolder_Click);
            // 
            // ribbonBtnOpenFile
            // 
            this.ribbonBtnOpenFile.Image = global::MELCORUncertaintyOutputFileHelper.Properties.Resources.newFile_48;
            this.ribbonBtnOpenFile.LargeImage = global::MELCORUncertaintyOutputFileHelper.Properties.Resources.newFile_48;
            this.ribbonBtnOpenFile.Name = "ribbonBtnOpenFile";
            this.ribbonBtnOpenFile.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonBtnOpenFile.SmallImage")));
            this.ribbonBtnOpenFile.Text = "File";
            this.ribbonBtnOpenFile.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.ribbonBtnOpenFile.Click += new System.EventHandler(this.RibbonBtnOpenFile_Click);
            // 
            // dockPnlMain
            // 
            this.dockPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPnlMain.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dockPnlMain.Location = new System.Drawing.Point(0, 143);
            this.dockPnlMain.Name = "dockPnlMain";
            this.dockPnlMain.Padding = new System.Windows.Forms.Padding(6);
            this.dockPnlMain.ShowAutoHideContentOnHover = false;
            this.dockPnlMain.Size = new System.Drawing.Size(784, 418);
            this.dockPnlMain.TabIndex = 1;
            this.dockPnlMain.Theme = this.vS2015DarkTheme1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dockPnlMain);
            this.Controls.Add(this.ribbon1);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MELCOR Output File Helper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPnlMain;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private System.Windows.Forms.RibbonTab ribbonTabFile;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribbonBtnOpenFolder;
        private System.Windows.Forms.RibbonButton ribbonBtnOpenFile;
    }
}

