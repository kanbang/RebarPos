using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
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
            // Create overrules
            DrawableOverrule.Overruling = true;
            // ObjectOverrule.AddOverrule(RXClass.GetClass(typeof(RebarPos)), HighlightGroupOverrule.Instance, true);
            ObjectOverrule.AddOverrule(RXClass.GetClass(typeof(RebarPos)), CountOverrule.Instance, true);
            ObjectOverrule.AddOverrule(RXClass.GetClass(typeof(RebarPos)), ShowShapesOverrule.Instance, true);

            // Add menu
            MenuUtility.MakeRibbonTab();
        }

        void IExtensionApplication.Terminate()
        {
            // Remove overrules
            //ObjectOverrule.RemoveOverrule(RXClass.GetClass(typeof(RebarPos)), HighlightGroupOverrule.Instance);
            ObjectOverrule.RemoveOverrule(RXClass.GetClass(typeof(RebarPos)), CountOverrule.Instance);
            ObjectOverrule.RemoveOverrule(RXClass.GetClass(typeof(RebarPos)), ShowShapesOverrule.Instance);
        }
    }
}
