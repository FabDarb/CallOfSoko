namespace CallOfSokoClient
{
    partial class Form1
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
            mainDisplay = new PictureBox();
            LifePictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)mainDisplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LifePictureBox).BeginInit();
            SuspendLayout();
            // 
            // mainDisplay
            // 
            mainDisplay.Dock = DockStyle.Fill;
            mainDisplay.Location = new Point(0, 0);
            mainDisplay.Name = "mainDisplay";
            mainDisplay.Size = new Size(800, 450);
            mainDisplay.TabIndex = 0;
            mainDisplay.TabStop = false;
            mainDisplay.Click += mainDisplay_Click;
            mainDisplay.Paint += mainDisplay_Paint;
            mainDisplay.MouseMove += mainDisplay_MouseMove;
            // 
            // LifePictureBox
            // 
            LifePictureBox.BackColor = Color.Transparent;
            LifePictureBox.Location = new Point(12, 415);
            LifePictureBox.Name = "LifePictureBox";
            LifePictureBox.Size = new Size(776, 23);
            LifePictureBox.TabIndex = 1;
            LifePictureBox.TabStop = false;
            LifePictureBox.Paint += LifePictureBox_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LifePictureBox);
            Controls.Add(mainDisplay);
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)mainDisplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)LifePictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox mainDisplay;
        private PictureBox LifePictureBox;
    }
}
