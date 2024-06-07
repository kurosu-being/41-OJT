using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanmakuGame
{
    public partial class FormGameOver : Form
    {
        public FormGameOver()
        {
            InitializeComponent();
        }
        private void FormContinue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
            }

            this.Visible = false;

            FormDanmaku Fdan = new FormDanmaku();
            Fdan.Show();
        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.KeyCode == Keys.S)
            //{
            //}

            this.Visible = false;

            //FormDanmaku.Exit();
            FormStart Fdan = new FormStart();
            Fdan.Show();
        }
    }
}


