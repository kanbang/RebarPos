using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeLicense
{
    public partial class MakeLicenseForm : Form
    {
        private static string Secret = "cpql8gzmi5pp";

        public MakeLicenseForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) txtActivationCode.Text = Clipboard.GetText();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtLicenseKey.Text);
        }

        private void btnCreateLicenseKey_Click(object sender, EventArgs e)
        {
            // MD5 hash of activation code: 33 characters
            string activationCode = txtActivationCode.Text.Replace("-", "").Trim();
            // Checksum
            string crc = Crypto.GetMd5Hash(activationCode.Substring(0, 32).ToUpper()).Substring(0, 1).ToUpper();
            if (string.Compare(crc, activationCode.Substring(32, 1), StringComparison.OrdinalIgnoreCase) != 0)
            {
                MessageBox.Show("Geçersiz aktivasyon kodu.", "Lisans Yöneticisi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Last use date: "YYYYMMDDhhmmss" 14 chars    
            string lastUsed = DateToString(DateTime.MinValue);
            // Expires date: "YYYYMMDDhhmmss" 14 chars    
            string expires = DateToString(DateTime.Now.AddDays((double)(udTimeLimit.Value == 0 ? 3650 : udTimeLimit.Value)));

            txtLicenseKey.Text = Crypto.Encrypt(activationCode + lastUsed + expires, Secret);
        }

        // Converts DateTime to YYYYMMDDhhmmss string
        private static string DateToString(DateTime date)
        {
            return string.Format("{0:yyyy}{0:MM}{0:dd}{0:hh}{0:mm}{0:ss}", date);
        }

        // Formats activation code for display
        public static string FormatActivationCode(string code)
        {
            return string.Join("-", SplitByLength(code, 4));
        }

        private static IEnumerable<string> SplitByLength(string str, int maxLength)
        {
            for (int index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }
    }
}
