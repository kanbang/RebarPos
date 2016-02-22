using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using Autodesk.AutoCAD.EditorInput;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void PosExplode()
        {
            PromptSelectionResult sel = DWGUtility.SelectAllPosAndBOQTableUser();
            if (sel.Status != PromptStatus.OK) return;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                    DBObjectCollection explodedEntites = new DBObjectCollection();

                    // Explode all selected entites
                    foreach (ObjectId id in sel.Value.GetObjectIds())
                    {
                        Entity en = (Entity)tr.GetObject(id, OpenMode.ForWrite);
                        en.Explode(explodedEntites);
                        en.Erase();
                    }

                    // Create new entities
                    foreach (DBObject obj in explodedEntites)
                    {
                        Entity en = (Entity)obj;
                        btr.AppendEntity(en);
                        tr.AddNewlyCreatedDBObject(en, true);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                tr.Commit();
            }
        }
    }
}
