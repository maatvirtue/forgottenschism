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
    public partial class Form1 : Form
    {
        Tilemap tm;

        public Form1()
        {
            InitializeComponent();

            tm = new Tilemap(4, 4);

            map.setTilemap(tm);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }
    }
}
