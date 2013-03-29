using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;
using System.Windows.Forms;
using System.IO;
using System;


// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(RebarPosCommands.MyCommands))]

namespace RebarPosCommands
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public partial class MyCommands
    {
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an instance member then the enclosing class is 
        // instantiated for each document. If the member is a static member then
        // the enclosing class is NOT instantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.
        public MyCommands()
        {
            SetCurrentGroup();
            ReadUserPosShapes();
            ReadUserTableStyles();

            ShowShapes = false;

            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.PointMonitor += new PointMonitorEventHandler(ed_PointMonitor);
            MonitoredPoint = Point3d.Origin;
        }

        private bool ShowShapes
        {
            get
            {
                return ShowShapesOverrule.Instance.Has();
            }
            set
            {
                if (value)
                    ShowShapesOverrule.Instance.Add();
                else
                    ShowShapesOverrule.Instance.Remove();
            }
        }

        public Point3d MonitoredPoint { get; private set; }

        [CommandMethod("RebarPos", "POS", "POS_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_Pos()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            // Edit entity if there is a pickset
            PromptSelectionResult selectionRes = ed.SelectImplied();
            if (selectionRes.Status != PromptStatus.Error && selectionRes.Value.Count > 0)
            {
                ObjectId id = selectionRes.Value[0].ObjectId;
                ed.SetImpliedSelection(new ObjectId[0]);
                ItemEdit(id, MonitoredPoint);
                return;
            }

            bool cont = true;
            while (cont)
            {
                PromptEntityOptions opts = new PromptEntityOptions("Poz secin veya [Yeni/Numaralandir/Kopyala/kOntrol/Metraj/bul Degistir/numara Sil/Acilimlar/Tablo stili/ayaRlar]: ",
                    "New Numbering Copy Check BOQ Find Empty Shapes Table Preferences");
                opts.AllowNone = false;
                PromptEntityResult result = ed.GetEntity(opts);

                if (result.Status == PromptStatus.Keyword)
                {
                    switch (result.StringResult)
                    {
                        case "New":
                            NewPos();
                            break;
                        case "Numbering":
                            NumberPos();
                            break;
                        case "Empty":
                            EmptyBalloons();
                            break;
                        case "Copy":
                            CopyPos();
                            break;
                        case "Check":
                            PosCheck();
                            break;
                        case "BOQ":
                            DrawBOQ();
                            break;
                        case "Find":
                            FindReplace(false);
                            break;
                        case "Shapes":
                            PosShapes();
                            break;
                        case "Table":
                            TableStyles();
                            break;
                        case "Preferences":
                            PosGroups();
                            break;
                    }
                    cont = false;
                }
                else if (result.Status == PromptStatus.OK)
                {
                    ItemEdit(result.ObjectId, result.PickedPoint);
                    cont = true;
                }
                else
                {
                    cont = false;
                }
            }
        }

        void ed_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            if (!e.Context.PointComputed)
                return;

            MonitoredPoint = e.Context.ComputedPoint;
        }

        // Edits a pos or table
        private void ItemEdit(ObjectId id, Point3d pt)
        {
            RXClass cls = id.ObjectClass;
            if (cls == RXObject.GetClass(typeof(RebarPos)))
            {
                PosEdit(id, pt);
            }
            else if (cls == RXObject.GetClass(typeof(BOQTable)))
            {
                BOQEdit(id);
            }
        }

        [CommandMethod("RebarPos", "POSEDIT", "POSEDIT_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_PosEdit()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            // Edit entity if there is a pickset
            PromptSelectionResult selectionRes = ed.SelectImplied();
            if (selectionRes.Status != PromptStatus.Error && selectionRes.Value.Count > 0)
            {
                ObjectId id = selectionRes.Value[0].ObjectId;
                ed.SetImpliedSelection(new ObjectId[0]);
                ItemEdit(id, MonitoredPoint);
                return;
            }

            PromptEntityOptions opts = new PromptEntityOptions("Select entity: ");
            opts.AllowNone = false;
            PromptEntityResult result = ed.GetEntity(opts);
            if (result.Status == PromptStatus.OK)
            {
                ItemEdit(result.ObjectId, result.PickedPoint);
            }
        }

        [CommandMethod("RebarPos", "BOQEDIT", "BOQEDIT_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_BOQEdit()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            // Edit entity if there is a pickset
            PromptSelectionResult selectionRes = ed.SelectImplied();
            if (selectionRes.Status != PromptStatus.Error && selectionRes.Value.Count > 0)
            {
                ObjectId id = selectionRes.Value[0].ObjectId;
                ed.SetImpliedSelection(new ObjectId[0]);
                ItemEdit(id, MonitoredPoint);
                return;
            }

            PromptEntityOptions opts = new PromptEntityOptions("Select entity: ");
            opts.AllowNone = false;
            PromptEntityResult result = ed.GetEntity(opts);
            if (result.Status == PromptStatus.OK)
            {
                ItemEdit(result.ObjectId, result.PickedPoint);
            }
        }

        [CommandMethod("RebarPos", "NEWPOS", "NEWPOS_Local", CommandFlags.Modal)]
        public void CMD_NewPos()
        {
            NewPos();
        }

        [CommandMethod("RebarPos", "NUMBERPOS", "NUMBERPOS_Local", CommandFlags.Modal)]
        public void CMD_NumberPos()
        {
            NumberPos();
        }

        [CommandMethod("RebarPos", "EMPTYPOS", "EMPTYPOS_Local", CommandFlags.Modal)]
        public void CMD_EmptyBalloons()
        {
            EmptyBalloons();
        }

        [CommandMethod("RebarPos", "POSCHECK", "POSCHECK_Local", CommandFlags.Modal)]
        public void CMD_PosCheck()
        {
            PosCheck();
        }

        [CommandMethod("RebarPos", "COPYPOS", "COPYPOS_Local", CommandFlags.Modal)]
        public void CMD_CopyPos()
        {
            CopyPos();
        }

        [CommandMethod("RebarPos", "COPYPOSDETAIL", "COPYPOSDETAIL_Local", CommandFlags.Modal)]
        public void CMD_CopyPosDetail()
        {
            CopyPosDetail();
        }

        [CommandMethod("RebarPos", "COPYPOSNUMBER", "COPYPOSNUMBER_Local", CommandFlags.Modal)]
        public void CMD_CopyPosNumber()
        {
            CopyPosNumber();
        }

        [CommandMethod("RebarPos", "BOQ", "BOQ_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_DrawBOQ()
        {
            DrawBOQ();
        }

        [CommandMethod("RebarPos", "POSFIND", "POSFIND_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_FindReplace()
        {
            FindReplace(true);
        }

        [CommandMethod("RebarPos", "POSSHAPES", "POSSHAPES_Local", CommandFlags.Modal)]
        public void CMD_PosShapes()
        {
            PosShapes();
        }

        [CommandMethod("RebarPos", "TOGGLESHAPES", "TOGGLESHAPES_Local", CommandFlags.Modal)]
        public void CMD_ToggleShapes()
        {
            ShowShapes = !ShowShapes;
            DWGUtility.RefreshAllPos();
        }

        [CommandMethod("RebarPos", "SHOWSHAPES", "SHOWSHAPES_Local", CommandFlags.Modal)]
        public void CMD_ShowShapes()
        {
            ShowShapes = true;
            DWGUtility.RefreshAllPos();
        }

        [CommandMethod("RebarPos", "HIDESHAPES", "HIDESHAPES_Local", CommandFlags.Modal)]
        public void CMD_HideShapes()
        {
            ShowShapes = false;
            DWGUtility.RefreshAllPos();
        }

        [CommandMethod("RebarPos", "TABLESTYLE", "TABLESTYLE_Local", CommandFlags.Modal)]
        public void CMD_TableStyle()
        {
            TableStyles();
        }

        [CommandMethod("RebarPos", "POSLENGTH", "POSLENGTH_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_PosLength()
        {
            PromptSelectionResult selresult = DWGUtility.SelectAllPosUser();
            if (selresult.Status != PromptStatus.OK) return;

            PromptKeywordOptions opts = new PromptKeywordOptions("L boyunu [Göster/giZle]: ", "Show Hide");
            opts.AllowNone = false;
            PromptResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetKeywords(opts);

            if (result.Status == PromptStatus.OK)
            {
                switch (result.StringResult)
                {
                    case "Show":
                        ShowPosLength(selresult.Value.GetObjectIds(), true);
                        break;
                    case "Hide":
                        ShowPosLength(selresult.Value.GetObjectIds(), false);
                        break;
                }
            }
        }

        [CommandMethod("RebarPos", "INCLUDEPOS", "INCLUDEPOS_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_IncludePos()
        {
            PromptSelectionResult selresult = DWGUtility.SelectAllPosUser();
            if (selresult.Status != PromptStatus.OK) return;

            PromptKeywordOptions opts = new PromptKeywordOptions("Metraja [Dahil et/metrajdan Cikar]: ", "Add Remove");
            opts.AllowNone = false;
            PromptResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetKeywords(opts);

            if (result.Status == PromptStatus.OK)
            {
                switch (result.StringResult)
                {
                    case "Add":
                        IcludePosinBOQ(selresult.Value.GetObjectIds(), true);
                        break;
                    case "Remove":
                        IcludePosinBOQ(selresult.Value.GetObjectIds(), false);
                        break;
                }
            }
        }

        [CommandMethod("RebarPos", "LASTPOSNUMBER", "LASTPOSNUMBER_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_LastPosNumber()
        {
            PromptSelectionResult sel = DWGUtility.SelectAllPosUser();
            if (sel.Status != PromptStatus.OK) return;
            ObjectId[] items = sel.Value.GetObjectIds();

            int lastNum = GetLastPosNumber(items);

            if (lastNum != -1)
            {
                MessageBox.Show("Son poz numarası: " + lastNum.ToString(), "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [CommandMethod("RebarPos", "POSHELP", "POSHELP_Local", CommandFlags.Modal)]
        public void CMD_PosHelp()
        {
            MessageBox.Show("Yardım dosyası henüz tamamlanmadı.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        [CommandMethod("RebarPos", "POSSETTINGS", "POSSETTINGS_Local", CommandFlags.Modal)]
        public void CMD_PosGroups()
        {
            PosGroups();
        }

        [CommandMethod("RebarPos", "POSMENU", "POSMENU_Local", CommandFlags.Modal)]
        public void CMD_PosMenu()
        {
            MenuUtility.LoadPosMenu();
        }

        [CommandMethod("RebarPos", "POSTEST", CommandFlags.Modal)]
        public void CMD_PosTest()
        {
            System.Collections.Generic.List<string> names = PosShape.GetAllPosShapes();
            PosShape sh = PosShape.GetPosShape("00");
            sh.Name = "Hello";
            int a = 1;
        }
    }
}
