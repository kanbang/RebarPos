using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RebarPosCommands
{
    public partial class RequestLicenseForm : Form
    {
        public string ActivationCode { get { return txtActivationCode.Text; } set { txtActivationCode.Text = value; } }
        public string LicenseKey { get { return txtLicenseKey.Text; } set { txtLicenseKey.Text = value; } }

        public RequestLicenseForm()
        {
            InitializeComponent();

            dlgIcon.Image = SystemIcons.Information.ToBitmap();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtActivationCode.Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
