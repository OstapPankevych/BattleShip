namespace BattleShip.DesktopUI.Field
{
    partial class UcField
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
            this.pb_PlayRegion = new System.Windows.Forms.PictureBox();
            this.pb_BackGroundRegion = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_PlayRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackGroundRegion)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_PlayRegion
            // 
            this.pb_PlayRegion.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_PlayRegion.Location = new System.Drawing.Point(44, 47);
            this.pb_PlayRegion.Name = "pb_PlayRegion";
            this.pb_PlayRegion.Size = new System.Drawing.Size(251, 205);
            this.pb_PlayRegion.TabIndex = 0;
            this.pb_PlayRegion.TabStop = false;
            this.pb_PlayRegion.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_PlayRegion_MouseClick);
            this.pb_PlayRegion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_PlayRegion_MouseDown);
            this.pb_PlayRegion.MouseLeave += new System.EventHandler(this.pb_PlayRegion_MouseLeave);
            this.pb_PlayRegion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_PlayRegion_MouseMove);
            this.pb_PlayRegion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_PlayRegion_MouseUp);
            // 
            // pb_BackGroundRegion
            // 
            this.pb_BackGroundRegion.BackColor = System.Drawing.SystemColors.Highlight;
            this.pb_BackGroundRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_BackGroundRegion.Location = new System.Drawing.Point(0, 0);
            this.pb_BackGroundRegion.Name = "pb_BackGroundRegion";
            this.pb_BackGroundRegion.Size = new System.Drawing.Size(326, 300);
            this.pb_BackGroundRegion.TabIndex = 1;
            this.pb_BackGroundRegion.TabStop = false;
            // 
            // UcField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pb_PlayRegion);
            this.Controls.Add(this.pb_BackGroundRegion);
            this.Name = "UcField";
            this.Size = new System.Drawing.Size(326, 300);
            ((System.ComponentModel.ISupportInitialize)(this.pb_PlayRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackGroundRegion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_PlayRegion;
        private System.Windows.Forms.PictureBox pb_BackGroundRegion;


    }
}
