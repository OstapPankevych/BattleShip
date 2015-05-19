namespace BattleShip.DesktopUI.InfoPanel
{
    partial class uc_InfoPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb_Region = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Region)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Region
            // 
            this.pb_Region.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Region.Location = new System.Drawing.Point(0, 0);
            this.pb_Region.Name = "pb_Region";
            this.pb_Region.Size = new System.Drawing.Size(531, 387);
            this.pb_Region.TabIndex = 0;
            this.pb_Region.TabStop = false;
            this.pb_Region.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_Region_MouseClick);
            this.pb_Region.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_Region_MouseMove);
            // 
            // uc_InfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.pb_Region);
            this.DoubleBuffered = true;
            this.Name = "uc_InfoPanel";
            this.Size = new System.Drawing.Size(531, 387);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Region)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_Region;

    }
}
