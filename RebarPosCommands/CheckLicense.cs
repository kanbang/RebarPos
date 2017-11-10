using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace RebarPosCommands
{
    public class CheckLicense
    {
        public static string DeveloperSymbol { get { return "OZOZ"; } }
        public static string LicensedAppName { get { return "XCOM Bundle"; } }
        public static string LicenseRegistryKey
        {
            get
            {
                return "SOFTWARE\\" + DeveloperSymbol + "\\" + LicensedAppName;
            }
        }

        private static DateTime LastLicenseCheck = DateTime.MinValue;
        private static TimeSpan LicenseCheckInterval = TimeSpan.FromHours(1);

        public static bool Check()
        {
            if (DateTime.Now - LastLicenseCheck < LicenseCheckInterval) return true;

            LicenseCheck.License license = LicenseCheck.License.FromRegistry(LicenseRegistryKey, LicensedAppName);
            if (license.Status == LicenseCheck.License.LicenseStatus.Valid)
            {
                license.SaveToRegistry(LicenseRegistryKey);

                LastLicenseCheck = DateTime.Now;
                return true;
            }
            else
            {
                return Request();
            }
        }

        public static bool Request()
        {
            using (LicenseCheck.RequestLicenseForm form = new LicenseCheck.RequestLicenseForm())
            {
                form.ActivationCode = LicenseCheck.License.FormatActivationCode(LicenseCheck.License.GetActivationCode(LicensedAppName));
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) != System.Windows.Forms.DialogResult.OK) return false;

                LicenseCheck.License license = LicenseCheck.License.FromFile(form.LicenseFile, LicensedAppName);
                if (license.Status != LicenseCheck.License.LicenseStatus.Valid) return false;

                license.SaveToRegistry(LicenseRegistryKey);

                LastLicenseCheck = DateTime.Now;

                // License information
                LicenseInformation();

                return true;
            }
        }

        public static void LicenseInformation()
        {
            LicenseCheck.License license = LicenseCheck.License.FromRegistry(LicenseRegistryKey, LicensedAppName);

            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(license.LicenseInfo);
        }
    }
}
