using System.Reflection;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.GraphicsInterface;

using OZOZ.RebarPosWrapper;

// This line is not mandatory, but improves loading performances
[assembly: ExtensionApplication(typeof(RebarPosCommands.RebarPosPlugin))]

namespace RebarPosCommands
{
    // This class is instantiated by AutoCAD once and kept alive for the 
    // duration of the session. If you don't do any one time initialization 
    // then you should remove this class.
    public class RebarPosPlugin : IExtensionApplication
    {
        void IExtensionApplication.Initialize()
        {
            // Load the dependent assembly
            try
            {
                Assembly.LoadFrom(System.IO.Path.Combine(RebarPosCommands.MyCommands.ApplicationBinPath, "ManagedRebarPos.dll"));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            // Create overrules
            DrawableOverrule.Overruling = true;
            ObjectOverrule.AddOverrule(RXClass.GetClass(typeof(RebarPos)), CountOverrule.Instance, true);
            ObjectOverrule.AddOverrule(RXClass.GetClass(typeof(RebarPos)), ShowShapesOverrule.Instance, true);
        }

        void IExtensionApplication.Terminate()
        {
            // Remove overrules
            ObjectOverrule.RemoveOverrule(RXClass.GetClass(typeof(RebarPos)), CountOverrule.Instance);
            ObjectOverrule.RemoveOverrule(RXClass.GetClass(typeof(RebarPos)), ShowShapesOverrule.Instance);
        }
    }
}
