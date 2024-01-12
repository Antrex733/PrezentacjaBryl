using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrezentacjaBryl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void mfbtnLabolatorium_Click(object sender, EventArgs e)
        {
            foreach (Form mfitem in Application.OpenForms)
                if (mfitem.Name == "Labolatorium")
                {
                    Hide();
                    mfitem.Show();
                    return;
                }

            Labolatorium mf = new Labolatorium();
            Hide();
            mf.Show();
        }

        private void mfbtnIndywidualny_Click(object sender, EventArgs e)
        {
            foreach (Form mfitem in Application.OpenForms)
                if (mfitem.Name == "Indywidualny")
                {
                    Hide();
                    mfitem.Show();
                    return;
                }

            Indywidualny mf = new Indywidualny();
            Hide();
            mf.Show();
        }
    }
}
