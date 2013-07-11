using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GPIBlibrary;

namespace gpibTestApplication
{
    public partial class Form1 : Form
    {
        GPIB bus = new GPIB();

        public Form1()
        {
            InitializeComponent();

           
              List<String> tList = bus.portlist();

              comboBox1.Items.Add("Select COM port...");
              comboBox1.Items.AddRange(tList.ToArray());
              comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // bus.start(comboBox1.Text, 1000);
                bus.start(comboBox1.Text, 1000);   
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bus.write( "A1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bus.write( "B1");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bus.write( "A2");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bus.write("B2");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bus.write( "A3");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bus.write("B3");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bus.write("A4");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bus.write("B4");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bus.write("A5");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bus.write("B5");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bus.write("A6");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bus.write("B6");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int a = int.Parse(textBox1.Text);
            bus.address(a);
        }

       
    }
}
