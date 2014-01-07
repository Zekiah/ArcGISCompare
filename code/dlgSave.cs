using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArcGISCompare
{
  public partial class dlgSave : Form
  {
    private Label label1;
    private Label label2;
    private Button btnSave;
    private Button btnCancel;
  
    public dlgSave()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(28, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(220, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Mapping have changed since the last save...";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(63, 42);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(139, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Save the Current Mappings!";
      // 
      // btnSave
      // 
      this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.btnSave.Location = new System.Drawing.Point(97, 80);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(82, 24);
      this.btnSave.TabIndex = 2;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
      this.btnCancel.Location = new System.Drawing.Point(197, 80);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(82, 24);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // dlgSave
      // 
      this.ClientSize = new System.Drawing.Size(291, 110);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgSave";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Mapping have Changed";
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();

    }
  }
}
