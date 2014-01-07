namespace ArcGISCompare
{
  partial class frmMaster
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripStatusLabel comment;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaster));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSource = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDestination = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSaveMappings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnalyzeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnalyzeSource = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnalyzeDestination = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnalyzeInstructions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMappings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowMappings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveMap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuInitializeMap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateMap = new System.Windows.Forms.ToolStripMenuItem();
            this.sOne = new System.Windows.Forms.SplitContainer();
            this.tcTop = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbSource = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TV = new System.Windows.Forms.TreeView();
            this.imgNodes = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lbDestination = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbSrcAttributes = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSourceFeature = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lstAttMappings = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbDestAttributes = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDestinationFeature = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.lbSourceValues = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.lstValueMappings = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbDestinationValues = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.helpBox = new System.Windows.Forms.RichTextBox();
            this.lblHelp = new System.Windows.Forms.Label();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.GPB = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.srcWorkspaceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.destWorkspaceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.instDLG = new System.Windows.Forms.OpenFileDialog();
            this.saveDLG = new System.Windows.Forms.SaveFileDialog();
            this.conMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInsertConstant = new System.Windows.Forms.ToolStripMenuItem();
            this.mapMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMapConstant = new System.Windows.Forms.ToolStripMenuItem();
            comment = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.sOne.Panel1.SuspendLayout();
            this.sOne.Panel2.SuspendLayout();
            this.sOne.SuspendLayout();
            this.tcTop.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.conMenu.SuspendLayout();
            this.mapMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // comment
            // 
            comment.Name = "comment";
            comment.Size = new System.Drawing.Size(118, 17);
            comment.Text = "toolStripStatusLabel1";
            comment.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuApplication,
            this.mnuAnalyze,
            this.mnuRun,
            this.mnuMappings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(859, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "Mapping Selected";
            // 
            // mnuApplication
            // 
            this.mnuApplication.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSource,
            this.mnuDestination,
            this.toolStripSeparator1,
            this.mnuSaveMappings,
            this.mnuSep,
            this.mnuExit});
            this.mnuApplication.Name = "mnuApplication";
            this.mnuApplication.Size = new System.Drawing.Size(80, 20);
            this.mnuApplication.Text = "Application";
            // 
            // mnuSource
            // 
            this.mnuSource.Name = "mnuSource";
            this.mnuSource.Size = new System.Drawing.Size(264, 22);
            this.mnuSource.Text = "Select Template Geodatabase";
            this.mnuSource.Click += new System.EventHandler(this.mnuSourceG_Click);
            // 
            // mnuDestination
            // 
            this.mnuDestination.Name = "mnuDestination";
            this.mnuDestination.Size = new System.Drawing.Size(264, 22);
            this.mnuDestination.Text = "Select Implementation Geodatabase";
            this.mnuDestination.Click += new System.EventHandler(this.mnuDestination_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(261, 6);
            // 
            // mnuSaveMappings
            // 
            this.mnuSaveMappings.Name = "mnuSaveMappings";
            this.mnuSaveMappings.Size = new System.Drawing.Size(264, 22);
            this.mnuSaveMappings.Text = "Save Results";
            this.mnuSaveMappings.Visible = false;
            this.mnuSaveMappings.Click += new System.EventHandler(this.mnuSaveMappings_Click);
            // 
            // mnuSep
            // 
            this.mnuSep.Name = "mnuSep";
            this.mnuSep.Size = new System.Drawing.Size(261, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(264, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuAnalyze
            // 
            this.mnuAnalyze.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAnalyzeAll,
            this.mnuAnalyzeSource,
            this.mnuAnalyzeDestination,
            this.mnuAnalyzeInstructions});
            this.mnuAnalyze.Name = "mnuAnalyze";
            this.mnuAnalyze.Size = new System.Drawing.Size(60, 20);
            this.mnuAnalyze.Text = "Analyze";
            this.mnuAnalyze.Visible = false;
            // 
            // mnuAnalyzeAll
            // 
            this.mnuAnalyzeAll.Name = "mnuAnalyzeAll";
            this.mnuAnalyzeAll.Size = new System.Drawing.Size(159, 22);
            this.mnuAnalyzeAll.Text = "All";
            this.mnuAnalyzeAll.Visible = false;
            // 
            // mnuAnalyzeSource
            // 
            this.mnuAnalyzeSource.Name = "mnuAnalyzeSource";
            this.mnuAnalyzeSource.Size = new System.Drawing.Size(159, 22);
            this.mnuAnalyzeSource.Text = "Template";
            this.mnuAnalyzeSource.Visible = false;
            this.mnuAnalyzeSource.Click += new System.EventHandler(this.mnuAnalyzeSource_Click);
            // 
            // mnuAnalyzeDestination
            // 
            this.mnuAnalyzeDestination.Name = "mnuAnalyzeDestination";
            this.mnuAnalyzeDestination.Size = new System.Drawing.Size(159, 22);
            this.mnuAnalyzeDestination.Text = "Implementation";
            this.mnuAnalyzeDestination.Visible = false;
            // 
            // mnuAnalyzeInstructions
            // 
            this.mnuAnalyzeInstructions.Name = "mnuAnalyzeInstructions";
            this.mnuAnalyzeInstructions.Size = new System.Drawing.Size(159, 22);
            this.mnuAnalyzeInstructions.Text = "Instructions";
            this.mnuAnalyzeInstructions.Visible = false;
            // 
            // mnuRun
            // 
            this.mnuRun.Name = "mnuRun";
            this.mnuRun.Size = new System.Drawing.Size(40, 20);
            this.mnuRun.Text = "Run";
            this.mnuRun.Visible = false;
            this.mnuRun.Click += new System.EventHandler(this.mnuRun_Click);
            // 
            // mnuMappings
            // 
            this.mnuMappings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowMappings,
            this.mnuSaveMap,
            this.toolStripSeparator2,
            this.mnuInitializeMap,
            this.mnuUpdateMap});
            this.mnuMappings.Name = "mnuMappings";
            this.mnuMappings.Size = new System.Drawing.Size(56, 20);
            this.mnuMappings.Text = "Results";
            this.mnuMappings.Visible = false;
            // 
            // mnuShowMappings
            // 
            this.mnuShowMappings.Name = "mnuShowMappings";
            this.mnuShowMappings.Size = new System.Drawing.Size(117, 22);
            this.mnuShowMappings.Text = "Show";
            this.mnuShowMappings.Click += new System.EventHandler(this.mnuShowMappings_Click);
            // 
            // mnuSaveMap
            // 
            this.mnuSaveMap.Name = "mnuSaveMap";
            this.mnuSaveMap.Size = new System.Drawing.Size(117, 22);
            this.mnuSaveMap.Text = "Save";
            this.mnuSaveMap.Click += new System.EventHandler(this.mnuSaveMappings_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(114, 6);
            // 
            // mnuInitializeMap
            // 
            this.mnuInitializeMap.Name = "mnuInitializeMap";
            this.mnuInitializeMap.Size = new System.Drawing.Size(117, 22);
            this.mnuInitializeMap.Text = "Initialize";
            // 
            // mnuUpdateMap
            // 
            this.mnuUpdateMap.Name = "mnuUpdateMap";
            this.mnuUpdateMap.Size = new System.Drawing.Size(117, 22);
            this.mnuUpdateMap.Text = "Update";
            // 
            // sOne
            // 
            this.sOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sOne.Location = new System.Drawing.Point(0, 24);
            this.sOne.Name = "sOne";
            this.sOne.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sOne.Panel1
            // 
            this.sOne.Panel1.Controls.Add(this.tcTop);
            this.sOne.Panel1MinSize = 350;
            // 
            // sOne.Panel2
            // 
            this.sOne.Panel2.Controls.Add(this.helpBox);
            this.sOne.Panel2.Controls.Add(this.lblHelp);
            this.sOne.Panel2.Controls.Add(this.statusStrip2);
            this.sOne.Panel2.Controls.Add(this.GPB);
            this.sOne.Panel2.Controls.Add(this.statusStrip1);
            this.sOne.Panel2MinSize = 100;
            this.sOne.Size = new System.Drawing.Size(859, 460);
            this.sOne.SplitterDistance = 350;
            this.sOne.TabIndex = 1;
            // 
            // tcTop
            // 
            this.tcTop.Controls.Add(this.tabPage1);
            this.tcTop.Controls.Add(this.tabPage2);
            this.tcTop.Controls.Add(this.tabPage3);
            this.tcTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTop.ItemSize = new System.Drawing.Size(275, 18);
            this.tcTop.Location = new System.Drawing.Point(0, 0);
            this.tcTop.Name = "tcTop";
            this.tcTop.SelectedIndex = 0;
            this.tcTop.Size = new System.Drawing.Size(859, 350);
            this.tcTop.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcTop.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(851, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Comparing Features";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel1.Controls.Add(this.lbSource);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(845, 318);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbSource
            // 
            this.lbSource.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbSource.FormattingEnabled = true;
            this.lbSource.Location = new System.Drawing.Point(0, 18);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(246, 296);
            this.lbSource.Sorted = true;
            this.lbSource.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Template Data Feature Classes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.TV);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer2.Panel2.Controls.Add(this.lbDestination);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(591, 318);
            this.splitContainer2.SplitterDistance = 337;
            this.splitContainer2.TabIndex = 0;
            // 
            // TV
            // 
            this.TV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV.ImageIndex = 0;
            this.TV.ImageList = this.imgNodes;
            this.TV.Location = new System.Drawing.Point(0, 18);
            this.TV.Name = "TV";
            this.TV.SelectedImageIndex = 0;
            this.TV.Size = new System.Drawing.Size(333, 296);
            this.TV.TabIndex = 1;
            // 
            // imgNodes
            // 
            this.imgNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNodes.ImageStream")));
            this.imgNodes.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNodes.Images.SetKeyName(0, "Check-32.png");
            this.imgNodes.Images.SetKeyName(1, "Error-32.png");
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Comparison Results";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDestination
            // 
            this.lbDestination.AllowDrop = true;
            this.lbDestination.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDestination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDestination.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbDestination.FormattingEnabled = true;
            this.lbDestination.Location = new System.Drawing.Point(0, 18);
            this.lbDestination.Name = "lbDestination";
            this.lbDestination.Size = new System.Drawing.Size(246, 296);
            this.lbDestination.Sorted = true;
            this.lbDestination.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Implementation Data Feature Classes";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(851, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Comparing Attributes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbSrcAttributes);
            this.splitContainer3.Panel1.Controls.Add(this.label10);
            this.splitContainer3.Panel1.Controls.Add(this.lblSourceFeature);
            this.splitContainer3.Panel1MinSize = 250;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(845, 318);
            this.splitContainer3.SplitterDistance = 250;
            this.splitContainer3.TabIndex = 0;
            // 
            // lbSrcAttributes
            // 
            this.lbSrcAttributes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbSrcAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSrcAttributes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbSrcAttributes.FormattingEnabled = true;
            this.lbSrcAttributes.Location = new System.Drawing.Point(0, 36);
            this.lbSrcAttributes.Name = "lbSrcAttributes";
            this.lbSrcAttributes.Size = new System.Drawing.Size(246, 278);
            this.lbSrcAttributes.Sorted = true;
            this.lbSrcAttributes.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(246, 18);
            this.label10.TabIndex = 4;
            this.label10.Text = "Template Attributes";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSourceFeature
            // 
            this.lblSourceFeature.BackColor = System.Drawing.Color.Gainsboro;
            this.lblSourceFeature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSourceFeature.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSourceFeature.Location = new System.Drawing.Point(0, 0);
            this.lblSourceFeature.Name = "lblSourceFeature";
            this.lblSourceFeature.Size = new System.Drawing.Size(246, 18);
            this.lblSourceFeature.TabIndex = 2;
            this.lblSourceFeature.Text = "Template Attributes";
            this.lblSourceFeature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lstAttMappings);
            this.splitContainer4.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lbDestAttributes);
            this.splitContainer4.Panel2.Controls.Add(this.label4);
            this.splitContainer4.Panel2.Controls.Add(this.lblDestinationFeature);
            this.splitContainer4.Size = new System.Drawing.Size(591, 318);
            this.splitContainer4.SplitterDistance = 328;
            this.splitContainer4.TabIndex = 0;
            // 
            // lstAttMappings
            // 
            this.lstAttMappings.BackColor = System.Drawing.Color.Gainsboro;
            this.lstAttMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAttMappings.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstAttMappings.FormattingEnabled = true;
            this.lstAttMappings.Location = new System.Drawing.Point(0, 18);
            this.lstAttMappings.Name = "lstAttMappings";
            this.lstAttMappings.Size = new System.Drawing.Size(324, 296);
            this.lstAttMappings.TabIndex = 3;
            this.lstAttMappings.SelectedIndexChanged += new System.EventHandler(this.lstAttMappings_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Silver;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(324, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "Currently Defined Attribute Comparisons";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDestAttributes
            // 
            this.lbDestAttributes.AllowDrop = true;
            this.lbDestAttributes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDestAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDestAttributes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbDestAttributes.FormattingEnabled = true;
            this.lbDestAttributes.Location = new System.Drawing.Point(0, 36);
            this.lbDestAttributes.Name = "lbDestAttributes";
            this.lbDestAttributes.Size = new System.Drawing.Size(255, 278);
            this.lbDestAttributes.Sorted = true;
            this.lbDestAttributes.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Implementation Attributes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDestinationFeature
            // 
            this.lblDestinationFeature.BackColor = System.Drawing.Color.Gainsboro;
            this.lblDestinationFeature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDestinationFeature.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDestinationFeature.Location = new System.Drawing.Point(0, 0);
            this.lblDestinationFeature.Name = "lblDestinationFeature";
            this.lblDestinationFeature.Size = new System.Drawing.Size(255, 18);
            this.lblDestinationFeature.TabIndex = 3;
            this.lblDestinationFeature.Text = "Implementation Attributes";
            this.lblDestinationFeature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(851, 324);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Comparing Values";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer7
            // 
            this.splitContainer7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.lbSourceValues);
            this.splitContainer7.Panel1.Controls.Add(this.label8);
            this.splitContainer7.Panel1MinSize = 250;
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer7.Size = new System.Drawing.Size(851, 324);
            this.splitContainer7.SplitterDistance = 250;
            this.splitContainer7.TabIndex = 0;
            // 
            // lbSourceValues
            // 
            this.lbSourceValues.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbSourceValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSourceValues.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbSourceValues.FormattingEnabled = true;
            this.lbSourceValues.Location = new System.Drawing.Point(0, 18);
            this.lbSourceValues.Name = "lbSourceValues";
            this.lbSourceValues.Size = new System.Drawing.Size(246, 302);
            this.lbSourceValues.Sorted = true;
            this.lbSourceValues.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(246, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "Template Data Values";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer8
            // 
            this.splitContainer8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.lstValueMappings);
            this.splitContainer8.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.lbDestinationValues);
            this.splitContainer8.Panel2.Controls.Add(this.label9);
            this.splitContainer8.Size = new System.Drawing.Size(597, 324);
            this.splitContainer8.SplitterDistance = 325;
            this.splitContainer8.TabIndex = 0;
            // 
            // lstValueMappings
            // 
            this.lstValueMappings.BackColor = System.Drawing.Color.Gainsboro;
            this.lstValueMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstValueMappings.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstValueMappings.FormattingEnabled = true;
            this.lstValueMappings.Location = new System.Drawing.Point(0, 18);
            this.lstValueMappings.Name = "lstValueMappings";
            this.lstValueMappings.Size = new System.Drawing.Size(321, 302);
            this.lstValueMappings.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(321, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Currently Defined Value Comparisons";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDestinationValues
            // 
            this.lbDestinationValues.AllowDrop = true;
            this.lbDestinationValues.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbDestinationValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDestinationValues.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbDestinationValues.FormattingEnabled = true;
            this.lbDestinationValues.Location = new System.Drawing.Point(0, 18);
            this.lbDestinationValues.Name = "lbDestinationValues";
            this.lbDestinationValues.Size = new System.Drawing.Size(264, 302);
            this.lbDestinationValues.Sorted = true;
            this.lbDestinationValues.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(264, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "Implementation Data Values";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // helpBox
            // 
            this.helpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpBox.Location = new System.Drawing.Point(0, 27);
            this.helpBox.Name = "helpBox";
            this.helpBox.Size = new System.Drawing.Size(859, 57);
            this.helpBox.TabIndex = 5;
            this.helpBox.Text = "";
            this.helpBox.Visible = false;
            // 
            // lblHelp
            // 
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelp.Location = new System.Drawing.Point(0, 8);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(859, 19);
            this.lblHelp.TabIndex = 4;
            this.lblHelp.Text = "Help/Information";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHelp.Visible = false;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            comment});
            this.statusStrip2.Location = new System.Drawing.Point(0, 149);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(859, 22);
            this.statusStrip2.TabIndex = 3;
            this.statusStrip2.Text = "statusStrip2";
            this.statusStrip2.Visible = false;
            // 
            // GPB
            // 
            this.GPB.Dock = System.Windows.Forms.DockStyle.Top;
            this.GPB.Location = new System.Drawing.Point(0, 0);
            this.GPB.Name = "GPB";
            this.GPB.Size = new System.Drawing.Size(859, 8);
            this.GPB.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.srcWorkspaceLabel,
            this.destWorkspaceLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 84);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(859, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // srcWorkspaceLabel
            // 
            this.srcWorkspaceLabel.AutoSize = false;
            this.srcWorkspaceLabel.Name = "srcWorkspaceLabel";
            this.srcWorkspaceLabel.Size = new System.Drawing.Size(422, 17);
            this.srcWorkspaceLabel.Spring = true;
            this.srcWorkspaceLabel.Text = "Select the Template Workspace";
            this.srcWorkspaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // destWorkspaceLabel
            // 
            this.destWorkspaceLabel.AutoSize = false;
            this.destWorkspaceLabel.Name = "destWorkspaceLabel";
            this.destWorkspaceLabel.Size = new System.Drawing.Size(422, 17);
            this.destWorkspaceLabel.Spring = true;
            this.destWorkspaceLabel.Text = "Select the Implementation Workspace";
            this.destWorkspaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveDLG
            // 
            this.saveDLG.DefaultExt = "mdb";
            this.saveDLG.Filter = "Mappings Database (*.mdb)|*.mdb";
            // 
            // conMenu
            // 
            this.conMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsertConstant});
            this.conMenu.Name = "conMenu";
            this.conMenu.Size = new System.Drawing.Size(155, 26);
            this.conMenu.Text = "Insert Constant";
            // 
            // mnuInsertConstant
            // 
            this.mnuInsertConstant.Name = "mnuInsertConstant";
            this.mnuInsertConstant.Size = new System.Drawing.Size(154, 22);
            this.mnuInsertConstant.Text = "Insert Constant";
            // 
            // mapMenu
            // 
            this.mapMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMapConstant});
            this.mapMenu.Name = "mapMenu";
            this.mapMenu.Size = new System.Drawing.Size(155, 26);
            // 
            // mnuMapConstant
            // 
            this.mnuMapConstant.Name = "mnuMapConstant";
            this.mnuMapConstant.Size = new System.Drawing.Size(154, 22);
            this.mnuMapConstant.Text = "Insert Constant";
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 484);
            this.Controls.Add(this.sOne);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Geodatabase Schema Comparison Tool";
            this.Load += new System.EventHandler(this.frmMaster_Load);
            this.ResizeEnd += new System.EventHandler(this.frmMaster_ResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.sOne.Panel1.ResumeLayout(false);
            this.sOne.Panel2.ResumeLayout(false);
            this.sOne.Panel2.PerformLayout();
            this.sOne.ResumeLayout(false);
            this.tcTop.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.conMenu.ResumeLayout(false);
            this.mapMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuApplication;
    private System.Windows.Forms.ToolStripMenuItem mnuSource;
    private System.Windows.Forms.ToolStripMenuItem mnuDestination;
    private System.Windows.Forms.ToolStripSeparator mnuSep;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.SplitContainer sOne;
    private System.Windows.Forms.TabControl tcTop;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListBox lbSource;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel srcWorkspaceLabel;
    private System.Windows.Forms.ToolStripStatusLabel destWorkspaceLabel;
    private System.Windows.Forms.ListBox lbDestination;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ToolStripMenuItem mnuAnalyze;
    private System.Windows.Forms.ToolStripMenuItem mnuAnalyzeAll;
    private System.Windows.Forms.ToolStripMenuItem mnuAnalyzeSource;
    private System.Windows.Forms.ToolStripMenuItem mnuAnalyzeDestination;
    private System.Windows.Forms.ToolStripMenuItem mnuAnalyzeInstructions;
    private System.Windows.Forms.ProgressBar GPB;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.ListBox lbSrcAttributes;
    private System.Windows.Forms.Label lblSourceFeature;
    private System.Windows.Forms.ListBox lbDestAttributes;
    private System.Windows.Forms.Label lblDestinationFeature;
    private System.Windows.Forms.ListBox lstAttMappings;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.OpenFileDialog instDLG;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveMappings;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem mnuRun;
    private System.Windows.Forms.SaveFileDialog saveDLG;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.SplitContainer splitContainer7;
    private System.Windows.Forms.ListBox lbSourceValues;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.SplitContainer splitContainer8;
    private System.Windows.Forms.ListBox lbDestinationValues;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ListBox lstValueMappings;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ContextMenuStrip conMenu;
    private System.Windows.Forms.ToolStripMenuItem mnuInsertConstant;
    private System.Windows.Forms.ContextMenuStrip mapMenu;
    private System.Windows.Forms.ToolStripMenuItem mnuMapConstant;
    private System.Windows.Forms.StatusStrip statusStrip2;
    private System.Windows.Forms.ToolStripMenuItem mnuSourceG;
    private System.Windows.Forms.ToolStripMenuItem mnuSourceD;
    private System.Windows.Forms.ToolStripMenuItem mnuMappings;
    private System.Windows.Forms.ToolStripMenuItem mnuShowMappings;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveMap;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem mnuInitializeMap;
    private System.Windows.Forms.ToolStripMenuItem mnuUpdateMap;
    private System.Windows.Forms.RichTextBox helpBox;
    private System.Windows.Forms.Label lblHelp;
    private System.Windows.Forms.TreeView TV;
    private System.Windows.Forms.ImageList imgNodes;
  }
}

