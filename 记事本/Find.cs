using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 记事本
{
    public partial class Find : Form
    {
        RichTextBox rtb;
        public Find()
        {
            InitializeComponent();
        }
       


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void Find_Load(object sender, EventArgs e)
        {
            rtb = (RichTextBox)this.Owner.Controls["richTextBox1"];

        }

        private void findnext_Click(object sender, EventArgs e)
        {
            int index = rtb.Find(textBox1.Text, 0, RichTextBoxFinds.None);
            rtb.Select(index,textBox1.Text.Length);
            rtb.Focus();
        }

       

       
    }
}
