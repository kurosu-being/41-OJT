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

        int EnemyX = 0;
        bool EnemyLeft = true;

        public FormDanmaku()
        {
            //IializeComponent();

            this.DoubleBuffered = true; //ちらつき防止
            this.Size = new Size(800, 600);
            this.BackColor = Color.Black;

            pictureBoxJiki = new PictureBox();
            pictureBoxJiki.Size = new Size(30, 30);
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
            enemyBullets4 = new List<EnemyBullet>();
            enemyBullets5 = new List<EnemyBullet>();
            enemyBullets6 = new List<EnemyBullet>();
            enemyBullets7 = new List<EnemyBullet>();
            enemyBullets8 = new List<EnemyBullet>();
            enemyBullets9 = new List<EnemyBullet>();
            enemyBullets10 = new List<EnemyBullet>();

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
            //CheckEnemyBulletsCollishion();

            countTimerTick++;
            Point pt = pictureBoxJiki.Location;

            if(EnemyLeft)
            {
                if(pictureBox_Teki1.Left > 50)
                {
                    pictureBox_Teki1.Left -= enemySpeed;
                    EnemyX = pictureBox_Teki1.Left;
                }
                else
                {
                    EnemyLeft = false;
                }
            }
            else
            {
                if(pictureBox_Teki1.Right < this.ClientSize.Width - 50)
                {
                    pictureBox_Teki1.Left += enemySpeed;
                    EnemyX = pictureBox_Teki1.Left;
                }
                else
                {
                    EnemyLeft = true;
                }
            }

            if (countTimerTick % 30 == 0) //タイマーのカウントを増加、○○カウントごとに自動で弾を打つ
            {
                LaunchEnemyBullet();
            }
            else if (countTimerTick % 20 == 0)
            {
                LuanchEnemyBullet2();
            }
            else if (countTimerTick % 30 == 0)
            {
                LuanchEnemyBullet3();
            }


            EnemyManager.EnemyBulletsMove(enemyBullets);　//enemyManager内のEnemybulletsMoveメソッドをイベントハンドラに呼び出す
            EnemyManager.EnemyBulletsMove2(enemyBullets2);
            EnemyManager.EnemyBulletsMove3(enemyBullets3);
            EnemyManager.EnemyBulletsMove4(enemyBullets4);
            EnemyManager.EnemyBulletsMove5(enemyBullets5);
            EnemyManager.EnemyBulletsMove6(enemyBullets6);
            EnemyManager.EnemyBulletsMove7(enemyBullets7);
            EnemyManager.EnemyBulletsMove8(enemyBullets8);
            EnemyManager.EnemyBulletsMove9(enemyBullets9);
            EnemyManager.EnemyBulletsMove10(enemyBullets10);

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
        private List<EnemyBullet> enemyBullets4;
        private List<EnemyBullet> enemyBullets5;
        private List<EnemyBullet> enemyBullets6;
        private List<EnemyBullet> enemyBullets7;
        private List<EnemyBullet> enemyBullets8;
        private List<EnemyBullet> enemyBullets9;
        private List<EnemyBullet> enemyBullets10;
        private Timer gameTimer;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool isMovingUp = false;
        private bool isMovingDown = false;
        private const int playerSpeed = 10;
        private const int BulletSpeed = 10;
        private const int BulletSpeed2 = 15;
        private const int enemySpeed = 5;



        public class EnemyBullet : PictureBox　//pictureboxを継承するenemybulletクラスを定義
        {

            public int speed = 0;
            public int directionX = 1;

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

                    enemyBullets2.RemoveAt(i);  // 弾丸をフォームから削除


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

                    enemyBullets3.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet4 = enemyBullets4[i];

                if (enemyBullet4.Left < pictureBoxJiki.Right && enemyBullet4.Right > pictureBoxJiki.Left && enemyBullet4.Top < pictureBoxJiki.Bottom && enemyBullet4.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets4[i]);　// 弾丸をフォームから削除   

                    enemyBullets4.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet5 = enemyBullets5[i];

                if (enemyBullet5.Left < pictureBoxJiki.Right && enemyBullet5.Right > pictureBoxJiki.Left && enemyBullet5.Top < pictureBoxJiki.Bottom && enemyBullet5.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets5[i]);　// 弾丸をフォームから削除   

                    enemyBullets5.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet6 = enemyBullets6[i];

                if (enemyBullet6.Left < pictureBoxJiki.Right && enemyBullet6.Right > pictureBoxJiki.Left && enemyBullet6.Top < pictureBoxJiki.Bottom && enemyBullet6.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets6[i]);　// 弾丸をフォームから削除   

                    enemyBullets6.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet7 = enemyBullets7[i];

                if (enemyBullet7.Left < pictureBoxJiki.Right && enemyBullet7.Right > pictureBoxJiki.Left && enemyBullet7.Top < pictureBoxJiki.Bottom && enemyBullet7.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets7[i]);　// 弾丸をフォームから削除   

                    enemyBullets7.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet8 = enemyBullets8[i];

                if (enemyBullet8.Left < pictureBoxJiki.Right && enemyBullet8.Right > pictureBoxJiki.Left && enemyBullet8.Top < pictureBoxJiki.Bottom && enemyBullet8.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets8[i]);　// 弾丸をフォームから削除   

                    enemyBullets8.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet9 = enemyBullets9[i];

                if (enemyBullet9.Left < pictureBoxJiki.Right && enemyBullet9.Right > pictureBoxJiki.Left && enemyBullet9.Top < pictureBoxJiki.Bottom && enemyBullet9.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets9[i]);　// 弾丸をフォームから削除   

                    enemyBullets9.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)  // 敵機の弾丸と自機の当たり判定をチェックするためのループ
            {
                EnemyBullet enemyBullet10 = enemyBullets10[i];

                if (enemyBullet10.Left < pictureBoxJiki.Right && enemyBullet10.Right > pictureBoxJiki.Left && enemyBullet10.Top < pictureBoxJiki.Bottom && enemyBullet10.Bottom > pictureBoxJiki.Top)　　// 敵の弾丸と自機の境界が交差しているかどうかをチェック
                {
                    this.Controls.Remove(enemyBullets10[i]);　// 弾丸をフォームから削除   

                    enemyBullets10.RemoveAt(i);  // 弾丸をフォームから削除


                    gameTimer.Stop();
                    FormGameOver Adan = new FormGameOver();
                    Adan.Show();
                    this.Visible = false;
                    //Application.Exit();                    
                }
            }
        }



        private void LaunchEnemyBullet()　　//１０カウントごと
        {
            //左から順にbullet1、bullet2、bullet3、bullet4、...
            int centerX = pictureBox_Teki1.Left + pictureBox_Teki1.Width / 2;
            int centerY = pictureBox_Teki1.Top + pictureBox_Teki1.Height / 2;
            EnemyBullet bullet1 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Left - 30, centerY - 10),  
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet2 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Left - 25, centerY + 15),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet3 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Left - 20, centerY + 20),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet4 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Left - 15, centerY + 25),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet5 = new EnemyBullet
            {
                Location = new Point(centerX - 10, pictureBox_Teki1.Bottom +5),
                Size = new Size(8, 20),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet6 = new EnemyBullet
            {
                Location = new Point(centerX +8, pictureBox_Teki1.Bottom +5),
                Size = new Size(8, 20),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet7 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Right + 15, centerY - 25),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet8 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Right + 20, centerY - 20),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet9 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Right + 25, centerY - 15),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
            };

            EnemyBullet bullet10 = new EnemyBullet
            {
                Location = new Point(pictureBox_Teki1.Right + 30, centerY - 10),
                Size = new Size(6, 10),
                BackColor = Color.Red,
                speed = 3
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

            enemyBullets4.Add(bullet4);
            this.Controls.Add(bullet4);
            bullet4.BringToFront();

            enemyBullets5.Add(bullet5);
            this.Controls.Add(bullet5);
            bullet5.BringToFront();

            enemyBullets6.Add(bullet6);
            this.Controls.Add(bullet6);
            bullet6.BringToFront();

            enemyBullets7.Add(bullet7);
            this.Controls.Add(bullet7);
            bullet7.BringToFront();

            enemyBullets8.Add(bullet8);
            this.Controls.Add(bullet8);
            bullet8.BringToFront();

            enemyBullets9.Add(bullet9);
            this.Controls.Add(bullet9);
            bullet9.BringToFront();

            enemyBullets10.Add(bullet10);
            this.Controls.Add(bullet10);
            bullet10.BringToFront();
        }

        private void LuanchEnemyBullet2()　　//２０カウントごと
        {
            
        }

        private void LuanchEnemyBullet3()  //３０カウントごと
        {
            ////左から順にbullet1、bullet2、bullet3、bullet4、...
            //int centerX = pictureBox_Teki1.Left + pictureBox_Teki1.Width / 2;
            //int centerY = pictureBox_Teki1.Top + pictureBox_Teki1.Height / 2;
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
            static List<EnemyBullet> _enemyBullets4 = new List<EnemyBullet>();
            static List<EnemyBullet> _enemyBullets5 = new List<EnemyBullet>();
            static List<EnemyBullet> _enemyBullets6 = new List<EnemyBullet>();
            static List<EnemyBullet> _enemyBullets7 = new List<EnemyBullet>();
            static List<EnemyBullet> _enemyBullets8 = new List<EnemyBullet>();
            static List<EnemyBullet> _enemyBullets9 = new List<EnemyBullet>();
            static List<EnemyBullet> _enemyBullets10 = new List<EnemyBullet>();

            static public void EnemyBulletsMove(List<EnemyBullet> aa)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet1 in aa)
                {
                    Point pt = bullet1.Location;　　//弾丸の位置を取得
                    pt.Y += bullet1.speed;          //弾丸のy軸を＋していく
                    pt.X -= bullet1.speed * bullet1.directionX * 4;          //弾丸のx軸を＋していく
                    bullet1.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet1.directionX++;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet1.directionX--;
                    }

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet1.Dispose();
                    }
                   
                }


            }

            static public void EnemyBulletsMove2(List<EnemyBullet> bb)  //弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet2 in bb)
                {
                    Point pt = bullet2.Location;  //弾丸の位置を取得
                    pt.Y += bullet2.speed;          //弾丸のy軸を＋していく
                    pt.X -= bullet2.speed * bullet2.directionX * 2;          //弾丸のx軸を＋していく
                    bullet2.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet2.directionX++;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet2.directionX--;
                    }


                    if (pt.Y > 700)   //弾丸が画面外に出た場合、弾丸をリストから破棄
                    {
                        bullet2.Dispose();
                    }
                        
                }


            }

            static public void EnemyBulletsMove3(List<EnemyBullet> cc)  //弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet3 in cc) // 左の弾丸
                {
                    Point pt = bullet3.Location;  //弾丸の位置を取得
                    pt.Y += bullet3.speed;          //弾丸のy軸を＋していく
                    pt.X -= bullet3.speed * bullet3.directionX ;          //弾丸のy軸を＋していく
                    bullet3.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet3.directionX++;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet3.directionX--;
                    }

                    if (pt.Y > 700){   //弾丸が画面外に出た場合、弾丸をリストから破棄
                        bullet3.Dispose();
                    }                       
                }
            }

            static public void EnemyBulletsMove4(List<EnemyBullet> dd)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet4 in dd)
                {
                    Point pt = bullet4.Location;　　//弾丸の位置を取得
                    pt.Y += bullet4.speed;          //弾丸のy軸を＋していく
                    pt.X -= bullet4.speed * bullet4.directionX * 1 / 2;
                    bullet4.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet4.directionX++;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet4.directionX--;
                    }

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet4.Dispose();
                    }
                }
            }

            static public void EnemyBulletsMove5(List<EnemyBullet> ee)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet5 in ee)
                {
                    Point pt = bullet5.Location;　　//弾丸の位置を取得
                    pt.Y += bullet5.speed;          //弾丸のy軸を＋していく
                    bullet5.Location = pt;          //移動後の位置を設定

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet5.Dispose();
                    }
                }
            }

            static public void EnemyBulletsMove6(List<EnemyBullet> ff)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet6 in ff)
                {
                    Point pt = bullet6.Location;　　//弾丸の位置を取得
                    pt.Y += bullet6.speed;          //弾丸のy軸を＋していく
                    bullet6.Location = pt;          //移動後の位置を設定
                                       
                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet6.Dispose();
                    }
                }
            }

            static public void EnemyBulletsMove7(List<EnemyBullet> gg)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet7 in gg)
                {
                    Point pt = bullet7.Location;　　//弾丸の位置を取得
                    pt.Y += bullet7.speed;          //弾丸のy軸を＋していく
                    pt.X += bullet7.speed * bullet7.directionX * 1 / 2;
                    bullet7.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet7.directionX--;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet7.directionX++;
                    }

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet7.Dispose();
                    }
                }
            }

            static public void EnemyBulletsMove8(List<EnemyBullet> hh)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet8 in hh)
                {
                    Point pt = bullet8.Location;　　//弾丸の位置を取得
                    pt.Y += bullet8.speed;          //弾丸のy軸を＋していく
                    pt.X += bullet8.speed * bullet8.directionX ;
                    bullet8.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet8.directionX--;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet8.directionX++;
                    }

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet8.Dispose();
                    }

                }


            }

            static public void EnemyBulletsMove9(List<EnemyBullet> ii)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet9 in ii)
                {
                    Point pt = bullet9.Location;　　//弾丸の位置を取得
                    pt.Y += bullet9.speed;        //弾丸のy軸を＋していく
                    pt.X += bullet9.speed * bullet9.directionX * 2;
                    bullet9.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet9.directionX--;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet9.directionX++;
                    }

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet9.Dispose();
                    }

                }


            }

            static public void EnemyBulletsMove10(List<EnemyBullet> jj)　　//弾丸リストの要素を処理
            {

                foreach (EnemyBullet bullet10 in jj)
                {
                    Point pt = bullet10.Location;　　//弾丸の位置を取得
                    pt.Y += bullet10.speed;          //弾丸のy軸を＋していく
                    pt.X += bullet10.speed * bullet10.directionX * 4;
                    bullet10.Location = pt;          //移動後の位置を設定

                    if (pt.X > 780) // 右端にきたとき
                    {
                        bullet10.directionX--;
                    }
                    if (pt.X <= 10) //左端にきたとき
                    {
                        bullet10.directionX++;
                    }

                    if (pt.Y > 700)   //弾丸が画面外に出て場合、弾丸をリストから破棄
                    {
                        bullet10.Dispose();
                    }

                }


            }

        }


    }
}

