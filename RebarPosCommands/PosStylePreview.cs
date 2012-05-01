using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RebarPosCommands
{
    public partial class PosStylePreview : UserControl
    {
        public Color TextColor { get; set; }
        public Color PosColor { get; set; }
        public Color CircleColor { get; set; }
        public Color MultiplierColor { get; set; }
        public Color GroupColor { get; set; }
        public Color NoteColor { get; set; }
        public Color CurrentGroupHighlightColor { get; set; }

        private enum PartType
        {
            None = 0,
            Pos = 1,
            Count = 2,
            Diameter = 3,
            Spacing = 4,
            Group = 5,
            Multiplier = 6,
            Length = 7,
            Note = 8,
        }

        private class DrawParams
        {
            public PartType type;
            public string text;
            public bool hasCircle;
            public float x;
            public float y;
            public float w;
            public float h;

            public DrawParams(PartType _type, string _text)
            {
                type = _type;
                text = _text;
            }
        }

        string mPos, mCount, mDiameter, mSpacing, mLength;
        string mFormula1, mFormula2, mFormula3;

        public PosStylePreview()
        {
            InitializeComponent();

            mPos = "1"; ;
            mCount = "2x4";
            mDiameter = "16";
            mSpacing = "200";
            mLength = "2400";

            mFormula1 = "[M:C][N][\"T\":D][\"/\":S][\" L=\":L]";
            mFormula2 = "[M:C][N][\"T\":D][\"/\":S]";
            mFormula3 = "[M:C]";

            TextColor = Color.Red;
            PosColor = Color.Red;
            CircleColor = Color.Yellow;
            MultiplierColor = Color.Gray;
            GroupColor = Color.Gray;
            NoteColor = Color.Orange;
            CurrentGroupHighlightColor = Color.Silver;
        }

        public void SetPos(string pos, string count, string diameter, string spacing, string length)
        {
            mPos = pos;
            mCount = count;
            mDiameter = diameter;
            mSpacing = spacing;
            mLength = length;

            Refresh();
        }

        public void SetFormula(string formula1, string formula2, string formula3)
        {
            mFormula1 = formula1;
            mFormula2 = formula2;
            mFormula3 = formula3;

            Refresh();
        }

        private void PosStylePreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackColor);

            List<DrawParams> list1 = new List<DrawParams>();
            List<DrawParams> list2 = new List<DrawParams>();
            List<DrawParams> list3 = new List<DrawParams>();
            if (!string.IsNullOrEmpty(mFormula1)) list1 = ParseFormula(mFormula1);
            if (!string.IsNullOrEmpty(mFormula2)) list2 = ParseFormula(mFormula2);
            if (!string.IsNullOrEmpty(mFormula3)) list3 = ParseFormula(mFormula3);

            float xmin = 0.0f, xmax = float.MinValue, ymin = 0.0f, ymax = float.MinValue;
            float circleRadius = 1.125f;
            float partSpacing = 0.15f;
            float x = 0;
            float y = 0;
            // Measure
            using (Font font = new Font(Font.FontFamily, 1.0f, GraphicsUnit.World))
            {
                x = 0;
                for (int i = 0; i < list1.Count; i++)
                {
                    DrawParams p = list1[i];
                    SizeF ext;
                    if (p.hasCircle)
                    {
                        ext = g.MeasureString(p.text, font);
                        p.x = x + (2.0f * circleRadius - ext.Width) / 2.0f;
                    }
                    else
                    {
                        ext = g.MeasureString(p.text, font);
                        p.x = x;
                    }
                    p.y = y;
                    p.w = ext.Width;
                    p.h = ext.Height;
                    if (p.hasCircle)
                    {
                        x += 2.0f * circleRadius + partSpacing;
                    }
                    else
                    {
                        x += ext.Width + partSpacing;
                    }
                    if (i == 0)
                        y += ext.Height + 2.0f * partSpacing;
                    xmax = Math.Max(p.x + p.w, xmax);
                    ymax = Math.Max(p.y + p.h, ymax);
                }
                x = 0;
                for (int i = 0; i < list2.Count; i++)
                {
                    DrawParams p = list2[i];
                    SizeF ext;
                    if (p.hasCircle)
                    {
                        ext = g.MeasureString(p.text, font);
                        p.x = x + (2.0f * circleRadius - ext.Width) / 2.0f;
                    }
                    else
                    {
                        ext = g.MeasureString(p.text, font);
                        p.x = x;
                    }
                    p.y = y;
                    p.w = ext.Width;
                    p.h = ext.Height;
                    if (p.hasCircle)
                    {
                        x += 2.0f * circleRadius + partSpacing;
                    }
                    else
                    {
                        x += ext.Width + partSpacing;
                    }
                    if (i == 0)
                        y += ext.Height + 2.0f * partSpacing;
                    xmax = Math.Max(p.x + p.w, xmax);
                    ymax = Math.Max(p.y + p.h, ymax);
                }
                x = 0;
                for (int i = 0; i < list3.Count; i++)
                {
                    DrawParams p = list3[i];
                    SizeF ext;
                    if (p.hasCircle)
                    {
                        ext = g.MeasureString(p.text, font);
                        p.x = x + (2.0f * circleRadius - ext.Width) / 2.0f;
                    }
                    else
                    {
                        ext = g.MeasureString(p.text, font);
                        p.x = x;
                    }
                    p.y = y;
                    p.w = ext.Width;
                    p.h = ext.Height;
                    if (p.hasCircle)
                    {
                        x += 2.0f * circleRadius + partSpacing;
                    }
                    else
                    {
                        x += ext.Width + partSpacing;
                    }
                    if (i == 0)
                        y += ext.Height + 2.0f * partSpacing;
                    xmax = Math.Max(p.x + p.w, xmax);
                    ymax = Math.Max(p.y + p.h, ymax);
                }
            }

            // Calculate scale
            float w = (float)ClientRectangle.Width;
            float h = (float)ClientRectangle.Height;

            float xscale = 0.9f * w / (xmax - xmin);
            float yscale = 0.9f * h / (ymax - ymin);
            float scale = Math.Min(xscale, yscale);

            // Client offsets
            float xoff = (w - scale * (xmax - xmin)) / 2.0f;
            float yoff = (h - scale * (ymax - ymin)) / 2.0f;

            // Transform
            g.ResetTransform();
            g.ScaleTransform(scale, scale, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.TranslateTransform(xoff, yoff, System.Drawing.Drawing2D.MatrixOrder.Append);

            // Draw
            List<DrawParams> list = new List<DrawParams>();
            list.AddRange(list1);
            list.AddRange(list2);
            list.AddRange(list3);
            foreach (DrawParams p in list)
            {
                Color col = TextColor;
                switch (p.type)
                {
                    case PartType.Pos:
                        col = PosColor;
                        break;
                    case PartType.Group:
                        col = GroupColor;
                        break;
                    case PartType.Multiplier:
                        col = MultiplierColor;
                        break;
                    case PartType.Note:
                        col = NoteColor;
                        break;
                }
                using (Brush brush = new SolidBrush(col))
                using (Font font = new Font(Font.FontFamily, scale, GraphicsUnit.World))
                {
                    g.DrawString(p.text, Font, brush, p.x, p.y);
                }
                if (p.hasCircle)
                {
                    using (Pen pen = new Pen(CircleColor, 0.0f))
                    {
                        g.DrawArc(pen, p.x + p.w / 2.0f - circleRadius, p.y + p.h / 2.0f + circleRadius, circleRadius * 2.0f, circleRadius * 2.0f, 0.0f, 360.0f);
                    }
                }
            }
        }

        private List<DrawParams> ParseFormula(string str)
        {
            List<DrawParams> list = new List<DrawParams>();

            // First pass: separate parts
            string part = string.Empty;
            bool indeco = false;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if ((!indeco && c == '[') || (indeco && (c == ']')) || (i == str.Length - 1))
                {
                    if ((i == str.Length - 1) && (c != '[') && (c != ']'))
                    {
                        part += c;
                    }
                    if (!indeco && !string.IsNullOrEmpty(part))
                    {
                        DrawParams p = new DrawParams(PartType.None, part);
                        list.Add(p);
                    }
                    else if (indeco && !string.IsNullOrEmpty(part))
                    {
                        DrawParams p = new DrawParams(PartType.Pos, part);
                        list.Add(p);
                    }
                    indeco = (c == '[');

                    part = string.Empty;
                }
                else
                {
                    part += c;
                }
            }

            // Second pass: separate format strings and decorators
            List<DrawParams> finallist = new List<DrawParams>();
            for (int j = 0; j < list.Count; j++)
            {
                DrawParams p = list[j];
                if (p.type == PartType.None)
                {
                    p.type = PartType.None;
                    p.hasCircle = false;
                    finallist.Add(p);
                }
                else
                {
                    p.type = PartType.None;
                    part = string.Empty;
                    List<string> parts = new List<string>();
                    bool inliteral = false;
                    for (int i = 0; i < p.text.Length; i++)
                    {
                        char c = p.text[i];
                        if (!inliteral && (c == '"'))
                        {
                            inliteral = true;
                        }
                        else if (inliteral && (c == '"'))
                        {
                            parts.Add(part);
                            part = string.Empty;
                            inliteral = false;
                        }
                        else if (c == ':' || (i == p.text.Length - 1))
                        {
                            if (i == p.text.Length - 1)
                            {
                                part += c;
                            }
                            if (part == "M")
                            {
                                p.type = PartType.Pos;
                                string pos = mPos;
                                if (pos.Length == 0) pos = " ";
                                parts.Add(pos);
                            }
                            else if (part == "MM")
                            {
                                p.type = PartType.Pos;
                                string pos = mPos;
                                if (pos.Length == 0) pos = "  ";
                                if (pos.Length == 1) pos = "0" + pos;
                                parts.Add(pos);
                            }
                            else if (part == "N" && !string.IsNullOrEmpty(mCount) && mCount.Length > 0)
                            {
                                p.type = PartType.Count;
                                parts.Add(mCount);
                            }
                            else if (part == "D" && !string.IsNullOrEmpty(mDiameter) && mDiameter.Length > 0)
                            {
                                p.type = PartType.Diameter;
                                parts.Add(mDiameter);
                            }
                            else if (part == "S" && !string.IsNullOrEmpty(mSpacing) && mSpacing.Length > 0)
                            {
                                p.type = PartType.Spacing;
                                parts.Add(mSpacing);
                            }
                            else if (part == "L" && !string.IsNullOrEmpty(mLength) && mLength.Length > 0)
                            {
                                p.type = PartType.Length;
                                parts.Add(mLength);
                            }
                            else if (part == "C")
                            {
                                p.hasCircle = true;
                            }
                            part = string.Empty;
                        }
                        else
                        {
                            part += c;
                        }
                    }
                    if (p.type != PartType.None)
                    {
                        p.text = string.Empty;
                        for (int k = 0; k < parts.Count; k++)
                        {
                            p.text += parts[k];
                        }
                        finallist.Add(p);
                    }
                }
            }
            return finallist;
        }
    }
}
