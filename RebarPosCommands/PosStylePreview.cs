using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public partial class PosStylePreview : DrawingPreview
    {
        private static string TempGroupKey = "TEMPPOSSTYLEPREVIEW";

        bool mInit;
        private RebarPos mPos;
        private RebarPos mVarLengthPos;
        private RebarPos mPosOnlyPos;

        private Color mTextColor;
        private Color mPosColor;
        private Color mCircleColor;
        private Color mMultiplierColor;
        private Color mGroupColor;
        private Color mNoteColor;
        private Color mCurrentGroupHighlightColor;
        private Color mCountColor;
        private string mFormula, mFormulaVariableLength, mFormulaLengthOnly, mFormulaPosOnly;

        public Color TextColor { get { return mTextColor; } set { mTextColor = value; Refresh(); } }
        public Color PosColor { get { return mPosColor; } set { mPosColor = value; Refresh(); } }
        public Color CircleColor { get { return mCircleColor; } set { mCircleColor = value; Refresh(); } }
        public Color MultiplierColor { get { return mMultiplierColor; } set { mMultiplierColor = value; Refresh(); } }
        public Color GroupColor { get { return mGroupColor; } set { mGroupColor = value; Refresh(); } }
        public Color NoteColor { get { return mNoteColor; } set { mNoteColor = value; Refresh(); } }
        public Color CurrentGroupHighlightColor { get { return mCurrentGroupHighlightColor; } set { mCurrentGroupHighlightColor = value; Refresh(); } }
        public Color CountColor { get { return mCountColor; } set { mCountColor = value; Refresh(); } }

        public string Formula { get { return mFormula; } set { mFormula = value; Refresh(); } }
        public string FormulaVariableLength { get { return mFormulaVariableLength; } set { mFormulaVariableLength = value; Refresh(); } }
        public string FormulaLengthOnly { get { return mFormulaLengthOnly; } set { mFormulaLengthOnly = value; Refresh(); } }
        public string FormulaPosOnly { get { return mFormulaPosOnly; } set { mFormulaPosOnly = value; Refresh(); } }

        public PosStylePreview()
        {
            InitializeComponent();

            mInit = false;
        }

        private void InitPos()
        {
            if (!mInit)
            {
                mPos = new RebarPos();
                mPos.Display = RebarPos.DisplayStyle.All;
                mPos.LengthGrip = new Point3d(9.25, 0.0, 0.0);
                mPos.NoteGrip = new Point3d(0.0, 2.0, 0);
                Point3d pt = new Point3d(0.0, 0.0, 0.0);
                mPos.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                mPos.TransformBy(Matrix3d.Scaling(25.0, pt));

                mVarLengthPos = new RebarPos();
                mVarLengthPos.Display = RebarPos.DisplayStyle.All;
                mVarLengthPos.LengthGrip = new Point3d(9.25, 0.0, 0.0);
                mVarLengthPos.NoteGrip = new Point3d(0.0, 0.0, 0.0);
                pt = new Point3d(0.0, -50.0, 0.0);
                mVarLengthPos.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                mVarLengthPos.TransformBy(Matrix3d.Scaling(25.0, pt));

                mPosOnlyPos = new RebarPos();
                mPosOnlyPos.Display = RebarPos.DisplayStyle.MarkerOnly;
                mPosOnlyPos.LengthGrip = new Point3d(9.25, 0, 0);
                mPosOnlyPos.NoteGrip = new Point3d(0.0, -2.0, 0.0);
                pt = new Point3d(0.0, -100.0, 0.0);
                mPosOnlyPos.TransformBy(Matrix3d.Displacement(pt.GetAsVector()));
                mPosOnlyPos.TransformBy(Matrix3d.Scaling(25.0, pt));

                mInit = true;
            }
        }

        private void RemoveGroup()
        {
            InitPos();

            mPos.GroupIdForDisplay = ObjectId.Null;
            mVarLengthPos.GroupIdForDisplay = ObjectId.Null;
            mPosOnlyPos.GroupIdForDisplay = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary dict = tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead) as DBDictionary;
                    if (dict.Contains(PosGroup.TableName))
                    {
                        DBDictionary groupDict = tr.GetObject(dict.GetAt(PosGroup.TableName), OpenMode.ForWrite) as DBDictionary;
                        if (groupDict.Contains(TempGroupKey))
                        {
                            groupDict.Remove(TempGroupKey);
                        }
                        tr.Commit();
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

        public void SetGroup()
        {
            InitPos();

            RemoveGroup();
            ObjectId id = ObjectId.Null;

            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary dict = tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead) as DBDictionary;
                    if (dict.Contains(PosGroup.TableName))
                    {
                        DBDictionary groupDict = tr.GetObject(dict.GetAt(PosGroup.TableName), OpenMode.ForWrite) as DBDictionary;

                        PosGroup group = new PosGroup();
                        group.TextColor = Autodesk.AutoCAD.Colors.Color.FromColor(mTextColor);
                        group.PosColor = Autodesk.AutoCAD.Colors.Color.FromColor(mPosColor);
                        group.CircleColor = Autodesk.AutoCAD.Colors.Color.FromColor(mCircleColor);
                        group.MultiplierColor = Autodesk.AutoCAD.Colors.Color.FromColor(mMultiplierColor);
                        group.GroupColor = Autodesk.AutoCAD.Colors.Color.FromColor(mGroupColor);
                        group.NoteColor = Autodesk.AutoCAD.Colors.Color.FromColor(mNoteColor);
                        group.CurrentGroupHighlightColor = Autodesk.AutoCAD.Colors.Color.FromColor(mCurrentGroupHighlightColor);
                        group.Formula = mFormula;
                        group.FormulaVariableLength = mFormulaVariableLength;
                        group.FormulaLengthOnly = mFormulaLengthOnly;
                        group.FormulaPosOnly = mFormulaPosOnly;

                        id = groupDict.SetAt(TempGroupKey, group);
                        tr.AddNewlyCreatedDBObject(group, true);
                        tr.Commit();
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.Message, "RebarPos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            mPos.GroupIdForDisplay = id;
            mVarLengthPos.GroupIdForDisplay = id;
            mPosOnlyPos.GroupIdForDisplay = id;

            ClearItems();
            AddItem(new RebarPos[] { mPos, mVarLengthPos, mPosOnlyPos });

            Refresh();
        }

        public void SetPos(string pos, string count, string diameter, string spacing, string length1, string length2)
        {
            InitPos();

            mPos.Pos = pos;
            mPos.Count = count;
            mPos.Diameter = diameter;
            mPos.Spacing = spacing;
            mPos.Shape = "GENEL";
            mPos.A = length1;

            mVarLengthPos.Pos = pos;
            mVarLengthPos.Count = count;
            mVarLengthPos.Diameter = diameter;
            mVarLengthPos.Spacing = spacing;
            mVarLengthPos.Shape = "GENEL";
            mVarLengthPos.A = length1 + "~" + length2;

            mPosOnlyPos.Pos = pos;
            mPosOnlyPos.Count = count;
            mPosOnlyPos.Diameter = diameter;
            mPosOnlyPos.Spacing = spacing;
            mPosOnlyPos.Shape = "GENEL";
            mPosOnlyPos.A = length1;

            Refresh();
        }
    }
}
