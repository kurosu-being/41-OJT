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
        public FormDanmaku()
        {
            //IializeComponent();

            this.DoubleBuffered = true;
            this.Size = new Size(800, 600);
            this.BackColor = Color.Black;

            pictureBoxJiki = new PictureBox();
            pictureBoxJiki.Size = new Size(50, 50);
            pictureBoxJiki.BackColor = Color.White;
            pictureBoxJiki.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 60);
            this.Controls.Add(pictureBoxJiki);

            pictureBox_Teki1 = new PictureBox();
            pictureBox_Teki1.Size = new Size(50, 70);
            pictureBox_Teki1.BackColor = Color.White;
            pictureBox_Teki1.Location = new Point(this.ClientSize.Width / 2 - 50);
            this.Controls.Add(pictureBox_Teki1);

            playerBullets = new List<PictureBox>();
            enemyBullets = new List<EnemyBullet>();

            this.KeyDown += FormDanmaku_KeyDown;
            this.KeyUp += FormDanmaku_KeyUp;

            gameTimer = new Timer();
            gameTimer.Tick += Gametimer_Tick;
            gameTimer.Interval = 30;
            gameTimer.Start();

            timer1 = new Timer();
            timer1.Tick += timer1_Tick;
            timer1.Interval = 50;
            timer1.Start();
        }
        private void Gametimer_Tick(object sender, EventArgs e)
        {
            MovepictureBoxJiki();
            MoveBullets();
            CheckBulletCollishion();

            countTimerTick++;
            if (countTimerTick % 50 == 0)
            {
                LaunchEnemyBullet();
            }

            EnemyManager.EnemyBulletsMove(enemyBullets);
        }

        public enum Direct
        {
            None = 0,
            Left = 1,
            Right = 2,
        }

        Direct direct = Direct.None;

        private void FormDanmaku_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                isMovingLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                isMovingRight = false;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                direct = Direct.None;
            }
        }

        private void FormDanmaku_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Left)
                {
                    isMovingLeft = true;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    isMovingRight = true;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    ShootBullet();
                }
            }

             void ShootBullet()
            {
                Point pt = pictureBoxJiki.Location;
                int width = pictureBoxJiki.Size.Width;
                int centerX = pt.X + pictureBoxJiki.Width / 2;

                PictureBox bullet = new PictureBox
                {
                    Location = new Point(centerX - 2, pt.Y),
                    Size = new Size(4, 10),
                    BackColor = Color.White,
                    Parent = panel1
                };

                playerBullets.Add(bullet);
                this.Controls.Add(bullet);
                bullet.BringToFront();
            }
        }


        void GameStart()
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }



        private void FormDanmaku_Load(object sender, EventArgs e)
        {

        }
        

        List<PictureBox> _bullets = new List<PictureBox>(); //なんだこの<>は
        List<PictureBox> Bullets
        {
            get
            {
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
                int BULLET_HEIGHT = 1; //自分で足してみた 謎の機能

                if (bullet.Location.Y < -BULLET_HEIGHT)
                    bullet.Dispose();
            }
        }



        private int countTimerTick;
        private List<PictureBox> playerBullets;
        private List<EnemyBullet> enemyBullets;
        private Timer gameTimer;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private const int playerSpeed = 10;
        private const int BulletSpeed = 10;
        private const int BulletSpeed2 = 15;




        public class EnemyBullet : PictureBox
        {
            public int speed = 0;

            static int ENEMY_BULLET_WIDTH = 2;
            static int ENEMY_BULLET_HEIGHT = 8;

            public static EnemyBullet CreateBullet(Enemy enemy, int speed)
            {
                int center = (enemy.Left + enemy.Right) / 2;
                EnemyBullet bullet = new EnemyBullet();
                bullet.Location = new Point(center, enemy.Bottom);
                bullet.Size = new Size(ENEMY_BULLET_WIDTH, ENEMY_BULLET_HEIGHT);
                bullet.BackColor = Color.Red;
                bullet.Parent = enemy.Parent;
                bullet.speed = speed;
                return bullet;
            }
        }
        private void MoveBullets()
        {
            
            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                playerBullets[i].Top -= BulletSpeed;
                if (playerBullets[i].Top < 0)
                {
                    this.Controls.Remove(playerBullets[i]);
                    playerBullets.RemoveAt(i);
                }
            }
        }

        private void CheckBulletCollishion()
        {
            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                if (playerBullets[i].Bounds.IntersectsWith(pictureBox_Teki1.Bounds))
                {
                    this.Controls.Remove(playerBullets[i]);
                    playerBullets.RemoveAt(i);
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                if (enemyBullets[i].Bounds.IntersectsWith(pictureBoxJiki.Bounds))
                {
                    this.Controls.Remove(enemyBullets[i]);
                    enemyBullets.RemoveAt(i);
                }
            }
        }

        private void LaunchEnemyBullet()
        {
            int centerX = pictureBox_Teki1.Left + pictureBox_Teki1.Width / 2;
            EnemyBullet bullet = new EnemyBullet
            {
                Location = new Point(centerX - 2, pictureBox_Teki1.Bottom),
                Size = new Size(6, 10),
                BackColor = Color.White,
                speed = 10
            };

            enemyBullets.Add(bullet);
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }
        private void MovepictureBoxJiki()
        {


            if (isMovingLeft && pictureBoxJiki.Left > 0)
            {
                pictureBoxJiki.Left -= playerSpeed;
            }
            if (isMovingRight && pictureBoxJiki.Right < FormDanmaku.ActiveForm.Width)
            {
                pictureBoxJiki.Left += playerSpeed;
            }
        }


        public class EnemyManager
        {

            static List<EnemyBullet> _enemyBullets = new List<EnemyBullet>();
            static public List<EnemyBullet> EnemyBullets
            {
                get
                {
                    _enemyBullets = _enemyBullets.Where(x => !x.IsDisposed).ToList();
                    return _enemyBullets;
                }
            }

            static public void EnemyBulletsMove(List<EnemyBullet> aa)
            {

                foreach (EnemyBullet bullet in aa)
                {
                    Point pt = bullet.Location;
                    pt.Y += bullet.speed;
                    bullet.Location = pt;

                    if (pt.Y > 700)
                        bullet.Dispose();
                }
            }
        


            private void pictureBox_Teki1_Click(object sender, EventArgs e)
            {

            }
            private const int playerSpeed = 10;
            private const int BulletSpeed = 10;
            private const int BulletSpeed2 = 15;
            private List<PictureBox> _bullets = new List<PictureBox>();
            private List<Enemy> enemies = new List<Enemy>();

            //public class EnemyBullet : PictureBox
            //{
            //    public int Speed { get; set; }
            //}

            private void FormDanmaku_Load(object sender, EventArgs e)
            {
            }

        }
    }
}

