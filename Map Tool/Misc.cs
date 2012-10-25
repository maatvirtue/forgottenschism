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
    public partial class Misc : Form
    {
        Tilemap tm;
        Main main;

        public Misc(Main fmain, Tilemap ftm)
        {
            InitializeComponent();

            tm = ftm;
            main = fmain;
        }

        private void cmd_clearref_Click(object sender, EventArgs e)
        {
            int nx = tm.NumX;
            int ny = tm.NumY;

            for (int i = 0; i < nx; i++)
                for (int j = 0; j < ny; j++)
                    tm.get(i, j).RegionName = "";

            main.updateRefList();
        }
    }
}
