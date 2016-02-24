using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Input;

using Autodesk.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Microsoft.Win32;

namespace RebarPosCommands
{
    public static class MenuUtility
    {
        public static bool LoadPosMenu()
        {
            string cuifile = RebarPosCommands.MyCommands.ApplicationInstallPath;
            if (string.IsNullOrEmpty(cuifile))
            {
                return false;
            }
            cuifile = System.IO.Path.Combine(cuifile, "Resources");
            cuifile = System.IO.Path.Combine(cuifile, "RebarPos.cuix");
            if (!System.IO.File.Exists(cuifile))
            {
                return false;
            }

            string mainCui = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("MENUNAME") + ".cuix";
            Autodesk.AutoCAD.Customization.CustomizationSection cs = new Autodesk.AutoCAD.Customization.CustomizationSection(mainCui);
            Autodesk.AutoCAD.Customization.PartialCuiFileCollection pcfc = cs.PartialCuiFiles;

            if (pcfc.Contains(cuifile))
            {
                return false;
            }

            LoadCuix(cuifile);

            return true;
        }

        private static void LoadCuix(string cuiFilename)
        {
            cuiFilename.Replace(Path.DirectorySeparatorChar, '/');

            Document doc = Application.DocumentManager.MdiActiveDocument;

            object oldCmdEcho = Application.GetSystemVariable("CMDECHO");
            object oldFileDia = Application.GetSystemVariable("FILEDIA");
            Application.SetSystemVariable("CMDECHO", 0);
            Application.SetSystemVariable("FILEDIA", 0);

            doc.SendStringToExecute(
              "_.cuiload "
              + "\"" + cuiFilename + "\""
              + " ",
              false, false, false
            );
            doc.SendStringToExecute(
              "(setvar \"FILEDIA\" "
              + oldFileDia.ToString()
              + ")(princ) ",
              false, false, false
            );
            doc.SendStringToExecute(
              "(setvar \"CMDECHO\" "
              + oldCmdEcho.ToString()
              + ")(princ) ",
              false, false, false
            );
        }
    }
}
