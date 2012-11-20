using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class MyCommands
    {
        private void EmptyBalloons()
        {
            PromptSelectionResult result = DWGUtility.SelectAllPosUser(true);
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
                            }
                        }
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
