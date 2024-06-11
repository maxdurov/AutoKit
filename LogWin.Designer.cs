namespace AutoKit
{
    partial class LogWin
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
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this._countProg = new System.Windows.Forms.Label();
            this._btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LogBox
            // 
            this.LogBox.Location = new System.Drawing.Point(12, 12);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(776, 345);
            this.LogBox.TabIndex = 0;
            this.LogBox.Text = "";
            this.LogBox.TextChanged += new System.EventHandler(this.LogBox_TextChanged);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(12, 378);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(776, 23);
            this.pBar.TabIndex = 1;
            // 
            // _countProg
            // 
            this._countProg.AutoSize = true;
            this._countProg.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._countProg.Location = new System.Drawing.Point(12, 407);
            this._countProg.Name = "_countProg";
            this._countProg.Size = new System.Drawing.Size(39, 25);
            this._countProg.TabIndex = 2;
            this._countProg.Text = "1/1";
            // 
            // _btnClose
            // 
            this._btnClose.Enabled = false;
            this._btnClose.Location = new System.Drawing.Point(713, 407);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(75, 31);
            this._btnClose.TabIndex = 3;
            this._btnClose.Text = "Закрыть\r\n";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // LogWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 444);
            this.Controls.Add(this._btnClose);
            this.Controls.Add(this._countProg);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.LogBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LogWin";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label _countProg;
        private System.Windows.Forms.Button _btnClose;
    }
}