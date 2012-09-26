using System;
using System.Text;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public class PosCheckResult
    {
        [Flags]
        public enum CheckResult
        {
            None = 0,
            DiameterError = 1,
            ShapeError = 2,
            LengthError = 4,
            PieceLengthError = 8,
            StandardDiameterError = 16,
            SamePosKeyWarning = 32
        }

        [Flags]
        public enum CheckType
        {
            None = 0,
            Errors = 1,
            Warnings = 2,
            All = Errors | Warnings
        }

        public string pos;
        public Dictionary<CheckResult, List<ObjectId>> results;

        public PosCheckResult()
        {
            pos = string.Empty;
            results = new Dictionary<CheckResult, List<ObjectId>>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<CheckResult, List<ObjectId>> item in results)
            {
                sb.Append("\n");
                sb.Append(pos);
                sb.Append(" Pozu: ");
                switch (item.Key)
                {
                    case CheckResult.DiameterError:
                        sb.Append("Çap Hatası");
                        break;
                    case CheckResult.LengthError:
                        sb.Append("Boy Hatası");
                        break;
                    case CheckResult.PieceLengthError:
                        sb.Append("Parça Boyu Hatası");
                        break;
                    case CheckResult.SamePosKeyWarning:
                        sb.Append("Poz Açılımı Aynı");
                        break;
                    case CheckResult.ShapeError:
                        sb.Append("Şekil Hatası");
                        break;
                    case CheckResult.StandardDiameterError:
                        sb.Append("Standart Olmayan Çap Hatası");
                        break;
                }
                sb.Append(" (");
                sb.Append(item.Value.Count);
                sb.Append(")");
            }
            return sb.ToString();
        }

        public static List<PosCheckResult> CheckAllInGroup(ObjectId groupId, CheckType checkType)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            List<PosCopy> pliste = PosCopy.ReadAllInGroup(groupId, PosCopy.PosGrouping.None);
            List<int> standardDiameters = new List<int>();
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(groupId, OpenMode.ForRead) as PosGroup;
                    foreach (string ds in group.StandardDiameters.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int d;
                        if (int.TryParse(ds, out d))
                        {
                            standardDiameters.Add(d);
                        }
                    }
                }
                catch
                {
                    ;
                }
            }

            List<PosCheckResult> results = new List<PosCheckResult>();
            foreach (PosCopy x in pliste)
            {
                PosCheckResult check = new PosCheckResult();
                check.pos = x.pos;

                int dia = 0;
                if (!int.TryParse(x.diameter, out dia) || !standardDiameters.Contains(dia))
                {
                    if (check.results.ContainsKey(CheckResult.StandardDiameterError))
                    {
                        check.results[CheckResult.StandardDiameterError].Add(x.list[0]);
                    }
                    else
                    {
                        check.results.Add(CheckResult.StandardDiameterError, new List<ObjectId>() { x.list[0] });
                    }
                }

                if (!string.IsNullOrEmpty(x.pos))
                {
                    foreach (PosCopy y in pliste)
                    {
                        if (x == y) continue;

                        CheckResult err = CheckResult.None;
                        if ((x.pos == y.pos) && ((checkType & CheckType.Errors) != CheckType.None))
                        {
                            // Farkli cap kontrolu
                            if (x.diameter != y.diameter) err = CheckResult.DiameterError;
                            // Farkli sekil kontrolu
                            if (x.shapeId != y.shapeId) err = CheckResult.ShapeError;
                            // Farkli boy kontrolu
                            if (x.length != y.length) err = CheckResult.LengthError;
                            // Farkli acilim kontrolu
                            if (x.a != y.a || x.b != y.b || x.c != y.c || x.d != y.d || x.e != y.e || x.f != y.f) err = CheckResult.PieceLengthError;
                        }
                        else if ((x.pos != y.pos) && ((checkType & CheckType.Warnings) != CheckType.None))
                        {
                            // Aynı sekil ve acilim kontrolu
                            if (x.key == y.key) err = CheckResult.SamePosKeyWarning;
                        }

                        // Add to error list
                        if (err != CheckResult.None)
                        {
                            if (check.results.ContainsKey(err))
                            {
                                check.results[err].Add(y.list[0]);
                            }
                            else
                            {
                                check.results.Add(err, new List<ObjectId>() { y.list[0] });
                            }
                        }
                    }
                }

                if (check.results.Count != 0)
                {
                    PosCheckResult same = results.Find(p => p.pos == check.pos);
                    if (same != null)
                    {
                        foreach (KeyValuePair<CheckResult, List<ObjectId>> item in check.results)
                        {
                            if (same.results.ContainsKey(item.Key))
                            {
                                foreach (ObjectId id in item.Value)
                                {
                                    same.results[item.Key].Add(id);
                                }
                            }
                            else
                            {
                                same.results.Add(item.Key, item.Value);
                            }
                        }
                    }
                    else
                    {
                        results.Add(check);
                    }
                }
            }

            results.Sort(new CompareByPosNumber());

            return results;
        }

        public static List<PosCheckResult> CheckAllInSelection(ObjectId groupId, IEnumerable<ObjectId> items, CheckType checkType)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            List<PosCopy> pliste = PosCopy.ReadAllInSelection(items, PosCopy.PosGrouping.None);
            List<int> standardDiameters = new List<int>();
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PosGroup group = tr.GetObject(groupId, OpenMode.ForRead) as PosGroup;
                    foreach (string ds in group.StandardDiameters.Split(new char[] { ' ', ',', ';', ':', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        int d;
                        if (int.TryParse(ds, out d))
                        {
                            standardDiameters.Add(d);
                        }
                    }
                }
                catch
                {
                    ;
                }
            }

            List<PosCheckResult> results = new List<PosCheckResult>();
            foreach (PosCopy x in pliste)
            {
                PosCheckResult check = new PosCheckResult();
                check.pos = x.pos;

                int dia = 0;
                if (!int.TryParse(x.diameter, out dia) || !standardDiameters.Contains(dia))
                {
                    if (check.results.ContainsKey(CheckResult.StandardDiameterError))
                    {
                        check.results[CheckResult.StandardDiameterError].Add(x.list[0]);
                    }
                    else
                    {
                        check.results.Add(CheckResult.StandardDiameterError, new List<ObjectId>() { x.list[0] });
                    }
                }

                if (!string.IsNullOrEmpty(x.pos))
                {
                    foreach (PosCopy y in pliste)
                    {
                        if (x == y) continue;

                        CheckResult err = CheckResult.None;
                        if ((x.pos == y.pos) && ((checkType & CheckType.Errors) != CheckType.None))
                        {
                            // Farkli cap kontrolu
                            if (x.diameter != y.diameter) err = CheckResult.DiameterError;
                            // Farkli sekil kontrolu
                            if (x.shapeId != y.shapeId) err = CheckResult.ShapeError;
                            // Farkli boy kontrolu
                            if (x.length != y.length) err = CheckResult.LengthError;
                            // Farkli acilim kontrolu
                            if (x.a != y.a || x.b != y.b || x.c != y.c || x.d != y.d || x.e != y.e || x.f != y.f) err = CheckResult.PieceLengthError;
                        }
                        else if ((x.pos != y.pos) && ((checkType & CheckType.Warnings) != CheckType.None))
                        {
                            // Aynı sekil ve acilim kontrolu
                            if (x.key == y.key) err = CheckResult.SamePosKeyWarning;
                        }

                        // Add to error list
                        if (err != CheckResult.None)
                        {
                            if (check.results.ContainsKey(err))
                            {
                                check.results[err].Add(y.list[0]);
                            }
                            else
                            {
                                check.results.Add(err, new List<ObjectId>() { y.list[0] });
                            }
                        }
                    }
                }

                if (check.results.Count != 0)
                {
                    PosCheckResult same = results.Find(p => p.pos == check.pos);
                    if (same != null)
                    {
                        foreach (KeyValuePair<CheckResult, List<ObjectId>> item in check.results)
                        {
                            if (same.results.ContainsKey(item.Key))
                            {
                                foreach (ObjectId id in item.Value)
                                {
                                    same.results[item.Key].Add(id);
                                }
                            }
                            else
                            {
                                same.results.Add(item.Key, item.Value);
                            }
                        }
                    }
                    else
                    {
                        results.Add(check);
                    }
                }
            }

            results.Sort(new CompareByPosNumber());

            return results;
        }

        private class CompareByPosNumber : IComparer<PosCheckResult>
        {
            public int Compare(PosCheckResult e1, PosCheckResult e2)
            {
                int p1 = 0;
                int p2 = 0;
                int.TryParse(e1.pos, out p1);
                int.TryParse(e2.pos, out p2);

                return (p1 == p2 ? 0 : (p1 < p2 ? -1 : 1));
            }
        }

        public static void ConsoleOut(List<PosCheckResult> list)
        {
            if (list.Count != 0)
            {
                Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("\nPozlandirmada asagidaki hatalar bulundu:");
                ed.WriteMessage("\n----------------------------------------");
                StringBuilder sb = new StringBuilder();
                foreach (PosCheckResult result in list)
                {
                    sb.Append(result.ToString());
                }
                ed.WriteMessage(sb.ToString());
            }
        }
    }
}
