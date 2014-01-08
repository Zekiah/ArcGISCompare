using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;

namespace ArcGISCompare
{
    public partial class frmMaster : Form
    {
        zHelpClass theHelper;
        IWorkspace srcWrkSpc;
        Boolean hasHelps = false;
        IWorkspace destWrkSpc;
        Boolean hasChanged = false;
        Boolean hasAttChanged = false;
        FeatureMappingData theMap;
        AttributeMappingData theAMap;
        String saveFileName = "";
        msAccProcs theInst;
        int serial = 1;

        String[] transformType = new String[27];

        public frmMaster()
        {
            InitializeComponent();

            this.lbSource.DrawMode = DrawMode.OwnerDrawFixed;
            this.lbSource.DrawItem += new DrawItemEventHandler(lbSource_DrawItem);

            this.lbSrcAttributes.DrawMode = DrawMode.OwnerDrawFixed;
            this.lbSrcAttributes.DrawItem += new DrawItemEventHandler(lbSrcAttributes_DrawItem);

            this.lbDestination.DrawMode = DrawMode.OwnerDrawFixed;
            this.lbDestination.DrawItem += new DrawItemEventHandler(lbDestination_DrawItem);

            this.lbDestAttributes.DrawMode = DrawMode.OwnerDrawFixed;
            this.lbDestAttributes.DrawItem += new DrawItemEventHandler(lbDestAttributes_DrawItem);

            this.lbSourceValues.DrawMode = DrawMode.OwnerDrawFixed;
            this.lbSourceValues.DrawItem += new DrawItemEventHandler(lbSourceValues_DrawItem);

            this.lbDestinationValues.DrawMode = DrawMode.OwnerDrawFixed;
            this.lbDestinationValues.DrawItem += new DrawItemEventHandler(lbDestinationValues_DrawItem);

            this.lstAttMappings.DrawMode = DrawMode.OwnerDrawVariable;
            this.lstAttMappings.DrawItem += new DrawItemEventHandler(lstAttMappings_DrawItem);

            this.lstValueMappings.DrawMode = DrawMode.OwnerDrawFixed;
            this.lstValueMappings.DrawItem += new DrawItemEventHandler(lstValueMappings_DrawItem);

            tcTop.TabPages.Remove(tabPage3);
            tcTop.TabPages.Remove(tabPage2);
            tcTop.Selecting += new TabControlCancelEventHandler(tcTop_Selecting);

            this.transformType[0] = "As Is";
            transformType[1] = "ShortInt to Double";
            transformType[2] = "Character to Double";
            transformType[3] = "Integer to Double";
            transformType[4] = "Single to Integer";
            transformType[5] = "Double to Integer";
            transformType[6] = "Character to Integer";
            transformType[7] = "Date to Integer";
            transformType[8] = "Integer to Date";
            transformType[9] = "Character to Date";
            transformType[10] = "Double to Character";
            transformType[11] = "ShortInt to Character";
            transformType[12] = "Single to Character";
            transformType[13] = "LongInt to Character";
            transformType[14] = "Date to Character";
            transformType[15] = "Character to Character";
            transformType[16] = "String to GUID";
            transformType[17] = "Generate Primary Key";
            transformType[18] = "Overwrite All";
            transformType[19] = "Insert Metadata";
            transformType[20] = "Value Translation";
            transformType[21] = "Insert SubType";
            transformType[25] = "Not Supported";
        }

        #region formEvents

        private void frmMaster_ResizeEnd(object sender, EventArgs e)
        {
            srcWorkspaceLabel.Width = this.Width / 3 - 10;
            if (srcWrkSpc != null) { srcWorkspaceLabel.Text = MiscProcs.TrimText(srcWrkSpc.PathName, srcWorkspaceLabel.Font, srcWorkspaceLabel.Size); }
            destWorkspaceLabel.Width = srcWorkspaceLabel.Width;
            //instWorkspaceLabel.Width = srcWorkspaceLabel.Width;
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            frmMaster_ResizeEnd(sender, e);
            FileInfo theAppFile = new FileInfo(Application.ExecutablePath);

            FileInfo theHelpFile = new FileInfo(theAppFile.Directory + "\\ArcGISCompare.xml");

            if (theHelpFile.Exists)
            {
                XmlDocument theStarter = new XmlDocument();
                theStarter.Load(theHelpFile.FullName);

                theHelper = new zHelpClass(theStarter);

                hasHelps = theHelper.isValidHelp;
            }
            else { theHelpFile = null; }
        }

        private void frmMaster_Unload(object sender, EventArgs e)
        {
            System.Windows.Forms.Form frmSave = new dlgSave();
            if (hasChanged)
            {
                DialogResult theAns = frmSave.ShowDialog(this);

                if (theAns == DialogResult.No) { return; }
                if (theAns == DialogResult.Yes) { mnuSaveMappings_Click(sender, e); }
            }
            this.Close();
        }

        private void lstAttMappings_SelectedIndexChanged(object sender, EventArgs e)
        {
            tcTop.TabPages.Remove(tabPage3);
            if (lstAttMappings.SelectedItems.Count == 1 && tcTop.TabPages.Count == 2)
            {
                theAMap = (AttributeMappingData)lstAttMappings.SelectedItem;
                lstAttMappings.ContextMenuStrip = conMenu;
            }
            lstAttMappings.ContextMenuStrip = null;
            lbSrcAttributes.SelectedIndex = getAttributeOrder(theAMap.srcAtt, lbSrcAttributes);
            lbDestAttributes.SelectedIndex = getAttributeOrder(theAMap.destAtt, lbDestAttributes);
        }

        void tcTop_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int ACount = 0;
            ICodedValueDomain theEnum;
            String theVal = "";

            if (tcTop.SelectedTab == tabPage2) // selected page is Attribute Mapping Page
            {
            }
            else if (tcTop.SelectedTab == tabPage3)  // Selected Page is Value Mapping Page
            {
                if (lstAttMappings.SelectedIndex >= 0)
                {
                    lbSourceValues.Items.Clear();
                    lbDestinationValues.Items.Clear();
                    Boolean hasValue = false;

                    AttributeMappingData theItem = (AttributeMappingData)lstAttMappings.SelectedItem;
                    if (theItem.destEnum != null)
                    {
                        IFeatureClass theFC = GeoDbProcs.GetFeatureClass(srcWrkSpc, theItem.srcFC);

                        IQueryFilter theQF = new QueryFilterClass();
                        theQF.WhereClause = theItem.srcAtt + " IS NOT NULL";

                        lbSourceValues.Items.Clear();
                        IFeatureCursor theCur = theFC.Search(theQF, false);
                        IFeature theF = theCur.NextFeature();

                        int theIndex = theFC.Fields.FindField(theItem.srcAtt);

                        while (theF != null)
                        {
                            switch (theItem.srcDT)
                            {
                                case esriFieldType.esriFieldTypeString:
                                    theVal = theF.get_Value(theIndex).ToString();

                                    if (lbSourceValues.Items.Count > 0)
                                    {
                                        for (int v = 0; v < lbSourceValues.Items.Count; v++)
                                            if (lbSourceValues.Items[v].ToString() == theVal) { hasValue = true; }

                                        if (hasValue != true) { lbSourceValues.Items.Add(new ValueData(theVal, theVal, "None")); }
                                    }
                                    else { lbSourceValues.Items.Add(new ValueData(theVal, theVal, "None")); }
                                    break;
                                default:
                                    break;
                            }
                            theF = theCur.NextFeature();
                        }
                    }
                    theEnum = GeoDbProcs.GetFieldEnumeration(destWrkSpc, "Implementation", theItem);

                    if (theEnum != null)
                    {
                        for (int i = 0; i < theEnum.CodeCount; i++)
                        { lbDestinationValues.Items.Add(new ValueData(theEnum.get_Name(i).ToString(), theEnum.get_Value(i).ToString(), theItem.destEnum)); }
                    }

                    lstValueMappings.Items.Clear();
                    if (theItem.theConversions != null)
                    {
                        foreach (ValueMappingData theDMap in theItem.theConversions)
                        {
                            lstValueMappings.Items.Add(theDMap);
                            ValueData theA = getValueListBoxItem(lbSourceValues, theDMap.srcV);
                            if (theA != null) { theA.numMappings += 1; }

                            theA = getValueListBoxItem(lbDestinationValues, theDMap.destV);
                            if (theA != null) { theA.numMappings += 1; }

                            this.Refresh();
                            Application.DoEvents();
                        }
                    }
                }
            }
            else  // Selected Page is Feature Mapping Page
            {
                tcTop.TabPages.Remove(tabPage3);
                tcTop.TabPages.Remove(tabPage2);
                theMap = null;
            }
        }

        private void initProgressBar(int theCount)
        {
            // initialize the Progress Bar
            GPB.Maximum = theCount;
            GPB.Visible = true;
            GPB.Value = 0;
        }

        private void clearProgressBar()
        {
            GPB.Value = 0;
            GPB.Visible = false;
        }

        #endregion


        void SetRunVisibility(ListBox theList)
        {
            mnuRun.Visible = false;
            if (hasChanged)
            {
                mnuSaveMappings.Visible = true;
                mnuRun.Visible = true;
            }
        }

        #region specialFunctions

        private FeatureData getListBoxItem(ListBox theList, String FCName)
        {
            if (theList.Items.Count > 0)
            {
                for (int i = 0; i < theList.Items.Count; i++)
                    if (theList.Items[i].ToString().ToLower() == FCName.ToLower())
                        return (FeatureData)theList.Items[i];
            }
            return null;
        }

        private AttributeData getAttListBoxItem(ListBox theList, String AttName, esriFieldType theT, int theL)
        {
            if (theList.Items.Count > 0)
            {
                for (int i = 0; i < theList.Items.Count; i++)
                    if (theList.Items[i].ToString().ToLower() == AttName.ToLower() + " - " + MiscProcs.UnBuildDataType(theT, theL).ToLower())
                        return (AttributeData)theList.Items[i];
            }
            return null;
        }

        private ValueData getValueListBoxItem(ListBox theList, String enumValue)
        {
            if (theList.Items.Count > 0)
            {
                for (int i = 0; i < theList.Items.Count; i++)
                    if (theList.Items[i].ToString().ToLower() == enumValue.ToLower())
                        return (ValueData)theList.Items[i];
            }
            return null;
        }

        public int getFeatureOrder(String theName, ListBox lstBox)
        {
            if (lstBox.Items.Count > 0)
                for (int i = 0; i < lstBox.Items.Count; i++)
                {
                    if (lstBox.Items[i].ToString() == theName) { return i; }
                }
            return -1;
        }

        public int getAttributeOrder(String theName, ListBox lstBox)
        {
            if (lstBox.Items.Count > 0)
                for (int i = 0; i < lstBox.Items.Count; i++)
                {
                    if (lstBox.Items[i].ToString().StartsWith(theName + " - ")) { return i; }
                }
            return -1;
        }

        public void CheckforGeoSpatialData(String strPath, String geoType, ListBox lstBox)
        {
            IWorkspaceFactory owrkspaceFact = null;
            IFeatureWorkspace theFWS;
            IFeatureClass theFClass;
            int FCount;

            try
            {
                switch (geoType)
                {
                    case "ShapeFile":
                        owrkspaceFact = new ShapefileWorkspaceFactory();
                        break;
                    default:
                        owrkspaceFact = new AccessWorkspaceFactoryClass();
                        break;
                }

                PropertySet pPropSet = new PropertySetClass();
                pPropSet.SetProperty("DATABASE", strPath);

                IWorkspace theWS = owrkspaceFact.Open(pPropSet, 0);
                theFWS = (IFeatureWorkspace)theWS;

                if (theWS != null)
                {
                    String[] theSrcNames = GeoDbProcs.GetFeatureClassNames(theWS);

                    foreach (String theSrcName in theSrcNames)
                    {
                        theFClass = theFWS.OpenFeatureClass(theSrcName);
                        lbSource.Items.Add(new FeatureData(theSrcName, theFClass));
                        Application.DoEvents();
                    }
                }
            }
            catch { }
        }

        private void FillSubDirectory(DirectoryInfo di)
        {
            IWorkspaceFactory theWSFact;
            IWorkspace theWS;
            IFeatureWorkspace theFWS;
            IFeatureClass theFClass;
            int FCount;

            try
            {
                foreach (DirectoryInfo iDir in di.GetDirectories())
                {
                    FillSubDirectory(iDir);
                }

                foreach (FileInfo theFile in di.GetFiles("*.mdb"))
                {
                    theWSFact = new AccessWorkspaceFactoryClass();

                    theWS = theWSFact.OpenFromFile(theFile.FullName, 0);
                    theFWS = (IFeatureWorkspace)theWS;

                    String[] theSrcNames = GeoDbProcs.GetFeatureClassNames(srcWrkSpc);

                    foreach (String theSrcName in theSrcNames)
                    {
                        theFClass = theFWS.OpenFeatureClass(theSrcName);
                        lbSource.Items.Add(new FeatureData(theSrcName, theFClass));
                        Application.DoEvents();
                    }
                }
            }
            catch { }
        }
        #endregion

        #region menuOperations

        private void mnuSourceG_Click(object sender, EventArgs e)
        {
            int FCount;
            IFeatureWorkspace theFWS;
            IFeatureClass theFClass;

            // get the workspace containing the source data
            srcWrkSpc = GeoDbProcs.GetWorkspace("Select the Template Geodatabase");
            if (srcWrkSpc != null)
            {
                srcWorkspaceLabel.Text = MiscProcs.TrimText(srcWrkSpc.PathName, srcWorkspaceLabel.Font, srcWorkspaceLabel.Size);

                theFWS = (IFeatureWorkspace)srcWrkSpc;

                String[] theSrcNames = GeoDbProcs.GetFeatureClassNames(srcWrkSpc);
                lbSource.Items.Clear();

                foreach (String theSrcName in theSrcNames)
                {
                    theFClass = theFWS.OpenFeatureClass(theSrcName);
                    lbSource.Items.Add(new FeatureData(theSrcName, theFClass));
                    Application.DoEvents();
                }

                // make the Analyze option Visible
                //mnuAnalyze.Visible = true;
                //mnuAnalyzeSource.Visible = true;

                theHelper.DisplayPage(2, helpBox);
            }
            else { MessageBox.Show("No Template Geodatabase Selected"); }
        }

        private void mnuSourceD_Click(object sender, EventArgs e)
        {
            int FCount;
            DirectoryInfo[] instDirs;
            IWorkspaceFactory theWSFact;
            IWorkspace theWS;
            IFeatureWorkspace theFWS;
            IFeatureClass theFClass;
            // get the directory that is to be scanned for Shapefiles or Geodatabases

            DialogResult theAns;
            FolderBrowserDialog DirBrowserDlg = new FolderBrowserDialog();
            DirBrowserDlg.RootFolder = Environment.SpecialFolder.MyComputer;

            DirBrowserDlg.Description = "Select the Parent Folder containing Geospatial Items.";

            theAns = DirBrowserDlg.ShowDialog();

            if (theAns == DialogResult.OK)
            {
                DirectoryInfo iDir = new DirectoryInfo(DirBrowserDlg.SelectedPath);

                try
                {
                    instDirs = iDir.GetDirectories();

                    foreach (DirectoryInfo theDir in instDirs)
                    {
                    }

                    foreach (FileInfo theFile in iDir.GetFiles("*.mdb"))
                    {
                        theWSFact = new AccessWorkspaceFactoryClass();

                        theWS = theWSFact.OpenFromFile(theFile.FullName, 0);

                        String[] theSrcNames = GeoDbProcs.GetFeatureClassNames(srcWrkSpc);

                        foreach (String theSrcName in theSrcNames)
                        {
                            theFWS = (IFeatureWorkspace)theWS;
                            theFClass = theFWS.OpenFeatureClass(theSrcName);
                            lbSource.Items.Add(new FeatureData(theSrcName, theFClass));
                            Application.DoEvents();
                        }
                        CheckforGeoSpatialData(iDir.FullName, "ShapeFile", lbSource);
                    }
                }
                catch (Exception ex) { }
            }
        }

        private void mnuDestination_Click(object sender, EventArgs e)
        {
            IFeatureWorkspace theFWS;
            IFeatureClass theFClass;
            // get the workspace containing the source data
            destWrkSpc = GeoDbProcs.GetWorkspace("Select the Implementation Geodatabase");
            if (destWrkSpc != null)
            {
                destWorkspaceLabel.Text = MiscProcs.TrimText(destWrkSpc.PathName, destWorkspaceLabel.Font, destWorkspaceLabel.Size);

                String[] theDestNames = GeoDbProcs.GetFeatureClassNames(destWrkSpc);
                lbDestination.Items.Clear();
                theFWS = (IFeatureWorkspace)destWrkSpc;

                foreach (String theDestName in theDestNames)
                {
                    theFClass = theFWS.OpenFeatureClass(theDestName);
                    lbDestination.Items.Add(new FeatureData(theDestName, theFClass));
                    Application.DoEvents();
                }

            }
            // make the Analyze option Visible
            //mnuAnalyze.Visible = true;
            //mnuAnalyzeDestination.Visible = true;
            mnuRun.Visible = true;
        }

        private void mnuAnalyzeSource_Click(object sender, EventArgs e)
        {
            // Set the Variable for storing the item
            FeatureData theItem;

            initProgressBar(lbSource.Items.Count);

            // check the contents
            for (int i = 0; i < lbSource.Items.Count; i++)
            {
                theItem = (FeatureData)lbSource.Items[i];
                theItem.fRecords = GeoDbProcs.GetFeatureCount(theItem.Name, srcWrkSpc);
                GPB.Increment(1);
                Application.DoEvents();
            }
            clearProgressBar();
        }

        private void mnuAnalyzeDestination_Click(object sender, EventArgs e)
        {
            // Set the Variable for storing the item
            FeatureData theItem;

            initProgressBar(lbDestination.Items.Count);

            // check the contents
            for (int i = 0; i < lbDestination.Items.Count; i++)
            {
                theItem = (FeatureData)lbDestination.Items[i];
                theItem.fRecords = GeoDbProcs.GetFeatureCount(theItem.Name, destWrkSpc);
                GPB.Increment(1);
                Application.DoEvents();
            }
            clearProgressBar();
        }

        private void mnuInstructions_Click(object sender, EventArgs e)
        {
            FileInfo fInfo;
            DialogResult resDlg;

            try
            {
                instDLG.Title = "Select the Mappings Database";
                instDLG.Filter = "Mappings Files (*.mdb)|*.mdb";
                resDlg = instDLG.ShowDialog();

                if (resDlg == DialogResult.OK)
                {
                    fInfo = new FileInfo(instDLG.FileName);
                    theInst = new msAccProcs(fInfo.FullName);

                    //instWorkspaceLabel.Text = MiscProcs.TrimText(fInfo.FullName, instWorkspaceLabel.Font, instWorkspaceLabel.Size);

                }
            }
            catch (Exception ex) { }

            GC.Collect();
        }

        private void mnuAnalyzeInstructions_Click(object sender, EventArgs e)
        {
            if (theInst != null && theInst.isConnected)
            {
                if (theInst.isMapped)
                {
                }
                else
                {
                    theInst.BuildMappingTables();

                    DialogResult theAns = MessageBox.Show(this, "No Mapping Exists..  Attempt Archive Load?", "Empty Instructions Database", MessageBoxButtons.OK);

                    if (theAns == DialogResult.OK)
                    {
                    }
                }
            }
        }

        private void mnuSaveMappings_Click(object sender, EventArgs e)
        {
            if (saveFileName == "")
            {
                saveDLG.Title = "Select the Location and File for Mappings";
                DialogResult theAns = saveDLG.ShowDialog();

                if (theAns == DialogResult.OK)
                {
                    FileInfo theFile = new FileInfo(saveDLG.FileName);

                    try
                    {
                        if (theFile.Exists) { theFile.Delete(); }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("The File cannot be deleted... it is in use!", "Error Deleting File", MessageBoxButtons.OK);
                        return;
                    }
                    // Create the Access Database
                    if (MiscProcs.CreateAccessDatabase(saveDLG.FileName))
                    {
                        // create the class object for mappings manipulation
                        theInst = new msAccProcs(saveDLG.FileName);

                        if (theInst.isConnected)
                        {
                            // store the file name for future use
                            saveFileName = saveDLG.FileName;

                        }
                    }
                }
            }
        }

        private void mnuRun_Click(object sender, EventArgs e)
        {
            int nodeCount = 1;

            mnuRun.Enabled = false;
            Application.DoEvents();

            if (lbSource.Items.Count > 0)
            {
                TreeNode G = new TreeNode("Geodatabases");
                G.Nodes.Add("Template is " + srcWrkSpc.PathName);
                G.Nodes.Add("Implementation is " + destWrkSpc.PathName);
                TV.Nodes.Add(G);
                foreach (FeatureData theSD in lbSource.Items)
                {
                    foreach (FeatureData theDD in lbDestination.Items)
                    {
                        if (theSD.Name == theDD.Name)
                        {
                            theSD.SetIdentical(1);
                            theDD.SetIdentical(1);
                            TreeNode theN = new TreeNode(theSD.Name);
                            theN.ImageIndex = 0;
                            CheckFeatureProperties(ref theN, theSD, theDD);
                            CheckFieldProperties(ref theN, theSD, theDD);
                            CheckSubTypeProperties(ref theN, theSD, theDD);
                            CheckDefaultValues(ref theN, theSD, theDD);
                            CheckDomainsAndValues(ref theN, theSD, theDD);
                            TV.Nodes.Add(theN);
                            Application.DoEvents();
                        }
                    }
                }
                lbSource.SelectedIndex = 1;
                lbSource.SelectedIndex = -1;

                lbDestination.SelectedIndex = 1;
                lbDestination.SelectedIndex = -1;

                mnuMappings.Visible = true;
                mnuShowMappings.Visible = true;
            }
        }

        private void mnuInsertConstant_Click(object sender, EventArgs e)
        {
            dlgConstant theDlg = new dlgConstant();

            AttributeData theItem = (AttributeData)lbDestAttributes.SelectedItem;
            theDlg.lblInsertHere.Text = theMap.destFC + " --- " + theItem.Name;
            theDlg.lblDataType.Text = MiscProcs.UnBuildDataType(theItem.dataType, theItem.charLength);
            theDlg.lblLength.Text = theItem.charLength.ToString();
            theDlg.forNulls.Text = "F";

            if (theItem.enumName != "")
            {
                theDlg.lblEnum.Text = theItem.enumName;
            }

            if (theItem.usesConstant)
            {
            }

            DialogResult theRes = theDlg.ShowDialog(this);

            if (theRes == DialogResult.OK)
            {
                AttributeData EmptyAttribute = new AttributeData(theMap.srcFC, null, 0);

                theAMap = new AttributeMappingData(theMap, EmptyAttribute, theItem);
                theAMap.constant = theDlg.txtConst.Text;

                lstAttMappings.Items.Add(theAMap);
                theAMap.transform = 20;
                theMap.AddTransform(theAMap);

                theItem.numMappings += 1;
                theItem.usesConstant = true;
            }
        }

        private void mnuMapConstant_Click(object sender, EventArgs e)
        {
            dlgConstant theDlg = new dlgConstant();

            AttributeMappingData theItem = (AttributeMappingData)lstAttMappings.SelectedItem;
            theDlg.lblInsertHere.Text = theItem.destFC + " --- " + theItem.destAtt;
            theDlg.lblDataType.Text = MiscProcs.UnBuildDataType(theItem.destDT, theItem.destLen);
            theDlg.lblLength.Text = theItem.destLen.ToString();
            theDlg.forNulls.Text = "T";

            if (theItem.destEnum != "")
            {
                theDlg.lblEnum.Text = theItem.destEnum;
            }

            if (theItem.constant != "")
            {
            }

            DialogResult theRes = theDlg.ShowDialog(this);

            if (theRes == DialogResult.OK)
            {
                theItem.constant = theDlg.txtConst.Text;
            }
        }

        private void mnuInitializeMap_Click(object sender, EventArgs e)
        {
            tcTop.SelectedTab = tabPage1;
            Application.DoEvents();
        }

        private void mnuShowMappings_Click(object sender, EventArgs e)
        {
            if (TV.Nodes.Count > 0)
            {
                frmMappings theResults = new frmMappings();

                theResults.refillMappings(TV);

                DialogResult theEnd = theResults.ShowDialog();
            }
        }

        private void mnuUnMapped_Click(object sender, EventArgs e)
        {
            if (lbSource.Items.Count > 0)
            {
                frmMappings notMapped = new frmMappings();

                DialogResult theEnd = notMapped.ShowDialog(this);
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form frmSave = new dlgSave();
            if (hasChanged)
            {
                DialogResult theAns = frmSave.ShowDialog(this);

                if (theAns == DialogResult.No) { return; }
                if (theAns == DialogResult.Yes) { mnuSaveMappings_Click(sender, e); }
            }
            this.Close();
        }
        #endregion

        public void CheckFeatureProperties(ref TreeNode N, FeatureData theS, FeatureData theD)
        {
            if (theS.theAlias != theD.theAlias)
            {
                N.ImageIndex = 1; N.SelectedImageIndex = 1;
                TreeNode Alias = new TreeNode("Mismatched Alias") { ImageIndex = 1, SelectedImageIndex = 1 };
                Alias.Nodes.Add("Template is " + theS.theAlias);
                Alias.Nodes.Add("Implementation is " + theD.theAlias);
                N.Nodes.Add(Alias);
            }

            if (theS.OIDName.ToUpper() != theD.OIDName.ToUpper())
            {
                N.ImageIndex = 1; N.SelectedImageIndex = 1;
                TreeNode OID = new TreeNode("Mismatched OID Field Name") { ImageIndex = 1, SelectedImageIndex = 1 };
                OID.Nodes.Add("Template is " + theS.OIDName.ToUpper());
                OID.Nodes.Add("Implementation is " + theD.OIDName.ToUpper());
                N.Nodes.Add(OID);
            }

            if (theS.theGType != theD.theGType)
            {
                N.ImageIndex = 1; N.SelectedImageIndex = 1;
                TreeNode OID = new TreeNode("Mismatched OID Field Name") { ImageIndex = 1, SelectedImageIndex = 1 };
                OID.Nodes.Add("Template is " + theS.theGType.ToString());
                OID.Nodes.Add("Implementation is " + theD.theGType.ToString());
                N.Nodes.Add(OID);
            }

            if (theS.ShapeName != theD.ShapeName) { N.ImageIndex = 1; N.SelectedImageIndex = 1; N.Nodes.Add("Mismatched Shape Field Name"); }

            //if (theS.theDS.Name != theD.theDS.Name) { N.Nodes.Add("Mismatched Feature Dataset Name"); }
        }

        public void CheckDefaultValues(ref TreeNode N, FeatureData theS, FeatureData theD)
        {
            ISubtypes theSS;
            ISubtypes theDS;
            Boolean firstTime = false;
            IEnumSubtype theE;
            IEnumSubtype theF;
            int theV, theQ;
            string strV, strQ;
            Boolean isFound;
            TreeNode FDV = null;
            string theSDVal, theDDVal, theVText;

            theSS = theS.theST;
            theDS = theD.theST;

            if (theSS.HasSubtype == theDS.HasSubtype && theSS.HasSubtype)
            {
                TreeNode S = new TreeNode("Default Values");
                theE = theSS.Subtypes;
                theVText = theE.Next(out theV);
                while (theV != 0)
                {
                    IFields theFlds = theS.theFlds;
                    IFields destFlds = theD.theFlds;
                    firstTime = true;

                    for (int C = 0; C < theFlds.FieldCount; C++)
                    {
                        if (theFlds.get_Field(C).Editable)
                        {
                            theDDVal = "";
                            theSDVal = theSS.get_DefaultValue(theV, theFlds.get_Field(C).Name).ToString();
                            if (destFlds.FindField(theFlds.get_Field(C).Name) >= 0)
                            {
                                theDDVal = theDS.get_DefaultValue(theV, theFlds.get_Field(C).Name).ToString();
                            }
                            if (theSDVal != theDDVal)
                            {
                                if (firstTime)
                                {
                                    FDV = new TreeNode("For Feature Type - " + theVText) { ImageIndex = 1, SelectedImageIndex = 1 };
                                }
                                TreeNode DV = new TreeNode("Default Value " + theSDVal + " for Field " + theFlds.get_Field(C).Name + " unmatched") { ImageIndex = 1, SelectedImageIndex = 1 };
                                DV.Nodes.Add(new Guid().ToString(), "Template is " + theSDVal, 1, 1);
                                DV.Nodes.Add(new Guid().ToString(), "Implementation is " + theDDVal, 1, 1);
                                FDV.Nodes.Add(DV);
                                if (firstTime)
                                {
                                    S.Nodes.Add(FDV);
                                    firstTime = false;
                                }
                            }
                        }
                    }
                    firstTime = true;
                    theVText = theE.Next(out theV);
                }
                if (S.Nodes.Count == 0)
                {
                    S.Nodes.Add("All Default Values Agree Template and Implementation");
                }
                else
                {
                    N.ImageIndex = 1;
                    N.SelectedImageIndex = 1;
                    S.ImageIndex = 1;
                    S.SelectedImageIndex = 1;
                }
                N.Nodes.Add(S);
            }
        }

        public void CheckDomainsAndValues(ref TreeNode N, FeatureData theS, FeatureData theD)
        {
            ISubtypes theSS;
            ISubtypes theDS;
            IEnumSubtype theE;
            int theV, theQ;
            string strV, strQ;
            TreeNode FDV = null;
            Boolean firstTime = false;
            Boolean isFound;
            IDomain theSDVal, theDDVal;
            string theVText;

            theSS = theS.theST;
            theDS = theD.theST;

            if (theSS.HasSubtype == theDS.HasSubtype && theSS.HasSubtype)
            {
                TreeNode S = new TreeNode("Constraints");
                theE = theSS.Subtypes;
                theVText = theE.Next(out theV);
                while (theV != 0)
                {
                    firstTime = true;
                    IFields theFlds = theS.theFlds;
                    IFields destFlds = theD.theFlds;

                    for (int C = 0; C < theFlds.FieldCount; C++)
                    {
                        if (theFlds.get_Field(C).Editable)
                        {
                            theDDVal = null;
                            theSDVal = theSS.get_Domain(theV, theFlds.get_Field(C).Name);
                            if (destFlds.FindField(theFlds.get_Field(C).Name) >= 0)
                            {
                                theDDVal = theDS.get_Domain(theV, theFlds.get_Field(C).Name);
                            }
                            if (theSDVal != null && theDDVal != null)
                            {
                                if (theSDVal.Name != theDDVal.Name)
                                {
                                    if (firstTime)
                                    {
                                        FDV = new TreeNode("For Feature Type - " + theVText);
                                    }
                                    TreeNode DV = new TreeNode("Domain " + theSDVal.Name + " for Field " + theFlds.get_Field(C).Name + " unmatched"){ ImageIndex = 1, SelectedImageIndex = 1};
                                    DV.Nodes.Add(new Guid().ToString(), "Template is " + theSDVal.Name, 1, 1);
                                    DV.Nodes.Add(new Guid().ToString(), "Implementation is " + theDDVal.Name, 1, 1);
                                    TreeNode V = new TreeNode("Domain Values Comparison"){ImageIndex = 1, SelectedImageIndex = 1};
                                    DomainValueComparison(ref V, theSDVal, theDDVal);
                                    FDV.ImageIndex = 1;
                                    FDV.SelectedImageIndex = 1;
                                    //V.ImageIndex = 1;
                                    FDV.Nodes.Add(DV);
                                    DV.Nodes.Add(V);
                                    if (firstTime)
                                    {
                                        S.Nodes.Add(FDV);
                                        firstTime = false;
                                    }
                                }
                                else
                                {
                                    TreeNode DV = new TreeNode("Domain " + theSDVal.Name + " for Field " + theFlds.get_Field(C).Name + " is Valid");
                                    TreeNode V = new TreeNode("Domain Values Comparison");
                                    //DomainValueComparison(ref V, theSDVal, theDDVal);
                                    ////FDV.Nodes.Add(DV);
                                    //DV.Nodes.Add(V);
                                    //S.Nodes.Add(DV);
                                    if (DomainValueComparison(ref V, theSDVal, theDDVal))
                                    {
                                        DV.Text = "Domain " + theSDVal.Name + " for Field " + theFlds.get_Field(C).Name + " is not Valid";
                                        DV.ImageIndex = 1;
                                        DV.SelectedImageIndex = 1;
                                        V.ImageIndex = 1;
                                        V.SelectedImageIndex = 1;
                                        DV.Nodes.Add(V);
                                        S.Nodes.Add(DV);
                                    }
                                }
                            }
                            else if (theSDVal == null && theDDVal == null) { }
                            else if (theSDVal == null)
                            {
                                TreeNode DV = new TreeNode("Domain " + theSDVal + " for Field " + theFlds.get_Field(C).Name + " Missing in Implementation"){ImageIndex = 1, SelectedImageIndex = 1};
                                S.Nodes.Add(DV);
                            }
                            else
                            {
                                TreeNode DV = new TreeNode("Domain " + theDDVal + " for Field " + theFlds.get_Field(C).Name + " Missing in Template"){ ImageIndex = 1, SelectedImageIndex = 1};
                                S.Nodes.Add(DV);
                            }
                        }
                    }
                    theVText = theE.Next(out theV);
                }
                if (S.Nodes.Count == 0)
                {
                    S.Nodes.Add("All Domains and Values Agree between Template and Implementation");
                }
                else
                {
                    N.ImageIndex = 1;
                    N.SelectedImageIndex = 1;
                    S.ImageIndex = 1;
                    S.SelectedImageIndex = 1;
                }
                N.Nodes.Add(S);
            }
        }

        public bool DomainValueComparison(ref TreeNode N, IDomain theS, IDomain theD)
        {
            ICodedValueDomain theSS;
            ICodedValueDomain theDS;
            int i, j;
            string sValue, sName, dName;
            bool anyBad = false;
            Boolean hasMatch = false;

            if (theS.Type == theD.Type && theS.Type == esriDomainType.esriDTCodedValue)
            {
                theSS = (ICodedValueDomain)theS;
                theDS = (ICodedValueDomain)theD;

                for (i = 0; i < theSS.CodeCount; i++)
                {
                    sValue = theSS.get_Value(i).ToString();
                    hasMatch = false;

                    for (j = 0; j < theDS.CodeCount; j++)
                    {
                        if (sValue == theDS.get_Value(j).ToString())
                        {
                            hasMatch = true;
                            sName = theSS.get_Name(i).ToString();
                            dName = theDS.get_Name(j).ToString();

                            if (sName != dName)
                            {
                                TreeNode defNode = new TreeNode("Matched Value " + sValue + " has mismatched definition") { ImageIndex = 1, SelectedImageIndex = 1 };
                                defNode.Nodes.Add(new Guid().ToString(),"Template Value Definition is " + sName, 1, 1);
                                defNode.Nodes.Add(new Guid().ToString(), "Implementation Value Definition is " + dName, 1, 1);
                                N.Nodes.Add(defNode);
                            }
                        }
                    }
                    if (hasMatch != true)
                    {
                        N.Nodes.Add(new Guid().ToString(), "Template Value " + sValue + " is missing in Implementation", 1, 1);
                        anyBad = true;
                    }
                }
                for (i = 0; i < theDS.CodeCount; i++)
                {
                    sValue = theDS.get_Value(i).ToString();
                    hasMatch = false;

                    for (j = 0; j < theSS.CodeCount; j++)
                    {
                        if (sValue == theSS.get_Value(j).ToString()) { hasMatch = true; }
                    }
                    if (hasMatch != true)
                    {
                        N.Nodes.Add(new Guid().ToString(), "Implementation Value " + sValue + " is missing in Template", 1, 1);
                        anyBad = true;
                    }
                }
            }
            return anyBad;
        }

        public void CheckSubTypeProperties(ref TreeNode N, FeatureData theS, FeatureData theD)
        {
            ISubtypes theSS;
            ISubtypes theDS;
            IEnumSubtype theE;
            IEnumSubtype theF;
            int theV, theQ;
            string strV, strQ;
            Boolean isFound;
            string theVText, theDText;

            theSS = theS.theST;
            theDS = theD.theST;

            if (theSS.HasSubtype != theDS.HasSubtype)
            {
                if (theSS.HasSubtype) { N.Nodes.Add(new Guid().ToString(), "Template Has Subtypes and Implementation does NOT", 1, 1); }
                else { N.Nodes.Add(new Guid().ToString(), "Implementation Has Subtypes and Template does NOT", 1, 1); }
            }
            else if (theSS.HasSubtype)
            {
                TreeNode S = new TreeNode("SubTypes");
                theE = theSS.Subtypes;
                theF = theDS.Subtypes;
                theVText = theE.Next(out theV);
                while (theV != 0)
                {
                    strV = theV.ToString() + " - " + theVText;
                    theF.Reset();
                    strQ = "";
                    isFound = false;
                    theDText = theF.Next(out theQ);
                    while (theQ != 0)
                    {
                        strQ = theQ.ToString() + " - " + theDText;
                        if (strQ == strV) { isFound = true; }
                        theDText = theF.Next(out theQ);
                    }
                    if (!isFound) { S.Nodes.Add(new Guid().ToString(), "SubType " + strQ + " missing from Implementation", 1, 1); }
                    theVText = theE.Next(out theV);
                }
                theF.Reset();
                theVText = theF.Next(out theV);
                while (theV != 0)
                {
                    strV = theV.ToString() + " - " + theVText;
                    theE.Reset();
                    strQ = "";
                    isFound = false;
                    theDText = theE.Next(out theQ);
                    while (theQ != 0)
                    {
                        strQ = theQ.ToString() + " - " + theDText;
                        if (strQ == strV) { isFound = true; }
                        theDText = theE.Next(out theQ);
                    }
                    if (!isFound) { S.Nodes.Add(new Guid().ToString(), "SubType " + strQ + " missing from Template", 1, 1); }
                    theVText = theE.Next(out theV);
                }
                if (S.Nodes.Count == 0)
                {
                    S.Nodes.Add("All SubTypes Agree Between Template and Implementation");
                }
                else
                {
                    N.ImageIndex = 1;
                    N.SelectedImageIndex = 1;
                    S.ImageIndex = 1;
                    S.SelectedImageIndex = 1;
                }
                N.Nodes.Add(S);
            }
        }

        public void CheckFieldProperties(ref TreeNode N, FeatureData theS, FeatureData theD)
        {
            IFields theSF;
            IFields theDF;
            string theFName;

            theSF = theS.theFlds;
            theDF = theD.theFlds;

            if (theSF.FieldCount != theDF.FieldCount)
            {
                N.Nodes.Add(new Guid().ToString(), "Field Counts Mismatched between Template and Implementation", 1, 1);
            }
            else
            {
                TreeNode F = new TreeNode("Attributes");
                for (int C = 0; C < theSF.FieldCount; C++)
                {
                    theFName = theSF.get_Field(C).Name;
                    if (theDF.FindField(theFName) < 0) { F.Nodes.Add(new Guid().ToString(), "Field " + theFName + " missing from Implementation", 1, 1); }
                    else
                    {
                        if (theSF.get_Field(C).Type != theDF.get_Field(theDF.FindField(theFName)).Type) { F.Nodes.Add(new Guid().ToString(), "Field " + theFName + " mismatched Data Type", 1, 1); }
                        if (theSF.get_Field(C).Type == esriFieldType.esriFieldTypeString)
                        {
                            if (theSF.get_Field(C).Length != theDF.get_Field(theDF.FindField(theFName)).Length)
                            {
                                TreeNode OID = new TreeNode("Field " + theFName + " mismatched Character Length") { ImageIndex = 1, SelectedImageIndex = 1 };
                                //TODO: fix detection of implementation field length
                                //OID.Nodes.Add(new Guid().ToString(), "Template is " + theSF.get_Field(C).Length.ToString(), 1);
                                //OID.Nodes.Add(new Guid().ToString(), "Implementation is " + theDF.get_Field(C).Length.ToString(), 1);
                                F.Nodes.Add(OID);
                            }
                        }
                        if (theSF.get_Field(C).AliasName != theDF.get_Field(theDF.FindField(theFName)).AliasName)
                        {
                            TreeNode OID = new TreeNode("Field " + theFName + " mismatched Alias Name") { ImageIndex = 1, SelectedImageIndex = 1 };
                            //OID.Nodes.Add(new Guid().ToString(), "Template is " + theSF.get_Field(C).AliasName, 1);
                            //OID.Nodes.Add(new Guid().ToString(), "Implementation is " + theDF.get_Field(theDF.FindField(theFName)).AliasName, 1);
                            F.Nodes.Add(OID);
                        }
                    }
                }
                for (int C = 0; C < theDF.FieldCount; C++)
                {
                    theFName = theDF.get_Field(C).Name;
                    if (theSF.FindField(theFName) < 0) { F.Nodes.Add(new Guid().ToString(), "Field " + theFName + " missing from Template", 1, 1); }
                    else
                    {
                        if (theDF.get_Field(C).Type != theSF.get_Field(theSF.FindField(theFName)).Type) { F.Nodes.Add(new Guid().ToString(), "Field " + theFName + " mismatched Data Type", 1, 1); }
                        if (theDF.get_Field(C).AliasName != theDF.get_Field(theDF.FindField(theFName)).AliasName)
                        {
                            TreeNode OID = new TreeNode("Field " + theFName + " mismatched Alias Name") { ImageIndex = 1, SelectedImageIndex = 1 };
                           // OID.ImageIndex = 1;
                            OID.Nodes.Add(new Guid().ToString(), "Template is " + theSF.get_Field(C).AliasName, 1, 1);
                            OID.Nodes.Add(new Guid().ToString(), "Implementation is " + theDF.get_Field(theDF.FindField(theFName)).AliasName, 1, 1);
                            //OID.Nodes[0].ImageIndex = 1;
                            //OID.Nodes[1].ImageIndex = 1;
                            F.Nodes.Add(OID);
                        }
                    }
                }

                if (F.Nodes.Count == 0)
                {
                    F.Nodes.Add("All Fields Agree");
                }
                else
                {
                    N.ImageIndex = 1;
                    F.ImageIndex = 1;
                }

                N.Nodes.Add(F);
            }
        }

        #region LoadSaveMappings
        public void SaveAllMappings(ListBox lstMap)
        {
            if (theInst.isMapped)
            {
                DialogResult theAns = MessageBox.Show(this, "Overwrite Existing Mappings?", "Delete Existing Mappings", MessageBoxButtons.YesNoCancel);

                if (theAns == DialogResult.Cancel) { return; }
                if (theAns == DialogResult.No) { return; }
                if (theAns == DialogResult.Yes)
                {
                    if (theInst.DeleteAllMappings())
                    {
                        for (int i = 0; i < lstMap.Items.Count; i++)
                        {
                            FeatureMappingData theItem = (FeatureMappingData)lstMap.Items[i];
                            theInst.WriteFeatureMapping(theItem);

                            if (theItem.theErrors != null)
                                foreach (LoadErrorData theE in theItem.theErrors)
                                    theInst.WriteLoadErrors(theE);
                            if (theItem.theTransforms != null)
                                foreach (AttributeMappingData theAMap in theItem.theTransforms)
                                {
                                    theInst.WriteAttributeMapping(theAMap);

                                    if (theAMap.theLoss != null)
                                        foreach (DataLossEntries theE in theAMap.theLoss)
                                            theInst.WriteDataLoss(theE);
                                    if (theAMap.theConversions != null)
                                        foreach (ValueMappingData theV in theAMap.theConversions)
                                            theInst.WriteValueMapping(theItem.serialNumber, theV);
                                }
                        }
                    }
                    hasChanged = false;
                }
            }
            else
            {
                theInst.BuildMappingTables();

                for (int i = 0; i < lstMap.Items.Count; i++)
                {
                    FeatureMappingData theItem = (FeatureMappingData)lstMap.Items[i];
                    theInst.WriteFeatureMapping(theItem);

                    if (theItem.theErrors != null)
                        foreach (LoadErrorData theE in theItem.theErrors)
                            theInst.WriteLoadErrors(theE);
                    if (theItem.theTransforms != null)
                        foreach (AttributeMappingData theAMap in theItem.theTransforms)
                        {
                            theInst.WriteAttributeMapping(theAMap);

                            if (theAMap.theLoss != null)
                                foreach (DataLossEntries theE in theAMap.theLoss)
                                    theInst.WriteDataLoss(theE);
                            if (theAMap.theConversions != null)
                                foreach (ValueMappingData theV in theAMap.theConversions)
                                    theInst.WriteValueMapping(theItem.serialNumber, theV);
                        }
                }
                hasChanged = false;
            }
        }

        private String ConvGeom(esriGeometryType theG)
        {
            switch (theG)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    return "Point";
                    break;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    return "Polyline";
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    return "Polygon";
                    break;
                default:
                    return "Polygon";
                    break;
            }
        }
        #endregion

        #region CustomDrawProcedures

        private void lbSource_DrawItem(object Sender, DrawItemEventArgs e)
        {
            Color CurrentListColor = Color.Brown;

            if (e.Index >= 0)
            {
                FeatureData ItemToAdd = (FeatureData)lbSource.Items[e.Index];
                if (ItemToAdd.isIdentical == 1) { CurrentListColor = Color.Black; }
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lbSource.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }

        private void lbDestination_DrawItem(object Sender, DrawItemEventArgs e)
        {
            Color CurrentListColor = Color.Brown;
            if (e.Index >= 0)
            {
                FeatureData ItemToAdd = (FeatureData)lbDestination.Items[e.Index];
                if (ItemToAdd.isIdentical == 1) { CurrentListColor = Color.Black; }
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lbDestination.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }

        void lbSrcAttributes_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                AttributeData ItemToAdd = (AttributeData)lbSrcAttributes.Items[e.Index];
                Color CurrentListColor = (ItemToAdd.recordCount > 0) ? Color.Black : Color.Red;
                CurrentListColor = (ItemToAdd.numMappings > 0) ? Color.Blue : Color.Black;
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lbSrcAttributes.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }

        void lbDestAttributes_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color CurrentListColor = Color.Black;

            if (e.Index >= 0)
            {
                AttributeData ItemToAdd = (AttributeData)lbDestAttributes.Items[e.Index];
                if (ItemToAdd.numMappings > 0) { CurrentListColor = Color.Blue; }
                else if (ItemToAdd.usesConstant) { CurrentListColor = Color.Green; }
                else if (ItemToAdd.recordCount > 0) { CurrentListColor = Color.Red; }
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lbDestAttributes.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }

        void lstAttMappings_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                AttributeMappingData ItemToAdd = (AttributeMappingData)lstAttMappings.Items[e.Index];
                Color CurrentListColor = Color.Black;
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lstAttMappings.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }

        void lbSourceValues_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ValueData ItemToAdd = (ValueData)lbSourceValues.Items[e.Index];
                Color CurrentListColor = (ItemToAdd.numMappings > 0) ? Color.Blue : Color.Black;
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lbSourceValues.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }

        void lbDestinationValues_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ValueData ItemToAdd = (ValueData)lbDestinationValues.Items[e.Index];
                Color CurrentListColor = (ItemToAdd.numMappings > 0) ? Color.Blue : Color.Black;
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lbDestinationValues.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }

        void lstValueMappings_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ValueMappingData ItemToAdd = (ValueMappingData)lstValueMappings.Items[e.Index];
                Color CurrentListColor = Color.Black;
                e.DrawBackground();

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    Graphics g = e.Graphics;
                    g.FillRectangle(new SolidBrush(Color.Silver), e.Bounds);
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(((String)lstValueMappings.Items[e.Index].ToString()), this.Font, new SolidBrush(CurrentListColor), e.Bounds);
            }
        }


        #endregion


    }
}