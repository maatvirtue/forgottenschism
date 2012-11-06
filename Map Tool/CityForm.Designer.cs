namespace Map_Tool
{
    partial class CityForm
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
            this.gb_general = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_owners = new System.Windows.Forms.ListBox();
            this.gb_seltile = new System.Windows.Forms.GroupBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.rb_none = new System.Windows.Forms.RadioButton();
            this.rb_left = new System.Windows.Forms.RadioButton();
            this.rb_right = new System.Windows.Forms.RadioButton();
            this.rb_top = new System.Windows.Forms.RadioButton();
            this.rb_bottom = new System.Windows.Forms.RadioButton();
            this.txt_owner = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_citystat = new System.Windows.Forms.Label();
            this.cmd_createCity = new System.Windows.Forms.Button();
            this.cmd_delcity = new System.Windows.Forms.Button();
            this.lbl_sel = new System.Windows.Forms.Label();
            this.gb_general.SuspendLayout();
            this.gb_seltile.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_general
            // 
            this.gb_general.Controls.Add(this.label1);
            this.gb_general.Controls.Add(this.lb_owners);
            this.gb_general.Location = new System.Drawing.Point(12, 12);
            this.gb_general.Name = "gb_general";
            this.gb_general.Size = new System.Drawing.Size(409, 153);
            this.gb_general.TabIndex = 0;
            this.gb_general.TabStop = false;
            this.gb_general.Text = "General";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Owners";
            // 
            // lb_owners
            // 
            this.lb_owners.FormattingEnabled = true;
            this.lb_owners.Location = new System.Drawing.Point(26, 44);
            this.lb_owners.Name = "lb_owners";
            this.lb_owners.Size = new System.Drawing.Size(128, 95);
            this.lb_owners.TabIndex = 0;
            // 
            // gb_seltile
            // 
            this.gb_seltile.Controls.Add(this.lbl_sel);
            this.gb_seltile.Controls.Add(this.txt_name);
            this.gb_seltile.Controls.Add(this.label4);
            this.gb_seltile.Controls.Add(this.lbl_name);
            this.gb_seltile.Controls.Add(this.rb_none);
            this.gb_seltile.Controls.Add(this.rb_left);
            this.gb_seltile.Controls.Add(this.rb_right);
            this.gb_seltile.Controls.Add(this.rb_top);
            this.gb_seltile.Controls.Add(this.rb_bottom);
            this.gb_seltile.Controls.Add(this.txt_owner);
            this.gb_seltile.Controls.Add(this.label3);
            this.gb_seltile.Controls.Add(this.label2);
            this.gb_seltile.Location = new System.Drawing.Point(12, 230);
            this.gb_seltile.Name = "gb_seltile";
            this.gb_seltile.Size = new System.Drawing.Size(409, 185);
            this.gb_seltile.TabIndex = 1;
            this.gb_seltile.TabStop = false;
            this.gb_seltile.Text = "Selected Tile";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(66, 57);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(100, 20);
            this.txt_name.TabIndex = 11;
            this.txt_name.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Name:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(6, 70);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(0, 13);
            this.lbl_name.TabIndex = 9;
            // 
            // rb_none
            // 
            this.rb_none.AutoSize = true;
            this.rb_none.Location = new System.Drawing.Point(271, 101);
            this.rb_none.Name = "rb_none";
            this.rb_none.Size = new System.Drawing.Size(51, 17);
            this.rb_none.TabIndex = 8;
            this.rb_none.TabStop = true;
            this.rb_none.Text = "None";
            this.rb_none.UseVisualStyleBackColor = true;
            this.rb_none.CheckedChanged += new System.EventHandler(this.rb_none_CheckedChanged);
            // 
            // rb_left
            // 
            this.rb_left.AutoSize = true;
            this.rb_left.Location = new System.Drawing.Point(201, 101);
            this.rb_left.Name = "rb_left";
            this.rb_left.Size = new System.Drawing.Size(43, 17);
            this.rb_left.TabIndex = 7;
            this.rb_left.TabStop = true;
            this.rb_left.Text = "Left";
            this.rb_left.UseVisualStyleBackColor = true;
            this.rb_left.CheckedChanged += new System.EventHandler(this.rb_left_CheckedChanged);
            // 
            // rb_right
            // 
            this.rb_right.AutoSize = true;
            this.rb_right.Location = new System.Drawing.Point(335, 101);
            this.rb_right.Name = "rb_right";
            this.rb_right.Size = new System.Drawing.Size(50, 17);
            this.rb_right.TabIndex = 6;
            this.rb_right.TabStop = true;
            this.rb_right.Text = "Right";
            this.rb_right.UseVisualStyleBackColor = true;
            this.rb_right.CheckedChanged += new System.EventHandler(this.rb_right_CheckedChanged);
            // 
            // rb_top
            // 
            this.rb_top.AutoSize = true;
            this.rb_top.Location = new System.Drawing.Point(271, 60);
            this.rb_top.Name = "rb_top";
            this.rb_top.Size = new System.Drawing.Size(44, 17);
            this.rb_top.TabIndex = 5;
            this.rb_top.TabStop = true;
            this.rb_top.Text = "Top";
            this.rb_top.UseVisualStyleBackColor = true;
            this.rb_top.CheckedChanged += new System.EventHandler(this.rb_top_CheckedChanged);
            // 
            // rb_bottom
            // 
            this.rb_bottom.AutoSize = true;
            this.rb_bottom.Location = new System.Drawing.Point(271, 145);
            this.rb_bottom.Name = "rb_bottom";
            this.rb_bottom.Size = new System.Drawing.Size(58, 17);
            this.rb_bottom.TabIndex = 4;
            this.rb_bottom.TabStop = true;
            this.rb_bottom.Text = "Bottom";
            this.rb_bottom.UseVisualStyleBackColor = true;
            this.rb_bottom.CheckedChanged += new System.EventHandler(this.rb_bottom_CheckedChanged);
            // 
            // txt_owner
            // 
            this.txt_owner.Location = new System.Drawing.Point(66, 98);
            this.txt_owner.Name = "txt_owner";
            this.txt_owner.Size = new System.Drawing.Size(100, 20);
            this.txt_owner.TabIndex = 3;
            this.txt_owner.TextChanged += new System.EventHandler(this.txt_owner_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tile Side";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Owner:";
            // 
            // lbl_citystat
            // 
            this.lbl_citystat.AutoSize = true;
            this.lbl_citystat.Location = new System.Drawing.Point(87, 190);
            this.lbl_citystat.Name = "lbl_citystat";
            this.lbl_citystat.Size = new System.Drawing.Size(0, 13);
            this.lbl_citystat.TabIndex = 2;
            // 
            // cmd_createCity
            // 
            this.cmd_createCity.Location = new System.Drawing.Point(191, 179);
            this.cmd_createCity.Name = "cmd_createCity";
            this.cmd_createCity.Size = new System.Drawing.Size(75, 23);
            this.cmd_createCity.TabIndex = 3;
            this.cmd_createCity.Text = "Create City";
            this.cmd_createCity.UseVisualStyleBackColor = true;
            this.cmd_createCity.Click += new System.EventHandler(this.cmd_createCity_Click);
            // 
            // cmd_delcity
            // 
            this.cmd_delcity.Location = new System.Drawing.Point(294, 180);
            this.cmd_delcity.Name = "cmd_delcity";
            this.cmd_delcity.Size = new System.Drawing.Size(75, 23);
            this.cmd_delcity.TabIndex = 4;
            this.cmd_delcity.Text = "Delete City";
            this.cmd_delcity.UseVisualStyleBackColor = true;
            this.cmd_delcity.Click += new System.EventHandler(this.cmd_delcity_Click);
            // 
            // lbl_sel
            // 
            this.lbl_sel.AutoSize = true;
            this.lbl_sel.Location = new System.Drawing.Point(9, 29);
            this.lbl_sel.Name = "lbl_sel";
            this.lbl_sel.Size = new System.Drawing.Size(0, 13);
            this.lbl_sel.TabIndex = 12;
            // 
            // CityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 426);
            this.Controls.Add(this.cmd_delcity);
            this.Controls.Add(this.cmd_createCity);
            this.Controls.Add(this.lbl_citystat);
            this.Controls.Add(this.gb_seltile);
            this.Controls.Add(this.gb_general);
            this.Name = "CityForm";
            this.Text = "CityForm";
            this.gb_general.ResumeLayout(false);
            this.gb_general.PerformLayout();
            this.gb_seltile.ResumeLayout(false);
            this.gb_seltile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_general;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lb_owners;
        private System.Windows.Forms.GroupBox gb_seltile;
        private System.Windows.Forms.RadioButton rb_none;
        private System.Windows.Forms.RadioButton rb_left;
        private System.Windows.Forms.RadioButton rb_right;
        private System.Windows.Forms.RadioButton rb_top;
        private System.Windows.Forms.RadioButton rb_bottom;
        private System.Windows.Forms.TextBox txt_owner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_citystat;
        private System.Windows.Forms.Button cmd_createCity;
        private System.Windows.Forms.Button cmd_delcity;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_sel;
    }
}