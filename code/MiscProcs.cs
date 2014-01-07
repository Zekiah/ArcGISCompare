using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace ArcGISCompare
{
  internal static class MiscProcs
  {
    internal static String TrimText(string OrigText, Font f, Size ProposedSize)
    {
      Size TheSize = TextRenderer.MeasureText(OrigText, f);

      String TheShortString = OrigText;
      String DerivedString = OrigText.Substring(0, 3);
      String PathString = TheShortString.Substring(2);
      try
      {
          while (TheSize.Width >= ProposedSize.Width)
          {
              PathString = PathString.Substring(1);
              if (PathString.Contains("\\"))
              {
                  PathString = PathString.Substring(PathString.IndexOf("\\") + 1);
                  TheShortString = DerivedString + "..." + PathString;
              }
              TheSize = TextRenderer.MeasureText(TheShortString, f);
          }
      }
      catch { }
      return TheShortString;
    }

    internal static Boolean CreateAccessDatabase(String DatabaseFullPath)
    {
      Boolean bAns = false;
      ADOX.Catalog cat = new ADOX.Catalog();

      try
      {
        string theConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DatabaseFullPath;

        cat.Create(theConnection);

        bAns = true;
      }
      catch (System.Runtime.InteropServices.COMException ex)
      {
        MessageBox.Show(ex.Message);
        bAns = false;
      }
      
      cat = null;

      return bAns;
    }

    internal static void addtoLog(string logText, TextBox theTBox)
    {
      theTBox.Text = theTBox.Text + logText + " " + DateTime.Now.ToString() + "  \r\n";
    }

    internal static String buildNONSDSName(String inName, esriWorkspaceType theWSType)
    {
      switch (theWSType)
      {
        case esriWorkspaceType.esriLocalDatabaseWorkspace:
          return "NONATT_" + inName;
          break;
        case esriWorkspaceType.esriFileSystemWorkspace:
					if (inName.Length <= 23) return "NONATT_" + inName;
          else
          {
            inName = inName.Substring(30 - inName.Length, inName.Length * 2 - 30);
						return "NONATT_" + inName;
          }
          break;
        case esriWorkspaceType.esriRemoteDatabaseWorkspace:
					if (inName.Length <= 23) return "NONATT_" + inName;
          else
          {
            inName = inName.Substring(30 - inName.Length, inName.Length * 2 - 30);
						return "NONATT_" + inName;
          }
          break;
        default:
          return inName;
          break;
      }
    }

    internal static ESRI.ArcGIS.Geodatabase.esriFieldType BuildDataType(String theType, int theLength)
    {
      esriFieldType theRetValue = esriFieldType.esriFieldTypeString;
      String lngtString;

      theType = theType.ToLower();
      try
      {
        if (theType.StartsWith("string"))
        {
          lngtString = theType.Substring(7, theType.Length - 8);
          theLength = (int)Convert.ToInt32(lngtString);
        }
        else
        {
        switch (theType.ToLower())
        {
          case "integer":
            theRetValue = esriFieldType.esriFieldTypeInteger;
            break;
          case "single":
            theRetValue = esriFieldType.esriFieldTypeSingle;
            break;
          case "double":
            theRetValue = esriFieldType.esriFieldTypeDouble;
            break;
          case "guid":
            theRetValue = esriFieldType.esriFieldTypeGUID;
            break;
          case "smallInteger":
            theRetValue = esriFieldType.esriFieldTypeSmallInteger;
            break;
          case "date":
            theRetValue = esriFieldType.esriFieldTypeDate;
            break;
        }
        }
      }
      catch { theRetValue = esriFieldType.esriFieldTypeString; }
      return theRetValue;
    }

    internal static String UnBuildDataType(esriFieldType theType, int theLength)
    {
      String retVal = "";

      switch (theType)
      {
        case esriFieldType.esriFieldTypeGUID:
          retVal = "GUID";
          break;
        case esriFieldType.esriFieldTypeInteger:
          retVal = "Integer";
          break;
        case esriFieldType.esriFieldTypeSmallInteger:
          retVal = "SmallInteger";
          break;
        case esriFieldType.esriFieldTypeSingle:
          retVal = "Single";
          break;
        case esriFieldType.esriFieldTypeDouble:
          retVal = "Double";
          break;
        case esriFieldType.esriFieldTypeString:
          retVal = "String(" + theLength.ToString() + ")";
          break;
        case esriFieldType.esriFieldTypeDate:
          retVal = "DateTime";
          break;
      }

      return retVal; 
    }

    internal static String fileNameONLY(String fullName)
    {
      FileInfo theF = new FileInfo(fullName);
      return theF.Name;
    }

    internal static int getTransformNumber(esriFieldType srcDT, esriFieldType destDT)
    {
      switch (destDT)
      {
        case esriFieldType.esriFieldTypeString:
          switch (srcDT)
          {
            case esriFieldType.esriFieldTypeInteger:
              return 13;
              break;
            case esriFieldType.esriFieldTypeSingle:
              return 12;
              break;
            case esriFieldType.esriFieldTypeDouble:
              return 10;
              break;
            case esriFieldType.esriFieldTypeString:
              return 0;
              break;
            case esriFieldType.esriFieldTypeDate:
              return 14;
              break;
            default:
              return -1;
              break;
          }
        case esriFieldType.esriFieldTypeInteger:
          switch (srcDT)
          {
            case esriFieldType.esriFieldTypeSingle:
              return 4;
              break;
            case esriFieldType.esriFieldTypeDouble:
              return 5;
              break;
            case esriFieldType.esriFieldTypeString:
              return 6;
              break;
            case esriFieldType.esriFieldTypeDate:
              return 7;
              break;
            case esriFieldType.esriFieldTypeInteger:
              return 0;
              break;
            default:
              return -1;
              break;
          }
          break;
        case esriFieldType.esriFieldTypeDouble:
          switch (srcDT)
          {
            case esriFieldType.esriFieldTypeSmallInteger:
              return 1;
              break;
            case esriFieldType.esriFieldTypeInteger:
              return 3;
              break;
            case esriFieldType.esriFieldTypeString:
              return 2;
              break;
            case esriFieldType.esriFieldTypeSingle:
              return 0;
              break;
            case esriFieldType.esriFieldTypeDouble:
              return 0;
              break;
            default:
              return -1;
              break;
          }
          break;
        case esriFieldType.esriFieldTypeGUID:
          switch (srcDT)
          {
            case esriFieldType.esriFieldTypeString:
              return 16;
              break;
            case esriFieldType.esriFieldTypeGUID:
              return 0;
              break;
            default:
              return -1;
              break;
          }
        default:
          return -1;
          break;
      }
    }

    internal static String getGeomTransform(int geomTran)
    {
        switch (geomTran)
        {
          case 1:
          case 2:
            return "Polygon to Point";
            break;
          case 3:
            return "Point to Polygon";
            break;
          case 4:
            return "Polyline to Point";
            break;
          default:
            return "Undefined";
            break;
        }
    }

		internal static Boolean NoLocks(IClass theLockClass)
		{
			Boolean Result = false;
			ISchemaLock theLock;
			ISchemaLockInfo oneLock;
			IEnumSchemaLockInfo allLocks;

			try
			{
				theLock = (ISchemaLock)theLockClass;
				theLock.GetCurrentSchemaLocks(out allLocks);

				allLocks.Reset();
				oneLock = allLocks.Next();
				oneLock = allLocks.Next();
				if (oneLock != null)
					Result = false;
				else { Result = true; }
				if (Result) { theLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock); }
			}
			catch { Result = false; }

		return Result;
		}

		internal static Boolean FreeLocks(IClass theLockClass)
		{
			Boolean Result = false;
			ISchemaLock theLock;

			try
			{
				theLock = (ISchemaLock)theLockClass;
				theLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
				Result = true;
			}
			catch { Result = false; }

			return Result;
		}

		internal static Boolean canDoRelates()
		{
			Boolean Result = false;

			IAoInitialize theInit;
			ILicenseInformation theLicense;

			theInit = new AoInitializeClass();

			theLicense = (ILicenseInformation)theInit;

			String theProduct = theLicense.GetLicenseProductName(theInit.InitializedProduct());

			if (theProduct == "esriLicenseProductCodeArcEditor" || theProduct == "esriLicenseProductCodeArcInfo") return true;

			return Result;
		}
  }
}

