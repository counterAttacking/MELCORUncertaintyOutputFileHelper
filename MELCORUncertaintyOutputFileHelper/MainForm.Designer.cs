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
            this.mainFormRibbon = new System.Windows.Forms.Ribbon();
            this.ribbonTabFile = new System.Windows.Forms.RibbonTab();
            this.ribbonPnlOpen = new System.Windows.Forms.RibbonPanel();
            this.ribbonBtnOpenFolder = new System.Windows.Forms.RibbonButton();
            this.ribbonBtnOpenFile = new System.Windows.Forms.RibbonButton();
            this.ribbonTabBuild = new System.Windows.Forms.RibbonTab();
            this.ribbonPnlExecute = new System.Windows.Forms.RibbonPanel();
            this.ribbonBtnRun = new System.Windows.Forms.RibbonButton();
            this.dockPnlMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.SuspendLayout();
            // 
            // mainFormRibbon
            // 
            this.mainFormRibbon.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.mainFormRibbon.Location = new System.Drawing.Point(0, 0);
            this.mainFormRibbon.Minimized = false;
            this.mainFormRibbon.Name = "mainFormRibbon";
            // 
            // 
            // 
            this.mainFormRibbon.OrbDropDown.BorderRoundness = 8;
            this.mainFormRibbon.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.mainFormRibbon.OrbDropDown.Name = "";
            this.mainFormRibbon.OrbDropDown.Size = new System.Drawing.Size(527, 447);
            this.mainFormRibbon.OrbDropDown.TabIndex = 0;
            this.mainFormRibbon.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.mainFormRibbon.OrbVisible = false;
            // 
            // 
            // 
            this.mainFormRibbon.QuickAccessToolbar.Visible = false;
            this.mainFormRibbon.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.mainFormRibbon.Size = new System.Drawing.Size(784, 143);
            this.mainFormRibbon.TabIndex = 0;
            this.mainFormRibbon.Tabs.Add(this.ribbonTabFile);
            this.mainFormRibbon.Tabs.Add(this.ribbonTabBuild);
            this.mainFormRibbon.TabSpacing = 4;
            this.mainFormRibbon.ThemeColor = System.Windows.Forms.RibbonTheme.Halloween;
            // 
            // ribbonTabFile
            // 
            this.ribbonTabFile.Name = "ribbonTabFile";
            this.ribbonTabFile.Panels.Add(this.ribbonPnlOpen);
            this.ribbonTabFile.Text = "File";
            // 
            // ribbonPnlOpen
            // 
            this.ribbonPnlOpen.ButtonMoreEnabled = false;
            this.ribbonPnlOpen.ButtonMoreVisible = false;
            this.ribbonPnlOpen.Items.Add(this.ribbonBtnOpenFolder);
            this.ribbonPnlOpen.Items.Add(this.ribbonBtnOpenFile);
            this.ribbonPnlOpen.Name = "ribbonPnlOpen";
            this.ribbonPnlOpen.Text = "Open";
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
            // ribbonTabBuild
            // 
            this.ribbonTabBuild.Name = "ribbonTabBuild";
            this.ribbonTabBuild.Panels.Add(this.ribbonPnlExecute);
            this.ribbonTabBuild.Text = "Build";
            // 
            // ribbonPnlExecute
            // 
            this.ribbonPnlExecute.ButtonMoreEnabled = false;
            this.ribbonPnlExecute.ButtonMoreVisible = false;
            this.ribbonPnlExecute.Items.Add(this.ribbonBtnRun);
            this.ribbonPnlExecute.Name = "ribbonPnlExecute";
            this.ribbonPnlExecute.Text = "Execute";
            // 
            // ribbonBtnRun
            // 
            this.ribbonBtnRun.Image = global::MELCORUncertaintyOutputFileHelper.Properties.Resources.start_48;
            this.ribbonBtnRun.LargeImage = global::MELCORUncertaintyOutputFileHelper.Properties.Resources.start_48;
            this.ribbonBtnRun.Name = "ribbonBtnRun";
            this.ribbonBtnRun.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonBtnRun.SmallImage")));
            this.ribbonBtnRun.Text = "Run";
            this.ribbonBtnRun.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.ribbonBtnRun.Click += new System.EventHandler(this.RibbonBtnRun_Click);
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
            this.Controls.Add(this.mainFormRibbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MELCOR Uncertainty Output File Helper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Ribbon mainFormRibbon;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPnlMain;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private System.Windows.Forms.RibbonTab ribbonTabFile;
        private System.Windows.Forms.RibbonPanel ribbonPnlOpen;
        private System.Windows.Forms.RibbonButton ribbonBtnOpenFolder;
        private System.Windows.Forms.RibbonButton ribbonBtnOpenFile;
        private System.Windows.Forms.RibbonTab ribbonTabBuild;
        private System.Windows.Forms.RibbonPanel ribbonPnlExecute;
        private System.Windows.Forms.RibbonButton ribbonBtnRun;
    }
}

