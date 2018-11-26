using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Z3;
using System.Diagnostics;

namespace MinesweeperGui
{
    public partial class Form1 : Form
    {
        public bool finished;
        public bool[,] areFlagged;
        public List<List<Button>> field;
        public List<List<Label>> fieldLabels;

        /// <summary>
        /// 0-8 =  number of mines near that square
        /// 9 = bomb
        /// 10 = uninitialized board
        /// 
        /// 
        /// </summary>
        public List<List<int>> buttonStates;
        public TableLayoutPanel cellarranger;
        
        public int colCount;
        public int rowCount;
        public int mineCount;

        public int flagcount;
        public int totalclicked;

        public bool wasGuessing;


        /// <summary>
        /// Default form constructor
        /// shouldnt ever be called
        /// </summary>
        public Form1()
        {
            isOutside = false;
            areFlagged = new bool[9, 9];
            child = new Form3(this);
            child.Show();

            flagcount = 0;
            totalclicked = 0;

            finished = false;

            colCount = 9;
            rowCount = 9;
            mineCount = 10;

            //colCount = 16;
            //rowCount = 16;
            //mineCount = 40;


            //colCount = 30;
            //rowCount = 16;
            //mineCount = 99;

            InitializeComponent();
            initField();
            buttonStates = new List<List<int>>();
            for (int x = 0; x < colCount; x++)
            {
                buttonStates.Add(new List<int>());
                for (int y = 0; y < rowCount; y++)
                {
                    areFlagged[x, y] = false;
                    buttonStates[x].Add(10);
                    field[x][y].BringToFront();

                }
            }

        }

        Form3 child;
        Form2 parent;

        /// <summary>
        /// Regular form constructor called with the input row column and mine count.
        /// </summary>
        
        public Form1(int incolcount, int inrowcount, int inminecount)
        {

            isOutside = false;
            child = new Form3(this);
            child.Show();

            finished = false;

            colCount = incolcount;
            rowCount = inrowcount;
            areFlagged = new bool[colCount, rowCount];
            mineCount = inminecount;
            flagcount = 0;
            totalclicked = 0;

            //colCount = 16;
            //rowCount = 16;
            //mineCount = 40;


            //colCount = 30;
            //rowCount = 16;
            //mineCount = 99;

            InitializeComponent();
            initField();
            buttonStates = new List<List<int>>();
            for (int x = 0; x < colCount; x++)
            {
                buttonStates.Add(new List<int>());
                for (int y = 0; y < rowCount; y++)
                {
                    areFlagged[x, y] = false;
                    buttonStates[x].Add(10);
                    field[x][y].BringToFront();

                }
            }


        }


        Boolean isOutside;

        /// <summary>
        /// Form constructor for outside solving.
        /// </summary>

        public Form1(int incolcount, int inrowcount, int inminecount,Boolean outside)
        {
            isOutside = true;
           // child = new Form3(this);
           // child.Show();

            finished = false;

            colCount = incolcount;
            rowCount = inrowcount;
            areFlagged = new bool[colCount, rowCount];
            mineCount = inminecount;
            flagcount = 0;
            totalclicked = 0;

            //colCount = 16;
            //rowCount = 16;
            //mineCount = 40;


            //colCount = 30;
            //rowCount = 16;
            //mineCount = 99;

            InitializeComponent();
            initField();
            buttonStates = new List<List<int>>();
            for (int x = 0; x < colCount; x++)
            {
                buttonStates.Add(new List<int>());
                for (int y = 0; y < rowCount; y++)
                {
                    areFlagged[x, y] = false;
                    buttonStates[x].Add(10);
                    field[x][y].BringToFront();

                }
            }

            findAll2(guessOne3);

        }
        


        bool testMode = false;
        Stopwatch stopwatch;


        /// <summary>
        /// the form thats initialized if we want to do testing.
        /// </summary>
       
        public Form1(int incolcount, int inrowcount, int inminecount, Form2 inpar,int inguesser, int method)
        {

            testMode = true;
            stopwatch = new Stopwatch();

           

            parent = inpar;
            //child = new Form3(this);
            //child.Show();

            finished = false;

            colCount = incolcount;
            rowCount = inrowcount;
            areFlagged = new bool[colCount, rowCount];
            mineCount = inminecount;
            flagcount = 0;
            totalclicked = 0;

      

            InitializeComponent();
            initField();
            buttonStates = new List<List<int>>();
            for (int x = 0; x < colCount; x++)
            {
                buttonStates.Add(new List<int>());
                for (int y = 0; y < rowCount; y++)
                {
                    areFlagged[x, y] = false;
                    buttonStates[x].Add(10);
                    field[x][y].BringToFront();

                }
            }


            guesser g = guessOne;
            if (inguesser == 1)
            {
                g = guessOne;
            }
            if (inguesser == 2)
            {
                g = guessOne2;
            }
            if (inguesser == 3)
            {
                g = guessOne3;
            }


            stopwatch.Start();
            if (method == 1)
            {
                findAll(g);
            }
            if (method == 2)
            {
                findAll2(g);
            }

            if (method == 3)
            {
                findAll3(g);
            }
               
               
          

        }

        bool hasSent = false;

        /// <summary>
        /// Method called to return the results of this session back to the original form
        /// </summary>
        /// <param name="p"></param>
        /// <param name="wasGuessing"></param>
        /// <param name="guessCount"></param>
        private void sendResults(bool p, bool wasGuessing, int guessCount)
        {
            //if (hasSent == false)
            //{
            //    hasSent = true;
                stopwatch.Stop();

                parent.testingCallback(p, wasGuessing, guessCount, stopwatch.Elapsed.Milliseconds);
                this.Close();
            //}
           
                
        }

        /*
        /// <summary>
        /// Unused
        /// </summary>
        /// <param name="incolcount"></param>
        /// <param name="inrowcount"></param>
        /// <param name="inminecount"></param>
        /// <param name="tree"></param>
        public Form1(int incolcount, int inrowcount, int inminecount, bool tree)
        {

            child = new Form3(this);
            child.Show();

            finished = false;

            colCount = incolcount;
            rowCount = inrowcount;
            areFlagged = new bool[colCount, rowCount];
            mineCount = inminecount;

            //colCount = 16;
            //rowCount = 16;
            //mineCount = 40;


            //colCount = 30;
            //rowCount = 16;
            //mineCount = 99;

            InitializeComponent();
            initField();
            buttonStates = new List<List<int>>();




            for (int x = 0; x < colCount; x++)
            {
                buttonStates.Add(new List<int>());
                for (int y = 0; y < rowCount; y++)
                {
                    areFlagged[x, y] = false;
                    buttonStates[x].Add(10);
                    field[x][y].BringToFront();



                }
            }

            int tempx = 0;
            int tempy = 0;

            buttonStates[tempx][tempy] = 0;
            tempx++;


        }
        */
        /// <summary>
        /// Sets up the mine field by creating buttons, labels, and the tablelayout panel organizer
        /// </summary>
        private void initField()
        {
            int cellsize = 25;
            int buttonsize = 25;
            int curTabIndex = 0;
            field = new List<List<Button>>();
            cellarranger = new TableLayoutPanel();
            fieldLabels = new List<List<Label>>();


            this.Controls.Add(cellarranger);
            //this.tableHolder.Controls.Add(cellarranger);

            cellarranger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));



            cellarranger.Location = new System.Drawing.Point(50, 50);
            cellarranger.Name = "cellarranger";
            cellarranger.Size = new System.Drawing.Size(cellsize * colCount, cellsize * rowCount);
            this.Size = new System.Drawing.Size(cellarranger.Size.Width + 11, cellarranger.Height + 33);
            cellarranger.TabIndex = curTabIndex++;

            cellarranger.ColumnCount = colCount;
            cellarranger.RowCount = rowCount;

            for (int x = 0; x < colCount; x++)
            {
                cellarranger.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, cellsize));
            }

            for (int y = 0; y < rowCount; y++)
            {
                cellarranger.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, cellsize));
            }

            for (int x = 0; x < colCount; x++)
            {
                field.Add(new List<Button>());
                fieldLabels.Add(new List<Label>());
                for (int y = 0; y < rowCount; y++)
                {
                    field[x].Add(new Button());
                    field[x][y].Location = new System.Drawing.Point(3, 3);
                    field[x][y].Name = "button" + x + "," + y;
                    field[x][y].Size = new System.Drawing.Size(buttonsize, buttonsize);
                    field[x][y].TabIndex = curTabIndex++;

                    field[x][y].Tag = new Tuple<int, int>(x, y);
                    //field[x][y].Text = (x +","+ y);
                    field[x][y].UseVisualStyleBackColor = true;
                    //field[x][y].BringToFront();
                    field[x][y].Click += new System.EventHandler(this.LeftClickHander);
                    field[x][y].Margin = new System.Windows.Forms.Padding(0);

                    field[x][y].MouseClick += new MouseEventHandler(RightClickHander);
                    field[x][y].MouseUp += new MouseEventHandler(RightClickHander);
                    cellarranger.Controls.Add(field[x][y], x, y);
                    
                    this.Controls.Add(field[x][y]);

                    fieldLabels[x].Add(new Label());
                    fieldLabels[x][y].AutoSize = false;
                    fieldLabels[x][y].Anchor = System.Windows.Forms.AnchorStyles.None;
                    fieldLabels[x][y].Size = new System.Drawing.Size(buttonsize-6, buttonsize-4);
                    fieldLabels[x][y].Name = "lbl" + x + y;
                    fieldLabels[x][y].TabIndex = curTabIndex++;
                    fieldLabels[x][y].Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //fieldLabels[x][y].Location = new Point(1,-3);
                    
                    //fieldLabels[x][y].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
           // | System.Windows.Forms.AnchorStyles.Left) 
           // | System.Windows.Forms.AnchorStyles.Right)));

                    //fieldLabels[x][y].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    cellarranger.Controls.Add(fieldLabels[x][y], x, y);
                    this.Controls.Add(fieldLabels[x][y]);
/*
                    this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.label6.Location = new System.Drawing.Point(331, 194);
                    this.label6.Name = "label6";
                    this.label6.Size = new System.Drawing.Size(37, 15);
                    this.label6.TabIndex = 19;
                    this.label6.Text = "label6";
                    this.label6.Click += new System.EventHandler(this.label6_Click);
*/
                    //this.Controls.Add(this.button1);
                }
            }

            // cellarranger.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            //cellarranger.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));


            // cellarranger.Controls.Add(this.button1, 1, 0);

        }

        /// <summary>
        /// Acts as if the user did a left click on the button for that x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void callLeftClick(int x, int y)
        {
            Button sender = field[x][y];

            LeftClickHander(sender, null);
        }

        /// <summary>
        /// Acts as if the user did a right click on the button for that x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void callRightClick(int x, int y)
        {
            Button sender = field[x][y];

            MouseEventArgs mousbutton = new MouseEventArgs(MouseButtons.Right, 1, 100, 100, 100);

            RightClickHander(sender, mousbutton);
        }


        /// <summary>
        /// Handles every right click to every button.
        /// Switches them between flagged and unflagged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RightClickHander(object sender, MouseEventArgs e)
        {
            totalclicked++;
            flagcount++;
            Button cur = ((Button)sender);
            if (e.Button == MouseButtons.Right)
            {
                if (cur.Enabled == true)
                {
                    if (cur.Text == "F")
                    {
                        cur.Text = "";
                        cur.Image = null;
                        cur.ForeColor = Color.Black;

                    }
                    else
                    {
                        cur.Image = global::MinesweeperGui.Properties.Resources.flag1;
                        cur.ForeColor = cur.BackColor;
                        cur.Text = "F";

                    }
                }
            }


            cur.Invalidate();
            cur.Update();

            checkWin();

            //  MessageBox.Show(e.Button.ToString());

        }

        /// <summary>
        /// Handles a left click to any button in the minefield
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftClickHander(object sender, EventArgs e)
        {

            totalclicked++;
            int x = ((Tuple<int, int>)((Button)sender).Tag).Item1;
            int y = ((Tuple<int, int>)((Button)sender).Tag).Item2;

            Button cur = ((Button)sender);

            if (cur.Text == "F")
            {
                cur.Text = "";
                cur.Image = null;
                cur.ForeColor = Color.Black;

            }

            //MessageBox.Show(x + " , " + y + " , " + buttonStates[x][y]);

            //switch depending on the hidden value of the clicked button
            switch (buttonStates[x][y])
            {
                case 10:
                    initNumbers(x, y);
                    if (buttonStates[x][y] == 0)
                        showZero(x, y);
                    else
                        showButton(x, y);
                    break;

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    showButton(x, y);
                    break;

                case 9:
                    // endGame(x, y);
                    //while (true) 
                    if (testMode == false)
                        MessageBox.Show("Sorry you lose :( at " + x + "," + y);
                    
                    if (wasGuessing && testMode == false)
                        MessageBox.Show("You lost on a guess.");
                    string hexValue = "#ee5050"; // You do need the hash
                    Color colour = System.Drawing.ColorTranslator.FromHtml(hexValue); // Yippee
                    field[x][y].BackColor = colour;

                    if(testMode == false)
                    MessageBox.Show("You guessed " + guessCount + " times.");


                    if (testMode == true)
                    {
                        sendResults(false, wasGuessing, guessCount);
                    }


                    finished = true;
                    break;
                case 0:
                    showZero(x, y);
                    break;

            }

            checkWin();



        }

       

        private void checkWin()
        {
            bool checker = true;
            for (int x = 0; x < colCount; x++)
            {

                for (int y = 0; y < rowCount; y++)
                {
                    if ((field[x][y].Text == "F" && buttonStates[x][y] == 9) || (field[x][y].Enabled == false && buttonStates[x][y] != 9))
                    {
                    }
                    else
                        checker = false;

                }
            }
            if (checker)
            {
                if (testMode == false)
                MessageBox.Show("You Win!");
                if (testMode == false)
                MessageBox.Show("You guessed " + guessCount + " times.");


                if (testMode == true)
                {
                    sendResults(true, wasGuessing, guessCount);

                }

                finished = true;
                for (int x = 0; x < colCount; x++)
                {

                    for (int y = 0; y < rowCount; y++)
                    {
                        field[x][y].Enabled = false;
                    }
                }
            }
        }

        private void showZero(int x, int y)
        {
            showButton(x, y);

            int tempx = -1; int tempy = -1;


            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }
            tempx++;




            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }


            tempx++;

            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }


            tempx = -1;
            tempy++;

            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }

            tempx++;
            tempx++;

            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }

            tempx = -1;
            tempy++;

            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }
            tempx++;

            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }
            tempx++;

            if (isValidSpot(x + tempx, y + tempy))
            {
                if (buttonStates[x + tempx][y + tempy] == 0 && field[x + tempx][y + tempy].Enabled == true)
                    showZero(x + tempx, y + tempy);
                else
                    showButton(x + tempx, y + tempy);
            }

            //MessageBox.Show(x + "'" + y);

            /*

                        if (isValidSpot(x - 1, y)  )
                        {
                            if(buttonStates[x - 1][y] == 0 && field[x - 1][y].Enabled == true)
                            showZero(x - 1, y);
                            else
                            showButton(x-1, y);
                        }

                        if (isValidSpot(x, y - 1) )
                        {
                            if (buttonStates[x ][y-1] == 0 && field[x ][y-1].Enabled == true)
                                showZero(x , y-1);
                            else
                                showButton(x , y-1);
                        }

                        if (isValidSpot(x + 1, y))
                        {
                            if (buttonStates[x + 1][y] == 0 && field[x + 1][y].Enabled == true)
                                showZero(x + 1, y);
                            else
                                showButton(x + 1, y);
                        }

                        if (isValidSpot(x, y + 1) )
                        {
                            if (buttonStates[x ][y+1] == 0 && field[x ][y+1].Enabled == true)
                                showZero(x , y+1);
                            else
                                showButton(x, y+1);
                        }
                        */

        }

        /// <summary>
        /// shows all buttons if the game was over
        /// </summary>
        /// <param name="xin"></param>
        /// <param name="yin"></param>
        private void endGame(int xin, int yin)
        {
            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    showButton(x, y);
                }
            }
        }

        /// <summary>
        /// Reveals the value of a button
        /// </summary>
        /// <param name="xin"></param>
        /// <param name="yin"></param>

        private void showButton(int xin, int yin)
        {
            switch (buttonStates[xin][yin])
            {

                case 0:
                    field[xin][yin].Text = "";
                    break;
                case 1:
                    field[xin][yin].ForeColor = Color.Blue;
                    //field[xin][yin].BackColor = Color.Blue;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];
                    fieldLabels[xin][yin].ForeColor = Color.Blue;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;
                case 2:
                     field[xin][yin].ForeColor = Color.Green;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];
                    fieldLabels[xin][yin].ForeColor = Color.Green;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;
                case 3:
                     field[xin][yin].ForeColor = Color.Red;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];
                    fieldLabels[xin][yin].ForeColor = Color.Red;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;
                case 4:
                     field[xin][yin].ForeColor = Color.DarkBlue;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];
                    fieldLabels[xin][yin].ForeColor = Color.DarkBlue;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;
                case 5:
                     field[xin][yin].ForeColor = Color.DarkRed;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];

                    fieldLabels[xin][yin].ForeColor = Color.DarkRed;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;
                case 6:
                     field[xin][yin].ForeColor = Color.Green;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];

                    fieldLabels[xin][yin].ForeColor = Color.Green;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;
                case 7:
                     field[xin][yin].ForeColor = Color.Teal;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];
                    fieldLabels[xin][yin].ForeColor = Color.Teal;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;
                case 8:
                     field[xin][yin].ForeColor = Color.Red;
                    field[xin][yin].Text = "" + buttonStates[xin][yin];
                    fieldLabels[xin][yin].ForeColor = Color.Red;
                    fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                    break;

                case 9:
                    field[xin][yin].Text = "M";
                    break;

            }
            

            field[xin][yin].Enabled = false;
            //field[xin][yin].Visible = false;
            if (buttonStates[xin][yin] != 0)
            {
                fieldLabels[xin][yin].Text = "" + buttonStates[xin][yin];
                fieldLabels[xin][yin].BringToFront();
            }

          

            field[xin][yin].Image = null;


            field[xin][yin].Invalidate();
            field[xin][yin].Update();

            fieldLabels[xin][yin].Invalidate();
            fieldLabels[xin][yin].Update();
        }

        /// <summary>
        /// Setting up the initial values for the minefield
        /// Randomly picks squares until enough mines are chosen.
        /// Then updates all the other squares to have the correct numbers
        /// </summary>
        /// <param name="xin"></param>
        /// <param name="yin"></param>
        private void initNumbers(int xin, int yin)
        {
            Random r = new Random();


            //generate some random spots for mines
            for (int x = 0; x < mineCount; x++)
            {
            //keep searching until minecount spots have been chosen
            StartLoop:
                int randomX = (int)(r.NextDouble() * (colCount - 1));
                int randomY = (int)(r.NextDouble() * (rowCount - 1));

                //if the spot is already chosen then pick another one
                if (buttonStates[randomX][randomY] != 10 || randomX == xin || randomY == yin)
                    goto StartLoop;
                else
                    //else choose it
                    buttonStates[randomX][randomY] = 9;
            }

            //update all locations to have their correct numbers
            for (int x = 0; x < colCount; x++)
            {

                for (int y = 0; y < rowCount; y++)
                {
                    if (buttonStates[x][y] == 10)
                    {
                        int tempMineCount = 0;

                        //3 spots above the current spot
                        if (isValidSpot(x - 1, y - 1) && buttonStates[x - 1][y - 1] == 9)
                            tempMineCount++;
                        if (isValidSpot(x, y - 1) && buttonStates[x][y - 1] == 9)
                            tempMineCount++;
                        if (isValidSpot(x + 1, y - 1) && buttonStates[x + 1][y - 1] == 9)
                            tempMineCount++;
                        //2 spots next to the curretn spot
                        if (isValidSpot(x - 1, y) && buttonStates[x - 1][y] == 9)
                            tempMineCount++;
                        if (isValidSpot(x + 1, y) && buttonStates[x + 1][y] == 9)
                            tempMineCount++;
                        //3 spots below the current spot
                        if (isValidSpot(x - 1, y + 1) && buttonStates[x - 1][y + 1] == 9)
                            tempMineCount++;
                        if (isValidSpot(x, y + 1) && buttonStates[x][y + 1] == 9)
                            tempMineCount++;
                        if (isValidSpot(x + 1, y + 1) && buttonStates[x + 1][y + 1] == 9)
                            tempMineCount++;

                        buttonStates[x][y] = tempMineCount;
                    }


                }
            }
        }

        /// <summary>
        /// Checks to see if the given coordinates are a valid location in the field
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool isValidSpot(int x, int y)
        {
            return (x > -1 && x < colCount && y > -1 && y < rowCount);
        }



        public BoolExpr[,] vars;

        /// <summary>
        /// A quick test of Z3 to make sure everything was working
        /// </summary>
        public void test()
        {
            using (Context c = new Context())
            {
                makeVars(c);

                List<BoolExpr> temp2 = new List<BoolExpr>();
                temp2.Add(vars[0, 0]);
                temp2.Add(vars[1, 0]);
                temp2.Add(vars[2, 0]);
                temp2.Add(vars[3, 0]);
                temp2.Add(vars[4, 0]);
                temp2.Add(vars[5, 0]);
                List<Tuple<List<BoolExpr>, List<BoolExpr>>> nchosenk = nchoosek(temp2, 1);
                MessageBox.Show("Size test1 " + nchosenk.Count);

                nchosenk = nchoosek(temp2, 2);
                MessageBox.Show("Size test2 " + nchosenk.Count);
                List<BoolExpr> constraints = new List<BoolExpr>();
                constraints.Add(c.MkOr(vars[0, 0], vars[1, 4]));

                Dictionary<string, bool> output = SolveOne(c, constraints);

                //  string[] coords = x.Name.ToString().Substring(1).Split('c');
                //   outp[Int32.Parse(coords[0]) , Int32.Parse(coords[1]) ] 

                foreach (KeyValuePair<string, bool> x in output)
                {
                    MessageBox.Show("Key=" + x.Key + " Value=" + x.Value);
                }

                MessageBox.Show("group 2");
                constraints.Add(reversed(output, c));

                output = SolveOne(c, constraints);

                //  string[] coords = x.Name.ToString().Substring(1).Split('c');
                //   outp[Int32.Parse(coords[0]) , Int32.Parse(coords[1]) ] 

                foreach (KeyValuePair<string, bool> x in output)
                {
                    MessageBox.Show("Key=" + x.Key + " Value=" + x.Value);
                }
            }
        }

       
        public delegate void guesser();

        /// <summary>
        /// Solves the minesweeper using the thereexists method
        /// </summary>
        /// <param name="guess"></param>
        public void findAll3( guesser guess)
        {
            guess();
            while (finished == false)
            {
                System.Threading.Thread.Sleep(50);
                if (findValues31() == false)
                {
                    guess();
                }
            }
        }

        /// <summary>
        /// Solves the minesweeper using the check all method
        /// </summary>
        /// <param name="guess"></param>
        public void findAll2(guesser guess)
        {
            guess();
            while (finished == false)
            {
                System.Threading.Thread.Sleep(50);
                if (findValues2() == false)
                {
                    guess();
                }
            }
        }


        /// <summary>
        /// Solves the minesweeper using the break early method
        /// </summary>
        /// <param name="guess"></param>
        public void findAll(guesser guess)
        {
            guess();
            while (finished == false)
            {
                System.Threading.Thread.Sleep(50);
                if (findValues() == false)
                {
                    guess();
                }
            }
        }

        /// <summary>
        /// The third guessing method.
        /// Finds the number with the best number - mines / nearby empty ratio
        /// Will pick a random spot if the best spot has a worse ratio then the average square (mines remaining / remaining squares)
        /// </summary>
        public void guessOne3()
        {

            wasGuessing = true;


            double bestcount = 1;
            List<Button> bestGuys = new List<Button>();


            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    if (field[x][y].Enabled == false && field[x][y].Text != "")
                    {

                        List<Button> temp = new List<Button>();
                        temp.AddRange(getEmptyNeighborsB(x, y));
                        List<BoolExpr> temp2 = new List<BoolExpr>();
                        temp2.AddRange(getMineNeighbors(x, y));

                        int number = Int32.Parse(field[x][y].Text);

                        //temp3.AddRange();
                        if (temp.Count > 0)
                        {
                            if (((number - temp2.Count + 0.0) / temp.Count) < bestcount)
                            {
                                bestGuys = new List<Button>();
                                bestGuys.AddRange(temp);

                                bestcount = ((number - temp2.Count + 0.0) / temp.Count);
                            }


                        }

                    }
                }
            }

            if (bestGuys.Count == 0)
            {
                guessOne();
               //  MessageBox.Show("failguess3 " + bestcount);
            }
            else
            {
                if ((this.mineCount - this.flagcount + 0.0) / ((0.0 + this.colCount * this.rowCount) - totalclicked) < bestcount)
                {
                  //  MessageBox.Show("ratioguess3 " + bestcount + " " + ((this.mineCount - this.flagcount + 0.0) / ((0.0 + this.colCount * this.rowCount) - totalclicked)));
                    guessOne();
                }
                else
                {
                   // MessageBox.Show("guess3 " + bestcount);
                    LeftClickHander(bestGuys[0], null);
                }


            }




            if (finished == false)
                wasGuessing = false;
        }


        /// <summary>
        /// Guesses by finding a square that is near a number
        /// 
        /// </summary>
        public void guessOne2()
        {

            wasGuessing = true;


            int bestcount = 0;
            List<Button> bestGuys = new List<Button>();
            

            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    if (field[x][y].Enabled == false && field[x][y].Text != "")
                    {
                      
                        List<Button>temp = new List<Button>();
                        temp.AddRange(getEmptyNeighborsB(x, y));

                        if (temp.Count > bestcount)
                        {
                            bestGuys = new List<Button>();
                            bestGuys.AddRange(temp);

                            bestcount = bestGuys.Count;
                        }

                    }
                }
            }

            if (bestGuys.Count == 0)
                guessOne();
            else
            LeftClickHander(bestGuys[0], null);


          
            



            
            if (finished == false)
                wasGuessing = false;
        }


        /// <summary>
        /// Guesses randomly
        /// </summary>
        public void guessOne()
        {
            wasGuessing = true;
            Random r = new Random();



        StartLoop:
            int randomX = (int)(r.NextDouble() * (colCount - 1));
            int randomY = (int)(r.NextDouble() * (rowCount - 1));

            //if the spot is already chosen then pick another one
            if (field[randomX][randomY].Enabled == false || areFlagged[randomX, randomY] == true || field[randomX][randomY].Text == "F")
                goto StartLoop;
            else
            {
                guessCount++;
                LeftClickHander(field[randomX][randomY], null);
            }
            //else choose it
            if (finished == false)
                wasGuessing = false;
        }

        int guessCount = 0;


        /// <summary>
        /// Reverses all the boolean expressions by putting a not in front of them all
        /// </summary>
        /// <param name="c"></param>
        /// <param name="currentVars"></param>
        /// <returns></returns>
        public HashSet<BoolExpr> MkNotAll(Context c, HashSet<BoolExpr> currentVars)
        {
            HashSet<BoolExpr> output = new HashSet<BoolExpr>();

            foreach (BoolExpr aVar in currentVars)
            {
                output.Add(c.MkNot(aVar));
            }
            return output;
        }

        /// <summary>
        /// Left clicks all the buttons represented by the variables in the input list
        /// </summary>
        /// <param name="input"></param>
        public void leftClickAll(List<BoolExpr> input)
        {
            foreach (BoolExpr x in input)
            {
                
                string[] coords = x.ToString().Substring(1).Split('c');
                int xcoord = Int32.Parse(coords[1]);
                int ycoord = Int32.Parse(coords[0]);

                LeftClickHander(field[xcoord][ycoord], null);
                //System.Threading.Thread.Sleep(50);
            }
        }
        /// <summary>
        /// Right clicks all the buttons represented by the variables in the input list
        /// </summary>
        /// <param name="input"></param>
        public void rightClickAll(List<BoolExpr> input)
        {
            foreach (BoolExpr x in input)
            {
                string[] coords = x.ToString().Substring(1).Split('c');
                int xcoord = Int32.Parse(coords[1]);
                int ycoord = Int32.Parse(coords[0]);

                
                    RightClickHander(field[xcoord][ycoord], new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
                    
                    areFlagged[xcoord, ycoord] = true;
               
            }
        }

        /// <summary>
        /// A fixed version of the exists method
        /// </summary>
        /// <returns></returns>
        public bool findValues31()
        {
            // Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> output;

            using (Context c = new Context())
            {
                makeVars(c);

                List<BoolExpr> constraints = new List<BoolExpr>();
                HashSet<BoolExpr> currentVars;
                constraints = gatherConstraints2(c, out currentVars);

                List<BoolExpr> mines = new List<BoolExpr>();
                List<BoolExpr> cleared = new List<BoolExpr>();


                foreach (BoolExpr currentVar in currentVars)
                {
                    Dictionary<string, bool> asolutionMine = ExistsOne(c, constraints, currentVar);

                    if (asolutionMine == null)
                    {
                        mines.Add(currentVar);
                    }



                    Dictionary<string, bool> asolutionCleared = ExistsOne(c, constraints, c.MkNot(currentVar));

                    if (asolutionCleared == null)
                    {
                        cleared.Add(currentVar);
                    }
                }

                leftClickAll(mines);

                rightClickAll(cleared);




                if (cleared.Count == 0 && mines.Count == 0)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Broken and unused version of the exists method
        /// </summary>
        /// <returns></returns>
        public bool findValues30()
        {
            // Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> output;

            using (Context c = new Context())
            {
                makeVars(c);

                List<BoolExpr> constraints = new List<BoolExpr>();
                HashSet<BoolExpr> currentVars;
                constraints = gatherConstraints2(c, out currentVars);
                
                Dictionary<string, bool> solutionMines = ExistsAll(c, constraints, currentVars);

                /*
                if(solutionMines != null)

                foreach (KeyValuePair<string, bool> x in solutionMines)
                {

                    MessageBox.Show(x.Key + " " + x.Value);
                }
                */
                HashSet<BoolExpr> negVars = MkNotAll(c, currentVars);

                Dictionary<string, bool> solutionClears = ExistsAll(c, constraints, negVars);
              

                return true;
            }
        }

        /// <summary>
        /// Trys to find a satisfying solution for the current group of visible squares using the check all method
        /// </summary>
        /// <returns></returns>
        public bool findValues2()
        {
            // Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> output;

            using (Context c = new Context())
            {
                makeVars(c);

                List<BoolExpr> constraints = new List<BoolExpr>();

                constraints = gatherConstraints(c);

                Dictionary<string, bool> solution = SolveOne(c, constraints);
                Dictionary<string, bool> solution2 = new Dictionary<string, bool>();
                Dictionary<string, bool> startsolution = solution;
                Dictionary<string, bool> unchanged = new Dictionary<string, bool>();
                int changedCount = solution.Count;
                bool end = false;
                eliminated = new Dictionary<string, bool>();

                int counter = 0;
                while (changedCount > 0 && end == false && counter < 50)
                {
                    //try
                    //{
                    //eliminated = new Dictionary<string, bool>();
                    //unchanged = new Dictionary<string, bool>(); 

                    //MessageBox.Show(reversed(solution, c).ToString());

                    constraints.Add(reversed(solution, c));

                    solution2 = SolveOne(c, constraints);

                    if (solution2 != null)
                    {
                        // MessageBox.Show("solution 2 isnt null");
                        changedCount = differentElements(solution, solution2, unchanged);
                        changedCount = 1;

                        foreach (KeyValuePair<string, bool> x in eliminated)
                        {
                            if (unchanged.ContainsKey(x.Key))
                            {
                                unchanged.Remove(x.Key);
                            }
                        }

                        if (eliminated.Count == solution.Count)
                        {
                            changedCount = 0;



                        }
                        counter++;
                        // MessageBox.Show(counter + " check done \n" + "elim count = "  + eliminated.Count + " unchanged count = " + unchanged.Count   + " solution count = " + solution.Count);
                    }
                    else
                    {
                        //MessageBox.Show("solution 2 is null");
                        end = true;

                    }



                    solution = solution2;
                    //}
                    //catch(Exception e)
                    //{
                    //    MessageBox.Show(e.Message);
                    //    end = true;
                    //}
                }

                if (counter > 1)
                {


                    if (unchanged.Count == 0)
                    {
                        guessCount++;
                        //MessageBox.Show("CANT SOLVE :(");
                        return false;
                    }


                    foreach (KeyValuePair<string, bool> x in unchanged)
                    {
                        string[] coords = x.Key.Substring(1).Split('c');
                        int xcoord = Int32.Parse(coords[1]);
                        int ycoord = Int32.Parse(coords[0]);

                        if (x.Value)
                        {

                            RightClickHander(field[xcoord][ycoord], new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
                            areFlagged[xcoord, ycoord] = true;
                            //System.Threading.Thread.Sleep(50);
                        }
                        else
                        {
                            LeftClickHander(field[xcoord][ycoord], null);
                            //System.Threading.Thread.Sleep(50);
                        }
                    }

                }
                else
                {
                    // MessageBox.Show("Entering singler");
                    foreach (KeyValuePair<string, bool> x in startsolution)
                    {
                        string[] coords = x.Key.Substring(1).Split('c');
                        int xcoord = Int32.Parse(coords[1]);
                        int ycoord = Int32.Parse(coords[0]);

                        if (x.Value)
                        {

                            RightClickHander(field[xcoord][ycoord], new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
                            areFlagged[xcoord, ycoord] = true;
                            //System.Threading.Thread.Sleep(50);
                        }
                        else
                        {
                            LeftClickHander(field[xcoord][ycoord], null);
                            //System.Threading.Thread.Sleep(50);
                        }
                    }
                }



                //    Dictionary<string, bool> solution = SolveOne(c, constraints);



            }
            return true;
            //MessageBox.Show("finished a round");
            // return null;
        }

        /// <summary>
        /// Trys to find a satisfying solution for the current group of visible squares using the break early method
        /// </summary>
        /// <returns></returns>
        public bool findValues()
        {
            // Tuple<List<Tuple<int, int>>, List<Tuple<int, int>>> output;

            using (Context c = new Context())
            {
                makeVars(c);

                List<BoolExpr> constraints = new List<BoolExpr>();

                constraints = gatherConstraints(c);

                Dictionary<string, bool> solution = SolveOne(c, constraints);
                Dictionary<string, bool> solution2 = new Dictionary<string, bool>();
                Dictionary<string, bool> startsolution = solution;
                Dictionary<string, bool> unchanged = new Dictionary<string, bool>();
                int changedCount = solution.Count;
                bool end = false;
                eliminated = new Dictionary<string, bool>();

                int counter = 0;
                while (changedCount > 0 && end == false && counter < 50)
                {
                    //try
                    //{
                    //eliminated = new Dictionary<string, bool>();
                    //unchanged = new Dictionary<string, bool>(); 

                    //MessageBox.Show(reversed(solution, c).ToString());

                    constraints.Add(reversed(solution, c));

                    solution2 = SolveOne(c, constraints);

                    if (solution2 != null)
                    {
                        // MessageBox.Show("solution 2 isnt null");
                        changedCount = differentElements(solution, solution2, unchanged);

                        foreach (KeyValuePair<string, bool> x in eliminated)
                        {
                            if (unchanged.ContainsKey(x.Key))
                            {
                                unchanged.Remove(x.Key);
                            }
                        }

                        if (eliminated.Count == solution.Count)
                        {
                            changedCount = 0;



                        }
                        counter++;
                        // MessageBox.Show(counter + " check done \n" + "elim count = "  + eliminated.Count + " unchanged count = " + unchanged.Count   + " solution count = " + solution.Count);
                    }
                    else
                    {
                        //MessageBox.Show("solution 2 is null");
                        end = true;

                    }



                    solution = solution2;
                    //}
                    //catch(Exception e)
                    //{
                    //    MessageBox.Show(e.Message);
                    //    end = true;
                    //}
                }

                if (counter > 1)
                {


                    if (unchanged.Count == 0)
                    {
                        guessCount++;
                        //MessageBox.Show("CANT SOLVE :(");
                        return false;
                    }


                    foreach (KeyValuePair<string, bool> x in unchanged)
                    {
                        string[] coords = x.Key.Substring(1).Split('c');
                        int xcoord = Int32.Parse(coords[1]);
                        int ycoord = Int32.Parse(coords[0]);

                        if (x.Value)
                        {

                            RightClickHander(field[xcoord][ycoord], new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
                            areFlagged[xcoord, ycoord] = true;
                            //System.Threading.Thread.Sleep(50);
                        }
                        else
                        {
                            LeftClickHander(field[xcoord][ycoord], null);
                            //System.Threading.Thread.Sleep(50);
                        }
                    }

                }
                else
                {
                    // MessageBox.Show("Entering singler");
                    foreach (KeyValuePair<string, bool> x in startsolution)
                    {
                        string[] coords = x.Key.Substring(1).Split('c');
                        int xcoord = Int32.Parse(coords[1]);
                        int ycoord = Int32.Parse(coords[0]);

                        if (x.Value)
                        {

                            RightClickHander(field[xcoord][ycoord], new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
                            areFlagged[xcoord, ycoord] = true;
                            //System.Threading.Thread.Sleep(50);
                        }
                        else
                        {
                            LeftClickHander(field[xcoord][ycoord], null);
                            //System.Threading.Thread.Sleep(50);
                        }
                    }
                }



                //    Dictionary<string, bool> solution = SolveOne(c, constraints);



            }
            return true;
            //MessageBox.Show("finished a round");
            // return null;
        }

        Dictionary<string, bool> eliminated;

        /// <summary>
        /// Compares the two dictionaries and finds the elements that differ between the two
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="solution2"></param>
        /// <param name="unchanged"></param>
        /// <returns></returns>
        public int differentElements(Dictionary<string, bool> solution, Dictionary<string, bool> solution2, Dictionary<string, bool> unchanged)
        {
            int total = 0;
            foreach (KeyValuePair<string, bool> x in solution)
            {
                if (solution2[x.Key] != x.Value)
                {
                    if (!eliminated.ContainsKey(x.Key))
                        eliminated.Add(x.Key, x.Value);

                    if (unchanged.ContainsKey(x.Key))
                    {
                        unchanged.Remove(x.Key);
                    }
                    total++;

                }
                else
                {
                    if (!(unchanged.ContainsKey(x.Key)))
                    {
                        unchanged.Add(x.Key, x.Value);
                    }
                }

            }
            return total;
        }

        /// <summary>
        /// Generates the reversed form of the given solution
        /// </summary>
        /// <param name="input"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public BoolExpr reversed(Dictionary<string, bool> input, Context c)
        {
            BoolExpr output = c.MkFalse();
            foreach (KeyValuePair<string, bool> x in input)
            {

                string[] coords = x.Key.Substring(1).Split('c');
                BoolExpr tempVar = vars[Int32.Parse(coords[1]), Int32.Parse(coords[0])];


                if (x.Value == false)
                    output = c.MkOr(output, tempVar);
                if (x.Value == true)
                    output = c.MkOr(output, c.MkNot(tempVar));
            }



            return output;
        }

        /// <summary>
        /// Gathers and creates the big boolean equation representing the current visible mine field
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public List<BoolExpr> gatherConstraints(Context c)
        {
            // LinkedList<BoolExpr> temp = new LinkedList<BoolExpr>();
            List<BoolExpr> temp2 = new List<BoolExpr>();
            List<BoolExpr> temp3 = new List<BoolExpr>();
            //LinkedList<LinkedList<BoolExpr>> perms;
            List<Tuple<List<BoolExpr>, List<BoolExpr>>> nchosenk;
            List<BoolExpr> output = new List<BoolExpr>();


            //c.mkex

            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    if (field[x][y].Enabled == false && field[x][y].Text != "")
                    {
                        // temp = getEmptyNeighbors(x, y);
                        temp2 = new List<BoolExpr>();
                        temp3 = new List<BoolExpr>();
                        temp2.AddRange(getEmptyNeighbors(x, y));
                        temp3.AddRange(getMineNeighbors(x, y));
                        // MessageBox.Show("minesnearby = " + temp3.Count);
                        if (temp2.Count > 0)
                        {
                            // perms = getPermutations(temp);
                            nchosenk = nchoosek(temp2, Int32.Parse(field[x][y].Text) - temp3.Count);

                            BoolExpr allcombined = c.MkFalse();

                            foreach (Tuple<List<BoolExpr>, List<BoolExpr>> z in nchosenk)
                            {
                                BoolExpr mines = c.MkTrue();
                                BoolExpr clears = c.MkTrue();
                                foreach (BoolExpr x2 in z.Item1)
                                {
                                    mines = c.MkAnd(mines, x2);
                                }
                                foreach (BoolExpr x2 in z.Item2)
                                {
                                    clears = c.MkAnd(clears, c.MkNot(x2));
                                }

                                BoolExpr combined = c.MkAnd(clears, mines);
                                //MessageBox.Show("combined = " + c.MkAnd(clears, mines).ToString());

                                allcombined = c.MkOr(combined, allcombined);
                                //MessageBox.Show("x" + x + " y" + y + " c" + allcombined.ToString());
                            }


                            output.Add(allcombined);

                        }

                    }


                }
            }
            return output;

        }


        /// <summary>
        /// A second version of the gatherconstraints that also returns the set of all variables that are used to generate it
        /// </summary>
        /// <param name="c"></param>
        /// <param name="currentVars"></param>
        /// <returns></returns>
        public List<BoolExpr> gatherConstraints2(Context c, out HashSet<BoolExpr> currentVars)
        {
            // LinkedList<BoolExpr> temp = new LinkedList<BoolExpr>();
            List<BoolExpr> temp2 = new List<BoolExpr>();
            List<BoolExpr> temp3 = new List<BoolExpr>();
            //LinkedList<LinkedList<BoolExpr>> perms;
            List<Tuple<List<BoolExpr>, List<BoolExpr>>> nchosenk;
            List<BoolExpr> output = new List<BoolExpr>();

            currentVars = new HashSet<BoolExpr>();
            //c.mkex

            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    if (field[x][y].Enabled == false && field[x][y].Text != "")
                    {
                        // temp = getEmptyNeighbors(x, y);
                        temp2 = new List<BoolExpr>();
                        temp3 = new List<BoolExpr>();
                        temp2.AddRange(getEmptyNeighbors(x, y));
                        foreach (BoolExpr theVars in temp2)
                            currentVars.Add(theVars);
                        temp3.AddRange(getMineNeighbors(x, y));
                        // MessageBox.Show("minesnearby = " + temp3.Count);
                        if (temp2.Count > 0)
                        {
                            // perms = getPermutations(temp);
                            nchosenk = nchoosek(temp2, Int32.Parse(field[x][y].Text) - temp3.Count);

                            BoolExpr allcombined = c.MkFalse();

                            foreach (Tuple<List<BoolExpr>, List<BoolExpr>> z in nchosenk)
                            {
                                BoolExpr mines = c.MkTrue();
                                BoolExpr clears = c.MkTrue();
                                foreach (BoolExpr x2 in z.Item1)
                                {
                                    mines = c.MkAnd(mines, x2);
                                }
                                foreach (BoolExpr x2 in z.Item2)
                                {
                                    clears = c.MkAnd(clears, c.MkNot(x2));
                                }

                                BoolExpr combined = c.MkAnd(clears, mines);
                                //MessageBox.Show("combined = " + c.MkAnd(clears, mines).ToString());

                                allcombined = c.MkOr(combined, allcombined);
                                //MessageBox.Show("x" + x + " y" + y + " c" + allcombined.ToString());
                            }


                            output.Add(allcombined);

                        }

                    }


                }
            }
            return output;

        }


        /// <summary>
        /// Combinations
        /// getting all the possible variations of n choose k with order matters
        ///  a,b,c choose 2 
        /// has values
        /// a,b
        /// a,c
        /// b,a
        /// b,c
        /// c,a
        /// c,b
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public List<Tuple<List<BoolExpr>, List<BoolExpr>>> nchoosek(List<BoolExpr> n, int k)
        {
            List<BoolExpr> ntemp = new List<BoolExpr>();
            List<Tuple<List<BoolExpr>, List<BoolExpr>>> output = new List<Tuple<List<BoolExpr>, List<BoolExpr>>>();
            List<BoolExpr> chosen = new List<BoolExpr>();
            List<BoolExpr> notChosen = new List<BoolExpr>();
            if (k > 0)
            {
                ntemp.AddRange(n);

                if (k == 1)
                {
                    foreach (BoolExpr x in n)
                    {
                        ntemp = new List<BoolExpr>();
                        ntemp.AddRange(n);
                        ntemp.Remove(x);
                        chosen = new List<BoolExpr>();
                        chosen.Add(x);
                        notChosen = ntemp;
                        output.Add(new Tuple<List<BoolExpr>, List<BoolExpr>>(chosen, notChosen));
                    }
                    return output;
                }
                else
                {
                    foreach (BoolExpr x in n)
                    {
                        ntemp = new List<BoolExpr>();
                        ntemp.AddRange(n);
                        ntemp.Remove(x);
                        chosen = new List<BoolExpr>();
                        chosen.Add(x);
                        notChosen = ntemp;
                        foreach (Tuple<List<BoolExpr>, List<BoolExpr>> y in nchoosek(notChosen, k - 1))
                        {
                            y.Item1.Add(x);
                            output.Add(y);
                        }
                    }
                    return output;
                }
            }
            else
            {

                chosen = new List<BoolExpr>();

                notChosen = n;
                output.Add(new Tuple<List<BoolExpr>, List<BoolExpr>>(chosen, notChosen));
                return output;
            }



        }


        /// <summary>
        /// Getting every possible permutation of the variables
        /// This is a copy of the code i wrote for the kenken solver
        /// </summary>
        /// <param name="variable_cells"></param>
        /// <returns></returns>
        public LinkedList<LinkedList<BoolExpr>> getPermutations(LinkedList<BoolExpr> variable_cells)
        {

            LinkedList<LinkedList<BoolExpr>> output = new LinkedList<LinkedList<BoolExpr>>();

            if (variable_cells.Count == 1)
            {
                output.AddLast(variable_cells);
                return output;
            }

            LinkedList<BoolExpr> temp = new LinkedList<BoolExpr>(variable_cells.ToArray().Skip(1));

            LinkedList<LinkedList<BoolExpr>> smaller_permutations = getPermutations(temp);

            foreach (LinkedList<BoolExpr> x in smaller_permutations)
            {
                for (int y = 0; y < x.Count; y++)
                {
                    temp = new LinkedList<BoolExpr>(x.ToArray());
                    temp.AddBefore(temp.Find(temp.ElementAt(y)), new LinkedListNode<BoolExpr>(variable_cells.First.Value));
                    output.AddLast(temp);
                }
                temp = new LinkedList<BoolExpr>(x.ToArray());
                temp.AddLast(new LinkedListNode<BoolExpr>(variable_cells.First.Value));
                output.AddLast(temp);

            }

            return output;
        }

        /// <summary>
        /// gets all the variables of the squares next to the input square that are mines
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LinkedList<BoolExpr> getMineNeighbors(int x, int y)
        {
            LinkedList<BoolExpr> output = new LinkedList<BoolExpr>();

            //3 spots above the current spot
            if (isValidSpot(x - 1, y - 1) && (field[x - 1][y - 1].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x - 1, y - 1]);
            if (isValidSpot(x, y - 1) && (field[x][y - 1].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x, y - 1]);
            if (isValidSpot(x + 1, y - 1) && (field[x + 1][y - 1].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x + 1, y - 1]);
            //2 spots next to the curretn spot
            if (isValidSpot(x - 1, y) && (field[x - 1][y].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x - 1, y]);
            if (isValidSpot(x + 1, y) && (field[x + 1][y].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x + 1, y]);
            //3 spots below the current spot
            if (isValidSpot(x - 1, y + 1) && (field[x - 1][y + 1].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x - 1, y + 1]);
            if (isValidSpot(x, y + 1) && (field[x][y + 1].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x, y + 1]);
            if (isValidSpot(x + 1, y + 1) && (field[x + 1][y + 1].Text == "F" || areFlagged[x, y] == true))
                output.AddLast(vars[x + 1, y + 1]);

            return output;
        }

        /// <summary>
        /// gets all the variables of the squares next to the input square that are empty
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LinkedList<BoolExpr> getEmptyNeighbors(int x, int y)
        {
            LinkedList<BoolExpr> output = new LinkedList<BoolExpr>();

            //3 spots above the current spot
            int curx = x - 1;
            int cury = y - 1;
            if (isValidSpot(curx, cury) && field[x - 1][y - 1].Text == "" && field[x - 1][y - 1].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x - 1, y - 1]);
            if (isValidSpot(x, y - 1) && field[x][y - 1].Text == "" && field[x][y - 1].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x, y - 1]);
            if (isValidSpot(x + 1, y - 1) && field[x + 1][y - 1].Text == "" && field[x + 1][y - 1].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x + 1, y - 1]);
            //2 spots next to the curretn spot
            if (isValidSpot(x - 1, y) && field[x - 1][y].Text == "" && field[x - 1][y].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x - 1, y]);
            if (isValidSpot(x + 1, y) && field[x + 1][y].Text == "" && field[x + 1][y].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x + 1, y]);
            //3 spots below the current spot
            if (isValidSpot(x - 1, y + 1) && field[x - 1][y + 1].Text == "" && field[x - 1][y + 1].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x - 1, y + 1]);
            if (isValidSpot(x, y + 1) && field[x][y + 1].Text == "" && field[x][y + 1].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x, y + 1]);
            if (isValidSpot(x + 1, y + 1) && field[x + 1][y + 1].Text == "" && field[x + 1][y + 1].Enabled == true && areFlagged[x, y] == false)
                output.AddLast(vars[x + 1, y + 1]);

            return output;
        }

        /// <summary>
        /// gets all the buttons of the squares next to the input square that are empty
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public List<Button> getEmptyNeighborsB(int x, int y)
        {
            List<Button> output = new List<Button>();

            //3 spots above the current spot
            int curx = x - 1;
            int cury = y - 1;
            if (isValidSpot(curx, cury) && field[x - 1][y - 1].Text == "" && field[x - 1][y - 1].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x - 1][ y - 1]);
            if (isValidSpot(x, y - 1) && field[x][y - 1].Text == "" && field[x][y - 1].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x][ y - 1]);
            if (isValidSpot(x + 1, y - 1) && field[x + 1][y - 1].Text == "" && field[x + 1][y - 1].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x + 1][ y - 1]);
            //2 spots next to the curretn spot
            if (isValidSpot(x - 1, y) && field[x - 1][y].Text == "" && field[x - 1][y].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x - 1][ y]);
            if (isValidSpot(x + 1, y) && field[x + 1][y].Text == "" && field[x + 1][y].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x + 1][ y]);
            //3 spots below the current spot
            if (isValidSpot(x - 1, y + 1) && field[x - 1][y + 1].Text == "" && field[x - 1][y + 1].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x - 1][ y + 1]);
            if (isValidSpot(x, y + 1) && field[x][y + 1].Text == "" && field[x][y + 1].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x][ y + 1]);
            if (isValidSpot(x + 1, y + 1) && field[x + 1][y + 1].Text == "" && field[x + 1][y + 1].Enabled == true && areFlagged[x, y] == false)
                output.Add(field[x + 1][ y + 1]);

            return output;
        }

        /// <summary>
        /// creates and adds the variables for the entire mine field to the z3 context
        /// </summary>
        /// <param name="c"></param>
        public void makeVars(Context c)
        {
            //the variables for every spot
            vars = new BoolExpr[colCount, rowCount];



            //setup the variables and add them to the z3 engine
            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {

                    vars[x, y] = c.MkBoolConst("r" + y + "c" + x);

                }
            }
        }


        /// <summary>
        /// A attempt to use exists all as a solver (didnt work)
        /// </summary>
        /// <param name="c"></param>
        /// <param name="constraints"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public Dictionary<string, bool> ExistsAll(Context c, List<BoolExpr> constraints, HashSet<BoolExpr> targets)
        {
            //using (Context c = new Context())
            //{
            //    makeVars(c);



            //the z3 engine initialization

            //variable that stores the constraints as we generate them
            //List<BoolExpr> constraints = new List<BoolExpr>();


            BoolExpr goal = c.MkAnd(constraints.ToArray());

            Solver s = c.MkSolver();

            //run z3 on the constraints
            s.Assert(goal);
            RealExpr[] lamb = new RealExpr[1];

            lamb[0] = c.MkRealConst("lamb");

            foreach (BoolExpr oneTarg in targets)
            {
                BoolExpr exist = c.MkExists(lamb, oneTarg, 1);


                s.Assert(exist);

            }




            //s.Assert(c.MkExists(null, null, 0, null, null, null, null));
            //getting the results
            Status st = s.Check();

            Dictionary<string, bool> output = new Dictionary<string, bool>();


            //if its is satisfiable
            if (st.ToString() == "SATISFIABLE")
            {
                MessageBox.Show("SAT");
                //for every variable in the model....
                MessageBox.Show(s.ToString());
                MessageBox.Show(s.Model.ToString());

                foreach (FuncDecl x in s.Model.ConstDecls)
                {

                    string name = x.Name.ToString();



                    bool value = Boolean.Parse(s.Model.ConstInterp(x).ToString());


                    output.Add(name, value);

                    //outp[Int32.Parse(coords[0]), Int32.Parse(coords[1])] = s.Model.ConstInterp(x).ToString();


                    //Console.WriteLine(x.Name.ToString() + " " + s.Model.ConstInterp(x).ToString());
                    //if (s.Model.ConstInterp(x).BoolValue == Z3_lbool.Z3_L_TRUE)
                    //{



                    //   // outp[int.Parse("" + x.Name.ToString()[0]), int.Parse("" + x.Name.ToString()[1])] = "" + (int.Parse("" + x.Name.ToString()[2]) + 1);


                    //}

                }
            }
            else
            {

                //MessageBox.Show("UNSAT");
                //for every variable in the model....
                //MessageBox.Show(s.ToString());
                //MessageBox.Show(s.Model.ToString());
                /*
                foreach (FuncDecl x in s.Model.ConstDecls)
                {

                    string name = x.Name.ToString();



                    bool value = Boolean.Parse(s.Model.ConstInterp(x).ToString());


                    output.Add(name, value);

                    //outp[Int32.Parse(coords[0]), Int32.Parse(coords[1])] = s.Model.ConstInterp(x).ToString();


                    //Console.WriteLine(x.Name.ToString() + " " + s.Model.ConstInterp(x).ToString());
                    //if (s.Model.ConstInterp(x).BoolValue == Z3_lbool.Z3_L_TRUE)
                    //{



                    //   // outp[int.Parse("" + x.Name.ToString()[0]), int.Parse("" + x.Name.ToString()[1])] = "" + (int.Parse("" + x.Name.ToString()[2]) + 1);


                    //}

                }
                 * */


                
            }
                return null;

            return output;

            //}




        }

        /// <summary>
        /// Checks if the given target satisfies exists for the giant boolean equation
        /// returns the satisfying values if it does and null if it does not
        /// </summary>
        /// <param name="c"></param>
        /// <param name="constraints"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Dictionary<string, bool> ExistsOne(Context c, List<BoolExpr> constraints, BoolExpr target)
        {
            //using (Context c = new Context())
            //{
            //    makeVars(c);



            //the z3 engine initialization

            //variable that stores the constraints as we generate them
            //List<BoolExpr> constraints = new List<BoolExpr>();


            BoolExpr goal = c.MkAnd(constraints.ToArray());

            Solver s = c.MkSolver();

            //run z3 on the constraints
            s.Assert(goal);
            RealExpr[] lamb = new RealExpr[1];

            lamb[0] = c.MkRealConst("lamb");

            BoolExpr exist = c.MkExists(lamb, target, 1);


            s.Assert(exist);






            //s.Assert(c.MkExists(null, null, 0, null, null, null, null));
            //getting the results
            Status st = s.Check();

            Dictionary<string, bool> output = new Dictionary<string, bool>();


            //if its is satisfiable
            if (st.ToString() == "SATISFIABLE")
            {
                //for every variable in the model....
                foreach (FuncDecl x in s.Model.ConstDecls)
                {

                    string name = x.Name.ToString();

                    if (name == target.ToString())
                    {

                        bool value = Boolean.Parse(s.Model.ConstInterp(x).ToString());


                        output.Add(name, value);
                    }
                    //outp[Int32.Parse(coords[0]), Int32.Parse(coords[1])] = s.Model.ConstInterp(x).ToString();


                    //Console.WriteLine(x.Name.ToString() + " " + s.Model.ConstInterp(x).ToString());
                    //if (s.Model.ConstInterp(x).BoolValue == Z3_lbool.Z3_L_TRUE)
                    //{



                    //   // outp[int.Parse("" + x.Name.ToString()[0]), int.Parse("" + x.Name.ToString()[1])] = "" + (int.Parse("" + x.Name.ToString()[2]) + 1);


                    //}

                }
            }
            else
                return null;

            return output;

            //}




        }


        /// <summary>
        /// Trys to solve the given boolean constraint
        /// Returns the satisfiying assignments if sat
        /// Returns null if unsat
        /// </summary>
        /// <param name="c"></param>
        /// <param name="constraints"></param>
        /// <returns></returns>
        public Dictionary<string, bool> SolveOne(Context c, List<BoolExpr> constraints)
        {
            //using (Context c = new Context())
            //{
            //    makeVars(c);



            //the z3 engine initialization

            //variable that stores the constraints as we generate them
            //List<BoolExpr> constraints = new List<BoolExpr>();


            BoolExpr goal = c.MkAnd(constraints.ToArray());

            Solver s = c.MkSolver();

            //run z3 on the constraints
            s.Assert(goal);







            // s.Assert(c.MkExists(null,null,0,null,null,null,null));
            //getting the results
            Status st = s.Check();

            Dictionary<string, bool> output = new Dictionary<string, bool>();


            //if its is satisfiable
            if (st.ToString() == "SATISFIABLE")
            {
                //for every variable in the model....
                foreach (FuncDecl x in s.Model.ConstDecls)
                {

                    string name = x.Name.ToString();

                    bool value = Boolean.Parse(s.Model.ConstInterp(x).ToString());


                    output.Add(name, value);
                    //outp[Int32.Parse(coords[0]), Int32.Parse(coords[1])] = s.Model.ConstInterp(x).ToString();


                    //Console.WriteLine(x.Name.ToString() + " " + s.Model.ConstInterp(x).ToString());
                    //if (s.Model.ConstInterp(x).BoolValue == Z3_lbool.Z3_L_TRUE)
                    //{



                    //   // outp[int.Parse("" + x.Name.ToString()[0]), int.Parse("" + x.Name.ToString()[1])] = "" + (int.Parse("" + x.Name.ToString()[2]) + 1);


                    //}

                }
            }
            else
                return null;

            return output;

            //}
        }

        /// <summary>
        /// Closes the child form (the solver form) if this form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void closingMethod(object sender, FormClosingEventArgs e)
        {
            child.Close();
        }

        /// <summary>
        /// adds the closing handler to this forms closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += closingMethod;
            // test();
        }


    }




}
