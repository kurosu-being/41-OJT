
using System;

namespace DanmakuGame
{
    partial class FormDanmaku
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDanmaku));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxJiki = new System.Windows.Forms.PictureBox();
            this.pictureBox_Teki1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJiki)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Teki1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxJiki
            // 
            this.pictureBoxJiki.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pictureBoxJiki, "pictureBoxJiki");
            this.pictureBoxJiki.Name = "pictureBoxJiki";
            this.pictureBoxJiki.TabStop = false;
            // 
            // pictureBox_Teki1
            // 
            this.pictureBox_Teki1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pictureBox_Teki1, "pictureBox_Teki1");
            this.pictureBox_Teki1.Name = "pictureBox_Teki1";
            this.pictureBox_Teki1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pictureBox_Teki1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // FormDanmaku
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDanmaku";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDanmaku_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormDanmaku_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJiki)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Teki1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //private void pictureBox_Teki1_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBoxJiki;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox_Teki1;
        private System.Windows.Forms.Panel panel1;
    }
}

