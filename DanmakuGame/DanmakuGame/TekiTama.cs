using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanmakuGame
{
    internal class TekiTama : FormDanmaku
    {
        private Timer gameTimer;
        private Timer shootTimer;
        private Rectangle player;
        private List<Rectangle> tamas;
        private EventHandler shootTimer_Tick;
        //private PaintEventHandler TekiTama_Paint;
        //private KeyEventHandler TekiTama_KeyDown;
        private const int PlayerSpeed = 10;
        private const int tamasSpeed = 15;
        private readonly EventHandler gameTimer_Tick;

        Rectangle[,] trimRects = new Rectangle[4, 3];
        int width, height, interval = 10, time_move, endOftime = 0;
        //Vector2 position = new(200, 125), speed = new(0, 0);

        public TekiTama()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(800, 600);
            this.Text = "Shooting Game";

            player = new Rectangle(this.ClientSize.Width / 2, this.ClientSize.Height - 50, 50, 50);
            tamas = new List<Rectangle>();

            gameTimer = new Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            shootTimer = new Timer();
            shootTimer.Interval = 500;
            shootTimer.Tick += shootTimer_Tick;
            shootTimer.Start();

            this.Paint += TekiTama_Paint;
            this.KeyDown += TekiTama_KeyDown;
        }

        public void Progress()
        {
            tamas = new List<Rectangle>();

            gameTimer = new Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Start();

            this.Paint += TekiTama_Paint;
            this.KeyDown += TekiTama_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Movetamas();
            this.Invalidate(); //フォームを再描画
        }

        private void ShootTimer_Tick(object sender, EventArgs e)
        {
            tamas.Add(new Rectangle(player.X + player.Width / 2 - 2, player.Y, 5, 20));
        }

        private void Movetamas()
        {
            for (int i = tamas.Count - 1; i >= 0; i-- )
            {
                Rectangle tama = tamas[i];
                tama = new Rectangle(tama.X, tama.Y - tamasSpeed, tama.Width, tama.Height);
                if (tama.Y < 0)
                {
                    tamas.RemoveAt(i);
                }
                else
                {
                    tamas[i] = tama;
                }
            }
        }

        private void TekiTama_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Blue, player);

            foreach (Rectangle tama in tamas) ;
        }

        private void TekiTama_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (player.X > 0)
                        player.X -= PlayerSpeed;
                    break;
                case Keys.Right:
                    if (player.X < this.ClientSize.Width - player.Width)
                        player.X += PlayerSpeed;
                    break;
                case Keys.Space:
                    tamas.Add(new Rectangle(player.X + player.Width / 2 - 2, player.Y, 5, 20));
                    break;
            }
        }
        public static void Danmaku()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TekiTama());
        }

        }


}
