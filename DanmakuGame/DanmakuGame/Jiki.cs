using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DanmakuGame
{
    /// <summary>
    /// 自機クラス
    /// </summary>
    class Jiki : FormDanmaku
    {
        private Timer gameTimer;
        private Rectangle player;
        private bool DoubleBuffered;
        private object Size;
        private const int PlayerSpeed = 10;

        public Jiki()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(110, 110);

            gameTimer = new Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.Paint += FormDanbaku_Paint;
            this.KeyDown += FormDanbaku_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {

        }
        private void FormDanbaku_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FormDanbaku_KeyDown(object sender, KeyEventArgs e)
        {
            new Form().Show();
        }
    }
}
