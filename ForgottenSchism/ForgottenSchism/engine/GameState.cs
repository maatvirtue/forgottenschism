﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    class GameState
    {
        class XmlTransaltor
        {
            public static XmlElement charToXml(XmlDocument doc, Character c, String org)
            {
                XmlElement e = doc.CreateElement("Character");

                e.AppendChild(stat(doc, c.Stat));
                if(org!="")
                    e.SetAttribute("org", org);
                e.SetAttribute("lvl", c.Lvl.ToString());
                e.SetAttribute("exp", c.Exp.ToString());
                e.SetAttribute("name", c.Name);
                e.SetAttribute("type", c.Type.ToString());

                return e;
            }

            public static List<Character> standbyChar(XmlElement e)
            {
                List<Character> cls = new List<Character>();

                foreach (XmlElement te in e.ChildNodes)
                    if(te.Name=="Character")
                        cls.Add(xmlToChar(te));

                return cls;
            }

            public static Army army(XmlElement e)
            {
                Army a = new Army();

                a.Standby=standbyChar(e["StandbyChar"]);

                List<Unit> uls = new List<Unit>();

                foreach (XmlElement te in e.ChildNodes)
                    if(te.Name=="Unit")
                        uls.Add(unit(te));

                a.Units = uls;

                return a;
            }

            public static Unit unit(XmlElement e)
            {
                Character c=null;
                Point lp=new Point();

                foreach(XmlElement te in e.ChildNodes)
                    if (te.Name=="Pos" && te.GetAttribute("leader") == "True")
                    {
                        c = xmlToChar(te["Character"]);
                        lp = new Point(int.Parse(te.GetAttribute("x")), int.Parse(te.GetAttribute("y")));
                    }

                if (c == null)
                    return null;

                Unit u = new Unit(c, lp.X, lp.Y);

                foreach (XmlElement te in e.ChildNodes)
                    if (te.Name=="Pos" && te.GetAttribute("leader") == "False")
                    {
                        c = xmlToChar(te["Character"]);
                        u.set(int.Parse(te.GetAttribute("x")), int.Parse(te.GetAttribute("y")), c);
                    }

                u.Name = e.GetAttribute("name");

                return u;
            }

            public static Character xmlToChar(XmlElement e)
            {
                Character c;
                String name = e.GetAttribute("name");
                Character.Class_Type type = (Character.Class_Type)Enum.Parse(typeof(Character.Class_Type), e.GetAttribute("type"));

                if (type == Character.Class_Type.FIGHTER)
                    c = new Fighter(name);
                else if (type == Character.Class_Type.ARCHER)
                    c = new Archer(name);
                else if (type == Character.Class_Type.CASTER)
                    c = new Caster(name);
                else if (type == Character.Class_Type.HEALER)
                    c = new Healer(name);
                else if (type == Character.Class_Type.SCOUT)
                    c = new Scout(name);
                else
                    c = new Fighter(name);

                c.Lvl = int.Parse(e.GetAttribute("lvl"));
                c.Exp = int.Parse(e.GetAttribute("exp"));

                return c;
            }

            public static XmlElement army(XmlDocument doc, Army a, String org)
            {
                XmlElement e = doc.CreateElement("Army");
                
                e.SetAttribute("org", org);
                e.AppendChild(standbyChar(doc, a.Standby));

                foreach (Unit u in a.Units)
                    e.AppendChild(unit(doc, u));

                return e;
            }

            public static XmlElement unit(XmlDocument doc, Unit u)
            {
                XmlElement e = doc.CreateElement("Unit");
                XmlElement te;

                e.SetAttribute("name", u.Name);

                for(int i=0; i<5; i++)
                    for (int j = 0; j < 5; j++)
                    {
                        if (!u.isChar(i, j))
                            continue;

                        te = doc.CreateElement("Pos");
                        te.SetAttribute("x", i.ToString());
                        te.SetAttribute("y", j.ToString());
                        te.SetAttribute("leader", u.isLeader(i, j).ToString());
                        te.AppendChild(charToXml(doc, u.get(i, j), ""));
                        e.AppendChild(te);
                    }

                return e;
            }

            public static XmlElement standbyChar(XmlDocument doc, List<Character> cls)
            {
                XmlElement e = doc.CreateElement("StandbyChar");

                foreach(Character c in cls)
                    e.AppendChild(charToXml(doc, c, ""));

                return e;
            }

            public static XmlElement fog(XmlDocument doc, Fog f)
            {
                XmlElement e = doc.CreateElement("Fog");

                e.SetAttribute("numx", f.NumX.ToString());
                e.SetAttribute("numy", f.NumY.ToString());
                e.SetAttribute("data", Gen.strhex(f.toByteArray()));

                return e;
            }

            public static Fog fog(XmlElement e)
            {
                int numx=int.Parse(e.GetAttribute("numx"));
                int numy=int.Parse(e.GetAttribute("numy"));

                Fog f=new Fog(numx, numy);
                
                bool[] ba = Gen.BitPacker.unpack(Gen.hexstr(e.GetAttribute("data")));

                for(int j=0; j<numy; j++)
                    for(int i=0; i<numx; i++)
                        f.set(i, j, ba[j*numy+i]);

                return f;
            }

            public static XmlElement stat(XmlDocument doc, Character.Stats stats)
            {
                XmlElement e = doc.CreateElement("Stat");

                e.AppendChild(traits(doc, stats.traits));
                e.SetAttribute("state", stats.state.ToString());
                e.SetAttribute("hp", stats.hp.ToString());
                e.SetAttribute("maxhp", stats.maxHp.ToString());
                e.SetAttribute("mana", stats.mana.ToString());
                e.SetAttribute("maxmana", stats.maxMana.ToString());
                e.SetAttribute("movement", stats.movement.ToString());

                return e;
            }

            public static Character.Stats stat(XmlElement e)
            {
                Character.Stats stat;

                stat.traits = traits(e["Traits"]);
                stat.state = (Character.Stats.State)Enum.Parse(typeof(Character.Stats.State), e.GetAttribute("state"));
                stat.hp=int.Parse(e.GetAttribute("hp"));
                stat.maxHp = int.Parse(e.GetAttribute("maxhp"));
                stat.mana = int.Parse(e.GetAttribute("mana"));
                stat.maxMana = int.Parse(e.GetAttribute("maxMana"));
                stat.movement = int.Parse(e.GetAttribute("movement"));

                return stat;
            }

            public static XmlElement traits(XmlDocument doc, Character.Stats.Traits traits)
            {
                XmlElement e = doc.CreateElement("Traits");

                e.SetAttribute("str", traits.str.ToString());
                e.SetAttribute("dex", traits.dex.ToString());
                e.SetAttribute("con", traits.con.ToString());
                e.SetAttribute("wis", traits.wis.ToString());
                e.SetAttribute("intel", traits.intel.ToString());
                e.SetAttribute("spd", traits.spd.ToString());

                return e;
            }

            public static Character.Stats.Traits traits(XmlElement e)
            {
                Character.Stats.Traits t;

                t.str = int.Parse(e.GetAttribute("str"));
                t.dex = int.Parse(e.GetAttribute("dex"));
                t.con = int.Parse(e.GetAttribute("con"));
                t.intel = int.Parse(e.GetAttribute("intel"));
                t.wis = int.Parse(e.GetAttribute("wis"));
                t.spd = int.Parse(e.GetAttribute("spd"));

                return t;
            }

            public static Point pos(XmlElement e)
            {
                return new Point(int.Parse(e.GetAttribute("x")), int.Parse(e.GetAttribute("y")));
            }

            public static XmlElement pos(XmlDocument doc, String name, Point p)
            {
                XmlElement e = doc.CreateElement(name);

                e.SetAttribute("x", p.X.ToString());
                e.SetAttribute("y", p.Y.ToString());

                return e;
            }
        }

        static GameState currentState;

        public bool saved;

        public Army mainArmy;
        public Character mainChar;
        public Point mainCharPos;
        public Fog gen;

        public GameState()
        {
            saved = false;
        }

        public void save(String path)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement e;

            //Root Element
            e = doc.CreateElement("Save");
            
            e.AppendChild(XmlTransaltor.charToXml(doc, mainChar, "main"));
            e.AppendChild(XmlTransaltor.pos(doc, "MainCharPos", mainCharPos));
            e.AppendChild(XmlTransaltor.fog(doc, gen));
            e.AppendChild(XmlTransaltor.army(doc, mainArmy, "main"));

            doc.AppendChild(e);


            XmlTextWriter xw = new XmlTextWriter(path, Encoding.UTF8);
            xw.Formatting = Formatting.Indented;
            xw.Indentation = 1;
            xw.IndentChar = '\t';
            xw.QuoteChar = '"';

            doc.WriteTo(xw);
            xw.Close();

            saved = true;
        }

        public void load(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            foreach (XmlElement e in doc.DocumentElement.ChildNodes)
                if (e.Name=="Character" && e.GetAttribute("org") == "main")
                    mainChar = XmlTransaltor.xmlToChar(e);

            foreach (XmlElement e in doc.DocumentElement.ChildNodes)
                if (e.Name=="Army" && e.GetAttribute("org") == "main")
                    mainArmy = XmlTransaltor.army(e);

            mainCharPos = XmlTransaltor.pos(doc.DocumentElement["MainCharPos"]);

            gen = XmlTransaltor.fog(doc.DocumentElement["Fog"]);

            saved = true;
        }

        public static GameState CurrentState
        {
            get
            {
                if (currentState == null)
                    currentState = new GameState();

                return currentState;
            }

            set { currentState = value; }
        }
    }
}
