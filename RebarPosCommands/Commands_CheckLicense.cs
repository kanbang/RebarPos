using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private DateTime LastLicenseCheck = DateTime.MinValue;
        private TimeSpan LicenseCheckInterval = TimeSpan.FromHours(1);

        private bool CheckLicense()
        {
            if (DateTime.Now - LastLicenseCheck < LicenseCheckInterval) return true;

            License license = License.FromRegistry(ApplicationRegistryKey, LicensedAppName);
            if (license.Status == License.LicenseStatus.Valid)
            {
                license.SaveToRegistry(ApplicationRegistryKey);

                LastLicenseCheck = DateTime.Now;
                return true;
            }

            using (RequestLicenseForm form = new RequestLicenseForm())
            {
                form.ActivationCode = License.FormatActivationCode(License.GetActivationCode(LicensedAppName));
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) != System.Windows.Forms.DialogResult.OK) return false;

                license = License.FromString(form.LicenseKey, LicensedAppName);
                if (license.Status != License.LicenseStatus.Valid) return false;

                license.SaveToRegistry(ApplicationRegistryKey);

                LastLicenseCheck = DateTime.Now;

                // License information
                LicenseInformation();

                return true;
            }
        }

        private void LicenseInformation()
        {
            License license = License.FromRegistry(ApplicationRegistryKey, LicensedAppName);

            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(license.LicenseInfo);
        }
    }
}
