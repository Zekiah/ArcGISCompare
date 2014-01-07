using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ArcGISCompare
{
  class msAccProcs
  {
    private Boolean isGood = false;
    private OleDbConnection theConn = null;
    private String theConnString = "";

    public msAccProcs(String fileaName)
    {
      try
      {
        Char quote = Convert.ToChar(34);
			  String Conn1 = "Data Source=" + quote;
			  String Conn2 = fileaName;
        String Conn3 = quote + ";Provider=" + quote + "Microsoft.Jet.OLEDB.4.0" + quote + ";";
        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\myFolder\myAccess2007file.accdb;Persist Security Info=False;
        theConnString = Conn1 + Conn2 + Conn3;
			  OleDbConnection EmptyDBConn = new OleDbConnection(theConnString);

        EmptyDBConn.Open();
        if (EmptyDBConn.State == System.Data.ConnectionState.Open)
        {
          EmptyDBConn.Close();
          theConn = EmptyDBConn;
          isGood = true;
        }
        else theConnString = "";
      }
      catch 
      {
        theConnString = "";
      }
    }

    public bool isConnected             // can proceed
    {
      protected set { }
      get { return isGood; }
    }

    public bool isMapped
    {
      protected set { }
      get { return hasMappingTables(); }
    }

    private Boolean hasMappingTables()
    {
      Boolean retval = false;
      OleDbCommand theCMD;
      OleDbDataReader theRead;

      try
      {
        checkOpen();

        String theSQL = "SELECT * FROM [FeatureMappings]";
        theCMD = new OleDbCommand(theSQL, theConn);

        theRead = theCMD.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        retval = true;
      }
      catch (Exception ex)
      {
        retval = false;
      }

      return retval;
    }

    private void checkOpen()
    {
      if (theConn.State != System.Data.ConnectionState.Open) { theConn.Open(); }
    }

    private int numericDataType(esriFieldType theDT)
    {
      switch (theDT)
      {
        case esriFieldType.esriFieldTypeSmallInteger:
          return 0;
          break;
        case esriFieldType.esriFieldTypeInteger:
          return 1;
          break;
        case esriFieldType.esriFieldTypeSingle:
          return 2;
          break;
        case esriFieldType.esriFieldTypeDouble:
          return 3;
          break;
        case esriFieldType.esriFieldTypeString:
          return 4;
          break;
        case esriFieldType.esriFieldTypeDate:
          return 5;
          break;
        case esriFieldType.esriFieldTypeOID:
          return 6;
          break;
        case esriFieldType.esriFieldTypeGUID:
          return 10;
          break;
        case esriFieldType.esriFieldTypeGlobalID:
          return 11;
          break;
        case esriFieldType.esriFieldTypeXML:
          return 12;
          break;
        default:
          return -1;
      }
    }

    private esriFieldType ToESRIDataType(int theDT)
    {
      switch (theDT)
      {
        case 0:
          return esriFieldType.esriFieldTypeSmallInteger;
          break;
        case 1:
          return esriFieldType.esriFieldTypeInteger;
          break;
        case 2:
          return esriFieldType.esriFieldTypeSingle;
          break;
        case 3:
          return esriFieldType.esriFieldTypeDouble;
          break;
        case 4:
          return esriFieldType.esriFieldTypeString;
          break;
        case 5:
          return esriFieldType.esriFieldTypeDate;
          break;
        case 6:
          return esriFieldType.esriFieldTypeOID;
          break;
        case 10:
          return esriFieldType.esriFieldTypeGUID;
          break;
        case 11:
          return esriFieldType.esriFieldTypeGlobalID;
          break;
        case 12:
          return esriFieldType.esriFieldTypeXML;
          break;
        default:
          return esriFieldType.esriFieldTypeBlob;
      }
    }


    public Boolean BuildMappingTables()
    {
      OleDbCommand theCMD;
      String theSQL = "";

      try
      {
        checkOpen();
        theSQL = "CREATE TABLE [FeatureMappings] ([SerialNumber] long, [srcGeodatabase] varchar(255), [sourceClass] varchar(45), [destinationClass] varchar(45), [sourceGeom] long, [destinationGeom] long, [geomTransform] long, [featureCount] long)";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();

        checkOpen();
        theSQL = "CREATE TABLE [AttributeMappings] ([SerialNumber] long, [sourceClass] varchar(45), [destinationClass] varchar(45), [sourceField] varchar(45), [sourceDT] long, [sourceLength] long, [sourceEnum] varchar(45), [destinationField] varchar(45), [destinationDT] long, [destinationLength] long, [destinationEnum] varchar(45), [valueCount] long, [constant] varchar(255), [results] memo, [transform] long)";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();

        checkOpen();
        theSQL = "CREATE TABLE [ValueMappings] ([srcEnumName] varchar(45), [destEnumName] varchar(45), [sourceValue] varchar(100), [destinationValue] varchar(100), [valueCount] long)";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();

        checkOpen();
        theSQL = "CREATE TABLE [FeatureLoadProblems] ([SerialNumber] long, [GeodatabaseName] varchar(100), [sourceClass] varchar(45), [destinationClass] varchar(45), [srcObjectID] long, [problemDescription] memo)";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();

        checkOpen();
        theSQL = "CREATE TABLE [DataLossProblems] ([SerialNumber] int, [sourceClass] varchar(45), [destinationClass] varchar(45), [sourceFieldName] varchar(45), [srcObjectID] long, [dataLoadError] memo)";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public Boolean DeleteAllMappings()
    {
      OleDbCommand theCMD;
      String theSQL = "";

      try
      {
        checkOpen();
        theSQL = "DELETE * FROM [FeatureMappings]";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        theSQL = "DELETE * FROM [AttributeMappings]";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        theSQL = "DELETE * FROM [FeatureLoadProblems]";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        theSQL = "DELETE * FROM [ValueMappings]";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        theSQL = "DELETE * FROM [DataLossProblems]";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    #region writeFunctions
    public Boolean WriteFeatureMapping(FeatureMappingData theDMap)
    {
       OleDbCommand theCMD;
      String theSQL = "";

      try
      {
        checkOpen();
        theSQL = "INSERT INTO [FeatureMappings] ([SerialNumber], [sourceClass], [destinationClass], [sourceGeom], [destinationGeom], [featureCount], [geomTransform]) VALUES (" + theDMap.serialNumber + ", '" + theDMap.srcFC + "', '" + theDMap.destFC + "', " + Convert.ToInt32(theDMap.srcGeom) + ", " + Convert.ToInt32(theDMap.destGeom) + ", " + theDMap.featCount + ", " + theDMap.geomTransform + ")";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
   }

    public Boolean WriteAttributeMapping(AttributeMappingData theDMap)
    {
      OleDbCommand theCMD;
      String theSQL = "";

      try
      {
        checkOpen();
        theSQL = "INSERT INTO [AttributeMappings] ([SerialNumber], [sourceClass], [destinationClass], [sourceField], [sourceDT], [sourceLength], [sourceEnum], [destinationField], [destinationDT], [destinationLength], [destinationEnum], [valueCount], [constant], [results], [Transform]) VALUES (" + theDMap.serialNumber + ", '" + theDMap.srcFC + "', '" + theDMap.destFC + "', '" + theDMap.srcAtt + "', " + numericDataType(theDMap.srcDT) + ", " + theDMap.srcLen.ToString() + ", '" + theDMap.srcEnum + "', '" + theDMap.destAtt + "', " + numericDataType(theDMap.destDT) + ", " + theDMap.destLen.ToString() + ", '" + theDMap.destEnum + "', " + theDMap.valueCount + ", '" + theDMap.constant + "', '" + theDMap.results + "', " + theDMap.transform + ")";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public Boolean WriteLoadErrors(LoadErrorData theE)
    {
      OleDbCommand theCMD;
      String theSQL = "";

      try
      {
        checkOpen();
        theSQL = "INSERT INTO [FeatureLoadProblems] ([SerialNumber], [sourceClass], [destinationClass], [srcObjectID], [problemDescription]) VALUES (" + theE.serialNumber + ", '" + theE.srcClass + "', '" + theE.destClass + "', " + theE.Count + ", '" + theE.errMsg + "')";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public Boolean WriteDataLoss(DataLossEntries theE)
    {
      OleDbCommand theCMD;
      String theSQL = "";

      try
      {
        checkOpen();
        theSQL = "INSERT INTO [DataLossProblems] ([SerialNumber], [sourceClass], [destinationClass], [sourceFieldName], [srcObjectID], [dataLoadError]) VALUES (" + theE.serialNumber + ", '" + theE.srcClass + "', '" + theE.destClass + "', '" + theE.srcAtt + "', " + theE.ObjectID + ", '" + theE.errMsg + "')";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public Boolean WriteValueMapping(int Serial, ValueMappingData theV)
    {
      OleDbCommand theCMD;
      String theSQL = "";

      try
      {
        checkOpen();
        theSQL = "INSERT INTO [ValueMappings] ([serialNumber], [srcFieldName], [destFieldName], [srcEnumName], [destEnumName], [sourceValue], [destinationValue]) VALUES (" + Serial + ", '" + theV.srcAtt + "', '" + theV.destAtt + "', '" + theV.srcEnum + "', '" + theV.destEnum + "', '" + theV.srcV + "', '" + theV.destV + "')";
        theCMD = new OleDbCommand(theSQL, theConn);
        theCMD.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
    #endregion

    #region readFunctions
    public int LoadAllMappings(ListBox lstBox, IWorkspace sWkSpc, IWorkspace dWkSpc)
    {
      OleDbCommand theCMD;
      OleDbDataReader theReader;
      int retVal = 0;
      String theSQL = "";
      int ser = 0;
      int srcG, destG, geomT, fCount;
      String srcC, destC;
      FeatureMappingData theMap;

      try
      {
        // retrieve the contents of the FeatureMappings table
        theSQL = "SELECT * FROM [FeatureMappings]";
        theCMD = new OleDbCommand(theSQL, theConn);

        checkOpen();
        theReader = theCMD.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        while (theReader.Read())
        {
          // the serial number is key to tracking linkages of the objects while in the database
          ser = theReader.GetInt32(0);      // FeatureMappings.SerialNumber
          srcC = theReader.GetString(2);    // FeatureMappings.sourceClass
          destC = theReader.GetString(3);   // FeatureMappings.destinationClass
          srcG = theReader.GetInt32(4);     // FeatureMappings.sourceGeom
          destG = theReader.GetInt32(5);    // FeatureMappings.destinationGeom
          geomT = theReader.GetInt32(6);    // FeatureMappings.geomTransform
          fCount = theReader.GetInt32(7);   // FeatureMappings.featureCount

          // create the FeatureMappingData object and add to the lstMappings ListBox
          theMap = new FeatureMappingData("", srcC, destC, (esriGeometryType)srcG, (esriGeometryType)destG, ser);
          lstBox.Items.Add(theMap);
          Application.DoEvents();
          theMap.UpdateFeatureCount(fCount);
          theMap.zmTransform = theMap.CalcMZTransform(sWkSpc, dWkSpc);

          // get associated AttributeMappingData objects based on the SerialNumber (ser)
          LoadAttributeMappings(theMap, dWkSpc, ser);

          // get prior DataLoadError objects based on the SerialNumber (ser)
          LoadDataErrors(theMap, ser);
        }

        retVal = ser + 1;
        return retVal;
      }
      catch (Exception ex) { return -1; }
    }

    public Boolean LoadAttributeMappings(FeatureMappingData theMap, IWorkspace theWS, int serial)
    {
      OleDbCommand theCMD;
      OleDbDataReader theReader;
      String theSQL = "";
      int srcDT, destDT, srcL, destL, fCount;
      String srcC, destC, srcF, destF, srcE, destE, constant, result;
      AttributeMappingData theAMap;

      try
      {
        theSQL = "SELECT * FROM [AttributeMappings] WHERE [SerialNumber] = " + serial.ToString();
        theCMD = new OleDbCommand(theSQL, theConn);

        checkOpen();
        theReader = theCMD.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        while (theReader.Read())
        {
          srcC = theReader.GetString(1);
          destC = theReader.GetString(2);
          srcF = theReader.GetString(3);
          destF = theReader.GetString(7);
          srcDT = theReader.GetInt32(4);
          destDT = theReader.GetInt32(8);
          srcL = theReader.GetInt32(5);
          destL = theReader.GetInt32(9);
          srcE = theReader.GetString(6);
          destE = theReader.GetString(10);
          constant = theReader.GetString(12);
          result = theReader.GetString(13);
          fCount = theReader.GetInt32(11);

          IField theSF = new FieldClass();
          IFieldEdit theES = (IFieldEdit)theSF;
          theES.Name_2 = srcF;
          theES.AliasName_2 = srcF;
          theES.Type_2 = ToESRIDataType(srcDT);
          theES.Length_2 = srcL;

          IFields destFld = GeoDbProcs.GetFeatureClassFields(theWS, destC);

          int theIndex = destFld.FindField(destF);

          if (theIndex >= 0)
          {
            IField theDF = destFld.get_Field(theIndex);
            AttributeData theS = new AttributeData(srcC, theSF, fCount);
            AttributeData theD = new AttributeData(destC, theDF, fCount);
            theAMap = new AttributeMappingData(theMap, theS, theD);

            theAMap.AddResults(result);
            theAMap.constant = constant;
            theMap.AddTransform(theAMap);

            LoadValueMappings(theAMap);

            LoadDataLoss(theAMap, serial);
          }
        }

        return true;
      }
      catch (Exception ex) { return false; }
    }

    public Boolean LoadValueMappings(AttributeMappingData theAMap)
    {
      OleDbCommand theCMD;
      OleDbDataReader theReader;
      ValueData theS, theD;
      String theSQL = "";
      int vCount;
      String srcV, destV;
      ValueMappingData theMap;

      try
      {
        theSQL = "SELECT [sourceValue], [destinationValue], [valueCount] FROM [ValueMappings] WHERE [serialNumber] = " + theAMap.serialNumber + " AND [srcFieldName] = '" + theAMap.srcAtt + "' AND [destFieldName] = '" + theAMap.destAtt + "'";
        theCMD = new OleDbCommand(theSQL, theConn);

        checkOpen();
        theReader = theCMD.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        while (theReader.Read())
        {
          srcV = theReader.GetString(0);
          destV = theReader.GetString(1);
          vCount = 0;

          theS = new ValueData(theAMap.srcFC, srcV, theAMap.srcEnum);

          theD = new ValueData(theAMap.destFC, destV, theAMap.destEnum);

          theMap = new ValueMappingData(theAMap, theS, theD);

          theAMap.AddValueConversion(theMap);
        }

        return true;
      }
      catch (Exception ex) { return false; }
    }

    public Boolean LoadDataErrors(FeatureMappingData theMap, int ser)
    {
      OleDbCommand theCMD;
      OleDbDataReader theReader;
      String theSQL = "";
      int theOID;
      String srcC, destC, theMsg;
      //LoadErrorData theEMap;

      try
      {
        theSQL = "SELECT [sourceClass], [destinationClass], [srcObjectID], [problemDescription] FROM [FeatureLoadProblems] WHERE [SerialNumber] = " + ser;
        theCMD = new OleDbCommand(theSQL, theConn);

        checkOpen();
        theReader = theCMD.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        while (theReader.Read())
        {
          srcC = theReader.GetString(0);
          destC = theReader.GetString(1);
          theOID = theReader.GetInt32(2);
          theMsg = theReader.GetString(3);

          //theEMap = new LoadErrorData(ser, srcC, destC, theOID, theMsg);

          //theMap.AddProblemLoad(theEMap);
        }

        return true;
      }
      catch (Exception ex) { return false; }
    }

    public Boolean LoadDataLoss(AttributeMappingData theAMap, int ser)
    {
      OleDbCommand theCMD;
      OleDbDataReader theReader;
      ValueData theS, theD;
      String theSQL = "";
      int theOID;
      String srcC, destC, srcF, theMsg;
      //DataLossEntries theEMap;

      try
      {
        theSQL = "SELECT [sourceClass], [destinationClass], [sourceFieldName], [srcObjectID], [dataLoadError] FROM [DataLossProblems] WHERE [SerialNumber] = " + ser;
        theCMD = new OleDbCommand(theSQL, theConn);

        checkOpen();
        theReader = theCMD.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        while (theReader.Read())
        {
          srcC = theReader.GetString(0);
          destC = theReader.GetString(1);
          srcF = theReader.GetString(2);
          theOID = theReader.GetInt32(3);
          theMsg = theReader.GetString(4);

          //theEMap = new DataLossEntries(ser, srcC, destC, srcF, theOID, theMsg);

          //theAMap.AddDataLoss(theEMap);
        }

        return true;
      }
      catch (Exception ex) { return false; }
    }


    #endregion
  }
}
