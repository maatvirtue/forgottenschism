using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ForgottenSchism.engine
{
    public class VersionSys
    {
        public struct VersionIdentity
        {
            public byte[] uid;
            public byte[] type;
            public byte[] ver;

            public VersionIdentity(int t)
            {
                uid = new byte[4];
                type = new byte[2];
                ver = new byte[2];
            }

            public VersionIdentity(byte[] fuid, byte[] ftype, byte[] fver)
            {
                uid = fuid;
                type = ftype;
                ver = fver;
            }
        };

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

        public static void writeHeader(FileStream fout, VersionIdentity vi)
        {
            fout.Write(magic, 0, 4);
            fout.Write(vi.uid, 0, 4);
            fout.Write(vi.type, 0, 2);
            fout.Write(vi.ver, 0, 2);
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

        public static bool match(String path, VersionIdentity vi)
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

            if (!Gen.eq(magic, h.magic))
                return false;

            return (Gen.eq(vi.uid, h.uid) && Gen.eq(vi.type, h.type) && Gen.eq(vi.ver, h.ver));
        }
    }
}
