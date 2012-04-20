﻿using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class RebarPosCommands
    {
        private void EmptyBalloons()
        {
            PromptSelectionResult result = DWGUtility.SelectPosUser();
            if (result.Status == PromptStatus.OK)
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        foreach (SelectedObject sel in result.Value)
                        {
                            RebarPos pos = tr.GetObject(sel.ObjectId, OpenMode.ForWrite) as RebarPos;
                            if (pos != null)
                            {
                                pos.Pos = "";
                                pos.BasePoint = new Autodesk.AutoCAD.Geometry.Point3d(100, 100, 100);
                            }
                        }
                        tr.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "RebarPos");
                    }
                }
            }
        }
    }
}