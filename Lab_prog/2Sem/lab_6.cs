using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Lab_6
{
    public partial class FireBall : Form
    {
        private Bitmap workBmp = null;
        private Random rnd = new Random(DateTime.Now.Millisecond);
        int viWidth = 640;
        int viHeight = 480;
        int fire_x = 100;
        int fire_y = 100;
        int size = 30;
        int R1 = 255; int G1 = 0; int B1 = 0;
        int R2 = 255; int G2 = 255; int B2 = 0;
        bool flag_start = false;
        bool first = true;
        bool f1 = true;
        bool f2 = true;
        public FireBall()
        {
            InitializeComponent();
            Size01.Text = size.ToString();
            hScrollBar1.Value = size;
        }
        private void vStartFlame()
        {
            workBmp = new Bitmap(viWidth, viHeight, PixelFormat.Format8bppIndexed);
            ColorPalette pal = workBmp.Palette;
            for (int i = 0; i < 64; i++)
            {
                pal.Entries[i] = Color.FromArgb(0, 0, 0); 
                pal.Entries[i + 64] = Color.FromArgb(R1, G1, B1); 
                pal.Entries[i + 128] = Color.FromArgb(R2, G2, B2);
                pal.Entries[i + 192] = Color.FromArgb(255, 255, 255);
            }
            workBmp.Palette = pal;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag_start)
            {
                fire_y = e.Y;
                fire_x = e.X - size / 2;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag_start)
            {
                unsafe
                {
                    int d = 80000;
                    if (fire_y + size / 2 >= 300 && fire_y + size / 2 < 445)
                    {
                        d = 20000;
                    }
                    else if (fire_y + size / 2 >= 445 - size / 2)
                    {
                        fire_y = 444 - size / 2;
                        d = -1000;
                    }
                    if (fire_x <= size / 4)
                    {
                        fire_x = size / 4;
                    }
                    else if ( fire_x >= viWidth - size)
                    {
                        fire_x = viWidth - size -2;
                    }
                    BitmapData bmpLook = workBmp.LockBits(
                    new Rectangle(Point.Empty, workBmp.Size),
                    ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                    Byte* pointtop = (Byte*)bmpLook.Scan0;
                    Byte* point_max = (Byte*)2500000000000;
                    Byte* pointbottom = pointtop + fire_x + ((fire_y + size / 2) * 640);
                    Byte* i = null;
                    Byte* last = (Byte*)0;
                    int j = 0;
                    int popr = 0;
                    int poprr = 0;
                    if ((viWidth - fire_x - size - size / 4) < 0)
                    {
                        popr = (viWidth - fire_x - size - size / 4);
                    }
                    else if ((fire_x - size / 4) < 0)
                    {
                        poprr = fire_x - size / 4;
                    }
                    for (int x = poprr; x < size ; x++)
                    {
                        *(pointbottom + x) = (Byte)rnd.Next(100, 255); //внутренний контур
                        *(pointtop + x) = (Byte)rnd.Next(0, 255);
                    }
                    for (i = pointtop; i < pointbottom + d; i++)
                    {
                        if (i < (Byte*)3210000000000)
                        {
                            if (i > pointtop + 2 && i < pointbottom - 2)
                            {
                                j = *(i - 1) + *(i - 2) + *(i + 1) + *(i + 2) +
                                        *(i + viWidth) * 5;
                                j /= 9;
                                if (j < 0) j = 0;
                                *i = (Byte)j;
                            }
                            if (i > pointbottom)
                            {
                                *i = (Byte)0;
                            }
                        }
                    }
                    workBmp.UnlockBits(bmpLook);
                    bmpLook = null;
                    Bitmap bmpA = new Bitmap(viWidth, viHeight);
                    bmpA = workBmp;
                    Bitmap bmpB = new Bitmap(viWidth, viHeight);
                    Graphics grtmpB = Graphics.FromImage(bmpB);
                    grtmpB.DrawImage(bmpA, 0, 0);
                    grtmpB.Dispose();
                    pictureBox1.Image = bmpB;
                    bmpB = null;
                    bmpA = null;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (flag_start)
            {
                flag_start = false;
                button3.Text = "Start";
            }
            else
            {
                flag_start = true;
                button3.Text = "Stop";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag_start = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            f2 = true;
            f1 = true;
            R.Text = "";
            B.Text = "";
            G.Text = "";
            pictureBox1.Image = null;
            vStartFlame();
            int fire_x = 100;
            int fire_y = 100;
            int size = 30;
            first = true;
        }

        private void Size01_TextChanged(object sender, EventArgs e)
        {
            bool res = int.TryParse(Size01.Text, out int xx);
            if (res == false)
            {
                MessageBox.Show("Вы ввели не число");
            }
            else
            {
                if (xx <= 0)
                {
                    MessageBox.Show("Вы ввели отрицательное число или 0");
                    Size01.Text = "";
                }
                else
                if (xx > 150)
                {
                    MessageBox.Show("Ширина не больше 150");
                }
                else
                {
                    size = xx;
                    Size01.Text = xx.ToString();
                    hScrollBar1.Value = xx;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!f1 || !f2)
            {
                parse_data(G.Text, "G");
            }
            else
            {
                if (G.Text != "")
                {
                    MessageBox.Show("У вас не выбранны параметры для типов пламени");
                }
                G.Text = "";
            }
        }

        public void parse_data(string text, string S)
        {
            bool res = int.TryParse(text, out int xx);
            if (text != "") { 
            if (res == false)
            {
                MessageBox.Show("Вы ввели не число");
            }
            else
            {
                    if (xx < 0)
                    {
                        MessageBox.Show("Вы ввели отрицательное число или 0");
                    }
                    else
                    if (xx > 255)
                    {
                        MessageBox.Show("Цвет не может быть ярче 255");
                    }
                    else
                    {
                        if (S == "R")
                        {
                            if (!f1)
                            {
                                R1 = xx;
                            }
                            else if (!f2)
                            {
                                R2 = xx;
                            }
                            R.Text = xx.ToString();
                        }
                        else if (S == "G")
                        {
                            if (!f1)
                            {
                                G1 = xx;
                            }
                            else if (!f2)
                            {
                                G2 = xx;
                            }
                            G.Text = xx.ToString();
                        }
                        else if (S == "B")
                        {
                            if (!f1)
                            {
                                B1 = xx;
                            }
                            else if (!f2)
                            {
                                B2 = xx;
                            }
                            B.Text = xx.ToString();
                        }
                    }
                }
            }
        }
        private void R_TextChanged(object sender, EventArgs e)
        {
            if (!f1 || !f2)
            {
                parse_data(R.Text, "R");
            }
            else
            {
                if (R.Text != "")
                {
                    MessageBox.Show("У вас не выбранны параметры для типов пламени");
                }
                R.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (f1 == false || f2 == false)
            {
                parse_data(B.Text, "B");
            }
            else
            {
                if (B.Text != "")
                {
                    MessageBox.Show("У вас не выбранны параметры для типов пламени");
                }
                B.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (f1)
            {
                f1 = false;
                R.Text = R1.ToString();
                B.Text = B1.ToString();
                G.Text = G1.ToString();
                
            }
            else
            {
                f1 = true;
                checkBox1.Checked = false;
                R.Text = "";
                B.Text = "";
                G.Text = "";
                
            }
            if (f1 == f2 && !f1)
            {
                MessageBox.Show("У вас выбранны параметры сразу для двух типов пламени, выберите только одно");
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                f2 = true;
                f1 = true;
                R.Text = "";
                B.Text = "";
                G.Text = "";
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Size01.Text = hScrollBar1.Value.ToString();
            size = hScrollBar1.Value;
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (first)
            {
                flag_start = true;
                vStartFlame();
                first = false;
            }
            else
            {
                if (!flag_start)
                {
                    flag_start = true;
                    button3.Text = "Stop";
                }
                else
                {
                    flag_start = false;
                    button3.Text = "Start";
                }
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (f2)
            {
                f2 = false;
                R.Text = R2.ToString();
                B.Text = B2.ToString();
                G.Text = G2.ToString();
            }
            else
            {
                f2 = true;
                checkBox2.Checked = false;
                R.Text = "";
                B.Text = "";
                G.Text = "";
            }
            if (f1 == f2 && !f1)
            {
                
                MessageBox.Show("У вас выбранны параметры сразу для двух типов пламени, выберите только одно");
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                f2 = true;
                f1 = true;
                R.Text = "";
                B.Text = "";
                G.Text = "";
                
            }
        }
    }
}
