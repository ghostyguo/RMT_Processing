using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMTProcessing
{
    public partial class DataSetupForm : Form
    {
        public static DateTime startDate, endDate;
        public static String rootPath;
        public DataSetupForm(String path)
        {
            InitializeComponent();
            startDate = datePickerStart.Value;
            endDate = datePickerEnd.Value;
            rootPath =path;
        }
        private void RevertHistoryDataForm_Load(object sender, EventArgs e)
        {
            tbFileRoot.Text = rootPath;
        }
        public void setMode(String mode)
        {
            tbMode.Text = mode;
        }
        private void btnBtnRootPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbFileRoot.Text = dlg.SelectedPath;
            }
        }
        private void btnAbort_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void chkSameAsStartDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameAsStartDate.Checked)
            {
                datePickerEnd.Value = datePickerStart.Value;
                datePickerEnd.Enabled = false;
            }
            else
            {
                datePickerEnd.Enabled = true;
            }
        }

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            if (chkSameAsStartDate.Checked)
            {
                datePickerEnd.Value = datePickerStart.Value;
            }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {
            startDate = datePickerStart.Value;
            endDate = datePickerEnd.Value;
            rootPath = tbFileRoot.Text;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
