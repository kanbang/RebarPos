using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;
using System.Windows.Forms;
using System.IO;


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
            DWGUtility.CreateDefaultShapes();
            ObjectId id = DWGUtility.CreateDefaultGroups();
            DWGUtility.CreateDefaultBOQStyles();
            SetCurrentGroup(id);

            ShowShapes = false;
        }

        private string CurrentGroupName { get; set; }
        private ObjectId CurrentGroupId { get; set; }
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
                PromptEntityOptions opts = new PromptEntityOptions("Poz secin veya [Yeni/Numaralandir/Kopyala/Grup/kOntrol/Metraj/bul Degistir/numara Sil/Acilimlar/Tablo stili]: ",
                    "New Numbering Copy Group Check BOQ Find Empty Shapes Table");
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
                        case "Group":
                            PosGroups();
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

        [CommandMethod("RebarPos", "POSGROUP", "POSGROUP_Local", CommandFlags.Modal)]
        public void CMD_PosGroups()
        {
            PosGroups();
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
            PromptSelectionResult result = DWGUtility.SelectGroup(CurrentGroupId);
            if (result.Status != PromptStatus.OK) return;

            int lastNum = GetLastPosNumber(result.Value.GetObjectIds());

            if (lastNum != -1)
            {
                MessageBox.Show("Son poz numarası (Grup " + CurrentGroupName + "): " + lastNum.ToString(), "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [CommandMethod("RebarPos", "POSHELP", "POSHELP_Local", CommandFlags.Modal)]
        public void CMD_PosHelp()
        {
            MessageBox.Show("Yardım dosyası henüz tamamlanmadı.", "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        [CommandMethod("RebarPos", "DUMPPOSSHAPES", CommandFlags.Modal)]
        public void CMD_DumpShapes()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                DBDictionary namedDict = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                DBDictionary dict = (DBDictionary)tr.GetObject(namedDict.GetAt(PosShape.TableName), OpenMode.ForRead);

                using (StreamWriter sw = new StreamWriter(@"C:\Users\Ozgur\Documents\Visual Studio 2008\Projects\ObjectARX 2010\samples\entity\RebarPos\s.txt"))
                {
                    int i = 1;
                    foreach (DBDictionaryEntry entry in dict)
                    {
                        PosShape shape = (PosShape)tr.GetObject(entry.Value, OpenMode.ForRead);

                        string n = "shape" + i.ToString();
                        sw.WriteLine("PosShape " + n + " = new PosShape();");
                        sw.WriteLine(n + ".Name = \"" + shape.Name + "\";");
                        sw.WriteLine(n + ".Fields = " + shape.Fields.ToString() + ";");
                        sw.WriteLine(n + ".Formula = \"" + shape.Formula + "\";");
                        sw.WriteLine(n + ".FormulaBending = \"" + shape.FormulaBending + "\";");
                        for (int j = 0; j < shape.Items.Count; j++)
                        {
                            PosShape.Shape s = shape.Items[i];
                            if (s is PosShape.ShapeLine)
                            {
                                PosShape.ShapeLine x = s as PosShape.ShapeLine;
                                sw.WriteLine(n + ".Items.AddLine(" + x.X1.ToString() + "," + x.Y1.ToString() + "," + x.X2.ToString() + "," + x.Y2.ToString() + ",Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci," + x.Color.ColorIndex.ToString() + ")," + (x.Visible ? "true" : "false") + ");");
                            }
                            else if (s is PosShape.ShapeArc)
                            {
                                PosShape.ShapeArc x = s as PosShape.ShapeArc;
                                sw.WriteLine(n + ".Items.AddArc(" + x.X.ToString() + "," + x.Y.ToString() + "," + x.R.ToString() + "," + x.StartAngle.ToString() + "," + x.EndAngle.ToString() + ",Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci," + x.Color.ColorIndex.ToString() + ")," + (x.Visible ? "true" : "false") + ");");
                            }
                            else if (s is PosShape.ShapeText)
                            {
                                PosShape.ShapeText x = s as PosShape.ShapeText;
                                sw.WriteLine(n + ".Items.AddText(" + x.X.ToString() + "," + x.Y.ToString() + "," + x.Height.ToString() + ",\"" + x.Text + "\",Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci," + x.Color.ColorIndex.ToString() + ")," + "TextHorizontalMode." + x.HorizontalAlignment.ToString() + "," + "TextVerticalMode." + x.VerticalAlignment.ToString() + "," + (x.Visible ? "true" : "false") + ");");
                            }
                        }
                        sw.WriteLine();
                        i++;
                    }
                }
            }
        }

    }
}
