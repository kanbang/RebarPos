using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System;

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
            
        }

        void IExtensionApplication.Terminate()
        {
            
        }
    }
}
