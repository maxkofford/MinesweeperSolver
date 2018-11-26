using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MinesweeperGui
{
    class outside
    {
        public static void colorcheck()
    {
        Cursor c = new Cursor(Cursor.Current.Handle);
        Point spot = Cursor.Position;

        Rectangle bounds = Screen.GetBounds(Point.Empty);
        using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }
            Color pix = bitmap.GetPixel(spot.X,spot.Y);
            MessageBox.Show("" + pix.R + "," + pix.G + "," + pix.B + " Alpha:" + pix.A);
            
        }
        
    }
        public static Color wall = Color.FromArgb(255, 8, 8, 8);
         public static Color blank =  Color.FromArgb(255,223,237,246);
         public static Color blue1 = Color.FromArgb(255, 63, 81, 192);
         public static Color green2 = Color.FromArgb(255, 32, 114, 15);     
         public static Color red3 = Color.FromArgb(255, 173, 8, 8);
         public static Color darkblue4 = Color.FromArgb(255, 8, 8, 136);
         public static Color darkred5 = Color.FromArgb(255, 123, 8, 8);
         public static Color cyan6 = Color.FromArgb(255, 1, 125, 121);
         public static Color bloodred78 = Color.FromArgb(255, 171, 8, 8);

        /*
         * blank - 223,237,246
2 green  - 32 ,114, 15
1 blue - 63,81,192
3 red 173, 5 , 4
4 darkblue 1,1,136
5 dark red 123,0,0
7&8 bloodred 171,6,6
6 cyan 1, 125 ,121
wall - 7,7,7
         */
        /// <summary>
        /// checks if the current color is within +-inValue of the target color for r g b
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static Boolean nearColor(Color current, Color target,int inR,int inG, int inB)
        {
            //int fuzz = inValue;
            if (target.R + inR > current.R && target.R - inR < current.R &&
                target.G + inG > current.G && target.G - inG < current.G &&
                target.B + inB > current.B && target.B - inB < current.B)
                return true;
            else return false;
        }
        /// <summary>
        /// Moves the mouse to the top left corner of the minefield
        /// </summary>
        public static void moveTopLeft()
        {
            int fuzz = 10;
            Cursor c = new Cursor(Cursor.Current.Handle);
            Point spot = Cursor.Position;

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                Cursor.Position = findTopLeft(bitmap, spot);
            }
        }
        /// <summary>
        /// Moves the mouse to the bottom right corner of the minefield
        /// </summary>
        public static void moveBottomRight()
        {
            int fuzz = 10;
            Cursor c = new Cursor(Cursor.Current.Handle);
            Point spot = Cursor.Position;

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                Cursor.Position = findBottomRight(bitmap, spot);
            }
        }

        /// <summary>
        /// Finds the bottomright most corner of the minefield
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Point findBottomRight(Bitmap b, Point p)
        {
            Point tmp = new Point(p.X, p.Y);
            Color current = b.GetPixel(tmp.X, tmp.Y);
            while (!nearColor(current, wall, 10,10,50))
            {
                tmp.X = tmp.X + 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
            /*
            int left = tmp.X;
            MessageBox.Show(left + "");
            while (nearColor(current, wall, 10,10,50))
            {
                tmp.X = tmp.X + 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
            tmp.X = tmp.X - 1;
            int right = tmp.X;
            MessageBox.Show(right + "");

            tmp.X = (right + left) / 2;
            MessageBox.Show(tmp.X + "");
             */
         //    Cursor.Position = tmp;
          //   MessageBox.Show(tmp.X + "," + tmp.Y +  "    " + current.R + "," + current.G + "," + current.B);
             System.Threading.Thread.Sleep(1000);
             while (nearColor(current, wall, 25, 25, 50))
            {
                tmp.Y = tmp.Y + 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
          //  Cursor.Position = tmp;
          //  MessageBox.Show(tmp.X + "," + tmp.Y + "    " + current.R + "," + current.G + "," + current.B);
            tmp.Y = tmp.Y - 1;
            current = b.GetPixel(tmp.X, tmp.Y);
             
             System.Threading.Thread.Sleep(1000);
             while (nearColor(current, wall, 25, 25, 50))
            {
                tmp.X = tmp.X + 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
       //     Cursor.Position = tmp;
        //    MessageBox.Show(tmp.X + "," + tmp.Y + "    " + current.R + "," + current.G + "," + current.B);
            tmp.X = tmp.X - 1;
            
             System.Threading.Thread.Sleep(1000);

            return tmp;
        }

        /// <summary>
        /// Finds the topleft most corner of the minefield
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Point findTopLeft(Bitmap b,Point p)
        {
            Point tmp = new Point(p.X, p.Y);
            Color current = b.GetPixel(tmp.X, tmp.Y);
            while (!nearColor(current, wall, 25, 25, 50))
            {
                tmp.X = tmp.X + 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
            /*
            int right = tmp.X;
            while (nearColor(current, wall, 25, 25, 50))
            {
                tmp.X = tmp.X - 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
            int left = tmp.X + 1;

            tmp.X = (right + left)/2;
             */ 
           // Cursor.Position = tmp;
           // MessageBox.Show(tmp.X + "," + tmp.Y +  "    " + current.R + "," + current.G + "," + current.B);
           // System.Threading.Thread.Sleep(1000);
            while (nearColor(current, wall, 25, 25, 50))
            {
                tmp.Y = tmp.Y - 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
          //  Cursor.Position = tmp;
          //  MessageBox.Show(tmp.X + "," + tmp.Y + "    " + current.R + "," + current.G + "," + current.B);
            tmp.Y = tmp.Y + 1;
            current = b.GetPixel(tmp.X, tmp.Y);
            
           // System.Threading.Thread.Sleep(1000);
            while (nearColor(current, wall, 25, 25, 50))
            {
                tmp.X = tmp.X - 1;
                current = b.GetPixel(tmp.X, tmp.Y);
            }
          //  Cursor.Position = tmp;
          //  MessageBox.Show(tmp.X + "," + tmp.Y + "    " + current.R + "," + current.G + "," + current.B);
            tmp.X = tmp.X + 1;
           
           // System.Threading.Thread.Sleep(1000);
           
            return tmp;
        }
        /// <summary>
        /// moves the mouse by +50 in both y and x
        /// </summary>
        public static void movePlus50()
        {
            Cursor c = new Cursor(Cursor.Current.Handle);
            Point spot = Cursor.Position;
            Cursor.Position = new Point(spot.X +50, spot.Y+50);
        }


        public static void showInfo()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                Point m = Cursor.Position;

                Point p = boxDimensions(bitmap, m);
                Point tl = findTopLeft(bitmap,m);
                Point br = findBottomRight(bitmap, m);

                int width = (br.X - tl.X)/p.X;
                int height = (br.Y - tl.Y) / p.Y;

                MessageBox.Show(width + " , " + height);

            }
        }

        /// <summary>
        /// Returns the dimensions of the box in (xwidth , ywidth)
        /// </summary>
        public static Point boxDimensions(Bitmap bitmap, Point spot)
        {
            int fuzz = 10;
           // Cursor c = new Cursor(Cursor.Current.Handle);
            //Point spot = Cursor.Position;

            Rectangle bounds = Screen.GetBounds(Point.Empty);
           // using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
           // {
              //  using (Graphics g = Graphics.FromImage(bitmap))
              //  {

              //      g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
              //  }

                Point origin = new Point(spot.X, spot.Y);
                Color current = bitmap.GetPixel(spot.X, spot.Y);

                while (!nearColor(current, wall, 25, 25, 50))
                {
                    spot.X = spot.X + 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int rightx = spot.X;

                spot.X = origin.X;
                spot.Y = origin.Y;
                current = bitmap.GetPixel(origin.X, origin.Y);


                while (!nearColor(current, wall, 25, 25, 50))
                {
                    spot.X = spot.X - 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int leftx = spot.X;

                spot.X = origin.X;
                spot.Y = origin.Y;
                current = bitmap.GetPixel(origin.X, origin.Y);


                while (!nearColor(current, wall, 25, 25, 50))
                {
                    spot.Y = spot.Y - 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int upy = spot.Y;

                spot.X = origin.X;
                spot.Y = origin.Y;
                current = bitmap.GetPixel(origin.X, origin.Y);


                while (!nearColor(current, wall, 25, 25, 50))
                {
                    spot.Y = spot.Y + 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int downy = spot.Y;


                return new Point(rightx - leftx, downy - upy);
               /// Cursor.Position = new Point((leftx + rightx) / 2, (upy + downy) / 2);

                //Cursor.Position = new Point(leftx , origin.Y);
                //Color pix = bitmap.GetPixel(Cursor.Position.X, Cursor.Position.Y);
               // MessageBox.Show("" + pix.R + "," + pix.G + "," + pix.B);

           // }

        }

        /// <summary>
        /// centers the mouse in the center of the given square
        /// </summary>
        public static void centerize()
        {
            int fuzz = 10;
            Cursor c = new Cursor(Cursor.Current.Handle);
            Point spot = Cursor.Position;

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                Point origin = new Point(spot.X, spot.Y);
                Color current =  bitmap.GetPixel(spot.X, spot.Y);

                while (!nearColor(current, wall, 25, 25, 50)) 
                {
                    spot.X = spot.X + 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int rightx = spot.X;
                
                spot.X = origin.X;
                spot.Y = origin.Y;
                current = bitmap.GetPixel(origin.X, origin.Y);


                while (!nearColor(current, wall, 25, 25, 50))
                {
                    spot.X = spot.X - 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int leftx = spot.X;

                spot.X = origin.X;
                spot.Y = origin.Y;
                current = bitmap.GetPixel(origin.X, origin.Y);


                while (!nearColor(current, wall, 25, 25, 50))
                {
                    spot.Y = spot.Y - 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int upy = spot.Y;

                spot.X = origin.X;
                spot.Y = origin.Y;
                current = bitmap.GetPixel(origin.X, origin.Y);


                while (!nearColor(current, wall, 25, 25, 50))
                {
                    spot.Y = spot.Y + 1;
                    current = bitmap.GetPixel(spot.X, spot.Y);
                }
                int downy = spot.Y;
                
                Cursor.Position = new Point((leftx + rightx) / 2, (upy + downy) / 2);
                 
                //Cursor.Position = new Point(leftx , origin.Y);
                Color pix = bitmap.GetPixel(Cursor.Position.X, Cursor.Position.Y);
                MessageBox.Show("" + pix.R + "," + pix.G + "," + pix.B);

            }

        }

        /*
         * Rectangle bounds = Screen.GetBounds(Point.Empty);
using(Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
{
    using(Graphics g = Graphics.FromImage(bitmap))
    {
         g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
    }
    bitmap.Save("test.jpg", ImageFormat.Jpeg);
}
         * */




        /*
         * private void MoveCursor()
{
   // Set the Current cursor, move the cursor's Position,
   // and set its clipping rectangle to the form. 

   this.Cursor = new Cursor(Cursor.Current.Handle);
   Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
  
}
         */ 



    }
}
