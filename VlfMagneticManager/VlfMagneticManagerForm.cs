using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VlfMagneticManager
{
    public partial class VlfMagneticManagerForm : Form
    {
        String ConfigFileName = "VlfMagneticManager.cfg";
        String[] sourceUrl = new string[] {
            "http://nsdp-3.cwb.gov.tw/aftershock/showeq/mag_tid/Mag_ULF_space/",
            "http://nsdp-3.cwb.gov.tw/aftershock/showeq/mag_tid/Mag_ULF_space_old/" };
        String[] defaultFileDir = new String[] {
            @"E:\RMT Processing\ULF Magnetic",
            @"E:\RMT Processing\ULF Magnetic_Old"
            };
        public VlfMagneticManagerForm()
        {
            InitializeComponent();
        }
        
        private void VlfMagneticManagerForm_Load(object sender, EventArgs e)
        {
            startDate.Value = new DateTime(2015, 1, 1);
            stopDate.Value = DateTime.Today;
            cbSourceUrl.Items.Add(sourceUrl[0]);
            cbSourceUrl.Items.Add(sourceUrl[1]);
            cbSourceUrl.Text = sourceUrl[0];

            LoadConfigFile();
        }

        #region Config File
        public bool LoadConfigFile()
        {
            StreamReader ConfigFileStream;

            statusMessage.Text = "Load Config File";

            if (!File.Exists(ConfigFileName))
            {
                SaveConfigFile();
                return false;
            }
            ConfigFileStream = new StreamReader(ConfigFileName);

            String Line;
            while ((Line = ConfigFileStream.ReadLine()) != null)
            {
                if (Line.Contains("sourceUrl="))
                {
                    cbSourceUrl.Text = Line.Substring("sourceUrl=".Length);
                }
                else if (Line.Contains("stopDate="))
                {
                    startDate.Value = Convert.ToDateTime(Line.Substring("stopDate=".Length));
                }
                else
                {
                    MessageBox.Show("Config 設定錯誤:\n" + Line);
                    return false;
                }
            }
            ConfigFileStream.Close();
            
            return true;
        }
        public void SaveConfigFile()
        {
            statusMessage.Text = "Save Config File";
            StreamWriter ConfigFileStream = new StreamWriter(ConfigFileName);

            ConfigFileStream.WriteLine(String.Format("sourceUrl={0}", cbSourceUrl.Text));
            ConfigFileStream.WriteLine(String.Format("stopDate={0}", stopDate.Value.ToShortDateString()));

            ConfigFileStream.Close();
            
        }


        delegate void updateStatusMessageHandler(string text);
        private void updateStatusMessage(string str)
        {
            statusMessage.Text = str;
        }

        #endregion

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DateTime downloadDate = new DateTime();

            if (!Directory.Exists(tbSaveFileDir.Text))
            {
                Directory.CreateDirectory(tbSaveFileDir.Text);
            }

            statusMessage.Text = "start download";
            for (downloadDate = startDate.Value; downloadDate < stopDate.Value; downloadDate = downloadDate.AddDays(1))
            {
                statusMessage.Text = "download " + downloadDate.ToString("yyyy-MM-dd");
                Thread  thread = new Thread(() => downloadThread(downloadDate, tbSaveFileDir.Text));
                thread.Start();
                thread.Join();
                if (!downloadResult)
                {
                    statusMessage.Text = "download " + downloadDate.ToString("yyyy-MM-dd") + " Fail";
                    break;
                }
            }
            statusMessage.Text = "download complete";
        }

        bool downloadResult;
        void downloadThread(DateTime downloadDate, String destRootDir)
        {

            String fileName = downloadDate.ToString("yyyyMMdd") + ".jpg";
            String destFileDir = destRootDir + "\\" + downloadDate.ToString("yyyyMM");
            String destFilePath = destFileDir + "\\" + fileName;
            String imgUrl = cbSourceUrl.Text + fileName;

            downloadResult = false;
            try
            {
                if (!Directory.Exists(destFileDir))
                {
                    Directory.CreateDirectory(destFileDir);
                }

                //begin download file
                WebRequest req = WebRequest.Create(imgUrl);
                req.Timeout = 10000; //10000ms
                WebResponse response = req.GetResponse();
                System.IO.Stream stream = response.GetResponseStream();
                Image img = Image.FromStream(stream);
                stream.Close();

                if (img != null)
                {
                    img.Save(destFilePath);
                }
                downloadResult = true;
            }
            catch
            {
                downloadResult = false;
            }
        }

        private void btnSaveDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbSaveFileDir.Text = dlg.SelectedPath;
            }
        }

        private void cbSourceUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSaveFileDir.Text = defaultFileDir[cbSourceUrl.SelectedIndex];
        }
    }
}
