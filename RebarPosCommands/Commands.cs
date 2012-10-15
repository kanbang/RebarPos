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
            DWGUtility.CreateDefaultBOQStyles();
            SetCurrentGroup();

            ShowShapes = false;
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

        [CommandMethod("RebarPos", "POS", "POS_Local", CommandFlags.Modal)]
        public void CMD_Pos()
        {
            bool cont = true;
            while (cont)
            {
                PromptEntityOptions opts = new PromptEntityOptions("Poz secin veya [Yeni/Numaralandir/Kopyala/kOntrol/Metraj/bul Degistir/numara Sil/Acilimlar/Tablo stili/ayaRlar]: ",
                    "New Numbering Copy Check BOQ Find Empty Shapes Table Preferences");
                opts.AllowNone = false;
                PromptEntityResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetEntity(opts);

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
                            CopyPos(true, false);
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

        [CommandMethod("RebarPos", "POSEDIT", "POSEDIT_Local", CommandFlags.Modal)]
        public void CMD_PosEdit()
        {
            PromptEntityOptions opts = new PromptEntityOptions("Select entity: ");
            opts.AllowNone = false;
            PromptEntityResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetEntity(opts);
            if (result.Status == PromptStatus.OK)
            {
                PosEdit(result.ObjectId, result.PickedPoint);
            }
        }

        [CommandMethod("RebarPos", "BOQEDIT", "BOQEDIT_Local", CommandFlags.Modal)]
        public void CMD_BOQEdit()
        {
            PromptEntityOptions opts = new PromptEntityOptions("Select entity: ");
            opts.AllowNone = false;
            PromptEntityResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetEntity(opts);
            if (result.Status == PromptStatus.OK)
            {
                BOQEdit(result.ObjectId);
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
            CopyPos(true, false);
        }

        [CommandMethod("RebarPos", "COPYPOSDETAIL", "COPYPOSDETAIL_Local", CommandFlags.Modal)]
        public void CMD_CopyPosDetail()
        {
            CopyPosDetail(true, false);
        }

        [CommandMethod("RebarPos", "COPYPOSNUMBER", "COPYPOSNUMBER_Local", CommandFlags.Modal)]
        public void CMD_CopyPosNumber()
        {
            CopyPos(false, true);
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

        [CommandMethod("RebarPos", "POSLENGTH", "POSLENGTH_Local", CommandFlags.Modal)]
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

        [CommandMethod("RebarPos", "INCLUDEPOS", "INCLUDEPOS_Local", CommandFlags.Modal)]
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

        [CommandMethod("RebarPos", "LASTPOSNUMBER", "LASTPOSNUMBER_Local", CommandFlags.Modal)]
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

        [CommandMethod("RebarPos", "DUMPPOSSHAPES2", CommandFlags.Modal)]
        public void CMD_DumpShapes2()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                int i=1;
                double y = 0;
                foreach (string shape in PosShape.GetAllPosShapes().Keys)
                {
                    Point3d pt = new Point3d(0, y, 0);

                    RebarPos pos = new RebarPos();
                    pos.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                    pos.TransformBy(Matrix3d.Scaling(25.0, pt));

                    pos.Pos = i.ToString();
                    pos.Count = "1";
                    pos.Diameter = "12";
                    pos.Spacing = "";
                    pos.Shape = shape;
                    pos.A = "100"; pos.B = "100"; pos.C = "100"; pos.D = "100"; pos.E = "100"; pos.F = "100";
                    pos.Note = "";

                    pos.SetDatabaseDefaults(db);

                    btr.AppendEntity(pos);
                    tr.AddNewlyCreatedDBObject(pos, true);

                    i++;
                    y += 35;
                }

                tr.Commit();
            }
        }

        [CommandMethod("RebarPos", "DUMPPOSSHAPES", CommandFlags.Modal)]
        public void CMD_DumpShapes()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Select Shape Dump File";
            sfd.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(sfd.FileName))
            {
                foreach (PosShape shape in PosShape.GetAllPosShapes().Values)
                {
                    sw.WriteLine("BEGIN");
                    sw.WriteLine("Name\t" + shape.Name);
                    sw.WriteLine("Fields\t" + shape.Fields.ToString());
                    sw.WriteLine("Formula\t" + shape.Formula);
                    sw.WriteLine("FormulaBending\t" + shape.FormulaBending);
                    sw.WriteLine("Count\t" + shape.Items.Count);
                    double dx = double.MaxValue; double dy = double.MaxValue;
                    for (int j = 0; j < shape.Items.Count; j++)
                    {
                        PosShape.Shape s = shape.Items[j];
                        if (s is PosShape.ShapeLine)
                        {
                            PosShape.ShapeLine x = s as PosShape.ShapeLine;
                            dx = Math.Min(dx, x.X1);
                            dy = Math.Min(dy, x.Y1);
                            dx = Math.Min(dx, x.X2);
                            dy = Math.Min(dy, x.Y2);
                        }
                        else if (s is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc x = s as PosShape.ShapeArc;
                            dx = Math.Min(dx, x.X);
                            dy = Math.Min(dy, x.Y);
                        }
                        else if (s is PosShape.ShapeText)
                        {
                            PosShape.ShapeText x = s as PosShape.ShapeText;
                            dx = Math.Min(dx, x.X);
                            dy = Math.Min(dy, x.Y);
                        }
                    }
                    for (int j = 0; j < shape.Items.Count; j++)
                    {
                        PosShape.Shape s = shape.Items[j];
                        string col = s.Color.ColorIndex.ToString();
                        string vis = s.Visible ? "V" : "I";
                        if (s is PosShape.ShapeLine)
                        {
                            PosShape.ShapeLine x = s as PosShape.ShapeLine;
                            string x1 = (x.X1 - dx).ToString("F2");
                            string y1 = (x.Y1 - dy).ToString("F2");
                            string x2 = (x.X2 - dx).ToString("F2");
                            string y2 = (x.Y2 - dy).ToString("F2");
                            sw.WriteLine("LINE\t" + x1 + "\t" + y1 + "\t" + x2 + "\t" + y2 + "\t" + col + "\t" + vis);
                        }
                        else if (s is PosShape.ShapeArc)
                        {
                            PosShape.ShapeArc x = s as PosShape.ShapeArc;
                            string x1 = (x.X - dx).ToString("F2");
                            string y1 = (x.Y - dy).ToString("F2");
                            string r = (x.R).ToString("F2");
                            string a1 = (x.StartAngle).ToString("F2");
                            string a2 = (x.EndAngle).ToString("F2");
                            sw.WriteLine("ARC\t" + x1 + "\t" + y1 + "\t" + r + "\t" + a1 + "\t" + a2 + "\t" + col + "\t" + vis);
                        }
                        else if (s is PosShape.ShapeText)
                        {
                            PosShape.ShapeText x = s as PosShape.ShapeText;
                            string x1 = (x.X - dx).ToString("F2");
                            string y1 = (x.Y - dy).ToString("F2");
                            string h = (x.Height).ToString("F2");
                            string th = ((int)x.HorizontalAlignment).ToString();
                            string tv = ((int)x.VerticalAlignment).ToString();
                            sw.WriteLine("TEXT\t" + x1 + "\t" + y1 + "\t" + h + "\t" + x.Text + "\t" + col + "\t" + th + "\t" + tv + "\t" + vis);
                        }
                    }
                    sw.WriteLine("END");
                    sw.WriteLine();
                }
            }
        }

        [CommandMethod("RebarPos", "DUMPPOSGROUPS", CommandFlags.Modal)]
        public void CMD_DumpGroups()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Select Group Dump File";
            sfd.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosGroup.TableName), OpenMode.ForRead);

                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    foreach (DBDictionaryEntry entry in dict)
                    {
                        PosGroup group = (PosGroup)tr.GetObject(entry.Value, OpenMode.ForRead);

                        sw.WriteLine("Name: " + group.Name);
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
