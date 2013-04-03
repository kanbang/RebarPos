using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using System.Collections.Generic;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        public void ReadUserPosShapes()
        {
            string userFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RebarPos");
            string userShapesFile = System.IO.Path.Combine(userFolder, "ShapeList.txt");

            try
            {
                if (System.IO.File.Exists(userShapesFile))
                {
                    PosShape.ClearPosShapes();
                    PosShape.ReadPosShapesFromFile(userShapesFile);
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void PosShapes()
        {
            using (PosShapesForm form = new PosShapesForm())
            {
                if (form.Init(ShowShapes))
                {
                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == DialogResult.OK)
                    {
                        ShowShapes = form.ShowShapes;

                        DWGUtility.RefreshAllPos();
                    }
                }
            }
        }
    }
}
