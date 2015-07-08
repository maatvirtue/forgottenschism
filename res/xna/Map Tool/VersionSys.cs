using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ForgottenSchism.engine
{
    class VersionSys
    {
        struct Header
        {
            public byte[] magic;
            public byte[] uid;
            public byte[] type;
            public byte[] ver;

            public Header(int t)
            {
                magic=new byte[4];
                uid=new byte[4];
                type=new byte[2];
                ver=new byte[2];
            }
        };

        static byte[] magic = { 0xbe, 0xe2, 0xc0, 0xde };

        public static bool eq(byte[] ba1, byte[] ba2)
        {
            if (ba1.GetLength(0) != ba2.GetLength(0))
                return false;

            for (int i = 0; i < ba1.GetLength(0); i++)
                if (ba1[i] != ba2[i])
                    return false;

            return true;
        }

        public static void writeHeader(FileStream fout, byte[] fuid, byte[] ftype, byte[] fver)
        {
            fout.Write(magic, 0, 4);
            fout.Write(fuid, 0, 4);
            fout.Write(ftype, 0, 2);
            fout.Write(fver, 0, 2);
        }

        private static Header read(String path)
        {
            Header ret=new Header(0);
            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Read(ret.magic, 0, 4);
            fin.Read(ret.uid, 0, 4);
            fin.Read(ret.type, 0, 2);
            fin.Read(ret.ver, 0, 2);

            fin.Close();

            return ret;
        }

        public static bool isRecognizable(String path)
        {
            Header h;

            try
            {
                h = read(path);
            }
            catch (Exception e)
            {
                return false;
            }

            return magic == h.magic;
        }

        public static bool match(String path, byte[] fuid)
        {
            Header h;

            try
            {
                h=read(path);
            }
            catch(Exception e)
            {
                return false;
            }

            if (magic != h.magic)
                return false;

            return fuid == h.uid;
        }

        public static bool match(String path, byte[] fuid, byte[] ftype)
        {
            Header h;

            try
            {
                h = read(path);
            }
            catch (Exception e)
            {
                return false;
            }
            
            if (magic != h.magic)
                return false;

            return (fuid == h.uid&&ftype==h.type);
        }

        public static bool match(String path, byte[] fuid, byte[] ftype, byte[] fver)
        {
            Header h;

            try
            {
                h = read(path);
            }
            catch (Exception e)
            {
                return false;
            }

            if (!eq(magic, h.magic))
                return false;

            return (eq(fuid, h.uid)&&eq(ftype, h.type)&&eq(fver, h.ver));
        }
    }
}
