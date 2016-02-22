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
        private class OldPos
        {
            // The scale ratio between new and old pos blocks
            private static double scaleRatio = 25.0;

            private static char diameterSymbol = 'ƒ';
            private static char spacingSymbol = '/';

            private static string multiplierDPName = "METRAJ_CARPAN";
            private static string multiplierDPValue_0 = "METRAJA_DAHIL_DEGIL";
            private static string multiplierDPValue_1 = "METRAJA_DAHIL";

            public ObjectId id = ObjectId.Null;

            public Point3d insPoint = Point3d.Origin;
            public double rotation = 0;
            public double scale = 1;

            public string pos = "";
            public string count = "";
            public string diameter = "";
            public string spacing = "";
            public string a = "", b = "", c = "", d = "", e = "", f = "";
            public string shapeName = "";
            public int multiplier = 0;
            public bool showLength = false;
            public string note = "";
            public ObjectId noteId = ObjectId.Null;
            public ObjectId lengthId = ObjectId.Null;
            public Point3d notePt;
            public Point3d lengthPt;
            public bool noteInText = false;
            public bool lengthInText = false;
            public bool detached = false;

            public OldPos(Transaction tr, ObjectId brefId)
            {
                id = brefId;
                BlockReference bref = tr.GetObject(id, OpenMode.ForRead) as BlockReference;

                if (bref != null)
                {
                    rotation = bref.Rotation;
                    scale = Math.Abs(bref.ScaleFactors[0]) * scaleRatio;
                    insPoint = bref.Position + new Vector3d(0.35, 0.65, 0) * scale;

                    foreach (ObjectId attId in bref.AttributeCollection)
                    {
                        AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForRead);
                        switch (attRef.Tag.ToLowerInvariant())
                        {
                            case "poz":
                                pos = attRef.TextString;
                                break;
                            case "boya":
                                a = attRef.TextString.Replace('-', '~');
                                break;
                            case "boyb":
                                b = attRef.TextString.Replace('-', '~');
                                break;
                            case "boyc":
                                c = attRef.TextString.Replace('-', '~');
                                break;
                            case "boyd":
                                d = attRef.TextString.Replace('-', '~');
                                break;
                            case "boye":
                                e = attRef.TextString.Replace('-', '~');
                                break;
                            case "boyf":
                                f = attRef.TextString.Replace('-', '~');
                                break;
                            case "sekil":
                                shapeName = attRef.TextString;
                                break;
                            case "carpan":
                                int val;
                                if (int.TryParse(attRef.TextString, out val))
                                {
                                    multiplier = val;
                                }
                                break;
                            case "boygoster":
                                showLength = (attRef.TextString == "1");
                                break;
                            case "not":
                                note = attRef.TextString;
                                noteId = attId;
                                notePt = attRef.Position;
                                break;
                            case "yazi":
                                string txt = attRef.TextString;
                                // Remove all text after note text
                                int i = txt.LastIndexOf(" L=");
                                if (i != -1)
                                {
                                    lengthInText = true;
                                    txt = txt.Substring(0, i);
                                }
                                // Remove all text after a space (length or note)
                                i = txt.IndexOf(' ');
                                if (i != -1)
                                {
                                    noteInText = true;
                                    note = txt.Substring(i + 1);
                                    txt = txt.Substring(0, i);
                                }
                                // Explode at spacing symbol
                                i = txt.LastIndexOf(spacingSymbol);
                                if (i != -1)
                                {
                                    spacing = txt.Substring(i + 1);
                                    txt = txt.Substring(0, i);
                                }
                                // Explode at diameter symbol
                                string[] parts = txt.Split(diameterSymbol);
                                if (parts.Length == 1)
                                {
                                    diameter = parts[0];
                                }
                                else if (parts.Length > 1)
                                {
                                    count = parts[0];
                                    diameter = parts[1];
                                }
                                break;
                            case "boy":
                                lengthId = attId;
                                lengthPt = attRef.Position;
                                break;
                        }
                    }
                    detached = (string.IsNullOrEmpty(shapeName));

                    if (bref.IsDynamicBlock)
                    {
                        foreach (DynamicBlockReferenceProperty prop in bref.DynamicBlockReferencePropertyCollection)
                        {
                            if (prop.PropertyName == multiplierDPName)
                            {
                                if ((string)prop.Value == multiplierDPValue_0)
                                    multiplier = 0;
                                else if ((string)prop.Value == multiplierDPValue_1 && multiplier == 0)
                                    multiplier = 1;
                            }
                        }
                    }
                }
            }
        }

        private void PosUpgrade()
        {
            TypedValue[] tvs = new TypedValue[] {
                new TypedValue((int)DxfCode.Start, "INSERT")
            };
            PromptSelectionResult res = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.GetSelection(new SelectionFilter(tvs));
            if (res.Status != PromptStatus.OK)
            {
                return;
            }

            string blockName = "POZ_*";

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Filter pos blocks
                    List<ObjectId> list = new List<ObjectId>();
                    foreach (ObjectId id in res.Value.GetObjectIds())
                    {
                        if (id.ObjectClass == Autodesk.AutoCAD.Runtime.RXObject.GetClass(typeof(BlockReference)))
                        {
                            BlockReference blockRef = tr.GetObject(id, OpenMode.ForRead) as BlockReference;

                            BlockTableRecord block = null;
                            if (blockRef.IsDynamicBlock)
                            {
                                block = tr.GetObject(blockRef.DynamicBlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                            }
                            else
                            {
                                block = tr.GetObject(blockRef.BlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                            }

                            if (block != null)
                            {
                                if (WCMatch(block.Name, blockName)) list.Add(id);
                            }
                        }
                    }

                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                    foreach (ObjectId id in list)
                    {
                        // Read pos block
                        OldPos oldPos = new OldPos(tr, id);

                        // Insert new pos
                        RebarPos pos = new RebarPos();

                        pos.SuspendUpdate();

                        pos.TransformBy(Matrix3d.Displacement(oldPos.insPoint.GetAsVector()));
                        pos.TransformBy(Matrix3d.Scaling(oldPos.scale, oldPos.insPoint));
                        pos.TransformBy(Matrix3d.Rotation(oldPos.rotation, Vector3d.ZAxis, oldPos.insPoint));

                        pos.Pos = oldPos.pos;
                        pos.Count = oldPos.count;
                        pos.Diameter = oldPos.diameter;
                        pos.Spacing = oldPos.spacing;
                        pos.Shape = oldPos.shapeName;
                        pos.A = oldPos.a;
                        pos.B = oldPos.b;
                        pos.C = oldPos.c;
                        pos.D = oldPos.d;
                        pos.E = oldPos.e;
                        pos.F = oldPos.f;
                        pos.Multiplier = oldPos.multiplier;
                        pos.Note = oldPos.note;

                        // Align note text
                        if (oldPos.noteInText)
                        {
                            pos.NoteAlignment = RebarPos.SubTextAlignment.Right;
                        }
                        else if (!oldPos.noteId.IsNull)
                        {
                            pos.NoteGrip = oldPos.notePt;
                        }

                        // Align length text
                        if (oldPos.lengthInText)
                        {
                            pos.LengthAlignment = RebarPos.SubTextAlignment.Right;
                        }
                        else if (!oldPos.lengthId.IsNull)
                        {
                            pos.LengthGrip = oldPos.lengthPt;
                        }

                        pos.Detached = oldPos.detached;
                        if (!oldPos.detached && !oldPos.showLength) pos.Display = RebarPos.DisplayStyle.WithoutLength;

                        pos.ResumeUpdate();

                        pos.SetDatabaseDefaults(db);

                        btr.AppendEntity(pos);
                        tr.AddNewlyCreatedDBObject(pos, true);

                        // Erase old pos block
                        BlockReference bref = tr.GetObject(id, OpenMode.ForWrite) as BlockReference;
                        bref.Erase();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "RebarPos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                tr.Commit();
            }
        }

        private bool WCMatch(string str, string pattern)
        {
            return Autodesk.AutoCAD.Internal.Utils.WcMatchEx(str, pattern, true);
        }
    }
}
