using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanmakuGame
{
    class TekiTama : FormDanmaku
    {
        private Timer gameTimer;
        private List<Rectangle> tamas;
        private const int tamasSpeed = 15;
        private readonly EventHandler gameTimer_Tick;

        public void Progress()
        {
            tamas = new List<Rectangle>();

            gameTimer = new Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Start();

            this.Paint += FormDanmaku_Paint;
            this.KeyDown += FormDanmaku_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Movetamas();
            this.Invalidate(); //フォームを再描画
        }



        private void Movetamas()
        {
            for (int i = tamas.Count - 1; i >= 0; i-- )
            {
                Rectangle tama = tamas[i];
                tamas.Y -= tamasSpeed;
                if (tamas.Y < 0)
                {

                }
            }
        }

        private void FormDanmaku_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FormDanmaku_KeyDown(object sender, KeyEventArgs e)
        {

        }


        }


}
