using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArcGISCompare
{
  public partial class dlgConstant : Form
  {
    Boolean isNumeric = true;
    public dlgConstant()
    {
      InitializeComponent();

      this.lblDataType.TextChanged += new EventHandler(lblDataType_TextChanged);
      this.lblLength.TextChanged += new EventHandler(lblLength_TextChanged);
      this.forNulls.TextChanged += new EventHandler(forNulls_TextChanged);
      this.lblEnum.TextChanged += new EventHandler(lblEnum_TextChanged);
    }

    void forNulls_TextChanged(object sender, EventArgs e)
    {
      if (forNulls.Text == "F") { chkNulls.Visible = false; }
      else
      {
        chkNulls.Checked = true;
        chkNulls.Visible = true;
      }
    }

    void lblEnum_TextChanged(object sender, EventArgs e)
    {
      if (lblEnum.Text != "")
      {
        cbValues.Visible = true;
        cbValues.Items.Clear();
      }
    }

    void lblLength_TextChanged(object sender, EventArgs e)
    {
      if (lblDataType.Text.StartsWith("String") && lblLength.Text != "")
      {
        isNumeric = false;
        txtConst.MaxLength = Convert.ToInt32(lblLength.Text);
      }
    }

    void lblDataType_TextChanged(object sender, EventArgs e)
    {
      if (lblDataType.Text.StartsWith("String") && lblLength.Text != "")
      {
        isNumeric = false;
        this.txtConst.MaxLength = Convert.ToInt32(lblLength.Text);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      Boolean isGood = false;

      if (txtConst.Text.Length > 0)
      {
        if (isNumeric)
        {
          switch (lblDataType.Text)
          {
            case "Integer":
              int ires;
              if (int.TryParse(txtConst.Text, out ires))
                isGood = true;
              break;
            case "Double":
              double dres;
              if (double.TryParse(txtConst.Text, out dres))
                isGood = true;
              break;
          }
        }
        else { isGood = true; }
      }
      if (isGood == false)
      {
        MessageBox.Show("The Value Entered DOES NOT Match the DataType", "Value Error");
        return;
      }
      else { this.Close(); }
    }
  }
}
