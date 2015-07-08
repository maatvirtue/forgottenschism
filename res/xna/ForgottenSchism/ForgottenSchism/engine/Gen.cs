using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;

namespace ForgottenSchism.engine
{
    public class Gen
    {
        public class BitPacker
        {
            List<byte> data;
            int bp;
            int sbp;

            //for add fun, effitiency if in a loop
            byte mask;

            public BitPacker()
            {
                data = new List<byte>();
                data.Add(0x00);

                bp = 0;
                sbp = 0;
            }

            public void add(bool b)
            {
                mask = (byte)(0x01 << (7 - sbp));

                if (b)
                {
                    data[bp] = (byte)(((int)data[bp]) | (int)mask);
                }
                else
                {
                    mask = (byte)(~(int)mask);

                    data[bp] = (byte)(((int)data[bp]) & (int)mask);
                }

                sbp++;

                if (sbp > 7)
                {
                    sbp = 0;
                    bp++;

                    if (bp > data.Count - 1)
                        data.Add(0x00);
                }
            }

            public static bool[] unpack(byte[] ba)
            {
                bool[] ret=new bool[ba.Length*8];
                int bp = 0;
                int sbp = 0;

                while (bp < ba.Length)
                {
                    ret[bp*8+sbp]=(ba[bp] & (1 << (7 - sbp)))>0;

                    sbp++;

                    if (sbp > 7)
                    {
                        bp++;
                        sbp = 0;
                    }
                }

                //Gen.p(ret);

                return ret;
            }

            public byte[] toByteArray()
            {
                if (bp == 0 && sbp == 0)
                    return new byte[0];

                int s = data.Count;

                if (sbp == 0)
                    s--;

                byte[] ret = new byte[s];
                 
                for (int i = 0; i < s; i++)
                    ret[i] = data[i];

                return ret;
            }
        }

        /// <summary>
        /// Are 2 points in diagonal (and the nearest possible)
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool isDiag(Point src, Point dest)
        {
            return ((dest.X == src.X - 1 || dest.X == src.X + 1) && (dest.Y == src.Y - 1 || dest.Y == src.Y + 1));
        }

        /// <summary>
        /// Are 2 points adjacent
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static bool isAdj(Point src, Point dest)
        {
            if (src == dest || isDiag(src, dest))
                return false;

            return (dest.X >= src.X - 1 && dest.X <= src.X + 1 && dest.Y >= src.Y - 1 && dest.Y <= src.Y + 1);
        }

        /// <summary>
        /// Distance between 2 points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static int dist(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        static Random r;

        /*public static void p(bool[] ba)
        {
            for (int i = 0; i < ba.Length; i++)
                System.Console.Out.Write(ba[i]+" ");

            System.Console.Out.WriteLine();
        }*/

        public static int max(int i1, int i2)
        {
            return i1 > i2 ? i1 : i2;
        }

        public static String[] rstra(FileStream fin)
        {
            int ns=fin.ReadByte();

            if(ns == 0)
                return new String[0];

            String[] ret=new String[ns];

            for (int i = 0; i < ns; i++)
                ret[i] = rstr(fin);

            return ret;
        }

        public static void wstra(FileStream fout, String[] stra)
        {
            int ns=stra.GetLength(0);

            fout.WriteByte((byte)ns);

            if (ns == 0)
                return;

            for (int i = 0; i < ns; i++)
                wstr(fout, stra[i]);
        }

        public static char chex(byte b)
        {
            if (b < 0x0a)
                return b.ToString()[0];
            else if(b==0x0a)
                return 'a';
            else if (b == 0x0b)
                return 'b';
            else if (b == 0x0c)
                return 'c';
            else if (b == 0x0d)
                return 'd';
            else if (b == 0x0e)
                return 'e';
            else
                return 'f';
        }

        public static String rstr(FileStream fin)
        {
            int l = fin.ReadByte();

            if(l==0)
                return"";

            char[] ca=new char[l];

            for (int i = 0; i < l; i++)
                ca[i] = (char)fin.ReadByte();

            return new String(ca);
        }

        public static void wstr(FileStream fout, String str)
        {
            fout.WriteByte((byte)str.Length);

            for (int i = 0; i < str.Length; i++)
                fout.WriteByte((byte)str[i]);
        }

        public static String strhex(byte b)
        {
            String s="";

            byte v = (byte)(((int)b) & 0xf0);
            v = (byte)((int)v >> 4);

            s += chex(v);

            v = (byte)(((int)b) & 0x0f);

            s += chex(v);

            return s;
        }

        public static byte[] hexstr(String str)
        {
            byte[] ba = new byte[str.Length / 2];

            for(int i=0; i<ba.GetLength(0); i++)
                ba[i]=parseHex(str.Substring(i*2, 2));

            System.Console.Out.WriteLine(strhex(ba));
            
            return ba;
        }

        public static byte parseHex(String str)
        {
            byte ret = 0;

            ret += parseHexSChar(str[1]);
            ret += (byte)(parseHexSChar(str[0])*0x10);

            return ret;
        }

        public static byte parseHexSChar(char c)
        {
            if (c >= '0' && c <= '9')
                return (byte)int.Parse(c.ToString());
            else if (c == 'a' || c == 'A')
                return 0x0a;
            else if (c == 'b' || c == 'B')
                return 0x0b;
            else if (c == 'c' || c == 'C')
                return 0x0c;
            else if (c == 'd' || c == 'D')
                return 0x0d;
            else if (c == 'e' || c == 'E')
                return 0x0e;
            else if (c == 'f' || c == 'F')
                return 0x0f;
            else
                return 0;
        }

        public static int d(int min, int max)
        {
            if (r == null)
                r = new Random();

            return r.Next(max-min+1)+min;
        }

        public static String strhex(byte[] ba)
        {
            String s = "";

            for(int i=0; i<ba.GetLength(0); i++)
                s+=strhex(ba[i]);

            return s;
        }

        public static String strbin(byte b)
        {
            String s = "";

            for (int i = 7; i >= 0; i--)
            {
                if ((b & ((int)1 << i)) > 0)
                    s += '1';
                else
                    s += '0';
            }

            return s;
        }

        public static byte conv(bool b)
        {
            if (b)
                return 0xff;
            else
                return 0x00;
        }

        public static bool eq(byte[] ba1, byte[] ba2)
        {
            if (ba1.GetLength(0) != ba2.GetLength(0))
                return false;

            for (int i = 0; i < ba1.GetLength(0); i++)
                if (ba1[i] != ba2[i])
                    return false;

            return true;
        }

        public static bool conv(byte b)
        {
            if (b == 0xff)
                return true;
            else
                return false;
        }

    }
}
