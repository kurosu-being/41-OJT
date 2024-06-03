
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDanmaku));
            this.pictureBox_Jiki = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_Teki1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Jiki)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Teki1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Jiki
            // 
            this.pictureBox_Jiki.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pictureBox_Jiki, "pictureBox_Jiki");
            this.pictureBox_Jiki.Name = "pictureBox_Jiki";
            this.pictureBox_Jiki.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // pictureBox_Teki1
            // 
            this.pictureBox_Teki1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pictureBox_Teki1, "pictureBox_Teki1");
            this.pictureBox_Teki1.Name = "pictureBox_Teki1";
            this.pictureBox_Teki1.TabStop = false;
            // 
            // FormDanmaku
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox_Jiki);
            this.Controls.Add(this.pictureBox_Teki1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.Name = "FormDanmaku";
            this.Load += new System.EventHandler(this.FormDanmaku_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Jiki)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Teki1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox_Jiki;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox_Teki1;
    }
}

