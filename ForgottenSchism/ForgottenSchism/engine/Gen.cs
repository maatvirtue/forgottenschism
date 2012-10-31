using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

                Gen.p(ret);

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

        public static void p(bool[] ba)
        {
            for (int i = 0; i < ba.Length; i++)
                System.Console.Out.Write(ba[i]+" ");

            System.Console.Out.WriteLine();
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
            System.Console.Out.WriteLine("hex str: " + str);

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

        public static bool conv(byte b)
        {
            if (b == 0xff)
                return true;
            else
                return false;
        }

    }
}
