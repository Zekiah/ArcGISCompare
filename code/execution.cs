using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace ArcGISCompare
{
  internal static class execOptions
  {
    internal static int runTransform(MigrationData theMigr, FeatureMappingData theMap, ProgressBar thePB)
    {
      IFields outFields;
      IQueryFilter QF;
      IFeature outF;
      IRow outR = null;
      String FldName, PKPre;
      String defValue = "";
      String recPriKey = "";
      AttributeMappingData theAttMap;
      ITopologicalOperator theTopo;
      DataLossEntries theDL;
      IMAware MM;
      IZAware ZZ;
      IGeometry GG;
      Boolean hasConversion = false;
      IZ ZF;
      IPoint PP;
      IPolygon AA;
      IPolyline LL;
      IMCollection MColl;
      IMSegmentation2 MSeg;
      int inType, outType, L1, L2;
      Double inVal;
      Int32 M, T;
      Int32 PKNum;
      int inPos, sNum;
      ITable outTB = null;
      esriFieldType TIn;
      esriFieldType TOut;

      IFeatureClass srcFC = GeoDbProcs.GetFeatureClass(theMigr.srcWS, theMap.srcFC);
      IFeatureClass destFC = GeoDbProcs.GetFeatureClass(theMigr.destWS, theMap.destFC);

      int MoveQty = GeoDbProcs.GetFeatureCount(theMap.srcFC, theMigr.srcWS);
      int FeatCnt = GeoDbProcs.GetFeatureCount(theMap.destFC, theMigr.destWS);

      IEnumIndex theSPIndexes = destFC.Indexes.FindIndexesByFieldName(destFC.ShapeFieldName);
      theSPIndexes.Reset();

      IIndex theSP = theSPIndexes.Next();
      if (theSP != null) { destFC.DeleteIndex(theSP); }

      IWorkspaceEdit outWE = (IWorkspaceEdit)theMigr.destWS;

      outWE.StartEditing(false);
      outWE.StartEditOperation();

      thePB.Visible = true;
      thePB.Maximum = MoveQty;
      thePB.Value = 0;

      Boolean FirstRecord = true;
      IFeatureCursor allFeats = srcFC.Search(null, false);

      if (theMigr.splitNon) { outTB = GeoDbProcs.GetTable(theMigr.destWS, theMigr.preSplit + theMap.destFC); }
      IFeature srcF = allFeats.NextFeature();

      int PKCounter = FeatCnt + 1;

      while (srcF != null)
      {
        try
        {
          if (theMigr.splitNon) { outR = outTB.CreateRow(); }
          outF = destFC.CreateFeature();
          outFields = outF.Fields;

          // populate the indicated default values
          for (int m = 1; m < outFields.FieldCount; m++)
            if (outFields.get_Field(m).Type != esriFieldType.esriFieldTypeGeometry)
              if (outFields.get_Field(m).Editable)
                if (outFields.get_Field(m).DefaultValue != null) { outF.set_Value(m, outFields.get_Field(m).DefaultValue); }

          // translate and reproject the geometry
          for (int m = 1; m < outFields.FieldCount; m++)
          {
            if (outFields.get_Field(m).Type == esriFieldType.esriFieldTypeGeometry)
            {
# region geometryTransformation
              switch (theMap.zmTransform)
              {
                case 0:  // No M or Z Value Changes
                  switch (srcF.Shape.GeometryType)
                  {
                    case esriGeometryType.esriGeometryPoint:  // geometry is Point
                      GG = srcF.ShapeCopy;
                      PP = (IPoint)GG;
                      if (theMap.geomTransform == 4 && theMigr.ptToPolygon)
                      {
                        theTopo = (ITopologicalOperator)PP;
                        IPolygon newP = (IPolygon)theTopo.Buffer(1.0);
                        ZZ = (IZAware)newP;
                        if (ZZ.ZAware != true)
                        {
                          ZZ.ZAware = true;
                          IPolygon thePoly = (IPolygon)ZZ;
                          IZ thePZ = (IZ)thePoly;
                          thePZ.SetConstantZ(PP.Z);
                          newP = (IPolygon)thePZ;
                        }
                        outF.Shape = newP;
                      }
                      else if (theMap.geomTransform == 4) { GeoDbProcs.DeleteFeature(destFC, outF.OID); }
                      else { outF.Shape = PP; }
                      break;
                    case esriGeometryType.esriGeometryPolyline: // geometry is line
                      GG = srcF.Shape;
                      LL = (IPolyline)GG;
                      if (theMap.geomTransform == 3 && theMigr.PolylineToPt)
                      {
                      }
                      outF.Shape = LL;
                      break;
                    case esriGeometryType.esriGeometryPolygon:
                      GG = srcF.Shape;
                      AA = (IPolygon)GG;
                      if (theMap.geomTransform == 1)
                      {
                        IArea theA = (IArea)AA;
                        IPoint theP = null;
                        if (theMigr.PolygonToPtI) { theP = theA.LabelPoint; }
                        if (theMigr.PolygonToPtA) { theP = theA.Centroid; }
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 2)
                      {
                        theTopo = (ITopologicalOperator)AA;
                        IGeometry theL = theTopo.Boundary;
                        outF.Shape = theL;
                      }
                      else { outF.Shape = AA; }
                      break;
                    default:
                      outF.Shape = srcF.ShapeCopy;
                      break;
                  }
                  break;
                case 1:  // remove Z values on source geometries
                  switch (srcF.Shape.GeometryType)
                  {
                    case esriGeometryType.esriGeometryPoint:
                      GG = srcF.Shape;
                      PP = (IPoint)GG;
                      ZZ = (IZAware)PP;
                      if (ZZ.ZAware) 
                      {
                        ZZ.DropZs();
                        ZZ.ZAware = false;
                      }
                      PP = (IPoint)ZZ;
                      if (theMap.geomTransform == 4 && theMigr.ptToPolygon)
                      {
                        theTopo = (ITopologicalOperator)PP;
                        IGeometry theP = theTopo.Buffer(1.0);
                        outF.Shape = theP;
                      }
                      else { outF.Shape = PP; }
                      break;
                    case esriGeometryType.esriGeometryPolyline:
                      GG = srcF.Shape;
                      LL = (IPolyline)GG;
                      ZZ = (IZAware)LL;
                      if (ZZ.ZAware) 
                      {
                        ZZ.DropZs();
                        ZZ.ZAware = false;
                      }
                      LL = (IPolyline)ZZ;
                      outF.Shape = LL;
                      break;
                    case esriGeometryType.esriGeometryPolygon:
                      GG = srcF.Shape;
                      AA = (IPolygon)GG;
                      ZZ = (IZAware)AA;
                      if (ZZ.ZAware) 
                      {
                        ZZ.DropZs();
                        ZZ.ZAware = false;
                      }
                      AA = (IPolygon)ZZ;
                      if (theMap.geomTransform == 1)
                      {
                        IArea theA = (IArea)AA;
                        IPoint theP = null;
                        if (theMigr.PolygonToPtI) { theP = theA.LabelPoint; }
                        if (theMigr.PolygonToPtA) { theP = theA.Centroid; }
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 2)
                      {
                        theTopo = (ITopologicalOperator)AA;
                        IGeometry theL = theTopo.Boundary;
                        outF.Shape = theL;
                      }
                      else { outF.Shape = AA; }
                      break;
                  }
                  break;
                case 2:  // add default Z Values
                  switch (srcF.Shape.GeometryType)
                  {
                    case esriGeometryType.esriGeometryPoint:
                      GG = srcF.Shape;
                      PP = (IPoint)GG;
                      ZZ = (IZAware)PP;
                      if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                      PP = (IPoint)ZZ;
                      PP.Z = theMigr.DefZ;
                      if (theMap.geomTransform == 4 && theMigr.ptToPolygon)
                      {
                        theTopo = (ITopologicalOperator)GG;
                        IGeometry theP = theTopo.Buffer(1.0);
                        ZZ = (IZAware)theP;
                        if (ZZ.ZAware != true)
                        {
                          ZZ.ZAware = true;
                          IPolygon thePoly = (IPolygon)ZZ;
                          IZ thePZ = (IZ)thePoly;
                          thePZ.SetConstantZ(theMigr.DefZ);
                          theP = (IPolygon)thePZ;
                        }
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 4) { GeoDbProcs.DeleteFeature(destFC, outF.OID); }
                      else { outF.Shape = PP; }
                      break;
                    case esriGeometryType.esriGeometryPolyline:
                      GG = srcF.Shape;
                      LL = (IPolyline)GG;
                      ZZ = (IZAware)LL;
                      if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                      LL = (IPolyline)ZZ;
                      ZF = (IZ)LL;
                      ZF.SetConstantZ(theMigr.DefZ);
                      LL = (IPolyline)ZF;
                      outF.Shape = LL;
                      break;
                    case esriGeometryType.esriGeometryPolygon:
                      GG = srcF.Shape;
                      AA = (IPolygon)GG;
                      if (theMap.geomTransform == 1)
                      {
                        IArea theA = (IArea)AA;
                        IPoint theP = null;
                        if (theMigr.PolygonToPtI) { theP = theA.LabelPoint; }
                        if (theMigr.PolygonToPtA) { theP = theA.Centroid; }
                        ZZ = (IZAware)theP;
                        if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                        theP.Z = theMigr.DefZ;
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 2)
                      {
                        theTopo = (ITopologicalOperator)AA;
                        IGeometry theL = theTopo.Boundary;
                        ZZ = (IZAware)theL;
                        if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                        theL = (IGeometry)ZZ;
                        ZF = (IZ)theL;
                        ZF.SetConstantZ(theMigr.DefZ);
                        theL = (IGeometry)ZF;
                        outF.Shape = theL;
                      }
                      else 
                      {
                        ZZ = (IZAware)AA;
                        if (ZZ.ZAware != true)
                        {
                          ZZ.ZAware = true; }
                          AA = (IPolygon)ZZ;
                          ZF = (IZ)AA;
                          ZF.SetConstantZ(theMigr.DefZ);
                          AA = (IPolygon)ZF;
                        outF.Shape = AA;
                      }
                      break;
                  }
                  break;
                case 3:  // remove M values
                  switch (srcF.Shape.GeometryType)
                  {
                    case esriGeometryType.esriGeometryPoint:
                      GG = srcF.Shape;
                      PP = (IPoint)GG;
                      MM = (IMAware)PP;
                      if (MM.MAware == true) 
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      PP = (IPoint)MM;
                      if (theMap.geomTransform == 4 && theMigr.ptToPolygon)
                      {
                        theTopo = (ITopologicalOperator)PP;
                        IGeometry theP = theTopo.Buffer(1.0);
                        ZZ = (IZAware)theP;
                        if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                        theP = (IPolygon)ZZ;
                        ZF = (IZ)theP;
                        ZF.SetConstantZ(PP.Z);
                        theP = (IPolygon)ZF;
                        outF.Shape = theP;
                      }
                      else
                      {
                        outF.Shape = PP;
                      }
                      break;
                    case esriGeometryType.esriGeometryPolyline:
                      GG = srcF.Shape;
                      LL = (IPolyline)GG;
                      MM = (IMAware)LL;
                      if (MM.MAware == true) 
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      LL = (IPolyline)MM;
                      outF.Shape = LL;
                      break;
                    case esriGeometryType.esriGeometryPolygon:
                      GG = srcF.Shape;
                      AA = (IPolygon)GG;
                      MM = (IMAware)AA;
                      if (MM.MAware == true) 
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      AA = (IPolygon)MM;
                      if (theMap.geomTransform == 1)
                      {
                        IArea theA = (IArea)AA;
                        IPoint theP = null;
                        if (theMigr.PolygonToPtI) { theP = theA.LabelPoint; }
                        if (theMigr.PolygonToPtA) { theP = theA.Centroid; }
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 2)
                      {
                        theTopo = (ITopologicalOperator)AA;
                        IGeometry theL = theTopo.Boundary;
                        outF.Shape = theL;
                      }
                      else { outF.Shape = AA; }
                      break;
                  }
                  break;
                case 4:  // Add M's to geometry
                  switch (srcF.Shape.GeometryType)
                  {
                    case esriGeometryType.esriGeometryPoint:
                      GG = srcF.Shape;
                      PP = (IPoint)GG;
                      MM = (IMAware)PP;
                      if (MM.MAware != true) { MM.MAware = true; }
                      PP = (IPoint)MM;
                      if (theMap.geomTransform == 4 && theMigr.ptToPolygon)
                      {
                        theTopo = (ITopologicalOperator)PP;
                        IGeometry theP = theTopo.Buffer(1.0);
                        outF.Shape = theP;
                      }
                      else
                      {
                        outF.Shape = PP;
                      }
                      outF.Shape = PP;
                      break;
                    case esriGeometryType.esriGeometryPolyline:
                      GG = srcF.Shape;
                      LL = (IPolyline)GG;
                      MM = (IMAware)LL;
                      if (MM.MAware != true) { MM.MAware = true; }
                      LL = (IPolyline)MM;
                      MColl = (IMCollection)LL;
                      MSeg = (IMSegmentation2)MColl;
                      MSeg.SetMsAsDistance(true);
                      LL = (IPolyline)MSeg;
                      outF.Shape = LL;
                      break;
                    case esriGeometryType.esriGeometryPolygon:
                      GG = srcF.Shape;
                      AA = (IPolygon)GG;
                      MM = (IMAware)AA;
                      if (MM.MAware != true) { MM.MAware = true; }
                      AA = (IPolygon)MM;
                      MColl = (IMCollection)AA;
                      MSeg = (IMSegmentation2)MColl;
                      MSeg.SetMsAsDistance(true);
                      AA = (IPolygon)MSeg;
                      if (theMap.geomTransform == 1)
                      {
                        IArea theA = (IArea)AA;
                        IPoint theP = null;
                        if (theMigr.PolygonToPtI) { theP = theA.LabelPoint; }
                        if (theMigr.PolygonToPtA) { theP = theA.Centroid; }
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 2)
                      {
                        theTopo = (ITopologicalOperator)AA;
                        IGeometry theL = theTopo.Boundary;
                        outF.Shape = theL;
                      }
                      else { outF.Shape = AA; }
                      break;
                  }
                  break;
                case 5:  // remove both M and Z Values
                  switch (srcF.Shape.GeometryType)
                  {
                    case esriGeometryType.esriGeometryPoint:
                      GG = srcF.Shape;
                      PP = (IPoint)GG;
                      MM = (IMAware)PP;
                      if (MM.MAware == true) 
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      PP = (IPoint)MM;
                      ZZ = (IZAware)PP;
                      ZZ.DropZs();
                      ZZ.ZAware = false;
                      PP = (IPoint)ZZ;
                      if (theMap.geomTransform == 4 && theMigr.ptToPolygon)
                      {
                        theTopo = (ITopologicalOperator)PP;
                        IGeometry theP = theTopo.Buffer(1.0);
                        outF.Shape = theP;
                      }
                      else
                      {
                        outF.Shape = PP;
                      }
                      outF.Shape = PP;
                      break;
                    case esriGeometryType.esriGeometryPolyline:
                      GG = srcF.Shape;
                      LL = (IPolyline)GG;
                      MM = (IMAware)LL;
                      if (MM.MAware == true) 
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      LL = (IPolyline)MM;
                      ZZ = (IZAware)LL;
                      ZZ.DropZs();
                      ZZ.ZAware = false;
                      LL = (IPolyline)ZZ;
                      outF.Shape = LL;
                      break;
                    case esriGeometryType.esriGeometryPolygon:
                      GG = srcF.Shape;
                      AA = (IPolygon)GG;
                      MM = (IMAware)AA;
                      if (MM.MAware == true) 
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      AA = (IPolygon)MM;
                      ZZ = (IZAware)AA;
                      ZZ.DropZs();
                      ZZ.ZAware = false;
                      AA = (IPolygon)ZZ;
                      if (theMap.geomTransform == 1)
                      {
                        IArea theA = (IArea)AA;
                        IPoint theP = null;
                        if (theMigr.PolygonToPtI) { theP = theA.LabelPoint; }
                        if (theMigr.PolygonToPtA) { theP = theA.Centroid; }
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 2)
                      {
                        theTopo = (ITopologicalOperator)AA;
                        IGeometry theL = theTopo.Boundary;
                        outF.Shape = theL;
                      }
                      else { outF.Shape = AA; }
                      break;
                  }
                  break;
                case 6:  // Add M and Z Values
                  switch (srcF.Shape.GeometryType)
                  {
                    case esriGeometryType.esriGeometryPoint:
                      GG = srcF.Shape;
                      PP = (IPoint)GG;
                      ZZ = (IZAware)PP;
                      if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                      PP = (IPoint)ZZ;
                      PP.Z = theMigr.DefZ;
                      MM = (IMAware)PP;
                      if (MM.MAware != true) { MM.MAware = true; }
                      PP = (IPoint)MM;
                      if (theMap.geomTransform == 4 && theMigr.ptToPolygon)
                      {
                        theTopo = (ITopologicalOperator)PP;
                        IGeometry theP = theTopo.Buffer(1.0);
                        outF.Shape = theP;
                      }
                      else
                      {
                        outF.Shape = PP;
                      }
                      outF.Shape = PP;
                      break;
                    case esriGeometryType.esriGeometryPolyline:
                      GG = srcF.Shape;
                      LL = (IPolyline)GG;
                      ZZ = (IZAware)LL;
                      if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                      LL = (IPolyline)ZZ;
                      ZF = (IZ)LL;
                      ZF.SetConstantZ(theMigr.DefZ);
                      LL = (IPolyline)ZF;
                      MM = (IMAware)LL;
                      if (MM.MAware == true)
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      LL = (IPolyline)MM;
                      outF.Shape = LL;
                      break;
                    case esriGeometryType.esriGeometryPolygon:
                      GG = srcF.Shape;
                      AA = (IPolygon)GG;
                      if (theMap.geomTransform == 1)
                      {
                        IArea theA = (IArea)AA;
                        IPoint theP = null;
                        if (theMigr.PolygonToPtI) { theP = theA.LabelPoint; }
                        if (theMigr.PolygonToPtA) { theP = theA.Centroid; }
                        outF.Shape = theP;
                      }
                      else if (theMap.geomTransform == 2)
                      {
                        theTopo = (ITopologicalOperator)AA;
                        IGeometry theL = theTopo.Boundary;
                        outF.Shape = theL;
                      }
                      else { outF.Shape = AA; }
                      ZZ = (IZAware)AA;
                      if (ZZ.ZAware != true) { ZZ.ZAware = true; }
                      AA = (IPolygon)ZZ;
                      ZF = (IZ)AA;
                      ZF.SetConstantZ(theMigr.DefZ);
                      AA = (IPolygon)ZF;
                      MM = (IMAware)AA;
                      if (MM.MAware == true)
                      {
                        MM.DropMs();
                        MM.MAware = false;
                      }
                      AA = (IPolygon)MM;
                      break;
                  }
                  break;
              }
#endregion
            }
            else
            {
# region fieldTransformation
              FldName = outFields.get_Field(m).Name;
              theAttMap = getAttributeMapping(theMap, FldName);
              if (theAttMap != null)
              {
                switch (theAttMap.transform)
                {
                  case -1:  //the field is not mapped
                    break;
                  case 0:  // the field is translatable As Is
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null) 
                      if (srcF.get_Value(inPos).ToString() != "") { outF.set_Value(m, srcF.get_Value(inPos)); }
                    if (FirstRecord) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " As Is"); }
                    break;
                  case 18: // insertion of constant values for nulls
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null) 
                      if (srcF.get_Value(inPos).ToString() != "") { outF.set_Value(m, srcF.get_Value(inPos)); }
                    if (FirstRecord) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " inserting " + defValue + " for NULLS"); }
                    break;
                  case 23:  // overwrite with a constant or place a constant in all records
                    switch (outF.Fields.get_Field(m).Type)
                    {
                      case esriFieldType.esriFieldTypeString:
                        outF.set_Value(m, theAttMap.constant.ToString());
                        break;
                      case esriFieldType.esriFieldTypeDouble:
                        outF.set_Value(m, Convert.ToDouble(theAttMap.constant));
                        break;
                      case esriFieldType.esriFieldTypeSingle:
                        outF.set_Value(m, Convert.ToSingle(theAttMap.constant));
                        break;
                      case esriFieldType.esriFieldTypeInteger:
                        outF.set_Value(m, Convert.ToInt32(theAttMap.constant));
                        break;
                      case esriFieldType.esriFieldTypeSmallInteger:
                        outF.set_Value(m, Convert.ToInt16(theAttMap.constant));
                        break;
                      case esriFieldType.esriFieldTypeDate:
                        outF.set_Value(m, Convert.ToDateTime(theAttMap.constant));
                        break;
                    }
                    if (FirstRecord) { theAttMap.AddResults("Placed Constant " + theAttMap.constant.ToString() + " into " + FldName + " Overwriting Existing Data"); }
                    break;
                  case 4:  // shorten a character/string field
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null) 
                      if (srcF.get_Value(inPos).ToString() != "")
                      {
                        if (srcF.get_Value(inPos).ToString().Length <= outF.Fields.get_Field(m).Length)
                        {
                          outF.set_Value(m, srcF.get_Value(inPos));
                        }
                        else 
                        {
                          theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "Data Loss in shortening a String Field"));
                          outF.set_Value(m, srcF.get_Value(inPos).ToString().Substring(0, outF.Fields.get_Field(m).Length));
                        }
                      }
                    if (theAttMap.dataLoss == 1) { theAttMap.AddResults("Data Loss in Moving " + srcF.get_Value(inPos) + " to " + FldName); }
                    break;
                  case 10:  // converting numeric to string
                  case 11:
                  case 12:
                  case 13:
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null) 
                      if (srcF.get_Value(inPos).ToString().Length <= outF.Fields.get_Field(m).Length) { outF.set_Value(m, srcF.get_Value(inPos)); }
                      else
                      { 
                        theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "Data Loss in converting Numeric Data Type to String"));
                        outF.set_Value(m, srcF.get_Value(inPos).ToString().Substring(0, outF.Fields.get_Field(m).Length));
                      }
                      if (theAttMap.dataLoss == 1) { theAttMap.AddResults("Data Loss in Moving " + srcF.get_Value(inPos) + " to " + FldName); }
                      if (theAttMap.dataLoss == 0) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " converting numeric Data Type"); }
                    break;
                  case 5: // converting string to integer
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null) 
                    {
                      Int32 i = 0;
                      Boolean result = Int32.TryParse(srcF.get_Value(inPos).ToString(), out i);
                      if (result != true) 
                      {
                        theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "Data Loss in converting String to Long Integer"));
                      }
                      if (theAttMap.dataLoss == 1) { theAttMap.AddResults("Data Loss in Moving " + srcF.get_Value(inPos) + " to " + FldName); }
                      if (theAttMap.dataLoss == 0) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " converting Data Type"); }
                    }
                    break;
                 case 6:
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null) 
                    {
                      Double i = 0;
                      Boolean result = Double.TryParse(srcF.get_Value(inPos).ToString(), out i);
                      if (result != true) { theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "Data Loss in converting String to Double")); }
                      if (theAttMap.dataLoss == 1) { theAttMap.AddResults("Data Loss in Moving " + srcF.get_Value(inPos) + " to " + FldName); }
                      if (theAttMap.dataLoss == 0) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " converting Data Type"); }
                    }
                    break;
                  case 14:
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null) 
                    {
                      int i = 0;
                      Boolean result = int.TryParse(srcF.get_Value(inPos).ToString(), out i);
                      if (result != true) { theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "Data Loss in converting String to Short Integer")); }
                    if (theAttMap.dataLoss == 1) { theAttMap.AddResults("Data Loss in Moving " + srcF.get_Value(inPos) + " to " + FldName); }
                    if (theAttMap.dataLoss == 0) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " converting Data Type"); }
                    }
                    break;
                  case 3:   // double/single to int/shortInt
                  case 16:
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null)
                    {
                      inVal = (Double)srcF.get_Value(inPos);
                      if (inVal == Convert.ToInt32(inVal)) { outF.set_Value(m, Convert.ToInt32(inVal)); }
                      else
                      {
                        outF.set_Value(m, Convert.ToInt32(inVal));
                        theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "Data Loss in converting Decimal to Integer"));
                      }
                      if (theAttMap.dataLoss == 1) { theAttMap.AddResults("Data Loss in Moving " + srcF.get_Value(inPos) + " to " + FldName); }
                      if (theAttMap.dataLoss == 0) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " converting Data Type"); }
                    }
                    break;
                  case 8:  // long integer to datetime
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null)
                    {
                      String yearEquiv = srcF.get_Value(inPos).ToString();
                      if (yearEquiv.Length != 8) { theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "Data Loss in converting Integer to DateTime")); }
                      else
                      {
                        DateTime theD = new DateTime(Convert.ToInt16(yearEquiv.Substring(0, 4)), Convert.ToInt16(yearEquiv.Substring(5, 2)), Convert.ToInt16(yearEquiv.Substring(7, 2)));
                        outF.set_Value(m, theD);
                      }
                      if (theAttMap.dataLoss == 1) { theAttMap.AddResults("Data Loss in Moving " + srcF.get_Value(inPos) + " to " + FldName); }
                      if (theAttMap.dataLoss == 0) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " converting Data Type"); }
                    }
                    break;
                  case 17:  // populating the Primary Keys
                    PKPre = theMigr.preK;
                    recPriKey = PKPre + PKCounter.ToString("0000000#");
                    outF.set_Value(m, recPriKey);
                    if (FirstRecord) { theAttMap.AddResults("Primary Key Values Auto-populated beginning with " + PKPre + PKCounter.ToString("0000000#")); }
                    PKCounter += 1;
                    break;
                  case 21:  // Autopopulate the SubTypeID
                    Int32 theST = Convert.ToInt32(theAttMap.constant);
                    outF.set_Value(m, theST);
                    if (FirstRecord) { theAttMap.AddResults("SubtypeID populated with " + theAttMap.constant.ToString()); }
                    break;
                  case 20:  // domain Value Conversion (reserved for future use
                    inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theAttMap.srcFC, theAttMap.srcAtt);
                    if (srcF.get_Value(inPos) != null)
                      if (srcF.get_Value(inPos).ToString() != "")
                      {
                        hasConversion = false;
                        if (theAttMap.theConversions != null)
                        {
                          foreach (ValueMappingData theConversion in theAttMap.theConversions)
                            if (theConversion.srcV.ToString().ToLower() == srcF.get_Value(inPos).ToString().ToLower())
                            {
                              hasConversion = true;
                              outF.set_Value(m, theConversion.destV.ToString());
                            }
                          if (hasConversion == false)
                          {
                            theAttMap.AddDataLoss(new DataLossEntries(theAttMap.serialNumber, theAttMap.srcFC, theAttMap.destFC, theAttMap.srcAtt, srcF.OID, "No Conversion Specified for " + srcF.get_Value(inPos)));
                          }
                        }
                      }
                      if (FirstRecord) { theAttMap.AddResults(srcF.Fields.get_Field(inPos).Name + " into " + FldName + " As Is"); }
                    break;
                }
              }
#endregion
            }
          }
          if (theMigr.splitNon)
          {
            for (int n = 0; n < outTB.Fields.FieldCount; n++)
            {
              if (outTB.Fields.get_Field(n).Editable == true)
              {
                FldName = outTB.Fields.get_Field(n).Name;
                int b = srcFC.Fields.FindField(FldName);
                TIn = srcFC.Fields.get_Field(b).Type;
                TOut = outTB.Fields.get_Field(n).Type;
                L1 = srcFC.Fields.get_Field(b).Length;
                L2 = outTB.Fields.get_Field(n).Length;
                String thePK = theMap.destPK.ToString().ToUpper();
                if (FldName.ToUpper() == theMap.destPK.ToString().ToUpper()) { outR.set_Value(n, recPriKey); }
                else
                {
                  inPos = GeoDbProcs.getFieldPosition(theMigr.srcWS, theMap.srcFC, FldName);
                  if (inPos >= 0) {  outR.set_Value(n, srcF.get_Value(inPos)); }
                }
              }
            }
            outR.Store();
          }
          outF.Store();
          thePB.Increment(1);
          if (MoveQty >= 10)
          {
            int Moved = Convert.ToInt32(MoveQty / 10);
            if (thePB.Value % Moved == 0)
            {
              outWE.StopEditOperation();
              outWE.StopEditing(true);
              outWE.StartEditing(false);
              outWE.StartEditOperation();
            }
          }
          Application.DoEvents();
        }
        catch (Exception ex)
        {
          if (srcF != null) {  theMap.AddProblemLoad(new LoadErrorData(theMap.serialNumber, theMap.srcFC, theMap.destFC, 1, ex.Message)); }
        }
        srcF = allFeats.NextFeature();
      }

      outWE.StopEditOperation();
      outWE.StopEditing(true);

      thePB.Value = 0;
      thePB.Visible = false;

      Application.DoEvents();
      return 0;
    }

    private static AttributeMappingData getAttributeMapping(FeatureMappingData theMap, String theAttName)
    {

      try
      {
        if (theMap.theTransforms.Length == 0) { return null; }
        else
        {
          foreach (AttributeMappingData theA in theMap.theTransforms)
          {
            if (theA.destAtt == theAttName) { return theA; }
          }
        }
      }
      catch (Exception ex) { }

      return null;
    }
  }
}
