using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

// -- VARIABLES --
        
        // background and foreground for double buffering
        Bitmap bg, fg; 
        Graphics g, bgg, fgg;

        Color color = new Color();
        Pen pen = new Pen(Color.Black);
        Point pt1, pt2; 

        int buttonNum = 1;  //default to line drawing
        bool toPaint = false;    
        int blueVal, greenVal, redVal;
        int calcX, calcY;

//-- SHAPES --
// create shapes based on button clicked by user

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
// color values for pen based tracker info by user

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

// -- MOUSE --
// mouse events click (down), release (up), and move 
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            toPaint = true;

            // -- DOUBLE BUFFERING -- 
            if (bg == null)
            {
                bg = fg = new Bitmap(this.Width, this.Height); 
                bgg = Graphics.FromImage(bg); 
                bgg.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height); // fills with White to prevent many shapes from being drawn
                fgg = Graphics.FromImage(fg); 
            }

            pt1 = e.Location; //starting point
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            toPaint = false;
            //current shape should be saved to background image from foreground
            //2 lines, including controlling the bool. so only 1 more... 
            bgg = Graphics.FromImage(fg);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            calcX = Math.Abs(pt2.X - pt1.X);
            calcY = Math.Abs(pt1.Y - pt2.Y);

            // -- CREATE SHAPES --
            if (toPaint == true)
            {
                //-- DOUBLE BUFFERING --
                fgg.DrawImage(bg, 0, 0);

                pen.Color = Color.FromArgb(redVal, greenVal, blueVal);
                pt2 = e.Location; //current point

                //-- SHAPE PER BUTTON -- 
                if (buttonNum == 1)
                {
                    fgg.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
                }

                if (buttonNum == 2)
                {
                    fgg.DrawRectangle(pen, pt1.X, pt1.Y, calcX, calcY);
                }

                if (buttonNum == 3)
                {
                    fgg.DrawEllipse(pen, pt1.X, pt1.Y, calcX, calcY);
                }

                g = this.CreateGraphics();
                g.DrawImage(fg, 0, 0);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            


        }


    }
}
