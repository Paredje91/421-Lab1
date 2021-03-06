/*  SWENG 421 - FA 21
 *  Jess Marquez & Hope Fisher
 *  Lab 1: paint program in C#, to include
 *      - 3 shapes: line, rectangle, and ellipse
 *      - rgb color options
 *      - arbitrary directionality
 *      - resizable window without losing drawings
 *      - real time shape sizing with mouse movement
 *      
 *  NOTES: double buffering included. Some extreme window size changes causes glitch, 
 *          where some drawings are cropped. Next mouse click will restore view.  
 * */
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;  
        }

// -- VARIABLES --
        
        //background and foreground for double buffering
        Bitmap bg, fg;
        Graphics g, bgg, fgg;

        Point pt1, pt2;
        Pen pen = new Pen(Color.Black);
        
        bool toPaint = false;

        int buttonNum;
        int blueVal, greenVal, redVal; 
        int calcX, calcY;
        int minX, minY; 

// -- SHAPES --
//   create shapes based on button clicked by user
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

//-- COLORS --
//  color values for pen based tracker info by user
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

//-- MOUSE --
//  mouse events click (down), release (up), and move
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            toPaint = true;
            pt1 = e.Location;

            //-- DOUBLE BUFFERING --
            if (bg == null)
            {
                bg = new Bitmap(this.Width, this.Height);
                fg = new Bitmap(this.Width, this.Height);
                bgg = Graphics.FromImage(bg);
                bgg.FillRectangle(Brushes.White, 0 ,0, this.Width, this.Height); //prevents interim shapes from staying on panel
                fgg = Graphics.FromImage(fg);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            toPaint = false;
            bgg.DrawImage(fg, 0, 0);  
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            pen.Color = Color.FromArgb(redVal, greenVal, blueVal);
            pt2 = e.Location; 
            
            //ellipse width & height 
            calcX = (pt2.X - pt1.X); 
            calcY = (pt2.Y - pt1.Y); 
            
            //rectangle start and end points for arbitrary direction
            var startpt= new Point(Math.Max(0, Math.Min(pt1.X, e.X)), Math.Max(0, Math.Min(pt1.Y, e.Y)));
            var endpt = new Point(Math.Min(this.Width, Math.Max(pt1.X, e.X)), Math.Min(this.Height, Math.Max(pt1.Y, e.Y)));

            //-- CREATE SHAPES --
            if (toPaint)
            {
                //-- DOUBLE BUFFERING --
                fgg.DrawImage(bg, 0, 0);

                //-- SHAPE BY BUTTON --
                if (buttonNum == 1)
                {
                    fgg.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
                }

                if (buttonNum == 2)
                {
                    fgg.DrawRectangle(pen, startpt.X, startpt.Y, endpt.X-startpt.X, endpt.Y-startpt.Y);
                }

                if (buttonNum == 3)
                {
                    fgg.DrawEllipse(pen, pt1.X, pt1.Y, calcX, calcY);
                }

                g = panel1.CreateGraphics();
                g.DrawImage(fg, 0, 0);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            if (buttonNum == 1)
            {
                g.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
            }

            if (buttonNum == 2)
            {
                g.DrawRectangle(pen, pt1.X, pt1.Y, calcX, calcY);
            }

            if (buttonNum == 3)
            {
                g.DrawEllipse(pen, pt1.X, pt1.Y, calcX, calcY);
            }
        }
    }
}
