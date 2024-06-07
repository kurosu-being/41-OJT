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
    public partial class FormGameClear : Form
    {
        public FormGameClear()
        {
            InitializeComponent();
        }

        private void FormGameClear_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {

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
