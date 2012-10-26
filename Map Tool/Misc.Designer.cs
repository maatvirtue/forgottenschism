namespace Map_Tool
{
    partial class Misc
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
            this.cmd_clearref = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmd_clearref
            // 
            this.cmd_clearref.Location = new System.Drawing.Point(12, 21);
            this.cmd_clearref.Name = "cmd_clearref";
            this.cmd_clearref.Size = new System.Drawing.Size(129, 23);
            this.cmd_clearref.TabIndex = 0;
            this.cmd_clearref.Text = "Clear Map References";
            this.cmd_clearref.UseVisualStyleBackColor = true;
            this.cmd_clearref.Click += new System.EventHandler(this.cmd_clearref_Click);
            // 
            // Misc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.cmd_clearref);
            this.Name = "Misc";
            this.Text = "Misc";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmd_clearref;
    }
}