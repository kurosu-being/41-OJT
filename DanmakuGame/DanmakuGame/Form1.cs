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
            pictureBoxJiki.Location = new Point(this.ClientSize.Width / 2 - pictureBoxJiki.Width / 2, this.ClientSize.Height - 60);
            //Clientはクライアント領域を指し、フォームからタイトルバーと境界線をのぞいたサイズを指す。
            this.Controls.Add(pictureBoxJiki);

            pictureBox_Teki1 = new PictureBox();
            pictureBox_Teki1.Size = new Size(70, 50);
            pictureBox_Teki1.BackColor = Color.White;
            pictureBox_Teki1.Location = new Point(this.ClientSize.Width / 2 - pictureBox_Teki1.Width / 2, 5);
            //int MaxLife = 0;　　//敵の最大Lifeを0に初期化
            //MaxLife = life;     //最大ライフを現在のライフ値に設定
            //Life = life;        //現在のライフを設定
            //IsDead = false;     //敵が生きている状態を設定
            this.Controls.Add(pictureBox_Teki1); //フォームのコントロールに追加

            playerBullets = new List<PictureBox>();　//弾のリストを初期化
            enemyBullets = new List<EnemyBullet>();
            enemyBullets2 = new List<EnemyBullet>();
            enemyBullets3 = new List<EnemyBullet>();

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
            EnemyManager.EnemyBulletsMove2(enemyBullets2);
            EnemyManager.EnemyBulletsMove3(enemyBullets3);
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
            else if(e.KeyCode == Keys.Up)
            {
                isMovingUp = false;
            }
            else if(e.KeyCode == Keys.Down)
            {
                isMovingDown = false;
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
            else if(e.KeyCode == Keys.Up)
            {
                isMovingUp = true;
                //MessageBox.Show("a");
            }
            else if(e.KeyCode == Keys.Down)
            {
                isMovingDown = true;
                //MessageBox.Show("b");
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
        private List<EnemyBullet> enemyBullets2;
        private List<EnemyBullet> enemyBullets3;
        private Timer gameTimer;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool isMovingUp = false;
        private bool isMovingDown = false;
        private const int playerSpeed = 10;
        private const int BulletSpeed = 10;
        private const int BulletSpeed2 = 15;



        public class EnemyBullet : PictureBox　//pictureboxを継承するenemybulletクラスを定義
        {

            public int speed = 0;

            static int ENEMY_BULLET_WIDTH = 2;
            static int ENEMY_BULLET_HEIGHT = 8;
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

                    enemyLife--;  //敵のライフを1減らす


                    if (enemyLife <= 0)  //敵のライフが０以下かどうかチェック
                    {
                        gameTimer.Stop();
                        FormGameClear Bdan = new FormGameClear();
                        Bdan.Show();
                        this.Visible = false;
                        
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
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }
            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet2 = enemyBullets2[i];

                if (enemyBullet2.Left < pictureBoxJiki.Right && enemyBullet2.Right > pictureBoxJiki.Left && enemyBullet2.Top < pictureBoxJiki.Bottom && enemyBullet2.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets2[i]);　// 弾丸をフォームから削除   
                    enemyBullets.RemoveAt(i);  // 弾丸をフォームから削除

                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }
            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet3 = enemyBullets3[i];

                if (enemyBullet3.Left < pictureBoxJiki.Right && enemyBullet3.Right > pictureBoxJiki.Left && enemyBullet3.Top < pictureBoxJiki.Bottom && enemyBullet3.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets3[i]);　// 弾丸をフォームから削除   
                    enemyBullets.RemoveAt(i);  // 弾丸をフォームから削除

                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

        }



        private void LaunchEnemyBullet()
        {
            int centerX = pictureBox_Teki1.Left + pictureBox_Teki1.Width / 2;
            EnemyBullet bullet1 = new EnemyBullet
            {
                Location = new Point(centerX - 2, pictureBox_Teki1.Bottom),
                Size = new Size(6, 10),
                BackColor = Color.White,
                speed = 10
            };

            EnemyBullet bullet2 = new EnemyBullet
            {
                Location = new Point(centerX - 10, pictureBox_Teki1.Bottom),
                Size = new Size(6, 10),
                BackColor = Color.White,
                speed = 10
            };

            EnemyBullet bullet3 = new EnemyBullet
            {
                Location = new Point(centerX + 10, pictureBox_Teki1.Bottom),
                Size = new Size(6, 10),
                BackColor = Color.White,
                speed = 10
            };

            enemyBullets.Add(bullet1);
            this.Controls.Add(bullet1);
            bullet1.BringToFront();

            enemyBullets2.Add(bullet2);
            this.Controls.Add(bullet2);
            bullet2.BringToFront();

            enemyBullets3.Add(bullet3);
            this.Controls.Add(bullet3);
            bullet3.BringToFront();
        }

        private void MovepictureBoxJiki()　　//自機が画面外にいかないように（または敵機に近づきすぎないように）するメソッド
        {
            var currentForm = this.FindForm();

            if (isMovingLeft && pictureBoxJiki.Left > 0)
            {
                pictureBoxJiki.Left -= playerSpeed;
            }
            if (isMovingRight && pictureBoxJiki.Right < currentForm.ClientSize.Width)
            {
                pictureBoxJiki.Left += playerSpeed;
            }
            if(isMovingUp && pictureBoxJiki.Top > pictureBox_Teki1.Bottom + 100)
            {
                pictureBoxJiki.Top -= playerSpeed;
            }
            if(isMovingDown && pictureBoxJiki.Bottom < currentForm.ClientSize.Height - 10)  //「< FormDanmaku.ActiveForm.Bottom - 50」では上手くいかなかったゴリ押し
            {
                pictureBoxJiki.Top += playerSpeed;
            }
        }



        public class EnemyManager
        {

            static List<EnemyBullet> _enemyBullets = new List<EnemyBullet>();　　//駅の弾丸を保持するリスト
            static List<EnemyBullet> _enemyBullets2 = new List<EnemyBullet>();
            static List<EnemyBullet> _enemyBullets3 = new List<EnemyBullet>();

            static public void EnemyBulletsMove(List<EnemyBullet> aa)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet1 in aa)
                {
                    Point pt = bullet1.Location;　　//弾丸の位置を取得
                    pt.Y += bullet1.speed;          //弾丸のy軸を＋していく
                    bullet1.Location = pt;          //移動後の位置を設定

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                        bullet1.Dispose();
                }


            }

            static public void EnemyBulletsMove2(List<EnemyBullet> bb)  //弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet2 in bb)
                {
                    Point pt = bullet2.Location;  //弾丸の位置を取得
                    pt.Y += bullet2.speed;          //弾丸のy軸を＋していく
                    pt.X += bullet2.speed;          //弾丸のy軸を＋していく
                    bullet2.Location = pt;          //移動後の位置を設定

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                        bullet2.Dispose();
                }


            }

            static public void EnemyBulletsMove3(List<EnemyBullet> bb)  //弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet3 in bb)
                {
                    Point pt = bullet3.Location;  //弾丸の位置を取得
                    pt.Y += bullet3.speed;          //弾丸のy軸を＋していく
                    pt.X -= bullet3.speed;          //弾丸のy軸を＋していく
                    bullet3.Location = pt;          //移動後の位置を設定

                    if (pt.Y > 700)   //弾丸が画面外に出た場合、弾丸をリストから破棄
                        bullet3.Dispose();
                }


            }

        }


    }
}

