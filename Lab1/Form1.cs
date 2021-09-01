using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        
        Bitmap bm;
        Graphics g;
        Color color = new Color();

        int oldX, oldY, newX, newY, calcX, calcY, x, y;
        bool paint = false;
        Pen pen = new Pen(Color.Black);
        int buttonNum = 1;
        int blueVal, greenVal, redVal;

        private void btn_rec_Click(object sender, EventArgs e)
        {
            buttonNum = 2;
        }

        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            buttonNum = 3;
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            buttonNum = 1;
        }

        private void greenTrackBar_Scroll(object sender, EventArgs e)
        {
            greenVal = greenTrackBar.Value;
        }

        private void blueTrackBar_Scroll(object sender, EventArgs e)
        {
            blueVal = blueTrackBar.Value;
        }

        private void redTrackBar_Scroll(object sender, EventArgs e)
        {
            redVal = redTrackBar.Value;
        }

        

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            
            paint = true;
            oldX = e.X;
            oldY = e.Y;

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        { 
            
            paint = false;


            

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            g = panel1.CreateGraphics();
            newX = e.X;
            newY = e.Y;

            calcX = Math.Abs(newX - oldX);
            calcY = Math.Abs(oldY - newY);

            pen.Color = Color.FromArgb(redVal, greenVal, blueVal);

            if (paint)
            {
                if (buttonNum == 1)
                {
                    g.DrawLine(pen, oldX, oldY, newX, newY);

                }

                if (buttonNum == 2)
                {
                    g.DrawRectangle(pen, oldX, oldY, calcX, calcY);
                }

                if (buttonNum == 3)
                {
                    g.DrawEllipse(pen, oldX, oldY, calcX, calcY);
                }

            }
            
           

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            


        }


    }
}
