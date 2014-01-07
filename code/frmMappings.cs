using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace ArcGISCompare
{
  public partial class frmMappings : Form
  {
    string TheOutput = "";
    string PrintedSection = "";

    public frmMappings()
    {
      InitializeComponent();
    }

    public void refillMappings(TreeView theRes)
    {
      lblTitle.Text = "The complete list of Results";
      this.Text = "All Detected Results";

      tvResults.Nodes.Clear();

      for (int i = 0; i < theRes.Nodes.Count; i++)
      {
        TreeNode newNode = (TreeNode)theRes.Nodes[i].Clone();
        tvResults.Nodes.Add(newNode);
      }
    }

    public void refillResults(TreeView theRes)
    {
      lblTitle.Text = "The complete list of Results";
      this.Text = "All Detected Results";

      tvResults.Nodes.Clear();

      for (int i = 0; i < theRes.Nodes.Count; i++)
      {
        TreeNode newNode = (TreeNode)theRes.Nodes[i].Clone();
        tvResults.Nodes.Add(newNode);
      }
    }

    private string gTransform(int geomChange)
    {
      switch (geomChange)
      {
        case 1:
          return "A Geometry Transformation of Polygon to Point";
          break;
        case 2:
          return "A Geometry Transformation of Polygon to Polyline";
          break;
        case 3:
          return "A Geometry Transformation of Polyline to Point";
          break;
        case 4:
          return "A Geometry Transformation of Point to Polygon";
          break;
        default:
          return "No Geometry Transformation";
          break;
      }
    }

    private string zTransform(int zChange)
    {
      switch (zChange)
      {
        case 1:  // Remove Z Values
          return "Remove Z Values";
          break;
        case 2:  // Add Z Values
          return "Add Default Z Values";
          break;
        case 3:  // Remove M Values
          return "Remove M Values";
          break;
        case 4:  // Add M Values
          return "Add Default M Values";
          break;
        case 5:  // Remove Z and M Values
          return "Remove Z and M Values";
          break;
        case 6:  // Add Z and M Values
          return "Add Default Z and M Values";
          break;
        default:
          return "No Z/M Adjustment";
          break;
      }
    }

    private string dTransform(int dtChange)
    {
      switch (dtChange)
      {
        case 1:
          return "Converting Integer to String";
          break;
        default:
          return "No Data Type Transformation";
          break;
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      int level = 0;
      TextWriter theOut;

      if (tvResults.Nodes.Count > 0)
      {
          string outpath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (StreamWriter sw = new StreamWriter(outpath + @"\ArcGISCompareResults_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_"
            + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".txt"))
        {
          for (int i = 0; i < tvResults.Nodes.Count; i++)
          {
            TreeNode mainNode = (TreeNode)tvResults.Nodes[i].Clone();

            sw.WriteLine(mainNode.Text);

            level += 1;
            AddSubNodes(mainNode, sw, ref level);
            level -= 1;
          }
        }
      }
    }

    private void btnOldPrint_Click(object sender, EventArgs e)
    {
      int level = 0;

      if (tvResults.Nodes.Count > 0)
      {
        for (int i = 0; i < tvResults.Nodes.Count; i++)
        {
          TreeNode mainNode = (TreeNode)tvResults.Nodes[i].Clone();

          TheOutput += mainNode.Text + Environment.NewLine;

          level += 1;
          AddOldSubNodes(mainNode, ref TheOutput, ref level);
          level -= 1;
        }

        PrintedSection = TheOutput;
        pPreview.Document = pDoc;
        pPreview.BringToFront();
        pPreview.Size = Screen.PrimaryScreen.WorkingArea.Size;
        pPreview.ShowDialog(this);
      }
    }

    private void AddSubNodes(TreeNode childNode, StreamWriter outWrite, ref int Lev)
    {
      string outString = "";

      if (childNode.Nodes.Count > 0)
      {
        for (int n = 0; n < childNode.Nodes.Count; n++)
        {
          outString = "";

          for (int i = 0; i < Lev; i++) outString += "  ";

          outString += childNode.Nodes[n].Text;
          outWrite.WriteLine(outString);

          Lev += 1;
          AddSubNodes(childNode.Nodes[n], outWrite, ref Lev);
          Lev -= 1;
        }
      }
    }

    private void AddOldSubNodes(TreeNode childNode, ref string outString, ref int Lev)
    {
      if (childNode.Nodes.Count > 0)
      {
        for (int n = 0; n < childNode.Nodes.Count; n++)
        {
          for (int i = 0; i < Lev; i++) outString = "    ";

          outString += childNode.Nodes[n].Text;

          Lev += 1;
          AddOldSubNodes(childNode.Nodes[n], ref outString, ref Lev);
          Lev -= 1;
        }
      }
    }

    private void pDoc_PrintPage(object sender, PrintPageEventArgs e)
    {
      int charactersOnPage = 0;
      int linesPerPage = 0;

      if (PrintedSection == "") { PrintedSection = TheOutput; }
      // Sets the value of charactersOnPage to the number of characters 
      // of stringToPrint that will fit within the bounds of the page.
      e.Graphics.MeasureString(PrintedSection, this.Font,
          e.MarginBounds.Size, StringFormat.GenericTypographic,
          out charactersOnPage, out linesPerPage);

      // Draws the string within the bounds of the page
      e.Graphics.DrawString(PrintedSection, this.Font, Brushes.Black,
          e.MarginBounds, StringFormat.GenericTypographic);

      // Remove the portion of the string that has been printed.
      PrintedSection = PrintedSection.Substring(charactersOnPage);

      // Check to see if more pages are to be printed.
      e.HasMorePages = (PrintedSection.Length > 0);
    }

    private void btnExpand_Click(object sender, EventArgs e)
    {
      tvResults.ExpandAll();
    }

    private void btnCollapse_Click(object sender, EventArgs e)
    {
      tvResults.CollapseAll();
    }

  }
}
