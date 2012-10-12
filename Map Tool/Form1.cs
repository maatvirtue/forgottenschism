using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ForgottenSchism.world;
using ForgottenSchism.engine;

namespace Map_Tool
{
    public partial class Form1 : Form
    {
        Tilemap tm;
        Map map;
        bool pch;

        public Form1()
        {
            InitializeComponent();

            pch = false;

            tm = new Tilemap(4, 4);

            map = new Map();
            map.Location = new Point(10, 20);

            grp_main.Controls.Add(map);

            num_numx.Value = tm.NumX;
            num_numy.Value = tm.NumY;
        }

        public Form1(Tilemap ftm)
        {
            InitializeComponent();

            pch = false;

            tm = ftm;

            map = new Map();
            map.Location = new Point(10, 20);

            grp_main.Controls.Add(map);

            num_numx.Value = tm.NumX;
            num_numy.Value = tm.NumY;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            map.setTilemap(tm);
            map.curChanged = selChange;
        }

        private void selChange(object o, EventArgs e)
        {
            Point p=(Point)((EventArgObject)e).o;

            pch = true;

            num_selx.Value = p.X;
            num_sely.Value = p.Y;

            Tile t=tm.get(p.X, p.Y);

            if (t.Region != null)
                lbl_refmap.Text = t.Region.Name;
            else
                lbl_refmap.Text = "";

            pch = false;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }

        private void cmd_browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = ".map";
            openFileDialog1.ShowDialog();

            txt_file.Text = openFileDialog1.FileName;
        }

        private void cmd_load_Click(object sender, EventArgs e)
        {
            grp_info.Enabled = false;
            grp_main.Enabled = false;
            grp_sel.Enabled = false;

            map.setCur(0, 0);

            Tilemap tmp = new Tilemap(4, 4);
            String[] refls;

            try
            {
                tmp.load(txt_file.Text);

                if (tmp.NumX < 4 || tmp.NumY < 4)
                    throw new Exception();

                map.setTilemap(tmp);
                refls = Tilemap.reflist(txt_file.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading file", "Error Loading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tm = tmp;
            num_numx.Value = tm.NumX;
            num_numy.Value = tm.NumY;

            foreach (String s in refls)
                lb_ref.Items.Add(s);

            grp_info.Enabled = true;
            grp_main.Enabled = true;
            grp_sel.Enabled = true;

            map.Refresh();
        }

        private void num_selx_ValueChanged(object sender, EventArgs e)
        {
            if (!pch)
                map.setCur((int)num_selx.Value, (int)num_sely.Value);
        }

        private void num_sely_ValueChanged(object sender, EventArgs e)
        {
            if (!pch)
                map.setCur((int)num_selx.Value, (int)num_sely.Value);
        }

        private void cmd_openref_Click(object sender, EventArgs e)
        {
            Tile t = tm.get((int)num_selx.Value, (int)num_sely.Value);

            if (t.Region == null)
                return;

            Form1 f = new Form1(t.Region);
            f.Show();
        }

        private void cmd_save_Click(object sender, EventArgs e)
        {
            grp_info.Enabled = false;
            grp_main.Enabled = false;
            grp_sel.Enabled = false;

            tm.save(txt_file.Text);

            grp_info.Enabled = true;
            grp_main.Enabled = true;
            grp_sel.Enabled = true;
        }
    }
}
