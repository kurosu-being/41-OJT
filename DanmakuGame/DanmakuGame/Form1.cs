using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DanmakuGame
{
    public partial class FormDanmaku : Form
    {
        public FormDanmaku()
        {
            //IializeComponent();

            this.DoubleBuffered = true; //ちらつき防止
            this.Size = new Size(800, 600);
            this.BackColor = Color.Black;

            pictureBoxJiki = new PictureBox();
            pictureBoxJiki.Size = new Size(50, 50);
            pictureBoxJiki.BackColor = Color.White;
            pictureBoxJiki.Location = new Point(this.ClientSize.Width / 2 - pictureBoxJiki.Width /2  , this.ClientSize.Height - 60);
            //Clientはクライアント領域を指し、フォームからタイトルバーと境界線をのぞいたサイズを指す。
            this.Controls.Add(pictureBoxJiki);

            pictureBox_Teki1 = new PictureBox();
            pictureBox_Teki1.Size = new Size(70, 50);
            pictureBox_Teki1.BackColor = Color.White;
            pictureBox_Teki1.Location = new Point(this.ClientSize.Width / 2 - pictureBox_Teki1.Width / 2 ,5);
            //int MaxLife = 0;　　//敵の最大Lifeを0に初期化
            //MaxLife = life;     //最大ライフを現在のライフ値に設定
            //Life = life;        //現在のライフを設定
            //IsDead = false;     //敵が生きている状態を設定
            this.Controls.Add(pictureBox_Teki1); //フォームのコントロールに追加

            playerBullets = new List<PictureBox>();　//弾のリストを初期化
            enemyBullets = new List<EnemyBullet>();

            this.KeyDown += FormDanmaku_KeyDown;　//イベントハンドラを追加
            this.KeyUp += FormDanmaku_KeyUp;

            gameTimer = new Timer();　//タイマーを追加。Tickイベントハンドラーを追加*/
            gameTimer.Tick += Gametimer_Tick;
            gameTimer.Interval = 30;
            gameTimer.Start();

        }

        private void Gametimer_Tick(object sender, EventArgs e)　//イベントハンドラ内で、プレイヤーの移動、弾の移動、弾の衝突判定を実行
        {
            MovepictureBoxJiki();
            MoveBullets();
            CheckBulletCollishion();

            countTimerTick++;
            if (countTimerTick % 30 == 0) //タイマーのカウントを増加、30ごとに自動で弾を打つ
            {
                LaunchEnemyBullet();
            }

            EnemyManager.EnemyBulletsMove(enemyBullets);　//enemyManager内のEnemybulletsMoveメソッドをイベントハンドラに呼び出す
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
        }

        private void ShootBullet()　　//プレイヤーの座標を取得、そこを弾の発射位置にする
        {
            Point pt = pictureBoxJiki.Location;
            int width = pictureBoxJiki.Size.Width;
            int centerX = pt.X + pictureBoxJiki.Width / 2;

            PictureBox bullet = new PictureBox
            {
                Location = new Point(centerX - 2, pt.Y),
                Size = new Size(5, 5), //弾が大きい可能性があったから弾一つのサイズを小さくした
                BackColor = Color.White,
                Parent = this
            };

            //新しい弾をリストに追加、フォームのコントロールに追加
            playerBullets.Add(bullet);
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void FormDanmaku_Load(object sender, EventArgs e)
        {

        }

        //弾のリストを定義。破壊・消滅していない弾のみを返す
        List<PictureBox> _bullets = new List<PictureBox>(); //なんだこの<>は
        List<PictureBox> Bullets
        {
            get
            {
                _bullets = _bullets.Where(x => !x.IsDisposed).ToList();
                return _bullets;
            }
        }

        //ライフや死亡条件のプロパティを定義
        public object Life { get; private set; }
        public bool IsDead { get; private set; }
        public int life { get; private set; }

        //弾を移動。画面外に出た弾は消滅（ReMove）させる
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


        private int countTimerTick;
        private List<PictureBox> playerBullets;
        private List<EnemyBullet> enemyBullets;
        private Timer gameTimer;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private Enemy enemy;
        private object bullet;
        private const int playerSpeed = 10;
        private const int BulletSpeed = 10;
        private const int BulletSpeed2 = 15;


        //public class jiki_Life
        //{
        //    int _Life = 0;

        //    public int Life
        //    {
        //        get { return _Life; }
        //        set
        //        {
        //            _Life = value;
        //            if (_Life <= 0)
        //                IsDead = true;
        //        }
        //    }
        //}

        

        public class EnemyBullet : PictureBox　//pictureboxを継承するenemybulletクラスを定義
        {
            public EnemyBullet()
            {

            }
            public int speed = 0;

            static int ENEMY_BULLET_WIDTH = 2;
            static int ENEMY_BULLET_HEIGHT = 8;


            //public static EnemyBullet CreateBullet(Enemy enemy, int speed)
            //{
            //    int center = (enemy.Left + enemy.Right) / 2;
            //    EnemyBullet bullet = new EnemyBullet();
            //    bullet.Location = new Point(center, enemy.Bottom);
            //    bullet.Size = new Size(ENEMY_BULLET_WIDTH, ENEMY_BULLET_HEIGHT);
            //    bullet.BackColor = Color.Red;
            //    bullet.Parent = enemy.Parent;
            //    bullet.speed = speed;
            //    return bullet;
            //}
            public static EnemyBullet CreateBullet(Enemy enemy, int speed)　//弾丸を形成する静的メソッド
            {
                int center = (enemy.Left + enemy.Right) / 2;
                EnemyBullet bullet = new EnemyBullet();            
                bullet.Location = new Point(center, enemy.Bottom);
                bullet.Size = new Size(ENEMY_BULLET_WIDTH, ENEMY_BULLET_HEIGHT);
                bullet.BackColor = Color.Red;
                bullet.Parent = enemy.Parent;　　//弾丸の親コントローラーを敵の親コントロールに追加
                bullet.speed = speed;       //弾丸の速度を設定
                return bullet;      //生成した弾丸をリストに返す
            }
        }

        public const int enemyInitialLife = 10; // 敵の初期HP
        public int enemyLife = enemyInitialLife; // 現在の敵のHP

        private void CheckBulletCollishion() //自機と敵機の当たり判定
        {
            for (int i = playerBullets.Count - 1; i >= 0; i--)  // プレイヤーの弾丸と敵機の当たり判定をチェックするためのループ
            {
                PictureBox bullet = playerBullets[i];
                if (bullet.Left < pictureBox_Teki1.Right && bullet.Right > pictureBox_Teki1.Left && bullet.Top + 10 < pictureBox_Teki1.Bottom && bullet.Bottom > pictureBox_Teki1.Top)　// プレイヤーの弾丸と敵機の境界が交わっているかをチェック
                {
                    this.Controls.Remove(playerBullets[i]);　// 弾丸をフォームから削除
                    playerBullets.RemoveAt(i);  // 弾丸をフォームから削除
                    
                    enemyLife--;　　//敵のライフを1減らす
                    

                    if (enemyLife <= 0)  //敵のライフが０以下かどうかチェック
                    {
                        gameTimer.Stop();
                        FormGameClear Bdan = new FormGameClear();
                        Bdan.Show();
                        //Application.Exit();
                        //Application.Run(new FormClear());
                    }
                }
            }
            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet = enemyBullets[i];

                if (enemyBullet.Left < pictureBoxJiki.Right && enemyBullet.Right > pictureBoxJiki.Left && enemyBullet.Top < pictureBoxJiki.Bottom && enemyBullet.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets[i]);　// 弾丸をフォームから削除   
                    enemyBullets.RemoveAt(i);  // 弾丸をフォームから削除

                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    //Application.Exit();
                    //Application.Run(new FormGameOver());
                }
            }
        }        

        private void LaunchEnemyBullet()
        {
            int centerX = pictureBox_Teki1.Left + pictureBox_Teki1.Width / 2;
            EnemyBullet bullet = new EnemyBullet //EnemyBulletは型　bulletがオブジェクト

            {
                Location = new Point(centerX - 2, pictureBox_Teki1.Bottom),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 10
            };

            enemyBullets.Add(bullet);
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void MovepictureBoxJiki()　　//自機が画面外にいかないようにするメソッド
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

            static List<EnemyBullet> _enemyBullets = new List<EnemyBullet>();　　//駅の弾丸を保持するリスト
            static public List<EnemyBullet> EnemyBullets
            {
                get
                {
                    _enemyBullets = _enemyBullets.Where(x => !x.IsDisposed).ToList();
                    return _enemyBullets;
                }
            }

            static public void EnemyBulletsMove(List<EnemyBullet> aa)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet in aa)
                {
                    Point pt = bullet.Location;　　//弾丸の位置を取得
                    pt.Y += bullet.speed;          //弾丸のy軸を＋していく
                    bullet.Location = pt;          //移動後の位置を設定

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                        bullet.Dispose();
                }
            }
        }
    }
}

