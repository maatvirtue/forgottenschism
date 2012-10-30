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

        private static char chex(byte b)
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
