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
        const int NY=4;
        const int NX=4;
        Point tlc;
        Point curp;
        Image tcur;
        Dictionary<Tile.TileType, Image> buf;
        Tilemap tm;

        public EventHandler curChanged;

        public Map()
        {
            InitializeComponent();

            SetDoubleBuffered(this);

            Size = new System.Drawing.Size(TW * NX, TH * NY);

            tlc = new Point(0, 0);
            curp = new Point(0, 0);

            buf = new Dictionary<Tile.TileType, Image>();
        }

        public void setCur(int x, int y)
        {
            if (x >= tm.NumX || y >= tm.NumY)
                return;

            if (x < tm.NumX - 4)
            {
                tlc.X = x;
                curp.X = 0;
            }
            else
            {
                tlc.X = tm.NumX - 4;
                curp.X = x - tlc.X;
            }

            if (y < tm.NumY - 4)
            {
                tlc.Y = y;
                curp.Y = 0;
            }
            else
            {
                tlc.Y = tm.NumY - 4;
                curp.Y = y - tlc.Y;
            }

            System.Console.Out.WriteLine("x: " + x + "y: " + y);
            System.Console.Out.WriteLine("tlc: " + tlc + " cur: " + curp);

            Refresh();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            tm = new Tilemap(NX, NY);

            Bitmap bmp = new Bitmap("Resources\\cur.png");

            tcur = new Bitmap(bmp, TW, TH);

            buf.Add(Tile.TileType.CITY, createRect(Color.Black));
            buf.Add(Tile.TileType.FOREST, createRect(Color.Green));
            buf.Add(Tile.TileType.MOUNTAIN, createRect(Color.Brown));
            buf.Add(Tile.TileType.PLAIN, createRect(Color.Yellow));
            buf.Add(Tile.TileType.ROADS, createRect(Color.Gray));
            buf.Add(Tile.TileType.WATER, createRect(Color.Blue));
        }

        public void setTilemap(Tilemap ftm)
        {
            if (ftm.NumX >= NX && ftm.NumY >= NY)
                tm = ftm;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int j = 0; j < NY; j++)
                for (int i = 0; i < NX; i++)
                    e.Graphics.DrawImage(buf[tm.get((int)(i + tlc.X), (int)(j + tlc.Y)).Type], new Point((i * TW), (j * TH)));

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
                if (curp.Y != NY - 1)
                    curp.Y++;
                else if (tlc.Y + NY <= tm.NumY - 1)
                    tlc.Y++;

            if (e.KeyCode == Keys.Left)
                if (curp.X != 0)
                    curp.X--;
                else if (tlc.X != 0)
                    tlc.X--;

            if (e.KeyCode == Keys.Right)
                if (curp.X != NX - 1)
                    curp.X++;
                else if (tlc.X + NX <= tm.NumX - 1)
                    tlc.X++;

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
