using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace ImitModelling
{
    public partial class Form1 : Form
    {
        private String fileName;
        private Grid field;
        public Form1()
        {
            field = new Grid();
            InitializeComponent();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            fileName = ofd.FileName;
            field.LoadRoom(fileName);
            this.pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            field.generateAgents();
            field.Draw(this.pictureBox1.Width, this.pictureBox1.Height, e.Graphics);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Invalidate();
        }
    }    
}
