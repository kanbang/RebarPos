using OZOZ.RebarPosWrapper;

namespace RebarPosCommands
{
    public class PosShapeView : DrawingPreview
    {
        private string mShapeName;
        public string ShapeName { get { return mShapeName; } }

        public PosShapeView()
        {
            this.Name = "PosShapeView";
            this.Size = new System.Drawing.Size(300, 150);

            mShapeName = string.Empty;
        }

        public void SetShape(string shapeName)
        {
            SuspendUpdate();
            ClearItems();
            mShapeName = shapeName;

            if (!string.IsNullOrEmpty(mShapeName))
            {
                PosShape shape = PosShape.GetPosShape(shapeName);
                if (shape != null)
                {
                    PosShape shapeCopy = shape.Clone() as PosShape;
                    shapeCopy.ClearShapeTexts();
                    AddItem(shapeCopy);
                }
            }

            ResumeUpdate();
        }


        public void SetShape(string shapeName, string a, string b, string c, string d, string e, string f)
        {
            SuspendUpdate();
            ClearItems();
            mShapeName = shapeName;

            if (!string.IsNullOrEmpty(mShapeName))
            {
                PosShape shape = PosShape.GetPosShape(shapeName);
                if (shape != null)
                {
                    PosShape shapeCopy = shape.Clone() as PosShape;
                    shapeCopy.ClearShapeTexts();
                    shapeCopy.SetShapeTexts(a, b, c, d, e, f);
                    AddItem(shapeCopy);
                }
            }

            ResumeUpdate();
        }
    }
}
