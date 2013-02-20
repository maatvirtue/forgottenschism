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

        /// <summary>
        /// Turns passed without the main character attacking an enemy city
        /// </summary>
        public int att;

        public GameState()
        {
            turn = 0;
            att = 0;
            saved = false;

            mainArmy = new Army();
            mainChar = null;
            mainCharPos = new Point(-1, -1);
            gen = null;

            citymap = new Dictionary<String, CityMap>();

            citymap.Add("gen", Content.Instance.gen.CityMap.clone());

            foreach (String s in Tilemap.reflist("map\\gen.map"))
                citymap.Add(s, new Tilemap(s).CityMap.clone());
        }

        public int getCaptureNum(String ownership)
        {
            int count = 0;
            CityMap gen = citymap["gen"];

            foreach (City c in gen.Cities)
            {
                if (c == null)
                    continue;

                if (c.Owner == ownership)
                    count++;
            }

            return count;
        }

        public bool isCaptured(String city, String ownership)
        {
            if (city == "")
                return true;

            CityMap gen = citymap["gen"];

            foreach (City c in gen.Cities)
            {
                if (c == null)
                    continue;

                if (c.Name == city)
                {
                    if (c.Owner == ownership)
                        return true;
                    else
                        return false;
                }
            }

            return false;
        }

        public void save(String path)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement e = doc.CreateElement("Save");

            e.SetAttribute("turn", turn.ToString());
            e.SetAttribute("att", att.ToString());

            e.AppendChild(XmlTransaltor.pos(doc, "MainCharPos", mainCharPos));
            e.AppendChild(XmlTransaltor.fog(doc, gen));
            e.AppendChild(XmlTransaltor.army(doc, mainArmy, "main"));
            
            XmlElement cmls=doc.CreateElement("CityMapList");

            foreach(KeyValuePair<String, CityMap> kv in citymap)
                cmls.AppendChild(XmlTransaltor.citymap(doc, kv.Value, kv.Key));

            e.AppendChild(cmls);

            XmlElement rcls = doc.CreateElement("RecruitmentList");

            foreach (String s in Content.Instance.recruitedLs)
                rcls.AppendChild(XmlTransaltor.recruit(doc, s));

            e.AppendChild(rcls);

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
            att = int.Parse(doc.DocumentElement.GetAttribute("att"));

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

            foreach (XmlElement e in doc.DocumentElement["RecruitmentList"].ChildNodes)
                Content.Instance.recruitedLs.Add(e.GetAttribute("name"));

            Content.Instance.cleanRecruitLs();

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
