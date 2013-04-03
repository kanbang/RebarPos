using System;
using System.Drawing;
using System.Windows.Forms;

namespace RebarPosCommands
{
    public partial class ConfimMessageBox : Form
    {
        public static DialogResult Show(string message, string caption, string confirmMessage, string registryKey, MessageBoxIcon icon, MessageBoxButtons buttons, DialogResult defaultResult)
        {
            string suppressDialog = (string)Microsoft.Win32.Registry.CurrentUser.GetValue(registryKey, "0");
            if (suppressDialog == "1")
            {
                return defaultResult;
            }

            using (ConfimMessageBox form = new ConfimMessageBox())
            {
                form.Text = caption;

                Bitmap bmp = SystemIcons.Error.ToBitmap();
                switch (icon)
                {
                    case MessageBoxIcon.Error: // Hand, Stop
                        bmp = SystemIcons.Error.ToBitmap();
                        break;
                    case MessageBoxIcon.Information: // Asterisk
                        bmp = SystemIcons.Information.ToBitmap();
                        break;
                    case MessageBoxIcon.Question:
                        bmp = SystemIcons.Question.ToBitmap();
                        break;
                    case MessageBoxIcon.Warning: // Exclamation
                        bmp = SystemIcons.Warning.ToBitmap();
                        break;
                }
                form.pbIcon.Image = bmp;

                form.lblMessage.Left = form.pbIcon.Left + bmp.Width + 7;
                form.lblMessage.MaximumSize = new Size(496 - bmp.Width, 0);
                form.lblMessage.Text = message;
                form.chkConfirm.Left = form.lblMessage.Left;

                form.Height = Math.Max(bmp.Height, form.lblMessage.Height) + 134;

                int buttonCount = 1;
                switch (buttons)
                {
                    case MessageBoxButtons.AbortRetryIgnore:
                        form.button1.Text = "Abort";
                        form.button1.DialogResult = DialogResult.Abort;
                        form.button2.Text = "Retry";
                        form.button2.DialogResult = DialogResult.Retry;
                        form.button3.Text = "Ignore";
                        form.button3.DialogResult = DialogResult.Ignore;
                        form.AcceptButton = form.button2;
                        form.CancelButton = form.button1;
                        buttonCount = 3;
                        break;
                    case MessageBoxButtons.OK:
                        form.button1.Text = "OK";
                        form.button1.DialogResult = DialogResult.OK;
                        form.AcceptButton = form.button1;
                        form.CancelButton = form.button1;
                        buttonCount = 1;
                        break;
                    case MessageBoxButtons.OKCancel:
                        form.button1.Text = "OK";
                        form.button1.DialogResult = DialogResult.OK;
                        form.button2.Text = "Cancel";
                        form.button2.DialogResult = DialogResult.Cancel;
                        form.AcceptButton = form.button1;
                        form.CancelButton = form.button2;
                        buttonCount = 2;
                        break;
                    case MessageBoxButtons.RetryCancel:
                        form.button1.Text = "Retry";
                        form.button1.DialogResult = DialogResult.Retry;
                        form.button2.Text = "Cancel";
                        form.button2.DialogResult = DialogResult.Cancel;
                        form.AcceptButton = form.button1;
                        form.CancelButton = form.button2;
                        buttonCount = 2;
                        break;
                    case MessageBoxButtons.YesNo:
                        form.button1.Text = "Yes";
                        form.button1.DialogResult = DialogResult.Yes;
                        form.button2.Text = "No";
                        form.button2.DialogResult = DialogResult.No;
                        form.AcceptButton = form.button1;
                        form.CancelButton = form.button2;
                        buttonCount = 2;
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        form.button1.Text = "Yes";
                        form.button1.DialogResult = DialogResult.Yes;
                        form.button2.Text = "No";
                        form.button2.DialogResult = DialogResult.No;
                        form.button3.Text = "Cancel";
                        form.button3.DialogResult = DialogResult.Cancel;
                        form.AcceptButton = form.button1;
                        form.CancelButton = form.button3;
                        buttonCount = 3;
                        break;
                }
                if (buttonCount == 1)
                {
                    form.button2.Visible = false;
                    form.button3.Visible = false;
                    form.button1.Left = (form.ClientRectangle.Width - form.button1.Width) / 2;
                }
                else if (buttonCount == 2)
                {
                    form.button3.Visible = false;
                    form.button1.Left = (form.ClientRectangle.Width - form.button1.Width - form.button2.Width - 6) / 2;
                    form.button2.Left = form.button1.Left + form.button1.Width + 6;
                }
                else
                {
                    form.button1.Left = (form.ClientRectangle.Width - form.button1.Width - form.button2.Width - form.button3.Width - 2 * 6) / 2;
                    form.button2.Left = form.button1.Left + form.button1.Width + 6;
                    form.button3.Left = form.button2.Left + form.button2.Width + 6;
                }

                form.chkConfirm.Text = confirmMessage;

                DialogResult result = form.ShowDialog();
                if (form.chkConfirm.Checked)
                {
                    Microsoft.Win32.Registry.CurrentUser.SetValue(registryKey, "1");
                }

                return result;
            }
        }

        public static void ResetDialog(string registryKey)
        {
            Microsoft.Win32.Registry.CurrentUser.SetValue(registryKey, "0");
        }

        public ConfimMessageBox()
        {
            InitializeComponent();
        }
    }
}
