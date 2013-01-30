﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    public class GameState
    {
        static GameState currentState;

        public bool saved;

        public Army mainArmy;
        public Character mainChar;
        public Point mainCharPos;
        public Fog gen;
        public Dictionary<String, CityMap> citymap;
        public int turn;

        public GameState()
        {
            turn = 0;
            saved = false;

            citymap = new Dictionary<string, CityMap>();

            citymap.Add("gen", Content.Instance.gen.CityMap);

            foreach (String s in Tilemap.reflist("map\\gen.map"))
                citymap.Add(s, new Tilemap(s).CityMap);
        }

        public void save(String path)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement e = doc.CreateElement("Save");

            e.SetAttribute("turn", turn.ToString());

            e.AppendChild(XmlTransaltor.pos(doc, "MainCharPos", mainCharPos));
            e.AppendChild(XmlTransaltor.fog(doc, gen));
            e.AppendChild(XmlTransaltor.army(doc, mainArmy, "main"));
            
            XmlElement cmls=doc.CreateElement("CityMapList");

            foreach(KeyValuePair<String, CityMap> kv in citymap)
                cmls.AppendChild(XmlTransaltor.citymap(doc, kv.Value, kv.Key));

            e.AppendChild(cmls);

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

            turn = int.Parse(doc.DocumentElement.GetAttribute("turn"));

            /*foreach (XmlElement e in doc.DocumentElement.ChildNodes)
                if (e.Name=="Character" && e.GetAttribute("org") == "main")
                    mainChar = XmlTransaltor.xmlToChar(e);*/

            foreach (XmlElement e in doc.DocumentElement.ChildNodes)
                if (e.Name=="Army" && e.GetAttribute("org") == "main")
                    mainArmy = XmlTransaltor.army(e);

            //mainChar = mainArmy.MainCharUnit.Leader;

            mainCharPos = XmlTransaltor.pos(doc.DocumentElement["MainCharPos"]);

            gen = XmlTransaltor.fog(doc.DocumentElement["Fog"]);

            citymap.Clear();

            foreach (XmlElement e in doc.DocumentElement["CityMapList"].ChildNodes)
                citymap.Add(e.GetAttribute("region"), XmlTransaltor.citymap(e));

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
