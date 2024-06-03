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
            if (e.KeyCode == Keys.Left)
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
            pictureBoxJikiMove();
            BulletsMove();

            countTimerTick++;

            //SpaceshipMove();
            //StandingByEnemysMove();
            //BeginAttack();
            //AttackEnemiesMove();
            EnemyManager.EnemyBulletsMove();
        }

        //private void AttackEnemiesMove()
        //{
        //    throw new NotImplementedException();
        //}

        //private void BeginAttack()
        //{
        //    throw new NotImplementedException();
        //}

        //private void StandingByEnemysMove()
        //{
        //    throw new NotImplementedException();
        //}

        //private void SpaceshipMove()
        //{
        //    throw new NotImplementedException();
        //}

        void pictureBoxJikiMove()
        {
            if (direct == Direct.Left)
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
                int BULLET_HEIGHT = 200; //自分で足してみた 謎の機能

                if (bullet.Location.Y < -BULLET_HEIGHT)
                    bullet.Dispose();
            }
        }

        int BULLET_SPEED_A = 9;
        int BULLET_SPEED_B = 8;
        int BULLET_SPEED_C = 7;
        private int countTimerTick;

        void EnemyBulletLaunch(Enemy enemy)
        {
            int bulletSpeed = BULLET_SPEED_C;
            if (enemy.ID < 100)
                bulletSpeed = BULLET_SPEED_A;
            else if (enemy.ID < 200)
                bulletSpeed = BULLET_SPEED_B;

            EnemyBullet bullet = EnemyBullet.CreateBullet(enemy, bulletSpeed);
            EnemyManager.EnemyBullets.Add(bullet);
        }

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

            static public void EnemyBulletsMove()
            {
                foreach (EnemyBullet bullet in EnemyBullets)
                {
                    Point pt = bullet.Location;
                    pt.Y += bullet.speed;
                    bullet.Location = pt;

                    if (pt.Y > bullet.Parent.Height)
                        bullet.Dispose();
                }
            }

            private void pictureBox_Teki1_Click(object sender, EventArgs e)
            {

            }
        }
    }
}

