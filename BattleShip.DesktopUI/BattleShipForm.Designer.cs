namespace BattleShip.DesktopUI
{
    partial class BattleShipForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleShipForm));
            this.ucField2 = new BattleShip.DesktopUI.Field.UcField();
            this.ucField1 = new BattleShip.DesktopUI.Field.UcField();
            this.ucPanel = new BattleShip.DesktopUI.InfoPanel.uc_InfoPanel();
            this.SuspendLayout();
            // 
            // ucField2
            // 
            this.ucField2.AvailableShipsDictionary = null;
            this.ucField2.CurrentGun = null;
            this.ucField2.GameProccesStep = BattleShip.DesktopUI.Field.StepGameProcces.SettingShip;
            this.ucField2.Location = new System.Drawing.Point(413, 0);
            this.ucField2.Name = "ucField2";
            this.ucField2.Size = new System.Drawing.Size(326, 300);
            this.ucField2.TabIndex = 7;
            // 
            // ucField1
            // 
            this.ucField1.AvailableShipsDictionary = null;
            this.ucField1.CurrentGun = null;
            this.ucField1.GameProccesStep = BattleShip.DesktopUI.Field.StepGameProcces.SettingShip;
            this.ucField1.Location = new System.Drawing.Point(0, 0);
            this.ucField1.Name = "ucField1";
            this.ucField1.Size = new System.Drawing.Size(326, 300);
            this.ucField1.TabIndex = 5;
            // 
            // ucPanel
            // 
            this.ucPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucPanel.BackColor = System.Drawing.SystemColors.Info;
            this.ucPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucPanel.Location = new System.Drawing.Point(0, 0);
            this.ucPanel.Name = "ucPanel";
            this.ucPanel.Size = new System.Drawing.Size(980, 668);
            this.ucPanel.TabIndex = 2;
            // 
            // BattleShipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(984, 565);
            this.Controls.Add(this.ucField2);
            this.Controls.Add(this.ucField1);
            this.Controls.Add(this.ucPanel);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BattleShipForm";
            this.Text = "BattleShip";
            this.Load += new System.EventHandler(this.BattleShipForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private InfoPanel.uc_InfoPanel ucPanel;
        private Field.UcField ucField1;
        private Field.UcField ucField2;
    }
}