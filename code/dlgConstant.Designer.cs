namespace ArcGISCompare
{
  partial class dlgConstant
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
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.chkNulls = new System.Windows.Forms.CheckBox();
      this.txtConst = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lblInsertHere = new System.Windows.Forms.Label();
      this.lblDataType = new System.Windows.Forms.Label();
      this.lblLength = new System.Windows.Forms.Label();
      this.forNulls = new System.Windows.Forms.Label();
      this.cbValues = new System.Windows.Forms.ComboBox();
      this.lblEnum = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(200, 80);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 20);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(275, 80);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 20);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // chkNulls
      // 
      this.chkNulls.AutoSize = true;
      this.chkNulls.Location = new System.Drawing.Point(12, 81);
      this.chkNulls.Name = "chkNulls";
      this.chkNulls.Size = new System.Drawing.Size(156, 17);
      this.chkNulls.TabIndex = 2;
      this.chkNulls.Text = "Only insert for NULL Values";
      this.chkNulls.UseVisualStyleBackColor = true;
      // 
      // txtConst
      // 
      this.txtConst.Location = new System.Drawing.Point(15, 53);
      this.txtConst.Name = "txtConst";
      this.txtConst.Size = new System.Drawing.Size(330, 20);
      this.txtConst.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 36);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(185, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Value to Insert";
      // 
      // lblInsertHere
      // 
      this.lblInsertHere.Location = new System.Drawing.Point(15, 9);
      this.lblInsertHere.Name = "lblInsertHere";
      this.lblInsertHere.Size = new System.Drawing.Size(330, 21);
      this.lblInsertHere.TabIndex = 5;
      this.lblInsertHere.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblDataType
      // 
      this.lblDataType.AutoSize = true;
      this.lblDataType.Location = new System.Drawing.Point(256, 32);
      this.lblDataType.Name = "lblDataType";
      this.lblDataType.Size = new System.Drawing.Size(0, 13);
      this.lblDataType.TabIndex = 6;
      // 
      // lblLength
      // 
      this.lblLength.AutoSize = true;
      this.lblLength.Location = new System.Drawing.Point(317, 30);
      this.lblLength.Name = "lblLength";
      this.lblLength.Size = new System.Drawing.Size(0, 13);
      this.lblLength.TabIndex = 7;
      this.lblLength.Visible = false;
      // 
      // forNulls
      // 
      this.forNulls.AutoSize = true;
      this.forNulls.Location = new System.Drawing.Point(343, 31);
      this.forNulls.Name = "forNulls";
      this.forNulls.Size = new System.Drawing.Size(0, 13);
      this.forNulls.TabIndex = 8;
      this.forNulls.Visible = false;
      // 
      // cbValues
      // 
      this.cbValues.FormattingEnabled = true;
      this.cbValues.Location = new System.Drawing.Point(18, 54);
      this.cbValues.Name = "cbValues";
      this.cbValues.Size = new System.Drawing.Size(325, 21);
      this.cbValues.TabIndex = 9;
      this.cbValues.Visible = false;
      // 
      // lblEnum
      // 
      this.lblEnum.AutoSize = true;
      this.lblEnum.Location = new System.Drawing.Point(209, 33);
      this.lblEnum.Name = "lblEnum";
      this.lblEnum.Size = new System.Drawing.Size(0, 13);
      this.lblEnum.TabIndex = 10;
      this.lblEnum.Visible = false;
      // 
      // dlgConstant
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(362, 107);
      this.Controls.Add(this.lblEnum);
      this.Controls.Add(this.cbValues);
      this.Controls.Add(this.forNulls);
      this.Controls.Add(this.lblLength);
      this.Controls.Add(this.lblDataType);
      this.Controls.Add(this.lblInsertHere);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtConst);
      this.Controls.Add(this.chkNulls);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "dlgConstant";
      this.ShowInTaskbar = false;
      this.Text = "Insert Constant in Implementation Attribute";
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.CheckBox chkNulls;
    public System.Windows.Forms.TextBox txtConst;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.Label lblInsertHere;
    public System.Windows.Forms.Label lblDataType;
    public System.Windows.Forms.Label lblLength;
    public System.Windows.Forms.Label forNulls;
    public System.Windows.Forms.ComboBox cbValues;
    public System.Windows.Forms.Label lblEnum;
  }
}