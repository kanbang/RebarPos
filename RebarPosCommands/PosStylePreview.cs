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
        private PosGroup mGroup;

        private Color mTextColor;
        private Color mPosColor;
        private Color mCircleColor;
        private Color mMultiplierColor;
        private Color mGroupColor;
        private Color mNoteColor;
        private Color mCurrentGroupHighlightColor;
        private Color mCountColor;
        private string mFormula, mFormulaVariableLength, mFormulaLengthOnly, mFormulaPosOnly;
        private ObjectId mTextStyleId;
        private ObjectId mNoteStyleId;
        private double mNoteScale;

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

        public ObjectId TextStyleId { get { return mTextStyleId; } set { mTextStyleId = value; Refresh(); } }
        public ObjectId NoteStyleId { get { return mNoteStyleId; } set { mNoteStyleId = value; Refresh(); } }
        public double NoteScale { get { return mNoteScale; } set { mNoteScale = value; Refresh(); } }

        public PosStylePreview()
        {
            this.Name = "PosStylePreview";
            this.Size = new System.Drawing.Size(300, 150);
        }

        public void SetGroup()
        {
            ClearItems();
            if (mGroup != null) mGroup.Dispose();

            mGroup = new PosGroup();

            mGroup.TextColor = Autodesk.AutoCAD.Colors.Color.FromColor(mTextColor);
            mGroup.PosColor = Autodesk.AutoCAD.Colors.Color.FromColor(mPosColor);
            mGroup.CircleColor = Autodesk.AutoCAD.Colors.Color.FromColor(mCircleColor);
            mGroup.MultiplierColor = Autodesk.AutoCAD.Colors.Color.FromColor(mMultiplierColor);
            mGroup.GroupColor = Autodesk.AutoCAD.Colors.Color.FromColor(mGroupColor);
            mGroup.NoteColor = Autodesk.AutoCAD.Colors.Color.FromColor(mNoteColor);
            mGroup.CurrentGroupHighlightColor = Autodesk.AutoCAD.Colors.Color.FromColor(mCurrentGroupHighlightColor);
            mGroup.Formula = mFormula;
            mGroup.FormulaVariableLength = mFormulaVariableLength;
            mGroup.FormulaLengthOnly = mFormulaLengthOnly;
            mGroup.FormulaPosOnly = mFormulaPosOnly;

            mGroup.TextStyleId = mTextStyleId;
            mGroup.NoteStyleId = mNoteStyleId;
            mGroup.NoteScale = mNoteScale;

            AddItem(mGroup);
        }

        protected override void Dispose(bool disposing)
        {
            ClearItems();
            if (mGroup != null) mGroup.Dispose();
        }
    }
}
