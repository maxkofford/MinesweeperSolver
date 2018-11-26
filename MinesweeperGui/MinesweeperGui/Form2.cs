using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MinesweeperGui
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 temp;
            if (radioButton1.Checked)
            {
                temp = new Form1();
                temp.Show();
            }
            if (radioButton2.Checked)
            {
                temp = new Form1(16, 16, 40);
                temp.Show();

                //Form3 temp2 = new Form3(temp);
                //temp2.Show();
            }
            if (radioButton3.Checked)
            {
                temp = new Form1(30, 16, 99);
                temp.Show();
                // new Form1(30, 16, 99).Show();
            }
            if (radioButton4.Checked)
            {
                int col, row, count;
                if (Int32.TryParse(textBox1.Text, out col) && Int32.TryParse(textBox2.Text, out row) && Int32.TryParse(textBox3.Text, out count))
                    if (col < 1 || row < 1 || count > col * row)
                        MessageBox.Show("Sorry not valid sizes");
                    else
                    {
                        temp = new Form1(col, row, count);
                        temp.Show();
                        //new Form1(col, row, count).Show();
                    }

                else
                    MessageBox.Show("Sorry you must only put in integer numbers");
            }
        }

        Form1 temp;
        int guesser;
        int method;
        int testcount;
        int col, row, count;
        int testsLeft;
        List<int> guessCountWon;
        List<int> guessCountLost;
        List<double> timeTaken;
        int wins;
        int losses;

        private void button2_Click(object sender, EventArgs e)
        {
            wins = 0;
            losses = 0;
            guessCountWon = new List<int>();
            guessCountLost = new List<int>();
            timeTaken = new List<double>();
          //Form1 temp;
            guesser = Int32.Parse(textBox4.Text);
             method = Int32.Parse(textBox5.Text);
             testcount = Int32.Parse(textBox6.Text);
          
            col = 9;
            row = 9;
            count = 10;


            if (radioButton1.Checked)
            {
                col = 9;
                row = 9;
                count = 10;

               // temp = new Form1();
               // temp.Show();
            }
            if (radioButton2.Checked)
            {
                col = 16;
                row = 16;
                count = 40;
                //temp = new Form1(16, 16, 40);
                //temp.Show();

                //Form3 temp2 = new Form3(temp);
                //temp2.Show();
            }
            if (radioButton3.Checked)
            {
                col = 30;
                row = 16;
                count = 99;
                //temp = new Form1(30, 16, 99);
                //temp.Show();
                // new Form1(30, 16, 99).Show();
            }
            if (radioButton4.Checked)
            {
                int col2, row2, count2;
                if (Int32.TryParse(textBox1.Text, out col2) && Int32.TryParse(textBox2.Text, out row2) && Int32.TryParse(textBox3.Text, out count2))
                    if (col2 < 1 || row2 < 1 || count2 > col2 * row2)
                        MessageBox.Show("Sorry not valid sizes");
                    else
                    {
                        col = col2;
                        row = row2;
                        count = count2;

                        //new Form1(col, row, count).Show();
                    }

                else
                    MessageBox.Show("Sorry you must only put in integer numbers");

            }

            testsLeft = testcount;
            testingCallback(false, false, -1,0);

           


        }

        private double averageThem(List<int> inputs)
        {
           
                double output = 0;
                foreach (int x in inputs)
                {
                    output += x;
                }
                output = output / inputs.Count;
                return output;
            
        }

        private double averageThem(List<double> inputs)
        {
            
                double output = 0;
                foreach (double x in inputs)
                {
                    output += x;
                }
                output = output / inputs.Count;
                return output;
           
        }

        bool hasShown = false;
        public void testingCallback(bool succeded, bool wasGuessing, int guessCount,double timetaken)
        {

            if (guessCount > -1)
            {

                if (testsLeft > 0)
                {
                    if (succeded)
                    {
                        wins++;
                        guessCountWon.Add(guessCount);
                        timeTaken.Add(timetaken);
                    }
                    else
                    {
                        losses++;
                        guessCountLost.Add(guessCount);
                    }
                    testsLeft--;

                    temp = new Form1(col, row, count, this, guesser, method);
                   // temp.Show();

                }
                else
                {
                    showResults();
                }
            }
            else
            {
                temp = new Form1(col, row, count, this, guesser, method);
               // temp.Show();
            }
        }

        private void showResults()
        {
            if (hasShown == false)
            {
                hasShown = true;
                double averageGuessWin = averageThem(guessCountWon);
                double averageGuessLose = averageThem(guessCountLost);
                double averageWinTime = averageThem(timeTaken);

                MessageBox.Show("Tested " + testcount + " games." + "\n" +
                    "Won " + wins + " times." + "\n" +
                    "Lost " + losses + " times." + "\n" +
                    "Guessed an average of " + averageGuessWin + " times on a win." + "\n" +
                    "Guessed an average of " + averageGuessLose + " times on a loss." + "\n" +
                    "Took an average of " + averageWinTime + " miliseconds on a win." + "\n");

            }

            this.Close();
        }

        private void btnWincheck_Click(object sender, EventArgs e)
        {
            Form1 temp;
            if (radioButton1.Checked)
            {
                temp = new Form1(9, 9, 10, true);
                temp.Show();
            }
            if (radioButton2.Checked)
            {
                temp = new Form1(16, 16, 40,true);
                temp.Show();

                //Form3 temp2 = new Form3(temp);
                //temp2.Show();
            }
            if (radioButton3.Checked)
            {
                temp = new Form1(30, 16, 99, true);
                temp.Show();
                // new Form1(30, 16, 99).Show();
            }
            if (radioButton4.Checked)
            {
                int col, row, count;
                if (Int32.TryParse(textBox1.Text, out col) && Int32.TryParse(textBox2.Text, out row) && Int32.TryParse(textBox3.Text, out count))
                    if (col < 1 || row < 1 || count > col * row)
                        MessageBox.Show("Sorry not valid sizes");
                    else
                    {
                        temp = new Form1(col, row, count,true);
                        temp.Show();
                        //new Form1(col, row, count).Show();
                    }

                else
                    MessageBox.Show("Sorry you must only put in integer numbers");
            }
        }

        private void btncolorchk_Click(object sender, EventArgs e)
        {
            //outside.movePlus50();
            System.Threading.Thread.Sleep(2000);
            //outside.colorcheck();
            //outside.centerize();
            //outside.moveTopLeft();
            //outside.moveBottomRight();
            outside.showInfo();
        }

       


    }
}
