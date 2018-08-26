using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorDot
{
    public partial class ColorDotForm : Form
    {        public ColorDotForm()
        {
            InitializeComponent();
        }
        
        private void ColorDotForm_Load(object sender, EventArgs e)
        {
            LoadColorBar();
        }

        private int hue(int index)
        {
            int h = 255 - (index * 360 / 100) + 360;
            return (h) % 360; 
        }

        private double saturation(int index)
        {
            return Math.Min(1.0, (float)index/100*0.2 + 0.6);
        }

        private double value(int index)
        {
            return Math.Min(1.0,index*0.01+0.7);
        }
        private void LoadColorBar()
        {
            picColorReference.Load("Depth_LEVEL.PNG");

            /*
            Bitmap bmp = new Bitmap(picColorReference.Width, picColorReference.Height);
            Graphics g = Graphics.FromImage(bmp);

            for (int index = 0; index < 100; index++)
            {
                Color color = ColorFromHSV(hue(index), saturation(index), value(index)); //Color.FromArgb(255,index, index, index);
                SolidBrush brush = new SolidBrush(color);
                g.FillRectangle(brush, new Rectangle(index*2, 0, (index+1)*2, bmp.Height));
            }
            picColorReference.Image = bmp;
            */
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Bitmap reference = new Bitmap(picColorReference.Image);
            String phpFileName = "level.php";

            if (File.Exists(phpFileName))
            {
                File.Delete(phpFileName);
            }
            StreamWriter phpStream = File.AppendText(phpFileName);

            for (int y=0; y<10; y++)
            {
                for (int x=0; x<10; x++)
                {

                    int level = x + y * 10;
                    int index = (int)((float)(level) / 99 * (reference.Width - 1));
                    int dotSize = 6; // (int)((float)level*level / 10000 * 15 + 6);

                    PictureBox picDot = new PictureBox();
                    picDot.Size = new Size(dotSize, dotSize);
                    picDot.Location = new Point(picColorReference.Location.X + x * 20, label2.Location.Y + y * 20);

                    Bitmap bmp = new Bitmap(dotSize,dotSize);
                    Graphics g = Graphics.FromImage(bmp);

                    Color color = reference.GetPixel(index, reference.Height/2);
                    SolidBrush brush = new SolidBrush(color);
                    g.FillEllipse(brush, new Rectangle(0,0,dotSize,dotSize));
                    picDot.Image = bmp;

                    String subPath = "dot\\";
                    String filename = String.Format("dot-level-{0}.png",level);
                    if (!Directory.Exists(subPath))
                    {
                        Directory.CreateDirectory(subPath);
                    }
                    picDot.Image.Save(subPath + filename);
                    String msg = "      level_" + level.ToString() + ": { icon: 'images/'" + filename + "' },";
                    phpStream.WriteLine(msg);

                    this.Controls.Add(picDot);
                }
            }
            phpStream.Close();
        }

        private void picColorReference_MouseMove(object sender, MouseEventArgs e)
        {
            statusMessage.Text = String.Format("index={0}", e.X/2);
        }
    }
}
