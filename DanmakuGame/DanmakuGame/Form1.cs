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
    public partial class FormDanmaku : Form
    {
        public enum Direct
        {
            None = 0,
            Left = 1,
            Right = 2,
        }

        Direct direct = Direct.None;

        public FormDanmaku()
        {
            InitializeComponent();

            KeyDown += FormDanmaku_KeyDown;
            KeyUp += FormDanmaku_KeyUp;

            timer1.Tick += timer1_Tick;
            timer1.Interval = 50;
        }
        private void FormDanmaku_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                direct = Direct.Left;
            }
            else if (e.KeyCode == Keys.Right)
            {
                direct = Direct.Right;
            }
            else if (e.KeyCode == Keys.Space)
            {
                Bulletlaunch(); 
            }
            else if (e.KeyCode == Keys.S)
            {
                GameStart();
            }
        }
        void MoveLeft()
        {
            Point pt = pictureBoxJiki.Location;
            pt.X -= 10;
            pictureBoxJiki.Location = pt;
        }
        void MoveRight()
        {
            Point pt = pictureBoxJiki.Location;
            pt.X += 10; //pt=X+10
            pictureBoxJiki.Location = pt;
        }

        private void FormDanmaku_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                direct = Direct.None;
            }
            else if (e.KeyCode == Keys.Right)
            {
                direct = Direct.None;
            }
        }
        void GameStart()
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBoxJikMove();
            BulletsMove();
        }
        void pictureBoxJikMove()
        {
            if(direct == Direct.Left)
            {
                MoveLeft();
            }
            else if (direct == Direct.Right)
            {
                MoveRight();
            }
        }

        private void FormDanmaku_Load(object sender, EventArgs e)
        {

        }
        void Bulletlaunch()
        {
            Point pt = pictureBoxJiki.Location;
            int width = pictureBoxJiki.Size.Width;
            int centerX = pt.X + width / 2; // 上部中心の半分ってこと

            int BULLET_WIDTH = 2;
            int BULLET_HEIGHT = 10; //フィールド変数

            Point point = new Point(centerX - BULLET_WIDTH / 2, pt.Y);
            PictureBox bullet = new PictureBox();
            bullet.Location = point;
            bullet.Size = new Size(BULLET_WIDTH, BULLET_HEIGHT);
            bullet.BackColor = Color.White;
            bullet.Parent = panel1;

            Bullets.Add(bullet);
        }

        List<PictureBox> _bullets = new List<PictureBox>(); //なんだこの<>は
        List<PictureBox> Bullets
        {
            get{
               _bullets = _bullets.Where(x => !x.IsDisposed).ToList();
                return _bullets;
            }
        }
        void BulletsMove()
        {
            foreach (var bullet in Bullets)
            {
                Point pt = bullet.Location;
                pt.Y -= 10;
                bullet.Location = pt;
                int BULLET_HEIGHT = 200; //自分で足してみた 謎の機能

                if (bullet.Location.Y < -BULLET_HEIGHT)
                    bullet.Dispose();
            }
        }
    }
}
