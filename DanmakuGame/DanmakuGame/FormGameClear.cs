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

        
        private void button1_KeyDown_1(object sender, KeyEventArgs e)
        {
            //FormGameClear.Exit();
            FormStart Fdan = new FormStart();
            Fdan.Show();
            this.Visible = false;
        }
    }
}
