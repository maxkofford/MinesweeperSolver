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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        Form1 parentGui;
        public Form3(Form1 parent)
        {
            parentGui = parent;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentGui.callLeftClick(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentGui.callRightClick(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parentGui.findValues();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                parentGui.findAll(parentGui.guessOne3);
            if(radioButton2.Checked == true)
            parentGui.findAll(parentGui.guessOne);
            if (radioButton1.Checked == true)
            parentGui.findAll(parentGui.guessOne2);
        }

        private void btnSolveOne2_Click(object sender, EventArgs e)
        {
            parentGui.findValues2();
        }

        private void btnSolveAll2_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                parentGui.findAll2(parentGui.guessOne3);
            if (radioButton2.Checked == true)
                parentGui.findAll2(parentGui.guessOne);
            if (radioButton1.Checked == true)
                parentGui.findAll2(parentGui.guessOne2);
        }

        private void btnSolveOne3_Click(object sender, EventArgs e)
        {
            parentGui.findValues31();
        }

        private void btnSolveAll3_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                parentGui.findAll3(parentGui.guessOne3);
            if (radioButton2.Checked == true)
                parentGui.findAll3(parentGui.guessOne);
            if (radioButton1.Checked == true)
                parentGui.findAll3(parentGui.guessOne2);
        }

        

        private void label6_dlick(object sender, EventArgs e)
        {

        }

        private void button3_Clickd_1(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        
    }
}
