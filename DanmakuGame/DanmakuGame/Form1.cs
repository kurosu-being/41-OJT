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
        private Timer gameTimer;
        private PictureBox pictureBoxJiki;
        private PictureBox pictureBoxTeki1;
        private List<PictureBox> playerBullets;
        private List<PictureBox> enemyBullets;
        private const int playerSpeed = 10;
        private const int BulletSpeed = 10;
        private const int BulletSpeed2 = 15;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private int countTimerTick;
        private Panel panel1;
        private List<PictureBox> _bullets = new List<PictureBox>();
        private List<Enemy> enemies = new List<Enemy>();

        public FormDanmaku()
        {
            //InitializeComponent();
            this.DoubleBuffered = true;
            this.Size = new Size(800, 600);
            this.BackColor = Color.Black;

            pictureBoxJiki = new PictureBox();
            pictureBoxJiki.Size = new Size(50, 50);
            pictureBoxJiki.BackColor = Color.White;
            pictureBoxJiki.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 60);
            this.Controls.Add(pictureBoxJiki);

            pictureBoxTeki1 = new PictureBox();
            pictureBoxTeki1.Size = new Size(50, 70);
            pictureBoxTeki1.BackColor = Color.White;
            pictureBoxTeki1.Location = new Point(this.ClientSize.Width / 2 - 50);
            this.Controls.Add(pictureBoxTeki1);

            playerBullets = new List<PictureBox>();
            enemyBullets = new List<PictureBox>();

            this.KeyDown += FormDanmaku_KeyDown;
            this.KeyUp += FormDanmaku_KeyUp;

            gameTimer = new Timer();
            gameTimer.Tick += Gametimer_Tick;
            gameTimer.Interval = 30;
            gameTimer.Start();

            //InitializeGameComponents();
        }

        private void Gametimer_Tick(object sender, EventArgs e)
        {
            MovepictureBoxJiki();
            MoveBullets();
            MoveEnemyBullets();
            CheckBulletCollishion();

            countTimerTick++;
            //EnemyManager.EnemyBulletsMove();
            if (countTimerTick % 50 == 0)
            {
                //foreach (var enemy in enemies)
                //{
                LaunchEnemyBullet();
            }
        }

        private void MovepictureBoxJiki()
        {
            //if (direct == Direct.Left)
            //{
            //    MoveLeft();
            //}
            //else if (direct == Direct.Right)
            //{
            //    MoveRight();
            //}

            if (isMovingLeft && pictureBoxJiki.Left > 0)
            {
                pictureBoxJiki.Left -= playerSpeed;
            }
            if (isMovingRight && pictureBoxJiki.Right < this.ClientSize.Width)
            {
                pictureBoxJiki.Left += playerSpeed;
            }
        }

        private void MoveBullets()
        {
            //foreach (var bullet in Bullets)
            //{
            //    Point pt = bullet.Location;
            //    pt.Y -= 10;
            //    bullet.Location = pt;
            //    int BULLET_HEIGHT = 200;

            //    if (bullet.Location.Y < -BULLET_HEIGHT)
            //        bullet.Dispose();
            //}
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

        public void MoveEnemyBullets()
        {
            //foreach (EnemyBullet bullet in EnemyBullets)
            //{
            //    Point pt = bullet.Location;
            //    pt.Y += bullet.speed;
            //    bullet.Location = pt;

            //    if (pt.Y > bullet.Parent.Height)
            //        bullet.Dispose();
            //}
            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                enemyBullets[i].Top += BulletSpeed2;
                if (enemyBullets[i].Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(enemyBullets[i]);
                    enemyBullets.RemoveAt(i);
                }
            }
        }

        private void CheckBulletCollishion()
        {
            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                if (playerBullets[i].Bounds.IntersectsWith(pictureBoxTeki1.Bounds))
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

        private void FormDanmaku_KeyDown(object sender, KeyEventArgs e)
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

            //if (e.KeyCode == Keys.Left)
            //{
            //    direct = Direct.Left;
            //}
            //else if (e.KeyCode == Keys.Right)
            //{
            //    direct = Direct.Right;
            //}
            //else if (e.KeyCode == Keys.Space)
            //{
            //    Bulletlaunch();
            //}
            //else if (e.KeyCode == Keys.S)
            //{
            //    GameStart();
            //}
        }

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
            //if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            //{
            //    direct = Direct.None;
            //}
        }

        private void ShootBullet()
        {
            Point pt = pictureBoxJiki.Location;
            //int width = pictureBoxJiki.Size.Width;
            int centerX = pt.X + pictureBoxJiki.Width / 2;

            PictureBox bullet = new PictureBox
            {
                Location = new Point(centerX - 2, pt.Y),
                Size = new Size(4, 10),
                BackColor = Color.White
                //Parent = panel1
            };

            playerBullets.Add(bullet);
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void LaunchEnemyBullet()
        {
            int centerX = pictureBoxTeki1.Left + pictureBoxTeki1.Width / 2;
            EnemyBullet bullet = new EnemyBullet
            {
                Location = new Point(centerX - 2, pictureBoxTeki1.Bottom),
                Size = new Size(6, 10),
                BackColor = Color.White,
                Speed = 10
            };
            //int bulletSpeed = BULLET_SPEED_C;
            //if (enemy.ID < 100)
            //    bulletSpeed = BULLET_SPEED_A;
            //else if (enemy.ID < 200)
            //    bulletSpeed = BULLET_SPEED_B;

            //EnemyBullet bullet = EnemyBullet.CreateBullet(enemy, bulletSpeed);
            //EnemyManager.EnemyBullets.Add(bullet);

            enemyBullets.Add(bullet);
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        public class EnemyBullet : PictureBox
        {
            public int Speed { get; set; }
        }

        //private void InitializeGameComponents()
        //{
        //    // Initialize panel1
        //    panel1 = new Panel();
        //    panel1.Dock = DockStyle.Fill;
        //    this.Controls.Add(panel1);

        //    // Initialize pictureBoxJiki
        //    pictureBoxJiki = new PictureBox();
        //    pictureBoxJiki.Size = new Size(50, 50);
        //    pictureBoxJiki.BackColor = Color.Blue;
        //    pictureBoxJiki.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 60);
        //    panel1.Controls.Add(pictureBoxJiki);

        //    // Initialize enemies
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Enemy enemy = new Enemy
        //        {
        //            ID = i,
        //            Size = new Size(50, 50),
        //            BackColor = Color.Red,
        //            Location = new Point(i * 100, 50)
        //        };
        //        enemies.Add(enemy);
        //        panel1.Controls.Add(enemy);
        //    }
        //}



        //private void MoveLeft()
        //{
        //    Point pt = pictureBoxJiki.Location;
        //    pt.X -= 10;
        //    pictureBoxJiki.Location = pt;
        //}

        //private void MoveRight()
        //{
        //    Point pt = pictureBoxJiki.Location;
        //    pt.X += 10;
        //    pictureBoxJiki.Location = pt;
        //}



        //private void GameStart()
        //{
        //    timer1.Start();
        //}

        //List<PictureBox> Bullets
        //{
        //    get
        //    {
        //        _bullets = _bullets.Where(x => !x.IsDisposed).ToList();
        //        return _bullets;
        //    }
        //}




        //int BULLET_SPEED_A = 9;
        //int BULLET_SPEED_B = 8;
        //int BULLET_SPEED_C = 7;
        //public int countTimerTick;


        //public class Enemy : PictureBox
        //{
        //    public int ID { get; set; }
        //}

        //public class EnemyManager
        //{
        //    static List<EnemyBullet> _enemyBullets = new List<EnemyBullet>();

        //    static public List<EnemyBullet> EnemyBullets
        //    {
        //        get
        //        {
        //            _enemyBullets = _enemyBullets.Where(x => !x.IsDisposed).ToList();
        //            return _enemyBullets;
        //        }
        //    }


        //}
        private void FormDanmaku_Load(object sender, EventArgs e) { 
        }

    }  
}



