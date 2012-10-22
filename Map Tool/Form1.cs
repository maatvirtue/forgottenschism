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
        const int NX = 10;
        const int NY = 5;

        public Form1()
        {
            InitializeComponent();

            tm = new Tilemap(NX, NY);

            map = new Map(NX, NY);
            map.Location = new Point(10, 20);

            grp_main.Controls.Add(map);
        }

        public Form1(Tilemap ftm)
        {
            InitializeComponent();

            if (tm.NumX >= NX && tm.NumY >= NY)
                tm = ftm;
            else
                tm = new Tilemap(NX, NY);

            map = new Map(NX, NY);
            map.Location = new Point(10, 20);

            grp_main.Controls.Add(map);
        }

        private void changeUp(object o, EventArgs e)
        {
            if (lb_tiles.SelectedIndex >= 1)
                lb_tiles.SelectedIndex--;
            else
                lb_tiles.SelectedIndex = lb_tiles.Items.Count - 1;
        }

        private void changeDown(object o, EventArgs e)
        {
            if (lb_tiles.SelectedIndex <= lb_tiles.Items.Count - 2)
                lb_tiles.SelectedIndex++;
            else
                lb_tiles.SelectedIndex = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            pch = false;

            num_numx.Value = tm.NumX;
            num_numy.Value = tm.NumY;

            map.setTilemap(tm);
            map.curChanged = selChange;
            map.set = setTile;
            map.changeDown = changeDown;
            map.changeUp = changeUp;

            lb_tiles.Items.Add(Tile.TileType.CITY);
            lb_tiles.Items.Add(Tile.TileType.FOREST);
            lb_tiles.Items.Add(Tile.TileType.MOUNTAIN);
            lb_tiles.Items.Add(Tile.TileType.PLAIN);
            lb_tiles.Items.Add(Tile.TileType.ROADS);
            lb_tiles.Items.Add(Tile.TileType.WATER);
        }

        private void selChange(object o, EventArgs e)
        {
            Point p=(Point)((EventArgObject)e).o;

            pch = true;

            num_selx.Value = p.X;
            num_sely.Value = p.Y;

            Tile t=tm.get(p.X, p.Y);

            if (t.Region != null)
                txt_refmap.Text = t.RegionName;
            else
                txt_refmap.Text = "";

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

        private void updateDim()
        {
            if (num_numx.Value < NX || num_numy.Value < NY)
                return;

            grp_info.Enabled = false;
            grp_main.Enabled = false;
            grp_sel.Enabled = false;

            map.setCur(0, 0);

            tm = new Tilemap((int)num_numx.Value, (int)num_numy.Value, tm);
            map.setTilemap(tm);

            updateRefList();

            selChange(this, new EventArgObject(new Point(0, 0)));

            grp_info.Enabled = true;
            grp_main.Enabled = true;
            grp_sel.Enabled = true;

            map.Refresh();
        }

        private void cmd_load_Click(object sender, EventArgs e)
        {
            grp_info.Enabled = false;
            grp_main.Enabled = false;
            grp_sel.Enabled = false;

            Tilemap tmp = new Tilemap(NX, NY);
            String[] refls;

            map.setCur(0, 0);

            try
            {
                tmp.load(txt_file.Text);

                if (tmp.NumX < NX || tmp.NumY < NY)
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

            pch = true;
            num_numx.Value = tm.NumX;
            num_numy.Value = tm.NumY;
            pch = false;

            lb_ref.Items.Clear();

            foreach (String s in refls)
                lb_ref.Items.Add(s);

            selChange(this, new EventArgObject(new Point(0,0)));

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

        private void setTile(object o, EventArgs e)
        {
            if (lb_tiles.SelectedIndex < 0)
                return;

            tm.get((int)num_selx.Value, (int)num_sely.Value).Type = (Tile.TileType)lb_tiles.SelectedItem;

            map.Refresh();
        }

        private void cmd_set_Click(object sender, EventArgs e)
        {
            setTile(this, null);
        }

        private void updateRefList()
        {
            lb_ref.Items.Clear();

            for (int i = 0; i < tm.NumX; i++)
                for (int e = 0; e < tm.NumY; e++)
                    if (tm.get(i, e).RegionName != "" && !lb_ref.Items.Contains(tm.get(i, e).RegionName))
                        lb_ref.Items.Add(tm.get(i, e).RegionName);
        }

        private void txt_refmap_TextChanged(object sender, EventArgs e)
        {
            tm.get((int)num_selx.Value, (int)num_sely.Value).RegionName = txt_refmap.Text;

            updateRefList();
        }

        private void num_numx_ValueChanged(object sender, EventArgs e)
        {
            if(!pch)
                updateDim();
        }

        private void num_numy_ValueChanged(object sender, EventArgs e)
        {
            if (!pch)
                updateDim();
        }

        private void rb_mapmode_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
