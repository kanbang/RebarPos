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

            public DrawText(Color color, float x, float y, float height, string text)
            {
                Color = color;
                X = x;
                Y = y;
                Height = height;
                Text = text;
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

        public void AddText(Color color, float x, float y, float height, string text)
        {
            texts.Add(new DrawText(color, x, y, height, text));
            Refresh();
        }

        private void PosShapeView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
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

            // Shape name
            if (m_ShowName)
            {
                using (Brush brush = new SolidBrush(ForeColor))
                {
                    g.DrawString(m_Name, Font, brush, 5, 8);
                }
            }

            bool hasEnt = false;
            // Calculate extents
            float xmin = float.MaxValue, xmax = float.MinValue, ymin = float.MaxValue, ymax = float.MinValue;
            foreach (DrawLine line in lines)
            {
                hasEnt = true;
                xmin = Math.Min(xmin, Math.Min(line.X1, line.X2));
                xmax = Math.Max(xmax, Math.Max(line.X1, line.X2));
                ymin = Math.Min(ymin, Math.Min(line.Y1, line.Y2));
                ymax = Math.Max(ymax, Math.Max(line.Y1, line.Y2));
            }
            foreach (DrawArc arc in arcs)
            {
                hasEnt = true;
                xmin = Math.Min(xmin, arc.X - arc.R);
                xmax = Math.Max(xmax, arc.X + arc.R);
                ymin = Math.Min(ymin, arc.Y - arc.R);
                ymax = Math.Max(ymax, arc.Y + arc.R);
            }
            foreach (DrawText text in texts)
            {
                hasEnt = true;
                xmin = Math.Min(xmin, text.X - text.Height);
                xmax = Math.Max(xmax, text.X + text.Height);
                ymin = Math.Min(ymin, text.Y - text.Height);
                ymax = Math.Max(ymax, text.Y + text.Height);
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

            // Draw shapes
            foreach (DrawLine line in lines)
            {
                using (Pen pen = new Pen(line.Color, 1.0f))
                {
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(pen, (float)line.X1, (float)line.Y1, (float)line.X2, (float)line.Y2);
                }
            }
            foreach (DrawArc arc in arcs)
            {
                using (Pen pen = new Pen(arc.Color, 1.0f))
                {
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawArc(pen, (float)arc.X, (float)arc.Y, 2.0f * (float)arc.R, 2.0f * (float)arc.R, (float)arc.StartAngle, (float)arc.EndAngle);
                }
            }
            foreach (DrawText text in texts)
            {
                using (Brush brush = new SolidBrush(text.Color))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    // Save state
                    GraphicsState state = g.Save();

                    // Draw
                    g.ResetTransform();
                    g.ScaleTransform(1.0f / scale, 1.0f / scale, MatrixOrder.Append);
                    g.RotateTransform(180, MatrixOrder.Append);
                    g.TranslateTransform((float)text.X, (float)text.Y, MatrixOrder.Append);
                    g.TranslateTransform(-xmin, -ymin, System.Drawing.Drawing2D.MatrixOrder.Append);
                    g.ScaleTransform(scale, -scale, System.Drawing.Drawing2D.MatrixOrder.Append);
                    g.TranslateTransform(xoff, h - yoff, System.Drawing.Drawing2D.MatrixOrder.Append);
                    g.DrawString(text.Text, Font, brush, 0.0f, 0.0f, format);

                    // Restore state
                    g.Restore(state);
                }
            }
        }
    }
}
