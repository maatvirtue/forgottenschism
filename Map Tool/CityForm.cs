using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ForgottenSchism.world;

namespace Map_Tool
{
    public partial class CityForm : Form
    {
        Tilemap tm;
        int x;
        int y;

        public CityForm(Tilemap ftm, int fx, int fy)
        {
            tm = ftm;
            x = fx;
            y = fy;

            InitializeComponent();

            updateOwnerList();

            selTile(x, y);
        }

        private void updateOwnerList()
        {
            lb_owners.Items.Clear();

            foreach (City c in tm.CityMap.toArray())
                if (c.Owner != "" && !lb_owners.Items.Contains(c.Owner))
                    lb_owners.Items.Add(c.Owner);
        }

        public void selTile(int fx, int fy)
        {
            x = fx;
            y = fy;

            lbl_sel.Text = "X: " + x + " Y: " + y;
            lbl_name.Text = tm.get(x, y).RegionName;

            if (!tm.CityMap.isCity(x, y))
            {
                lbl_citystat.Text = "No City here";
                lbl_citystat.ForeColor = Color.Red;

                gb_seltile.Enabled = false;

                cmd_createCity.Enabled = true;
                cmd_delcity.Enabled = false;

                return;
            }
            else
            {
                lbl_citystat.Text = "";
                gb_seltile.Enabled = true;

                cmd_createCity.Enabled = false;
                cmd_delcity.Enabled = true;
            }

            //if (!tm.CityMap.isCity(x, y))
                //tm.CityMap.set(x, y, new City(tm.get(x, y).RegionName));

            City c = tm.CityMap.get(x, y);

            txt_owner.Text = c.Owner;

            if (c.Side == City.CitySide.NONE)
                rb_none.Checked = true;
            else if (c.Side == City.CitySide.TOP)
                rb_top.Checked = true;
            else if (c.Side == City.CitySide.RIGHT)
                rb_right.Checked = true;
            else if (c.Side == City.CitySide.BOTTOM)
                rb_bottom.Checked = true;
            else if (c.Side == City.CitySide.LEFT)
                rb_left.Checked = true;
        }

        private void txt_owner_TextChanged(object sender, EventArgs e)
        {
            tm.CityMap.get(x, y).Owner = txt_owner.Text;

            updateOwnerList();
        }

        private void updateSide()
        {
            City c=tm.CityMap.get(x, y);

            if (rb_none.Checked == true)
                c.Side = City.CitySide.NONE;
            else if (rb_top.Checked == true)
                c.Side = City.CitySide.TOP;
            else if (rb_right.Checked == true)
                c.Side = City.CitySide.RIGHT;
            else if (rb_bottom.Checked == true)
                c.Side = City.CitySide.BOTTOM;
            else if (rb_left.Checked == true)
                c.Side = City.CitySide.LEFT;
        }

        private void rb_top_CheckedChanged(object sender, EventArgs e)
        {
            updateSide();
        }

        private void rb_none_CheckedChanged(object sender, EventArgs e)
        {
            updateSide();
        }

        private void rb_right_CheckedChanged(object sender, EventArgs e)
        {
            updateSide();
        }

        private void rb_bottom_CheckedChanged(object sender, EventArgs e)
        {
            updateSide();
        }

        private void rb_left_CheckedChanged(object sender, EventArgs e)
        {
            updateSide();
        }

        private void cmd_createCity_Click(object sender, EventArgs e)
        {
            tm.CityMap.set(x, y, new City(tm.get(x, y).RegionName));

            selTile(x, y);
        }

        private void cmd_delcity_Click(object sender, EventArgs e)
        {
            tm.CityMap.set(x, y, null);

            selTile(x, y);
        }
    }
}
