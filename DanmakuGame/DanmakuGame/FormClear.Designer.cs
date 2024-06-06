
namespace DanmakuGame
{
    partial class FormClear
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxJiki = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJiki)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HGP創英ﾌﾟﾚｾﾞﾝｽEB", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(206, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 97);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clear!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("UD デジタル 教科書体 NP-R", 18F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(275, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 42);
            this.label2.TabIndex = 1;
            this.label2.Text = "再出撃";
            // 
            // pictureBoxJiki
            // 
            this.pictureBoxJiki.BackColor = System.Drawing.Color.White;
            this.pictureBoxJiki.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxJiki.Location = new System.Drawing.Point(323, 510);
            this.pictureBoxJiki.Name = "pictureBoxJiki";
            this.pictureBoxJiki.Size = new System.Drawing.Size(70, 70);
            this.pictureBoxJiki.TabIndex = 5;
            this.pictureBoxJiki.TabStop = false;
            // 
            // FormClear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(678, 644);
            this.Controls.Add(this.pictureBoxJiki);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(700, 700);
            this.MinimumSize = new System.Drawing.Size(700, 700);
            this.Name = "FormClear";
            this.Text = "クリア画面";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJiki)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxJiki;
    }
}