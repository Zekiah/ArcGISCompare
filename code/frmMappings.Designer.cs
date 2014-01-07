namespace ArcGISCompare
{
  partial class frmMappings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMappings));
            this.lblTitle = new System.Windows.Forms.Label();
            this.tvResults = new System.Windows.Forms.TreeView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExpand = new System.Windows.Forms.Button();
            this.btnCollapse = new System.Windows.Forms.Button();
            this.pDoc = new System.Drawing.Printing.PrintDocument();
            this.pPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.imgNodes = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(602, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Complete List of Assigned Mappings";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tvResults
            // 
            this.tvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvResults.ImageIndex = 0;
            this.tvResults.ImageList = this.imgNodes;
            this.tvResults.Location = new System.Drawing.Point(0, 23);
            this.tvResults.Name = "tvResults";
            this.tvResults.SelectedImageIndex = 0;
            this.tvResults.Size = new System.Drawing.Size(602, 423);
            this.tvResults.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPrint.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPrint.Location = new System.Drawing.Point(536, -1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(66, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExpand
            // 
            this.btnExpand.Location = new System.Drawing.Point(448, 0);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(82, 23);
            this.btnExpand.TabIndex = 4;
            this.btnExpand.Text = "Expand All";
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // btnCollapse
            // 
            this.btnCollapse.Location = new System.Drawing.Point(72, 0);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(82, 23);
            this.btnCollapse.TabIndex = 5;
            this.btnCollapse.Text = "Collapse All";
            this.btnCollapse.UseVisualStyleBackColor = true;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // pDoc
            // 
            this.pDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pDoc_PrintPage);
            // 
            // pPreview
            // 
            this.pPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.pPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.pPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.pPreview.Enabled = true;
            this.pPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("pPreview.Icon")));
            this.pPreview.Name = "pPreview";
            this.pPreview.Visible = false;
            // 
            // imgNodes
            // 
            this.imgNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNodes.ImageStream")));
            this.imgNodes.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNodes.Images.SetKeyName(0, "Check-32.png");
            this.imgNodes.Images.SetKeyName(1, "Error-32.png");
            // 
            // frmMappings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 446);
            this.Controls.Add(this.btnCollapse);
            this.Controls.Add(this.btnExpand);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tvResults);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmMappings";
            this.ShowInTaskbar = false;
            this.Text = "Currently Assigned Mappings";
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.TreeView tvResults;
    private System.Windows.Forms.Button btnPrint;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnExpand;
    private System.Windows.Forms.Button btnCollapse;
    private System.Drawing.Printing.PrintDocument pDoc;
    private System.Windows.Forms.PrintPreviewDialog pPreview;
    private System.Windows.Forms.ImageList imgNodes;
  }
}