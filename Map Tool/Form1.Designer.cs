namespace Map_Tool
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
            this.txt_file = new System.Windows.Forms.TextBox();
            this.cmd_browse = new System.Windows.Forms.Button();
            this.cmd_load = new System.Windows.Forms.Button();
            this.cmd_save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grp_file = new System.Windows.Forms.GroupBox();
            this.grp_info = new System.Windows.Forms.GroupBox();
            this.num_numy = new System.Windows.Forms.NumericUpDown();
            this.num_numx = new System.Windows.Forms.NumericUpDown();
            this.lb_ref = new System.Windows.Forms.ListBox();
            this.grp_main = new System.Windows.Forms.GroupBox();
            this.cmd_set = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_tiles = new System.Windows.Forms.ListBox();
            this.grp_sel = new System.Windows.Forms.GroupBox();
            this.cmd_openref = new System.Windows.Forms.Button();
            this.lbl_refmap = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.num_sely = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.num_selx = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.grp_file.SuspendLayout();
            this.grp_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_numy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_numx)).BeginInit();
            this.grp_main.SuspendLayout();
            this.grp_sel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_sely)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_selx)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File:";
            // 
            // txt_file
            // 
            this.txt_file.Location = new System.Drawing.Point(62, 38);
            this.txt_file.Name = "txt_file";
            this.txt_file.Size = new System.Drawing.Size(255, 20);
            this.txt_file.TabIndex = 1;
            // 
            // cmd_browse
            // 
            this.cmd_browse.Location = new System.Drawing.Point(336, 36);
            this.cmd_browse.Name = "cmd_browse";
            this.cmd_browse.Size = new System.Drawing.Size(75, 23);
            this.cmd_browse.TabIndex = 2;
            this.cmd_browse.Text = "Browse";
            this.cmd_browse.UseVisualStyleBackColor = true;
            this.cmd_browse.Click += new System.EventHandler(this.cmd_browse_Click);
            // 
            // cmd_load
            // 
            this.cmd_load.Location = new System.Drawing.Point(82, 64);
            this.cmd_load.Name = "cmd_load";
            this.cmd_load.Size = new System.Drawing.Size(75, 23);
            this.cmd_load.TabIndex = 3;
            this.cmd_load.Text = "Load";
            this.cmd_load.UseVisualStyleBackColor = true;
            this.cmd_load.Click += new System.EventHandler(this.cmd_load_Click);
            // 
            // cmd_save
            // 
            this.cmd_save.Location = new System.Drawing.Point(204, 64);
            this.cmd_save.Name = "cmd_save";
            this.cmd_save.Size = new System.Drawing.Size(75, 23);
            this.cmd_save.TabIndex = 4;
            this.cmd_save.Text = "Save";
            this.cmd_save.UseVisualStyleBackColor = true;
            this.cmd_save.Click += new System.EventHandler(this.cmd_save_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "NumX:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "NumY:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Map Referenced";
            // 
            // grp_file
            // 
            this.grp_file.Controls.Add(this.txt_file);
            this.grp_file.Controls.Add(this.label1);
            this.grp_file.Controls.Add(this.cmd_browse);
            this.grp_file.Controls.Add(this.cmd_load);
            this.grp_file.Controls.Add(this.cmd_save);
            this.grp_file.Location = new System.Drawing.Point(139, 12);
            this.grp_file.Name = "grp_file";
            this.grp_file.Size = new System.Drawing.Size(434, 114);
            this.grp_file.TabIndex = 10;
            this.grp_file.TabStop = false;
            this.grp_file.Text = "File";
            // 
            // grp_info
            // 
            this.grp_info.Controls.Add(this.num_numy);
            this.grp_info.Controls.Add(this.num_numx);
            this.grp_info.Controls.Add(this.lb_ref);
            this.grp_info.Controls.Add(this.label3);
            this.grp_info.Controls.Add(this.label2);
            this.grp_info.Controls.Add(this.label4);
            this.grp_info.Location = new System.Drawing.Point(12, 143);
            this.grp_info.Name = "grp_info";
            this.grp_info.Size = new System.Drawing.Size(227, 174);
            this.grp_info.TabIndex = 11;
            this.grp_info.TabStop = false;
            this.grp_info.Text = "Info";
            // 
            // num_numy
            // 
            this.num_numy.Location = new System.Drawing.Point(155, 20);
            this.num_numy.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_numy.Name = "num_numy";
            this.num_numy.Size = new System.Drawing.Size(57, 20);
            this.num_numy.TabIndex = 12;
            this.num_numy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_numx
            // 
            this.num_numx.Location = new System.Drawing.Point(57, 20);
            this.num_numx.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_numx.Name = "num_numx";
            this.num_numx.Size = new System.Drawing.Size(55, 20);
            this.num_numx.TabIndex = 11;
            this.num_numx.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lb_ref
            // 
            this.lb_ref.FormattingEnabled = true;
            this.lb_ref.Location = new System.Drawing.Point(15, 69);
            this.lb_ref.Name = "lb_ref";
            this.lb_ref.Size = new System.Drawing.Size(197, 95);
            this.lb_ref.TabIndex = 10;
            // 
            // grp_main
            // 
            this.grp_main.Controls.Add(this.cmd_set);
            this.grp_main.Controls.Add(this.label5);
            this.grp_main.Controls.Add(this.lb_tiles);
            this.grp_main.Location = new System.Drawing.Point(266, 143);
            this.grp_main.Name = "grp_main";
            this.grp_main.Size = new System.Drawing.Size(427, 311);
            this.grp_main.TabIndex = 12;
            this.grp_main.TabStop = false;
            this.grp_main.Text = "Main";
            // 
            // cmd_set
            // 
            this.cmd_set.Location = new System.Drawing.Point(348, 254);
            this.cmd_set.Name = "cmd_set";
            this.cmd_set.Size = new System.Drawing.Size(43, 23);
            this.cmd_set.TabIndex = 2;
            this.cmd_set.Text = "Set";
            this.cmd_set.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Tiles";
            // 
            // lb_tiles
            // 
            this.lb_tiles.FormattingEnabled = true;
            this.lb_tiles.Location = new System.Drawing.Point(301, 32);
            this.lb_tiles.Name = "lb_tiles";
            this.lb_tiles.Size = new System.Drawing.Size(120, 212);
            this.lb_tiles.TabIndex = 0;
            // 
            // grp_sel
            // 
            this.grp_sel.Controls.Add(this.cmd_openref);
            this.grp_sel.Controls.Add(this.lbl_refmap);
            this.grp_sel.Controls.Add(this.label8);
            this.grp_sel.Controls.Add(this.num_sely);
            this.grp_sel.Controls.Add(this.label7);
            this.grp_sel.Controls.Add(this.label6);
            this.grp_sel.Controls.Add(this.num_selx);
            this.grp_sel.Location = new System.Drawing.Point(12, 323);
            this.grp_sel.Name = "grp_sel";
            this.grp_sel.Size = new System.Drawing.Size(248, 131);
            this.grp_sel.TabIndex = 13;
            this.grp_sel.TabStop = false;
            this.grp_sel.Text = "Selected Tile";
            // 
            // cmd_openref
            // 
            this.cmd_openref.Location = new System.Drawing.Point(40, 102);
            this.cmd_openref.Name = "cmd_openref";
            this.cmd_openref.Size = new System.Drawing.Size(127, 23);
            this.cmd_openref.TabIndex = 16;
            this.cmd_openref.Text = "Open Referenced map";
            this.cmd_openref.UseVisualStyleBackColor = true;
            this.cmd_openref.Click += new System.EventHandler(this.cmd_openref_Click);
            // 
            // lbl_refmap
            // 
            this.lbl_refmap.AutoSize = true;
            this.lbl_refmap.Location = new System.Drawing.Point(23, 74);
            this.lbl_refmap.Name = "lbl_refmap";
            this.lbl_refmap.Size = new System.Drawing.Size(0, 13);
            this.lbl_refmap.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Referenced map:";
            // 
            // num_sely
            // 
            this.num_sely.Location = new System.Drawing.Point(127, 21);
            this.num_sely.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_sely.Name = "num_sely";
            this.num_sely.Size = new System.Drawing.Size(57, 20);
            this.num_sely.TabIndex = 13;
            this.num_sely.ValueChanged += new System.EventHandler(this.num_sely_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(95, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "X:";
            // 
            // num_selx
            // 
            this.num_selx.Location = new System.Drawing.Point(29, 19);
            this.num_selx.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_selx.Name = "num_selx";
            this.num_selx.Size = new System.Drawing.Size(55, 20);
            this.num_selx.TabIndex = 13;
            this.num_selx.ValueChanged += new System.EventHandler(this.num_selx_ValueChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 466);
            this.Controls.Add(this.grp_sel);
            this.Controls.Add(this.grp_main);
            this.Controls.Add(this.grp_info);
            this.Controls.Add(this.grp_file);
            this.Name = "Form1";
            this.Text = "Map Tool";
            this.grp_file.ResumeLayout(false);
            this.grp_file.PerformLayout();
            this.grp_info.ResumeLayout(false);
            this.grp_info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_numy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_numx)).EndInit();
            this.grp_main.ResumeLayout(false);
            this.grp_main.PerformLayout();
            this.grp_sel.ResumeLayout(false);
            this.grp_sel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_sely)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_selx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_file;
        private System.Windows.Forms.Button cmd_browse;
        private System.Windows.Forms.Button cmd_load;
        private System.Windows.Forms.Button cmd_save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grp_file;
        private System.Windows.Forms.GroupBox grp_info;
        private System.Windows.Forms.ListBox lb_ref;
        private System.Windows.Forms.GroupBox grp_main;
        private System.Windows.Forms.Button cmd_set;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lb_tiles;
        private System.Windows.Forms.NumericUpDown num_numy;
        private System.Windows.Forms.NumericUpDown num_numx;
        private System.Windows.Forms.GroupBox grp_sel;
        private System.Windows.Forms.Button cmd_openref;
        private System.Windows.Forms.Label lbl_refmap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown num_sely;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown num_selx;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

