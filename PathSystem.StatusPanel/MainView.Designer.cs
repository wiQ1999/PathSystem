
namespace PathSystem.StatusPanel
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EntitiesList = new System.Windows.Forms.ListBox();
            this.MapControll = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MapControll)).BeginInit();
            this.SuspendLayout();
            // 
            // EntitiesList
            // 
            this.EntitiesList.FormattingEnabled = true;
            this.EntitiesList.ItemHeight = 15;
            this.EntitiesList.Location = new System.Drawing.Point(665, 12);
            this.EntitiesList.Name = "EntitiesList";
            this.EntitiesList.Size = new System.Drawing.Size(307, 634);
            this.EntitiesList.TabIndex = 2;
            // 
            // MapControll
            // 
            this.MapControll.BackColor = System.Drawing.SystemColors.Control;
            this.MapControll.Location = new System.Drawing.Point(12, 12);
            this.MapControll.Name = "MapControll";
            this.MapControll.Size = new System.Drawing.Size(640, 640);
            this.MapControll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MapControll.TabIndex = 3;
            this.MapControll.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.MapControll);
            this.Controls.Add(this.EntitiesList);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainView";
            this.Text = "API Status Panel";
            ((System.ComponentModel.ISupportInitialize)(this.MapControll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MapControl;
        private System.Windows.Forms.ListBox EntitiesList;
        private System.Windows.Forms.PictureBox MapControll;
    }
}

