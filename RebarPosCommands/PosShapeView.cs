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
            public bool Visible;

            public DrawLine(Color color, float x1, float y1, float x2, float y2, bool visible)
            {
                Color = color;
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
                Visible = visible;
            }
        }
        private struct DrawArc
        {
            public Color Color;
            public float X, Y, R;
            public float StartAngle, EndAngle;
            public bool Visible;

            public DrawArc(Color color, float x, float y, float r, float startAngle, float endAngle, bool visible)
            {
                Color = color;
                X = x;
                Y = y;
                R = r;
                StartAngle = startAngle;
                EndAngle = endAngle;
                Visible = visible;
            }
        }
        private struct DrawText
        {
            public Color Color;
            public float X, Y, Height;
            public string Text;
            public StringAlignment HorizontalAlignment;
            public StringAlignment VerticalAlignment;
            public bool Visible;

            public DrawText(Color color, float x, float y, float height, string text, StringAlignment horizontalAlignment, StringAlignment verticalAlignment, bool visible)
            {
                Color = color;
                X = x;
                Y = y;
                Height = height;
                Text = text;
                HorizontalAlignment = horizontalAlignment;
                VerticalAlignment = verticalAlignment;
                Visible = visible;
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
            Refresh();
        }

        public void AddLine(Color color, float x1, float y1, float x2, float y2, bool visible)
        {
            lines.Add(new DrawLine(color, x1, y1, x2, y2, visible));
            Refresh();
        }

        public void AddArc(Color color, float x, float y, float r, float startAngle, float endAngle, bool visible)
        {
            arcs.Add(new DrawArc(color, x, y, r, startAngle, endAngle, visible));
            Refresh();
        }

        public void AddText(Color color, float x, float y, float height, string text, StringAlignment horizontalAlignment, StringAlignment verticalAlignment, bool visible)
        {
            texts.Add(new DrawText(color, x, y, height, text, horizontalAlignment, verticalAlignment, visible));
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
                dlines.Add(new DrawLine(Color.Red, 0, 0, 100, 0, true));
                dtexts.Add(new DrawText(Color.Yellow, 50, 2, 10, "A", StringAlignment.Center, StringAlignment.Near, true));
                dlines.Add(new DrawLine(Color.Red, 0, 0, 0, 20, true));
                dtexts.Add(new DrawText(Color.Yellow, -2, 10, 10, "B", StringAlignment.Far, StringAlignment.Center, true));
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
                float da = (arc.EndAngle - arc.StartAngle) / 10.0f;
                for (float a = arc.StartAngle; a <= arc.EndAngle; a += da)
                {
                    float x = arc.X + (float)Math.Cos(a * Math.PI / 180.0) * arc.R;
                    float y = arc.Y + (float)Math.Sin(a * Math.PI / 180.0) * arc.R;
                    xmin = Math.Min(xmin, x);
                    xmax = Math.Max(xmax, x);
                    ymin = Math.Min(ymin, y);
                    ymax = Math.Max(ymax, y);
                }
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
                    ymin = Math.Min(ymin, text.Y - bottom);
                    ymax = Math.Max(ymax, text.Y + top);
                }
            }
            if (!hasEnt)
                return;

            // Calculate scale
            float w = (float)ClientRectangle.Width;
            float h = (float)ClientRectangle.Height;
            float xscale = w / (xmax - xmin);
            float yscale = h / (ymax - ymin);
            float scale = Math.Min(xscale, yscale);
            // Client offsets
            float xoff = (w - scale * (xmax - xmin)) / 2.0f;
            float yoff = h - (h - scale * (ymax - ymin)) / 2.0f;

            // Transform
            g.ResetTransform();
            g.TranslateTransform(-xmin, -ymin, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.ScaleTransform(scale, -scale, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.TranslateTransform(xoff, yoff, System.Drawing.Drawing2D.MatrixOrder.Append);

            // Calculate conversion from world units to em-size
            float fontScale = 1.0f;
            using (Font font = new Font(Font.FontFamily, 1.0f))
            {
                fontScale = 1.0f / g.MeasureString("M", font).Height;
            }

            // Draw shapes
            foreach (DrawLine line in dlines)
            {
                if (line.Visible)
                {
                    using (Pen pen = new Pen(line.Color)) // Thick pen: 2.0f / scale
                    {
                        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        g.DrawLine(pen, line.X1, line.Y1, line.X2, line.Y2);
                    }
                }
            }
            foreach (DrawArc arc in darcs)
            {
                if (arc.Visible)
                {
                    using (Pen pen = new Pen(arc.Color)) // Thick pen: 2.0f / scale
                    {
                        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        float sweep = arc.EndAngle - arc.StartAngle;
                        while (sweep > 360.0f) sweep -= 360.0f;
                        while (sweep < 0.0f) sweep += 360.0f;
                        g.DrawArc(pen, arc.X - arc.R, arc.Y - arc.R, 2.0f * arc.R, 2.0f * arc.R, arc.StartAngle, sweep);
                    }
                }
            }

            foreach (DrawText text in dtexts)
            {
                if (text.Visible)
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
                        g.TranslateTransform(xoff, yoff, System.Drawing.Drawing2D.MatrixOrder.Append);

                        // Draw
                        g.DrawString(text.Text, font, brush, text.X + txoff, -text.Y - tyoff - size.Height);
                    }
                }
            }
        }
    }
}
