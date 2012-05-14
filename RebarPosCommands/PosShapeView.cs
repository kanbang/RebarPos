using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace RebarPosCommands
{
    public partial class PosShapeView : UserControl
    {
        private struct DrawLine
        {
            public Color Color;
            public float X1, X2, Y1, Y2;

            public DrawLine(Color color, float x1, float y1, float x2, float y2)
            {
                Color = color;
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
            }
        }
        private struct DrawArc
        {
            public Color Color;
            public float X, Y, R;
            public float StartAngle, EndAngle;

            public DrawArc(Color color, float x, float y, float r, float startAngle, float endAngle)
            {
                Color = color;
                X = x;
                Y = y;
                R = r;
                StartAngle = startAngle;
                EndAngle = endAngle;
            }
        }
        private struct DrawText
        {
            public Color Color;
            public float X, Y, Height;
            public string Text;
            public StringAlignment HorizontalAlignment;
            public StringAlignment VerticalAlignment;

            public DrawText(Color color, float x, float y, float height, string text, StringAlignment horizontalAlignment, StringAlignment verticalAlignment)
            {
                Color = color;
                X = x;
                Y = y;
                Height = height;
                Text = text;
                HorizontalAlignment = horizontalAlignment;
                VerticalAlignment = verticalAlignment;
            }
        }
        private List<DrawLine> lines;
        private List<DrawArc> arcs;
        private List<DrawText> texts;

        public PosShapeView()
        {
            InitializeComponent();

            lines = new List<DrawLine>();
            arcs = new List<DrawArc>();
            texts = new List<DrawText>();

            m_Name = "";
            m_ShowName = false;
            m_Selected = false;
            m_SelectionColor = SystemColors.Highlight;
        }

        private string m_Name;
        private bool m_ShowName;
        private bool m_Selected;
        private Color m_SelectionColor;

        [Browsable(true), Category("Appearance"), DefaultValue("")]
        public string ShapeName { get { return m_Name; } set { m_Name = value; Refresh(); } }
        [Browsable(true), Category("Appearance"), DefaultValue(false)]
        public bool ShowName { get { return m_ShowName; } set { m_ShowName = value; Refresh(); } }
        [Browsable(true), Category("Appearance"), DefaultValue(false)]
        public bool Selected { get { return m_Selected; } set { m_Selected = value; Refresh(); } }
        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "Highlight")]
        public Color SelectionColor { get { return m_SelectionColor; } set { m_SelectionColor = value; Refresh(); } }

        public void Reset()
        {
            lines.Clear();
            arcs.Clear();
            texts.Clear();
        }

        public void AddLine(Color color, float x1, float y1, float x2, float y2)
        {
            lines.Add(new DrawLine(color, x1, y1, x2, y2));
            Refresh();
        }

        public void AddArc(Color color, float x, float y, float r, float startAngle, float endAngle)
        {
            arcs.Add(new DrawArc(color, x, y, r, startAngle, endAngle));
            Refresh();
        }

        public void AddText(Color color, float x, float y, float height, string text, StringAlignment horizontalAlignment, StringAlignment verticalAlignment)
        {
            texts.Add(new DrawText(color, x, y, height, text, horizontalAlignment, verticalAlignment));
            Refresh();
        }

        private void PosShapeView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!Enabled)
                g.Clear(SystemColors.Control);
            else
                g.Clear(BackColor);

            // Selection mark
            if (m_Selected)
            {
                using (Pen pen = new Pen(m_SelectionColor, 2.0f))
                {
                    Rectangle rec = ClientRectangle;
                    rec.Inflate(-2, -2);
                    g.DrawRectangle(pen, rec);
                }
            }

            // Show a default shape in design mode
            string dname = string.Empty;
            List<DrawLine> dlines = null;
            List<DrawArc> darcs = null;
            List<DrawText> dtexts = null;
            if (DesignMode)
            {
                dname = "Shape";
                dlines = new List<DrawLine>();
                darcs = new List<DrawArc>();
                dtexts = new List<DrawText>();
                dlines.Add(new DrawLine(Color.Red, 0, 0, 100, 0));
                dtexts.Add(new DrawText(Color.Yellow, 50, 2, 10, "A", StringAlignment.Center, StringAlignment.Near));
                dlines.Add(new DrawLine(Color.Red, 0, 0, 0, 20));
                dtexts.Add(new DrawText(Color.Yellow, -2, 10, 10, "B", StringAlignment.Far, StringAlignment.Center));
            }
            else
            {
                dname = m_Name;
                dlines = lines;
                darcs = arcs;
                dtexts = texts;
            }

            // Shape name
            if (m_ShowName)
            {
                using (Brush brush = new SolidBrush(ForeColor))
                {
                    g.DrawString(dname, Font, brush, 5, 8);
                }
            }

            bool hasEnt = false;
            // Calculate extents
            float xmin = float.MaxValue, xmax = float.MinValue, ymin = float.MaxValue, ymax = float.MinValue;
            foreach (DrawLine line in dlines)
            {
                hasEnt = true;
                xmin = Math.Min(xmin, Math.Min(line.X1, line.X2));
                xmax = Math.Max(xmax, Math.Max(line.X1, line.X2));
                ymin = Math.Min(ymin, Math.Min(line.Y1, line.Y2));
                ymax = Math.Max(ymax, Math.Max(line.Y1, line.Y2));
            }
            foreach (DrawArc arc in darcs)
            {
                hasEnt = true;
                xmin = Math.Min(xmin, arc.X - arc.R);
                xmax = Math.Max(xmax, arc.X + arc.R);
                ymin = Math.Min(ymin, arc.Y - arc.R);
                ymax = Math.Max(ymax, arc.Y + arc.R);
            }
            foreach (DrawText text in dtexts)
            {
                using (Font font = new Font(Font.FontFamily, 1.0f))
                {
                    hasEnt = true;
                    float left = 0, right = 0, top = 0, bottom = 0;
                    SizeF size = g.MeasureString(text.Text, font);
                    float fscale = text.Height / size.Height;
                    size = new SizeF(size.Width * fscale, size.Height * fscale);
                    switch (text.HorizontalAlignment)
                    {
                        case StringAlignment.Near:
                            right = size.Width;
                            break;
                        case StringAlignment.Far:
                            left = size.Width;
                            break;
                        case StringAlignment.Center:
                            left = right = size.Width / 2.0f;
                            break;
                    }
                    switch (text.VerticalAlignment)
                    {
                        case StringAlignment.Near:
                            top = size.Height;
                            break;
                        case StringAlignment.Far:
                            bottom = size.Height;
                            break;
                        case StringAlignment.Center:
                            top = bottom = size.Height / 2.0f;
                            break;
                    }
                    xmin = Math.Min(xmin, text.X - left);
                    xmax = Math.Max(xmax, text.X + right);
                    ymin = Math.Min(ymin, text.Y - top);
                    ymax = Math.Max(ymax, text.Y + bottom);
                }
            }
            if (!hasEnt)
                return;

            // Calculate scale
            float w = (float)ClientRectangle.Width;
            float h = (float)ClientRectangle.Height;
            if (m_ShowName)
            {
                h -= 15.0f;
            }
            float xscale = 0.9f * w / (xmax - xmin);
            float yscale = 0.9f * h / (ymax - ymin);
            float scale = Math.Min(xscale, yscale);
            // Client offsets
            float xoff = (w - scale * (xmax - xmin)) / 2.0f;
            float yoff = (h - scale * (ymax - ymin)) / 2.0f;
            if (m_ShowName)
            {
                yoff -= 15.0f / 2.0f;
            }
            // Transform
            g.ResetTransform();
            g.TranslateTransform(-xmin, -ymin, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.ScaleTransform(scale, -scale, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.TranslateTransform(xoff, h - yoff, System.Drawing.Drawing2D.MatrixOrder.Append);

            // Calculate conversion from world units to em-size
            float fontScale = 1.0f;
            using (Font font = new Font(Font.FontFamily, 1.0f))
            {
                fontScale = 1.0f / g.MeasureString("M", font).Height;
            }

            // Draw shapes
            foreach (DrawLine line in dlines)
            {
                using (Pen pen = new Pen(line.Color, 1.0f))
                {
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(pen, line.X1, line.Y1, line.X2, line.Y2);
                }
            }
            foreach (DrawArc arc in darcs)
            {
                using (Pen pen = new Pen(arc.Color, 1.0f))
                {
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawArc(pen, arc.X, arc.Y, 2.0f * arc.R, 2.0f * arc.R, arc.StartAngle, arc.EndAngle);
                }
            }

            foreach (DrawText text in dtexts)
            {
                g.ResetTransform();

                using (Brush brush = new SolidBrush(text.Color))
                using (Font font = new Font(Font.FontFamily, text.Height * fontScale))
                {
                    float txoff = 0, tyoff = 0;
                    SizeF size = g.MeasureString(text.Text, font);
                    switch (text.HorizontalAlignment)
                    {
                        case StringAlignment.Near:
                            txoff = 0;
                            break;
                        case StringAlignment.Far:
                            txoff = -size.Width;
                            break;
                        case StringAlignment.Center:
                            txoff = -size.Width / 2.0f;
                            break;
                    }
                    switch (text.VerticalAlignment)
                    {
                        case StringAlignment.Near:
                            tyoff = 0;
                            break;
                        case StringAlignment.Far:
                            tyoff = -size.Height;
                            break;
                        case StringAlignment.Center:
                            tyoff = -size.Height / 2.0f;
                            break;
                    }

                    // Transform upside-down text
                    g.ResetTransform();
                    g.ScaleTransform(1.0f, -1.0f, MatrixOrder.Append);
                    g.TranslateTransform(-xmin, -ymin, System.Drawing.Drawing2D.MatrixOrder.Append);
                    g.ScaleTransform(scale, -scale, System.Drawing.Drawing2D.MatrixOrder.Append);
                    g.TranslateTransform(xoff, h - yoff, System.Drawing.Drawing2D.MatrixOrder.Append);

                    // Draw
                    g.DrawString(text.Text, font, brush, text.X + txoff, -text.Y - tyoff - size.Height);
                    //g.DrawRectangle(Pens.Green, text.X + txoff, -text.Y - tyoff - size.Height, size.Width, size.Height);
                }
            }
        }

        private RectangleF GetTextBounds(Graphics graphics, string text, Font font)
        {
            CharacterRange[] characterRanges = { new CharacterRange(0, text.Length) };
            StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
            stringFormat.SetMeasurableCharacterRanges(characterRanges);

            Region region = graphics.MeasureCharacterRanges(text, font, new RectangleF(0, 0, float.MaxValue, float.MaxValue), stringFormat)[0];
            return region.GetBounds(graphics);
        }
    }
}
