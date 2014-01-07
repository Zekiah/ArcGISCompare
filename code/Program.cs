using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace ArcGISCompare
{
  static class Program
  {
    private static LicenseInitializer m_AOLicenseInitializer6 = new ArcGISCompare.LicenseInitializer();
    private static LicenseInitializer m_AOLicenseInitializer5 = new ArcGISCompare.LicenseInitializer();
    private static LicenseInitializer m_AOLicenseInitializer4 = new ArcGISCompare.LicenseInitializer();
    private static LicenseInitializer m_AOLicenseInitializer3 = new ArcGISCompare.LicenseInitializer();
    private static LicenseInitializer m_AOLicenseInitializer2 = new ArcGISCompare.LicenseInitializer();
    private static LicenseInitializer m_AOLicenseInitializer1 = new ArcGISCompare.LicenseInitializer();
    private static LicenseInitializer m_AOLicenseInitializer = new ArcGISCompare.LicenseInitializer();
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer6.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
      new esriLicenseExtensionCode[] { });
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer5.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
      new esriLicenseExtensionCode[] { });
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer4.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
      new esriLicenseExtensionCode[] { });
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer3.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
      new esriLicenseExtensionCode[] { });
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer2.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
      new esriLicenseExtensionCode[] { });
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer1.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
      new esriLicenseExtensionCode[] { });
      //ESRI License Initializer generated code.
      m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeBasic, esriLicenseProductCode.esriLicenseProductCodeStandard, esriLicenseProductCode.esriLicenseProductCodeAdvanced },
      new esriLicenseExtensionCode[] { });
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new frmMaster());
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer.ShutdownApplication();
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer1.ShutdownApplication();
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer2.ShutdownApplication();
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer3.ShutdownApplication();
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer4.ShutdownApplication();
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer5.ShutdownApplication();
      //ESRI License Initializer generated code.
      //Do not make any call to ArcObjects after ShutDownApplication()
      m_AOLicenseInitializer6.ShutdownApplication();
    }
  }
}