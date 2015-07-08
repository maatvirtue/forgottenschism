using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Drawing.Imaging;

using ForgottenSchism.world;
using ForgottenSchism.engine;

namespace Map_Tool
{
    public partial class Map : UserControl
    {
        const int TW=64;
        const int TH=64;
        int nx;
        int ny;
        Point tlc;
        Point curp;
        Image tcur;
        Dictionary<Tile.TileType, Image> buf;
        Image fog;
        Tilemap tm;
        bool fogmode;

        public EventHandler curChanged;
        public EventHandler set;
        public EventHandler changeUp;
        public EventHandler changeDown;
        
        public Map(int fnx, int fny)
        {
            InitializeComponent();

            SetDoubleBuffered(this);

            fogmode = false;

            nx = fnx;
            ny = fny;

            Size = new System.Drawing.Size(TW * nx, TH * ny);

            tlc = new Point(0, 0);
            curp = new Point(0, 0);

            buf = new Dictionary<Tile.TileType, Image>();
        }

        public Map()
        {
            fogmode = false;

            InitializeComponent();

            SetDoubleBuffered(this);

            nx = 4;
            ny = 4;

            Size = new System.Drawing.Size(TW * nx, TH * ny);

            tlc = new Point(0, 0);
            curp = new Point(0, 0);
        }

        public bool FogMode
        {
            get { return fogmode; }
            set { fogmode = value; }
        }

        public void setCur(int x, int y)
        {
            if (x >= tm.NumX || y >= tm.NumY)
                return;

            Point tp = new Point(-1, -1);

            int i = 3;
            int e = 3;

            while (!(x - i >= 0 && y - e >= 0) || i < 0 || e < 0)
            {
                if (x - i < 0)
                    i--;

                if (y - e < 0)
                    e--;
            }

            tp.X = x;
            tp.Y = y;
            x = x - i;
            y = y - e;

            if (x < tm.NumX - nx)
            {
                tlc.X = x;
                curp.X = 0;
            }
            else
            {
                tlc.X = tm.NumX - nx;
                curp.X = x - tlc.X;
            }

            if (y < tm.NumY - ny)
            {
                tlc.Y = y;
                curp.Y = 0;
            }
            else
            {
                tlc.Y = tm.NumY - ny;
                curp.Y = y - tlc.Y;
            }

            if (tp.X >= 0 && tp.Y >= 0)
            {
                curp = tp;
            }

            Refresh();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            tm = new Tilemap(nx, ny);

            tcur = new Bitmap(Map_Tool.Properties.Resources.cur, TW, TH);

            fog = createRect(Color.Black);

            buf.Add(Tile.TileType.CITY, new Bitmap(Map_Tool.Properties.Resources.town, TW, TH));
            buf.Add(Tile.TileType.FOREST, new Bitmap(Map_Tool.Properties.Resources.forest, TW, TH));
            buf.Add(Tile.TileType.MOUNTAIN, new Bitmap(Map_Tool.Properties.Resources.mountain, TW, TH));
            buf.Add(Tile.TileType.PLAIN, new Bitmap(Map_Tool.Properties.Resources.plains, TW, TH));
            buf.Add(Tile.TileType.ROADS, new Bitmap(Map_Tool.Properties.Resources.road, TW, TH));
            buf.Add(Tile.TileType.WATER, new Bitmap(Map_Tool.Properties.Resources.sea, TW, TH));
        }

        public void setTilemap(Tilemap ftm)
        {
            if (ftm.NumX >= nx && ftm.NumY >= ny)
                tm = ftm;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int j = 0; j < ny; j++)
                for (int i = 0; i < nx; i++)
                {
                    if (tm.Fog.get((int)(i + tlc.X), (int)(j + tlc.Y)) && fogmode)
                        e.Graphics.DrawImage(fog, new Point((i * TW), (j * TH)));
                    else
                        e.Graphics.DrawImage(buf[tm.get((int)(i + tlc.X), (int)(j + tlc.Y)).Type], new Point((i * TW), (j * TH)));
                }

            e.Graphics.DrawImage(tcur, new Point(curp.X * TW, curp.Y * TH));
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Up)
                if (curp.Y != 0)
                    curp.Y--;
                else if (tlc.Y != 0)
                    tlc.Y--;

            if (e.KeyCode == Keys.Down)
                if (curp.Y != ny - 1)
                    curp.Y++;
                else if (tlc.Y + ny <= tm.NumY - 1)
                    tlc.Y++;

            if (e.KeyCode == Keys.Left)
                if (curp.X != 0)
                    curp.X--;
                else if (tlc.X != 0)
                    tlc.X--;

            if (e.KeyCode == Keys.Right)
                if (curp.X != nx - 1)
                    curp.X++;
                else if (tlc.X + nx <= tm.NumX - 1)
                    tlc.X++;

            if (e.KeyCode == Keys.Space && set != null)
                set(this, null);

            if (e.KeyCode == Keys.A && changeUp != null)
                changeUp(this, null);

            if (e.KeyCode == Keys.S && changeDown != null)
                changeDown(this, null);

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (curChanged != null)
                    curChanged(this, new EventArgObject(new Point(curp.X + tlc.X, curp.Y + tlc.Y)));

                Refresh();
            }
        }

        private static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        private Image createRect(Color c)
        {
            Bitmap b = new Bitmap(64, 64, PixelFormat.Format32bppArgb);

            BitmapData bmpData = b.LockBits(new Rectangle(0, 0, 64, 64), ImageLockMode.WriteOnly, b.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int[] rgbValues = new int[64*64];

            for (int i = 0; i < 64 * 64; i++)
                rgbValues[i] = c.ToArgb();

            Marshal.Copy(rgbValues, 0, ptr, 64*64);

            b.UnlockBits(bmpData);

            return b;
        }
    }
}
