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
                AddItem(PosShape.GetPosShape(shapeName));
            }

            ResumeUpdate();
        }
    }
}
