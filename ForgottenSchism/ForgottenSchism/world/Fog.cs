﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Fog
    {
        String name;
        bool[,] map;
        static byte[] uid = { 0, 0, 0, 1 };
        static byte[] type = { 0, 2 };
        static byte[] ver = { 1, 0 };

        public Fog(int fw, int fh)
        {
            map=new bool[fw,fh];
        }

        public bool get(int x, int y)
        {
            return map[x, y];
        }

        public void set(int x, int y, bool b)
        {
            map[x, y] = b;
        }
    }
}