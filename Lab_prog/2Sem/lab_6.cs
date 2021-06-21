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

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Bitmap workBmp = null;
        private Random rnd = new Random(DateTime.Now.Millisecond);
        int viWidth = 640;
        int viHeight = 480;
        int fire_x = 100;
        int fire_y = 300;
        int size = 30;
        bool flag_start = false;
        bool first = true;
        public Form1()
        {
            InitializeComponent();
            //vStartFlame();
            Size01.Text = size.ToString();
            hScrollBar1.Value = size;

        }
        private void vStartFlame()
        {
            workBmp = new Bitmap(viWidth, viHeight, PixelFormat.Format8bppIndexed);
            ColorPalette pal = workBmp.Palette;
            for (int i = 0; i < 64; i++)
            {
                pal.Entries[i] = Color.FromArgb(i << 2, 0, 0); //заливка сзади
                pal.Entries[i + 64] = Color.FromArgb(255, 0, 0); //внешний контур
                pal.Entries[i + 128] = Color.FromArgb(255, 255, 0); // внутренний огонек
                //Попытка увеличить белую составляющую
                pal.Entries[i + 192] = Color.FromArgb(255, 255, 255); // белые блики снизу
            }
            workBmp.Palette = pal;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag_start)
            {
                fire_y = e.Y;
                fire_x = e.X - size/2;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Прямая работа с памятью - не забудем в свойствах проекта на 
            //закладке Build поставить галочку Allow unsafe code.
            if (flag_start)
            {
                unsafe
                {
                    Debug.Text = fire_y.ToString();
                    if (fire_y < 8000)
                    {
                        BitmapData bmpLook = workBmp.LockBits(
                        new Rectangle(Point.Empty, workBmp.Size),
                        ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                        //bufdata - указатель на левую верхнюю точку рисунка
                        Byte* pointtop = (Byte*)bmpLook.Scan0;
                        //pointbottom - указатель на точку начала нижней линии рисунка
                        Byte* pointbottom = pointtop + fire_x + ((fire_y + size / 2) * 640);
                        //Byte* pointbottom = pointtop + ((viHeight-50) * viWidth);
                        Byte* i = null;
                        int j = 0;
                        int popr = 0;
                        int poprr = 0;
                        //Заполняем нижнюю линию точками, со случайно выбранными цветами
                        if ((viWidth - fire_x - size - size / 4) < 0)
                        {
                            popr = (viWidth - fire_x - size - size / 4);
                            //Debug.Text = popr.ToString();
                        }
                        else if ((fire_x - size / 4) < 0)
                        {
                            poprr = fire_x - size / 4;
                        }
                        //Debug.Text = (fire_x).ToString();
                        for (int x = 0 - poprr; x < size + popr; x++)
                        {
                            *(pointbottom + x) = (Byte)rnd.Next(100, 255);
                            *(pointtop + x) = (Byte)rnd.Next(100, 255);
                        }
                        //Заполняем остальные точки, значениями усредненными по алгоритму
                        for (i = pointtop; i < pointbottom+ 10000; i++)
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

                        //Разблокируем память
                        workBmp.UnlockBits(bmpLook);
                        bmpLook = null;
                        //Два вспомогательных Bitmap для переноса изображения 
                        //в PictureBox и для удаления ~ 4 pix из нижней части
                        //Убирает нежелательный мусор при растяжении рисунка в 
                        //PictureBox
                        Bitmap bmpA = new Bitmap(viWidth, viHeight);
                        bmpA = workBmp;
                        Bitmap bmpB = new Bitmap(viWidth, viHeight);
                        Graphics grtmpB = Graphics.FromImage(bmpB);
                        //Graphics grtmpB = Graphics.FromImage(bmpA);
                        grtmpB.DrawImage(bmpA, 0, 0);
                        grtmpB.Dispose();
                        pictureBox1.Image = bmpB;
                        bmpB = null;
                        bmpA = null;
                    }
                }
            }
        }
        int r1, r2, g1, g2,  b1, b2;

        private void button3_Click(object sender, EventArgs e)
        {
            // Stop button
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
            // Refresh button
            flag_start = false;
            pictureBox1.Image = null;
            vStartFlame();
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
                if (xx > 250)
                {
                    MessageBox.Show("Ширина не больше 250");
                }
                else
                {
                    size = xx;
                    Size01.Text = xx.ToString();
                    hScrollBar1.Value = xx;
                }
            }
           }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Size01.Text = hScrollBar1.Value.ToString();
            size = hScrollBar1.Value;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        int r01, r02, g01, g02, b01, b02;

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (first)
            {
                flag_start = true;
                vStartFlame();
                first = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
    }

