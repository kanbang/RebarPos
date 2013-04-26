using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using OZOZ.RebarPosWrapper;
using System.Collections.Generic;
using Autodesk.AutoCAD.EditorInput;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void BOQEdit(ObjectId tableid)
        {
            using (EditBOQForm form = new EditBOQForm())
            {
                if (!form.Init(tableid))
                    return;

                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(null, form, false) != System.Windows.Forms.DialogResult.OK)
                    return;

                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        BOQTable table = tr.GetObject(tableid, OpenMode.ForWrite) as BOQTable;
                        if (table == null) return;

                        table.SuspendUpdate();

                        Point3d pt = table.BasePoint;
                        table.TransformBy(Matrix3d.Scaling(form.TextHeight / table.Scale, pt));

                        table.Note = form.TableNote;
                        table.Heading = form.TableHeader;
                        table.Footing = form.TableFooter;

                        table.Multiplier = form.Multiplier;

                        table.MaxRows = form.TableRows;
                        table.TableSpacing = form.TableMargin;

                        table.DisplayUnit = form.DisplayUnit;
                        table.Precision = form.Precision;
                        
                        table.ResumeUpdate();

                        tr.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
