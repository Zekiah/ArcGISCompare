using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;

namespace ArcGISCompare
{
  internal static class GeoDbProcs
  {
    internal static ESRI.ArcGIS.Geodatabase.IWorkspace GetWorkspace(String title)
    {
      IGxDialog theGDlg = new GxDialogClass();
      IGxObject theObject;
      IEnumGxObject theResults;

      IGxObject theGeoDb = (IGxObject)new GxDatabase();
      IGxDatabase theWorkspace;
      ESRI.ArcGIS.Catalog.esriDoubleClickResult theClick = esriDoubleClickResult.esriDCRChooseAndDismiss;

      theGDlg.Title = title;
      theGDlg.ButtonCaption = "Open";
      theGDlg.RememberLocation = true;
      IGxObjectFilter theFilter = new GxFilterWorkspacesClass();
      theFilter.CanChooseObject(theGeoDb, ref theClick);

      theGDlg.ObjectFilter = theFilter;
      if (theGDlg.DoModalOpen(0, out theResults))
      {
        Application.DoEvents();
        theObject = theResults.Next();
        if (theObject == null) return null;
        theWorkspace = (IGxDatabase)theObject;
        return theWorkspace.Workspace;
      }
      return null;
    }

    internal static String[] GetFeatureClassNames(IWorkspace TheWrkspc)
    {
      String[] TheNames = new String[0];

      IEnumDatasetName TheFCNames;
      IDatasetName OneFCName;

      // iterate through all the Feature Datasets...
      IEnumDatasetName TheFDNames = TheWrkspc.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
      IDatasetName OneFDName = TheFDNames.Next();
      while (OneFDName != null)
      {

        // iterate through all the Feature Classes in the Dataset...
        TheFCNames = OneFDName.SubsetNames;
        OneFCName = TheFCNames.Next();
        while (OneFCName != null)
        {
          if (OneFCName.Type == esriDatasetType.esriDTFeatureClass)
          {
            // add each Feature Class name to the listbox
            System.Array.Resize<String>(ref TheNames, TheNames.Length + 1);
            TheNames[TheNames.Length - 1] = OneFCName.Name;
          }
          // ...and continue iterating...
          OneFCName = TheFCNames.Next();
        }

        // ...continue iterating through each feature dataset...
        OneFDName = TheFDNames.Next();
      }

      // now iterate through all the non-dataset feature classes...
      TheFCNames = TheWrkspc.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
      OneFCName = TheFCNames.Next();
      while (OneFCName != null)
      {
        // add each feature class name to the listbox
        System.Array.Resize<String>(ref TheNames, TheNames.Length + 1);
        TheNames[TheNames.Length - 1] = OneFCName.Name;
        // ...and continue iterating...
        OneFCName = TheFCNames.Next();
      }

      return TheNames;
    }

    internal static Int32 GetFeatureCount(String TheFCName, IWorkspace TheWrkSpc)
    {
      IFeatureClass theFClass = GetFeatureClass(TheWrkSpc, TheFCName);

      if (theFClass != null) { return theFClass.FeatureCount(null); }
      else return 0;
    }

    internal static esriGeometryType GetGeometryType(String theFCName, IWorkspace TheWrkSpc)
    {
      IFeatureClass theFClass = GetFeatureClass(TheWrkSpc, theFCName);

      if (theFClass != null) { return theFClass.ShapeType; }
      else return esriGeometryType.esriGeometryNull;
    }

    internal static IFeatureClass GetFeatureClass(IWorkspace TheWrkspc, String TheFCName)
    {
      try
      {
        IFeatureWorkspace theFWorkSpace = (IFeatureWorkspace)TheWrkspc;
        
        return theFWorkSpace.OpenFeatureClass(TheFCName);
      }
      catch (Exception ex) { }

      return null;
    }

    internal static ITable GetTable(IWorkspace TheWrkspc, String TheTName)
    {
      IFeatureWorkspace theFWorkSpace = (IFeatureWorkspace)TheWrkspc;

      return theFWorkSpace.OpenTable(TheTName);
    }

    internal static IFields GetFeatureClassFields(IWorkspace TheWrkspc, String TheFCName)
    {
      IFeatureClass theFClass = GetFeatureClass(TheWrkspc, TheFCName);

      if (theFClass != null) { return theFClass.Fields; }
      else return null;
    }

    internal static IClass BuildNONSDS(IWorkspace theWS, String NONName)
    {
      ITable t;
      IFields wFlds;
      IFieldsEdit wFldsEdit;
      IFieldEdit wFldEdit;
      UID theUID;
      Boolean buildTable = true;

      IFeatureWorkspace theFWS = (IFeatureWorkspace)theWS;
      try { t = theFWS.OpenTable(NONName); }
      catch { t = null; }
      finally { }

      if (t != null)
      {
        DialogResult theAns = MessageBox.Show("The table " + NONName + " Already Exists! Delete?", "Table Already Exists!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        buildTable = false;
        if (theAns == DialogResult.Yes)
        {
          try
          {
            IDataset theClass = (IDataset)t;
            if (theClass.CanDelete())
            {
              theClass.Delete();
              buildTable = true;
            }
          }
          catch (Exception ex) { }
        }
      }

      if (buildTable)
      {
        wFlds = new FieldsClass();
        wFldsEdit = (IFieldsEdit)wFlds;

        wFldsEdit.FieldCount_2 = 1;

        IField wFld = new FieldClass();
        wFldEdit = (IFieldEdit)wFld;

        wFldEdit.Name_2 = "OBJECTID";
        wFldEdit.AliasName_2 = "Object Identifier";
        wFldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
        wFldsEdit.set_Field(0, wFld);

        theUID = new UIDClass();
        theUID.Value = "esriGeoDatabase.Object";

        t = theFWS.CreateTable(NONName, wFlds, theUID, null, "");

        return (IClass)t;
      }
      else return null;
    }

    internal static int GetValueOccurrences(IWorkspace theWS, String which, AttributeMappingData theMap, String theV)
    {
      IFeatureClass theFC = null;
      String fieldName = "";
      esriFieldType theT = esriFieldType.esriFieldTypeString;

      ISQLSyntax theS = (ISQLSyntax)theWS;
      switch (which)
      {
        case "Template":
          theFC = GetFeatureClass(theWS, theMap.srcFC);
          fieldName = theS.GetSpecialCharacter(esriSQLSpecialCharacters.esriSQL_DelimitedIdentifierPrefix) + theMap.srcAtt + theS.GetSpecialCharacter(esriSQLSpecialCharacters.esriSQL_DelimitedIdentifierSuffix);
          theT = theMap.srcDT;
          break;
        case "Implementation":
          theFC = GetFeatureClass(theWS, theMap.destFC);
          fieldName = theS.GetSpecialCharacter(esriSQLSpecialCharacters.esriSQL_DelimitedIdentifierPrefix) + theMap.destAtt + theS.GetSpecialCharacter(esriSQLSpecialCharacters.esriSQL_DelimitedIdentifierSuffix);
          theT = theMap.destDT;
          break;
      }

      IQueryFilter theQF = new QueryFilterClass();
      switch (theT)
      {
        case esriFieldType.esriFieldTypeString:
          if (theV == "") { theQF.WhereClause = fieldName + " IS NOT NULL AND " + fieldName + " <> ''"; }
          else { theQF.WhereClause = fieldName + " = '" + theV + "'"; }
          break;
        case esriFieldType.esriFieldTypeInteger:
          if (theV == "") { theQF.WhereClause = fieldName + " IS NOT NULL AND " + fieldName + " <> 0"; }
          else { theQF.WhereClause = fieldName + " = " + theV; }
          break;
      }

      return theFC.FeatureCount(theQF);
    }

    internal static int GetAttributeCounts(IWorkspace theWS, String which, String theFCName, String fieldN, esriFieldType theT)
    {
      ISQLSyntax theS = (ISQLSyntax)theWS;
      IFeatureClass theFC = GetFeatureClass(theWS, theFCName);
      String fieldName = theS.GetSpecialCharacter(esriSQLSpecialCharacters.esriSQL_DelimitedIdentifierPrefix) + fieldN + theS.GetSpecialCharacter(esriSQLSpecialCharacters.esriSQL_DelimitedIdentifierSuffix);

      IQueryFilter theQF = new QueryFilterClass();
      switch (theT)
      {
        case esriFieldType.esriFieldTypeString:
          theQF.WhereClause = fieldName + " IS NOT NULL AND " + fieldName + " <> ''";
          break;
        case esriFieldType.esriFieldTypeInteger:
        case esriFieldType.esriFieldTypeDouble:
          theQF.WhereClause = fieldName + " IS NOT NULL AND " + fieldName + " <> 0";
          break;
      }

      return theFC.FeatureCount(theQF);
    }


    internal static ICodedValueDomain GetFieldEnumeration(IWorkspace theWS, String which, AttributeMappingData theAMap)
    {
      IDomain theD;
      ICodedValueDomain theCVD = null;

      IWorkspaceDomains theDWS = (IWorkspaceDomains)theWS;

      if (which == "Source")
      {
        theD = theDWS.get_DomainByName(theAMap.srcEnum);
      }
      else
      {
        theD = theDWS.get_DomainByName(theAMap.destEnum);
      }

      if (theD != null) { theCVD = (ICodedValueDomain)theD; }

      return theCVD;
    }

    internal static int getFieldPosition(IWorkspace theWS, String theFC, string theAtt)
    {
      int retValue = -1;

      IFeatureClass theClass = GetFeatureClass(theWS, theFC);
      retValue = theClass.Fields.FindField(theAtt);

      return retValue;
    }

    public static void DeleteFeature(IFeatureClass theFC, int OID)
    {
      try
      {
        IFeature theNF = theFC.GetFeature(OID);

        if (theNF != null) { theNF.Delete(); }
      }
      catch (Exception ex) { }
    }

    internal static string ConvertGeometryType(esriGeometryType theT)
    {
      if (theT == esriGeometryType.esriGeometryPoint) { return "Point"; }
      if (theT == esriGeometryType.esriGeometryPolygon) { return "Polygon"; }
      if (theT == esriGeometryType.esriGeometryPolyline) { return "Polyline"; }
      return "None";
    }

    internal static int seekGeometryTransform(esriGeometryType theS, esriGeometryType theD)
    {
      // acceptable transforms include
      // 1 Polygon to Point as Centroid
      // 2 Polygon to Polyline as Boundary
      // 3 Polyline to Point as MidPoint
      // 4 Point to Polygon as Buffer 1
      switch (theS)
      {
        case esriGeometryType.esriGeometryPoint:
          if (theD == esriGeometryType.esriGeometryPolygon) { return 4; } // Point to Polygon as Buffer
          break;
        case esriGeometryType.esriGeometryPolygon:
          if (theD == esriGeometryType.esriGeometryPoint) { return 1; } // Polygon to Point as Centroid
          if (theD == esriGeometryType.esriGeometryPolyline) { return 2; } // Polygon to Polyline as Boundary
          break;
        case esriGeometryType.esriGeometryPolyline:
          if (theD == esriGeometryType.esriGeometryPoint) { return 3; } // Polyline to Point as Midpoint
          break;
      }
      return -1;
    }

    #region metadata

    internal static string GetFeatureMetadata(IWorkspace theWS, String theFC)
    {
      String theFDef = "None Metadata Found";
      IPropertySet curPS;

      try
      {
        IFeatureClass theFClass = GetFeatureClass(theWS, theFC);
        IDataset theDS = (IDataset)theFClass;
        IFeatureClassName FCN = (IFeatureClassName)theDS.FullName;

        IMetadata MasMeta = (IMetadata)FCN;
        MasMeta.Synchronize(esriMetadataSyncAction.esriMSAAlways, 1);
        curPS = MasMeta.Metadata;

        object[] theO = (object[])curPS.GetProperty("eainfo/detailed/enttyp/enttypd");

        object theS = theO[0];
        return theS.ToString();
      }
      catch (Exception ex) { }

      return theFDef;
    }

    internal static string GetAttributeMetadata(IWorkspace theWS, String theFC, AttributeData theD)
    {
      String theFDef = "None Metadata Found";
      IPropertySet curPS;
      int Index = -1;

      try
      {
        IFeatureClass theFClass = GetFeatureClass(theWS, theFC);
        IDataset theDS = (IDataset)theFClass;
        IFeatureClassName FCN = (IFeatureClassName)theDS.FullName;

        Index = theFClass.Fields.FindField(theD.Name);

        IMetadata MasMeta = (IMetadata)FCN;
        MasMeta.Synchronize(esriMetadataSyncAction.esriMSAAlways, 1);
        curPS = MasMeta.Metadata;

        object[] theO = (object[])curPS.GetProperty("eainfo/detailed/attr[" + Index.ToString() + "]/attrdef");

        object theS = theO[0];
        return theS.ToString();
      }
      catch (Exception ex) { }

      return theFDef;
    }
  }
   
  #endregion

  #region ListBoxClasses
  public class FeatureData
  {
    internal String Name, OIDName, theAlias, ShapeName;
    internal int fRecords, isIdentical;
    internal esriGeometryType theGType;
    internal IFeatureDataset theDS;
    internal IFields theFlds;
    internal ISubtypes theST;

    internal FeatureData(String theName, IFeatureClass theFClass)
    {
      this.Name = theName;
      this.theGType = theFClass.ShapeType;
      this.theAlias = theFClass.AliasName;
      this.OIDName = theFClass.OIDFieldName;
      this.ShapeName = theFClass.ShapeFieldName;
      this.theDS = theFClass.FeatureDataset;
      this.theST = (ISubtypes)theFClass;
      this.theFlds = theFClass.Fields;
      this.isIdentical = 0;
    }

    public void SetIdentical(int id)
    {
      this.isIdentical = id;
    }

    public override string ToString()
    {
      return this.Name;
    }
  }

  public class AttributeData
  {
    internal String Name, ClassName, enumName;
    internal Boolean usesConstant;
    internal int recordCount, charLength, numMappings;
    internal esriFieldType dataType;

    internal AttributeData(String FCName, IField theField, int recordCount)
    {
      this.ClassName = FCName;
      this.usesConstant = false;
      this.numMappings = 0;
      if (theField != null)
      {
        this.Name = theField.Name;
        this.dataType = theField.Type;
        this.recordCount = recordCount;
        this.charLength = theField.Length;
        if (theField.Domain != null) { this.enumName = theField.Domain.Name; }
        else { this.enumName = ""; }
      }
      else
      {
        this.Name = "";
        this.enumName = "";
      }
    }

    public override string ToString()
    {
      return this.Name + " - " + MiscProcs.UnBuildDataType(this.dataType, this.charLength);
    }
  }

  public class ValueData
  {
    internal String consName, value, definition;
    internal int numMappings;

    internal ValueData(String cName, String theV, String theD)
    {
      this.value = theV;
      this.definition = theD;
      this.consName = cName;
      this.numMappings = 0;
    }

    public override string ToString()
    {
      return this.value;
    }
  }

  public class FeatureMappingData
  {
    internal String srcFC, destFC, srcGeoDb;
    internal esriGeometryType srcGeom, destGeom;
    internal int serialNumber, featCount, geomTransform, loadErrors, zmTransform;
    internal String srcPK, destPK;
    internal int status;
    internal LoadErrorData[] theErrors;
    internal AttributeMappingData[] theTransforms;

    internal FeatureMappingData(String srcDB, String source, String destination, esriGeometryType srcGT, esriGeometryType destGT, int serial)
    {
      this.serialNumber = serial;
      if (srcDB != "") { this.srcGeoDb = MiscProcs.fileNameONLY(srcDB); }
      else { this.srcGeoDb = ""; }
      this.srcFC = source;
      this.destFC = destination;
      this.srcGeom = srcGT;
      this.destGeom = destGT;
      if (this.srcGeom == this.destGeom) { this.geomTransform = 0; }
      else { this.geomTransform = GeoDbProcs.seekGeometryTransform(srcGT, destGT); }
      this.theTransforms = null;
      this.zmTransform = 0;
      this.featCount = 0;
      this.loadErrors = -1;
      this.srcPK = "";
      this.destPK = "";
      this.status = 0;
    }

    public void AddTransform(AttributeMappingData theAMap)
    {
      if (theTransforms == null)
      {
        System.Array.Resize(ref this.theTransforms, 1);
        this.theTransforms[0] = theAMap;
      }
      else
      {
        System.Array.Resize(ref this.theTransforms, this.theTransforms.Length + 1);
        this.theTransforms[this.theTransforms.Length - 1] = theAMap;
      }
    }

    public int CalcMZTransform(IWorkspace theInWS, IWorkspace theOutWS)
    {
      IFeatureClass theInFC = GeoDbProcs.GetFeatureClass(theInWS, this.srcFC);
      IFeatureClass theOutFC = GeoDbProcs.GetFeatureClass(theOutWS, this.destFC);

      IField theInF = theInFC.Fields.get_Field(theInFC.Fields.FindField(theInFC.ShapeFieldName));
      IField theOutF = theOutFC.Fields.get_Field(theOutFC.Fields.FindField(theOutFC.ShapeFieldName));

      IGeometryDef inDef = theInF.GeometryDef;
      IGeometryDef outDef = theOutF.GeometryDef;

      if (inDef.HasZ == outDef.HasZ)
      {
        if (inDef.HasM == outDef.HasM) { return 0; }
        if (inDef.HasM == false && outDef.HasM == true) { return 4; }  // Add M Values
        if (inDef.HasM == true && outDef.HasM == false) { return 3; }  // Remove M
      }

      if (inDef.HasM == outDef.HasM)
      {
        if (inDef.HasZ == outDef.HasZ) { return 0; }
        if (inDef.HasZ == false && outDef.HasZ == true) { return 2; }  // Add Z Values
        if (inDef.HasZ == true && outDef.HasZ == false) { return 1; }  // Remove Z
      }

      if (inDef.HasM == false && outDef.HasM == true && inDef.HasZ == false && outDef.HasZ == true) { return 6; }  // Add Both M and Z
      return 5; // Remove both M and Z
    }

    public void UpdateFeatureCount(int theC)
    {
      this.featCount = theC;
    }

    public void AddProblemLoad(LoadErrorData theErr)
    {
      if (this.theErrors == null)
      {
        System.Array.Resize(ref this.theErrors, 1);
        this.theErrors[0] = theErr;
        loadErrors = 1;
      }
      else
      {
        Boolean makeNew = true;
        for (int p = 0; p < this.theErrors.Length; p++)
        {
          if (this.theErrors[p].errMsg == theErr.errMsg.ToString()) { this.theErrors[p].Count += 1; makeNew = false;  }
        }
        if (makeNew)
        {
          System.Array.Resize(ref this.theErrors, this.theErrors.Length + 1);
          this.theErrors[this.theErrors.Length - 1] = theErr;
          loadErrors += 1;
        }
      }
    }

    public override string ToString()
    {
      return this.srcFC + " ---> " + this.destFC;
    }
  }

  public class LoadErrorData
  {
    internal Int32 Count;
    internal String errMsg, srcClass, destClass;
    internal int serialNumber;

    internal LoadErrorData(int Serial, String inFC, String outFC, Int32 Count, String theMsg)
    {
      this.serialNumber = Serial;
      this.Count = Count;
      this.errMsg = theMsg;
      this.srcClass = inFC;
      this.destClass = outFC;
    }

    public override string ToString()
    {
      return this.Count.ToString() + " failed load with " + this.errMsg;
    }
  }

  public class DataLossEntries
  {
    internal Int32 ObjectID;
    internal String errMsg, srcClass, destClass, srcAtt;
    internal int serialNumber;

    internal DataLossEntries(int Serial, String inFC, String outFC, String theAtt, Int32 theOID, String theMsg)
    {
      this.serialNumber = Serial;
      this.ObjectID = theOID;
      this.srcClass = inFC;
      this.destClass = outFC;
      this.srcAtt = theAtt;
      this.errMsg = theMsg;
    }
  }

  public class AttributeMappingData
  {
    internal String srcFC, destFC, srcAtt, destAtt, srcEnum, destEnum;
    internal esriFieldType srcDT, destDT;
    internal int serialNumber, srcLen, valueCount, destLen, dataLoss;
    internal string constant, results;
    internal int transform;
    internal ValueMappingData[] theConversions;
    internal DataLossEntries[] theLoss;
    internal FeatureMappingData Parent;

    internal AttributeMappingData(FeatureMappingData aMap, AttributeData sourceA, AttributeData destA)
    {
      this.serialNumber = aMap.serialNumber;
      this.srcFC = aMap.srcFC;
      this.destFC = aMap.destFC;
      if (sourceA.Name != "")
      {
        this.srcAtt = sourceA.Name;
        this.srcDT = sourceA.dataType;
        this.srcLen = sourceA.charLength;
        this.srcEnum = sourceA.enumName;
      }
      else
      {
        this.srcAtt = "Constant";
        this.srcDT = esriFieldType.esriFieldTypeBlob;
        this.srcLen = 0;
        this.srcEnum = "";
      }
      this.destAtt = destA.Name;
      this.destDT = destA.dataType;
      this.destLen = destA.charLength;
      this.destEnum = destA.enumName;
      this.transform = MiscProcs.getTransformNumber(this.srcDT, this.destDT);
      if (destA.dataType == esriFieldType.esriFieldTypeString && destA.charLength < this.srcLen) { this.transform = 4; }
      if (destA.enumName != "") { this.transform = 20; }
      this.valueCount = 0;
      this.dataLoss = 0;
      this.constant = "";
      this.results = "";
      this.theConversions = null;
      this.Parent = aMap;
    }

    public void AddDataLoss(DataLossEntries theE)
    {
      if (theLoss == null)
      {
        System.Array.Resize(ref this.theLoss, 1);
        this.theLoss[0] = theE;
      }
      else
      {
        System.Array.Resize(ref this.theLoss, this.theLoss.Length + 1);
        this.theLoss[this.theLoss.Length - 1] = theE;
      }
      this.dataLoss += 1;
    }

    public void AddResults(String theEntry)
    {
      this.results = theEntry;
    }

    public void AddValueConversion(ValueMappingData theVMap)
    {
      if (theConversions == null)
      {
        System.Array.Resize(ref this.theConversions, 1);
        this.theConversions[0] = theVMap;
      }
      else
      {
        System.Array.Resize(ref this.theConversions, this.theConversions.Length + 1);
        this.theConversions[this.theConversions.Length - 1] = theVMap;
      }
    }

    public override string ToString()
    {
      if (this.constant != "") { return this.srcAtt + " ---> " + this.destAtt + " using '" + this.constant + "'"; }
      else { return this.srcAtt + " ---> " + this.destAtt; }
    }
  }

  public class ValueMappingData
  {
    internal int serialNumber;
    internal String srcFC, destFC, srcAtt, destAtt, srcEnum, destEnum;
    internal String srcV, destV;

    internal ValueMappingData(AttributeMappingData aMap, ValueData sourceV, ValueData destV)
    {
      this.serialNumber = aMap.serialNumber;
      this.srcFC = aMap.srcFC;
      this.destFC = aMap.destFC;
      this.srcAtt = aMap.srcAtt;
      this.srcEnum = aMap.srcEnum;
      this.destEnum = aMap.destEnum;
      this.destAtt = aMap.destAtt;
      this.srcV = sourceV.value;
      this.destV = destV.value;
    }

    public override string ToString()
    {
      {
        return this.srcV + " ---> " + this.destV;
      }
    }
  }

  public class MigrationData
  {
    internal IWorkspace srcWS, destWS;
    internal Boolean splitNon, ptToPolygon, PolygonToPtI, PolygonToPtA, PolylineToPt, PolygonToLine;
    internal Boolean buildKeys;
    internal String preK;
    internal String preSplit;
    internal int DefZ;

    internal MigrationData(IWorkspace theS, IWorkspace theD, Boolean S, Boolean B, String K, String preS)
    {
      this.srcWS = theS;
      this.destWS = theD;
      this.splitNon = S;
      this.buildKeys = B;
      this.preK = K;
      this.preSplit = preS;
      this.DefZ = 1;
      this.ptToPolygon = false;
      this.PolygonToPtI = false;
      this.PolygonToPtA = false;
      this.PolygonToLine = false;
      this.PolylineToPt = false;
    }
  }
  #endregion

}
