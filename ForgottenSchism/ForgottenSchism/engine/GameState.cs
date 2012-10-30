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
            public static XmlElement charToXml(XmlDocument doc, Character c, String id)
            {
                XmlElement e = doc.CreateElement("Character");

                e.AppendChild(charStat(doc, c.Stat));
                if(id!="")
                    e.SetAttribute("id", id);
                e.SetAttribute("lvl", c.Lvl.ToString());
                e.SetAttribute("exp", c.Exp.ToString());
                e.SetAttribute("name", c.Name);
                e.SetAttribute("type", c.Type.ToString());

                return e;
            }

            public static XmlElement army(XmlDocument doc, Army a, String id)
            {
                XmlElement e = doc.CreateElement("Army");

                e.SetAttribute("id", id);
                e.AppendChild(standbyChar(doc, a.Standby));
                //

                return e;
            }

            public static XmlElement unit(XmlDocument doc, Unit u)
            {
                XmlElement e = doc.CreateElement("Unit");

                //

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

                e.SetAttribute("data", Gen.strhex(f.toByteArray()));

                return e;
            }

            public static XmlElement charStat(XmlDocument doc, Character.Stats stats)
            {
                XmlElement e = doc.CreateElement("Stat");

                e.AppendChild(charStatTraits(doc, stats.traits));
                e.SetAttribute("state", stats.state.ToString());
                e.SetAttribute("hp", stats.hp.ToString());
                e.SetAttribute("maxhp", stats.maxHp.ToString());
                e.SetAttribute("mana", stats.mana.ToString());
                e.SetAttribute("maxmana", stats.maxMana.ToString());
                e.SetAttribute("movement", stats.movement.ToString());

                return e;
            }

            public static XmlElement charStatTraits(XmlDocument doc, Character.Stats.Traits traits)
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
            //
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
