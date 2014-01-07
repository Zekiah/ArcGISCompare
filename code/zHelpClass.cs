using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ArcGISCompare
{
  class zHelpClass
  {
    private XmlDocument zHelpFile;
    public Boolean isValidHelp;
    private XmlElement aPage;
    public int totalPages = 0;
    public int currentPage = 0;
    public string currentName = "";
    public string currentHelp = "";

    public zHelpClass(XmlDocument zFile)
    {
      zHelpFile = zFile;

      if (zHelpFile.DocumentElement.Name == "HelpFile")
      {
        DateTime expiredLicense = new DateTime(2012, 1, 1);

        if (DateTime.Compare(expiredLicense.Date, DateTime.Now.Date) < 0) { isValidHelp = false; }
        else { isValidHelp = true; }

        totalPages = zHelpFile.DocumentElement.ChildNodes.Count;
        currentPage = 1;
      }
      else { isValidHelp = false; }
    }

    public Boolean DisplayPage(int pageNumber, RichTextBox theHelpBox)
    {
      XmlElement theElement, thePage;

      currentPage = pageNumber;
      theElement = zHelpFile.DocumentElement;

      totalPages = theElement.ChildNodes.Count;

      for (int i = 0; i < totalPages; i++)
      {
        thePage = (XmlElement)theElement.ChildNodes[i];

        if (thePage.ChildNodes[0].InnerText.ToString() == currentPage.ToString())
        {
          aPage = thePage;
          currentName = thePage.ChildNodes[1].InnerText;
          currentHelp = thePage.ChildNodes[2].InnerText;
          theHelpBox.Rtf = currentHelp;
          
          return true;
        }
      }
      return false;
    }
  }
}
