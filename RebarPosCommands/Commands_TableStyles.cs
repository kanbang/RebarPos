using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        public void ReadUserTableStyles()
        {
            string userFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RebarPos");
            string userStylesFile = System.IO.Path.Combine(userFolder, "TableStyles.txt");

            try
            {
                if (System.IO.File.Exists(userStylesFile))
                {
                    BOQStyle.ClearBOQStyles();
                    BOQStyle.ReadBOQStylesFromFile(userStylesFile);
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void TableStyles()
        {
            TableStyleForm form = new TableStyleForm();

            if (form.Init())
            {
                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) == DialogResult.OK)
                {
                    ;
                }
            }
        }
    }
}