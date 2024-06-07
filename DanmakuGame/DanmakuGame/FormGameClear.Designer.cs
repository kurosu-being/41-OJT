
using System.Windows.Forms;

namespace DanmakuGame
{
    partial class FormGameClear
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
            this.pictureBoxJiki = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJiki)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxJiki
            // 
            this.pictureBoxJiki.BackColor = System.Drawing.Color.White;
            this.pictureBoxJiki.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxJiki.Location = new System.Drawing.Point(180, 319);
            this.pictureBoxJiki.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxJiki.Name = "pictureBoxJiki";
            this.pictureBoxJiki.Size = new System.Drawing.Size(42, 47);
            this.pictureBoxJiki.TabIndex = 8;
            this.pictureBoxJiki.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HGP創英ﾌﾟﾚｾﾞﾝｽEB", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(110, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 64);
            this.label1.TabIndex = 6;
            this.label1.Text = "Clear!";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("UD デジタル 教科書体 N-B", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(4, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(388, 60);
            this.button1.TabIndex = 10;
            this.button1.Text = "Press Key for Continue";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown_1);
            // 
            // FormGameClear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(404, 441);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBoxJiki);
            this.Controls.Add(this.label1);
            this.Name = "FormGameClear";
            this.Text = "クリア画面";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJiki)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxJiki;
        private System.Windows.Forms.Label label1;
        private Button button1;
    }
}