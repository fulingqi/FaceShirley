using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace UI
{
    public partial class Mask : Form
    {
        public Mask()
        {
            InitializeComponent();
        }


        private void Mask_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FaceRegis.staus = 0;
            this.Close();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MingYiPrivacyPolicy m = new MingYiPrivacyPolicy();
            m.StartPosition = FormStartPosition.CenterScreen;
            m.Show();

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            MingYiPrivacyPolicy m = new MingYiPrivacyPolicy();
            m.StartPosition = FormStartPosition.CenterScreen;
            m.Show();
        }
    }
}
