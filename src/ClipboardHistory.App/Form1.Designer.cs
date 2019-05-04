namespace ClipboardHistory.App
{
    partial class Form1
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
                _clipboardRegister.Dispose();
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
            this.lstClipboard = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstClipboard
            // 
            this.lstClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstClipboard.Location = new System.Drawing.Point(0, 0);
            this.lstClipboard.Name = "lstClipboard";
            this.lstClipboard.Size = new System.Drawing.Size(800, 450);
            this.lstClipboard.TabIndex = 0;
            this.lstClipboard.UseCompatibleStateImageBehavior = false;
            this.lstClipboard.View = System.Windows.Forms.View.List;
            this.lstClipboard.DoubleClick += new System.EventHandler(this.lstClipboard_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstClipboard);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstClipboard;
    }
}

