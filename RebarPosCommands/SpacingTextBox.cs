using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace RebarPosCommands
{
    public class SpacingTextBox : TextBox
    {
        [Browsable(true), DefaultValue(true)]
        public bool AllowEmpty { get; set; }

        public SpacingTextBox()
        {
            AllowEmpty = true;
        }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                string text = base.Text;
                if (string.IsNullOrEmpty(text))
                {
                    if (AllowEmpty)
                        return true;
                    else
                        return false;
                }

                string[] parts = text.Split(new char[] { '~', '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0)
                {
                    return true;
                }
                else if (parts.Length == 1)
                {
                    float num = 0;
                    if (float.TryParse(parts[0], out num))
                        return true;
                    else
                        return false;
                }
                else if (parts.Length == 2)
                {
                    float num = 0;
                    if (float.TryParse(parts[0], out num) && float.TryParse(parts[1], out num))
                        return true;
                    else
                        return false;
                }
                else // if (parts.Length > 2)
                {
                    return false;
                }
            }
        }

        [Browsable(false)]
        public string Text1
        {
            get
            {
                string text = base.Text;
                if (string.IsNullOrEmpty(text))
                    return "";

                if (!IsValid)
                    return "";

                string[] parts = text.Split(new char[] { '~', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0)
                {
                    return "";
                }
                else if (parts.Length == 1 || parts.Length == 2)
                {
                    float num = 0;
                    if (float.TryParse(parts[0], out num))
                        return num.ToString();
                    else
                        return "";
                }
                else // if (parts.Length > 2)
                {
                    return "";
                }
            }
        }

        [Browsable(false)]
        public string Text2
        {
            get
            {
                string text = base.Text;
                if (string.IsNullOrEmpty(text))
                    return "";

                if (!IsValid)
                    return "";

                string[] parts = text.Split(new char[] { '~', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0)
                {
                    return "";
                }
                else if (parts.Length == 2)
                {
                    float num = 0;
                    if (float.TryParse(parts[1], out num))
                        return num.ToString();
                    else
                        return "";
                }
                else // if (parts.Length != 2)
                {
                    return "";
                }
            }
        }

        [Browsable(false)]
        public float Value1
        {
            get
            {
                string text = Text1;
                if (string.IsNullOrEmpty(text)) return 0;

                float num = 0;
                if (float.TryParse(text, out num))
                    return num;
                else
                    return 0;
            }
        }

        [Browsable(false)]
        public float Value2
        {
            get
            {
                string text = Text2;
                if (string.IsNullOrEmpty(text)) return 0;

                float num = 0;
                if (float.TryParse(text, out num))
                    return num;
                else
                    return 0;
            }
        }
    }
}
