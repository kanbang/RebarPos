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
    public partial class TableStylePreview : DrawingPreview
    {
        private BOQTable mTable;

        private string mColumns;

        private string mPosLabel;
        private string mCountLabel;
        private string mDiameterLabel;
        private string mLengthLabel;
        private string mShapeLabel;
        private string mTotalLengthLabel;
        private string mDiameterListLabel;
        private string mDiameterLengthLabel;
        private string mUnitWeightLabel;
        private string mWeightLabel;
        private string mGrossWeightLabel;
        private string mMultiplierHeadingLabel;

        private ObjectId mTextStyleId;
        private ObjectId mHeadingStyleId;
        private ObjectId mFootingStyleId;

        public string Columns { get { return mColumns; } set { mColumns = value; Refresh(); } }

        public string PosLabel { get { return mPosLabel; } set { mPosLabel = value; Refresh(); } }
        public string CountLabel { get { return mCountLabel; } set { mCountLabel = value; Refresh(); } }
        public string DiameterLabel { get { return mDiameterLabel; } set { mDiameterLabel = value; Refresh(); } }
        public string LengthLabel { get { return mLengthLabel; } set { mLengthLabel = value; Refresh(); } }
        public string ShapeLabel { get { return mShapeLabel; } set { mShapeLabel = value; Refresh(); } }
        public string TotalLengthLabel { get { return mTotalLengthLabel; } set { mTotalLengthLabel = value; Refresh(); } }
        public string DiameterListLabel { get { return mDiameterListLabel; } set { mDiameterListLabel = value; Refresh(); } }
        public string DiameterLengthLabel { get { return mDiameterLengthLabel; } set { mDiameterLengthLabel = value; Refresh(); } }
        public string UnitWeightLabel { get { return mUnitWeightLabel; } set { mUnitWeightLabel = value; Refresh(); } }
        public string WeightLabel { get { return mWeightLabel; } set { mWeightLabel = value; Refresh(); } }
        public string GrossWeightLabel { get { return mGrossWeightLabel; } set { mGrossWeightLabel = value; Refresh(); } }
        public string MultiplierHeadingLabel { get { return mMultiplierHeadingLabel; } set { mMultiplierHeadingLabel = value; Refresh(); } }

        public ObjectId TextStyleId { get { return mTextStyleId; } set { mTextStyleId = value; Refresh(); } }
        public ObjectId HeadingStyleId { get { return mHeadingStyleId; } set { mHeadingStyleId = value; Refresh(); } }
        public ObjectId FootingStyleId { get { return mFootingStyleId; } set { mFootingStyleId = value; Refresh(); } }

        public TableStylePreview()
        {
            this.Name = "PosStylePreview";
            this.Size = new System.Drawing.Size(300, 150);
        }

        public void SetTable()
        {
            ClearItems();

            if (mTable != null) mTable.Dispose();

            mTable = new BOQTable();
            mTable.SuspendUpdate();

            string shape = "GENEL";
            if (PosShape.HasPosShape("00"))
                shape = "00";
            mTable.Items.Add(1, 6, 14, 240, shape, "240", "", "", "", "", "");
            if (PosShape.HasPosShape("11"))
                shape = "11";
            mTable.Items.Add(1, 12, 16, 320, shape, "200", "120", "", "", "", "");

            mTable.Multiplier = 2;
            mTable.Precision = 2;
            mTable.ColumnDef = Columns;

            mTable.PosLabel = PosLabel;
            mTable.CountLabel = CountLabel;
            mTable.DiameterLabel = DiameterLabel;
            mTable.LengthLabel = LengthLabel;
            mTable.ShapeLabel = ShapeLabel;
            mTable.TotalLengthLabel = TotalLengthLabel;
            mTable.DiameterListLabel = DiameterListLabel;
            mTable.DiameterLengthLabel = DiameterLengthLabel;
            mTable.UnitWeightLabel = UnitWeightLabel;
            mTable.WeightLabel = WeightLabel;
            mTable.GrossWeightLabel = GrossWeightLabel;
            mTable.MultiplierHeadingLabel = MultiplierHeadingLabel;

            mTable.TextStyleId = TextStyleId;
            mTable.HeadingStyleId = HeadingStyleId;
            mTable.FootingStyleId = FootingStyleId;

            mTable.ResumeUpdate();

            AddItem(mTable);
        }

        protected override void Dispose(bool disposing)
        {
            ClearItems();
            if (mTable != null) mTable.Dispose();

            base.Dispose(disposing);
        }
    }
}
