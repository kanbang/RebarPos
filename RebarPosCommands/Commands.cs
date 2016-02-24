using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;
using System.Windows.Forms;
using System.IO;
using System;
using System.ComponentModel;


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
        public static string DeveloperSymbol { get { return "OZOZ"; } }
        public static string LicensedAppName { get { return "RebarPos"; } }

        public static string ApplicationRegistryKey
        {
            get
            {
                return "SOFTWARE\\" + DeveloperSymbol + "\\" + LicensedAppName;
            }
        }

        public static string ApplicationInstallPath
        {
            get
            {
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(ApplicationRegistryKey);
                    if (key == null)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        object val = key.GetValue("InstallPath");
                        if (val == null)
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return val.ToString();
                        }
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public static string ApplicationBinPath
        {
            get
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

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
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.PointMonitor += new PointMonitorEventHandler(ed_PointMonitor);
            MonitoredPoint = Point3d.Origin;

            SetCurrentGroup();
            ReadUserPosShapes();
            ReadUserTableStyles();

            ShowShapes = false;

            // Load prompt
            string heading = "Donatı Pozlandırma ve Metraj Programı v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(2) + " yüklendi.";
            ed.WriteMessage("\n");
            ed.WriteMessage(heading);
            ed.WriteMessage("\n");
            ed.WriteMessage(new string('=', heading.Length));
            PosCategories();

            // License information
            LicenseInformation();
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

        [Category("Pozlandırma komutları")]
        [Description("Donatı pozlandırma ve metraj komutları.")]
        [CommandMethod("OZOZRebarPos", "POS", "POS_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_Pos()
        {
            if (!CheckLicense()) return;

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

        [Category("Pozlandırma komutları")]
        [Description("Seçilen pozu düzenler.")]
        [CommandMethod("OZOZRebarPos", "POSEDIT", "POSEDIT_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_PosEdit()
        {
            if (!CheckLicense()) return;

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

        [Category("Metraj komutları")]
        [Description("Seçilen metraj tablosunu düzenler.")]
        [CommandMethod("OZOZRebarPos", "BOQEDIT", "BOQEDIT_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_BOQEdit()
        {
            if (!CheckLicense()) return;

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

        [Category("Pozlandırma komutları")]
        [Description("Çizime bir poz bloğu ekler.")]
        [CommandMethod("OZOZRebarPos", "NEWPOS", "NEWPOS_Local", CommandFlags.Modal)]
        public void CMD_NewPos()
        {
            if (!CheckLicense()) return;

            NewPos();
        }

        [Category("Pozlandırma komutları")]
        [Description("Tum pozlara otomatik numara verir. Pozlar çap, şekil, ve boylarına gore gruplandırılır. Ayrıca seçilen kriterlere göre sıralandırılır.")]
        [CommandMethod("OZOZRebarPos", "NUMBERPOS", "NUMBERPOS_Local", CommandFlags.Modal)]
        public void CMD_NumberPos()
        {
            if (!CheckLicense()) return;

            NumberPos();
        }

        [Category("Pozlandırma komutları")]
        [Description("Poz numaralarını siler.")]
        [CommandMethod("OZOZRebarPos", "EMPTYPOS", "EMPTYPOS_Local", CommandFlags.Modal)]
        public void CMD_EmptyBalloons()
        {
            if (!CheckLicense()) return;

            EmptyBalloons();
        }

        [Category("Pozlandırma komutları")]
        [Description("Pozları kontrol eder.")]
        [CommandMethod("OZOZRebarPos", "POSCHECK", "POSCHECK_Local", CommandFlags.Modal)]
        public void CMD_PosCheck()
        {
            if (!CheckLicense()) return;

            PosCheck();
        }

        [Category("Pozlandırma komutları")]
        [Description("Poz içeriğini diger pozlara kopyalar.")]
        [CommandMethod("OZOZRebarPos", "COPYPOS", "COPYPOS_Local", CommandFlags.Modal)]
        public void CMD_CopyPos()
        {
            if (!CheckLicense()) return;

            CopyPos();
        }

        [Category("Pozlandırma komutları")]
        [Description("Poz içeriğini diger pozlara kopyalar. Değiştirilen pozlar metraja dahil edilmez.")]
        [CommandMethod("OZOZRebarPos", "COPYPOSDETAIL", "COPYPOSDETAIL_Local", CommandFlags.Modal)]
        public void CMD_CopyPosDetail()
        {
            if (!CheckLicense()) return;

            CopyPosDetail();
        }

        [Category("Pozlandırma komutları")]
        [Description("Seçilen textlere poz numarası yazar.")]
        [CommandMethod("OZOZRebarPos", "COPYPOSNUMBER", "COPYPOSNUMBER_Local", CommandFlags.Modal)]
        public void CMD_CopyPosNumber()
        {
            if (!CheckLicense()) return;

            CopyPosNumber();
        }

        [Category("Pozlandırma komutları")]
        [Description("Verilen özelliklere sahip pozları seçer. Seçilen poz bloklarının özelliklerini bir seferde değiştirir.")]
        [CommandMethod("OZOZRebarPos", "POSFIND", "POSFIND_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_FindReplace()
        {
            if (!CheckLicense()) return;

            FindReplace(true);
        }

        [Category("Pozlandırma komutları")]
        [Description("Donatı açılımlarını düzenler.")]
        [CommandMethod("OZOZRebarPos", "POSSHAPES", "POSSHAPES_Local", CommandFlags.Modal)]
        public void CMD_PosShapes()
        {
            if (!CheckLicense()) return;

            PosShapes();
        }

        [Category("Pozlandırma komutları")]
        [Description("Donatı açılımlarını pozların üzerinde gösterir veya gizler.")]
        [CommandMethod("OZOZRebarPos", "TOGGLESHAPES", "TOGGLESHAPES_Local", CommandFlags.Modal)]
        public void CMD_ToggleShapes()
        {
            if (!CheckLicense()) return;

            ShowShapes = !ShowShapes;
            DWGUtility.RefreshAllPos();
        }

        [Category("Pozlandırma komutları")]
        [Description("Donatı açılımlarını pozların üzerinde gösterir.")]
        [CommandMethod("OZOZRebarPos", "SHOWSHAPES", "SHOWSHAPES_Local", CommandFlags.Modal)]
        public void CMD_ShowShapes()
        {
            if (!CheckLicense()) return;

            ShowShapes = true;
            DWGUtility.RefreshAllPos();
        }

        [Category("Pozlandırma komutları")]
        [Description("Pozların üzerinde gösterilen tüm açılımları siler.")]
        [CommandMethod("OZOZRebarPos", "HIDESHAPES", "HIDESHAPES_Local", CommandFlags.Modal)]
        public void CMD_HideShapes()
        {
            if (!CheckLicense()) return;

            ShowShapes = false;
            DWGUtility.RefreshAllPos();
        }

        [Category("Pozlandırma komutları")]
        [Description("Poz boylarını gösterir veya gizler.")]
        [CommandMethod("OZOZRebarPos", "POSLENGTH", "POSLENGTH_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_PosLength()
        {
            if (!CheckLicense()) return;

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

        [Category("Pozlandırma komutları")]
        [Description("Pozların metraja dahil edilip edilmemesini düzenler.")]
        [CommandMethod("OZOZRebarPos", "INCLUDEPOS", "INCLUDEPOS_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_IncludePos()
        {
            if (!CheckLicense()) return;

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

        [Category("Pozlandırma komutları")]
        [Description("Son poz numarasını gösterir.")]
        [CommandMethod("OZOZRebarPos", "LASTPOSNUMBER", "LASTPOSNUMBER_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_LastPosNumber()
        {
            if (!CheckLicense()) return;

            PromptSelectionResult sel = DWGUtility.SelectAllPosUser();
            if (sel.Status != PromptStatus.OK) return;
            ObjectId[] items = sel.Value.GetObjectIds();

            int lastNum = GetLastPosNumber(items);

            if (lastNum != -1)
            {
                MessageBox.Show("Son poz numarası: " + lastNum.ToString(), "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [Category("Metraj komutları")]
        [Description("Seçilen pozların metrajını yapar.")]
        [CommandMethod("OZOZRebarPos", "BOQ", "BOQ_Local", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void CMD_DrawBOQ()
        {
            if (!CheckLicense()) return;

            DrawBOQ();
        }

        [Category("Metraj komutları")]
        [Description("Metraj tablosu sitillerini düzenler.")]
        [CommandMethod("OZOZRebarPos", "TABLESTYLE", "TABLESTYLE_Local", CommandFlags.Modal)]
        public void CMD_TableStyle()
        {
            if (!CheckLicense()) return;

            TableStyles();
        }

        [Category("Diğer komutlar")]
        [Description("Pozlandırma ayarlarını değiştirir.")]
        [CommandMethod("OZOZRebarPos", "POSSETTINGS", "POSSETTINGS_Local", CommandFlags.Modal)]
        public void CMD_PosGroups()
        {
            if (!CheckLicense()) return;

            PosGroups();
        }

        [Category("Diğer komutlar")]
        [Description("Program menüsünü yeniden yükler.")]
        [CommandMethod("OZOZRebarPos", "POSMENU", "POSMENU_Local", CommandFlags.Modal)]
        public void CMD_PosMenu()
        {
            MenuUtility.LoadPosMenu();
        }

        [Category("Diğer komutlar")]
        [Description("Poz bloğu değişikliklerini çizime uygular.")]
        [CommandMethod("OZOZRebarPos", "POSUPGRADE", "POSUPGRADE_Local", CommandFlags.Modal)]
        public void CMD_PosUpgrade()
        {
            if (!CheckLicense()) return;

            PosUpgrade();
        }

        [Category("Diğer komutlar")]
        [Description("Poz bloklarını ve metraj tablolarını patlatır.")]
        [CommandMethod("OZOZRebarPos", "POSEXPLODE", "POSEXPLODE_Local", CommandFlags.Modal)]
        public void CMD_PosExplode()
        {
            if (!CheckLicense()) return;

            PosExplode();
        }

        [Category("Diğer komutlar")]
        [Description("Donatı pozlandırma ve metraj komutlarının açıklamalarını gösterir.")]
        [CommandMethod("OZOZRebarPos", "POSHELP", "POSHELP_Local", CommandFlags.Modal)]
        public void CMD_PosHelp()
        {
            PosHelp();
        }

        [Category("Diğer komutlar")]
        [Description("Lisans bilgilerini gosterir.")]
        [CommandMethod("OZOZRebarPos", "POSLICENSE", "POSLICENSE_Local", CommandFlags.Modal)]
        public void CMD_PosLicense()
        {
            LicenseInformation();

            Autodesk.AutoCAD.ApplicationServices.Application.DisplayTextScreen = true;

            PromptKeywordOptions opts = new PromptKeywordOptions("Lisansınızı değiştirmek istiyor musunuz? [Evet/Hayır]: ", "Yes No");
            opts.AllowNone = false;
            PromptResult result = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetKeywords(opts);

            if (result.Status == PromptStatus.OK)
            {
                switch (result.StringResult)
                {
                    case "Yes":
                        RequestLicense();
                        break;
                }
            }
        }

        [CommandMethod("OZOZRebarPos", "DUMPSHAPES", CommandFlags.Modal)]
        public void CMD_DumpShapes()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointResult result = ed.GetPoint("Baz noktası: ");
            if (result.Status != PromptStatus.OK)
                return;

            Point3d pt = result.Value;
            foreach (string name in PosShape.GetAllPosShapes())
            {
                DWGUtility.DrawShape(name, pt, 100);

                pt = pt.Add(new Vector3d(0, 120, 0));
            }
        }
    }
}
