using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    public class XmlTransaltor
    {
        public static XmlElement charToXml(XmlDocument doc, Character c)
        {
            XmlElement e = doc.CreateElement("Character");

            e.AppendChild(stat(doc, c.stats));

            if (c == GameState.CurrentState.mainChar)
                e.SetAttribute("main", "main");

            e.SetAttribute("org", c.Organization);

            e.SetAttribute("lvl", c.Lvl.ToString());
            e.SetAttribute("exp", c.Exp.ToString());
            e.SetAttribute("name", c.Name);
            e.SetAttribute("type", c.Type.ToString());

            return e;
        }

        public static XmlElement citymap(XmlDocument doc, CityMap cmap, String region)
        {
            XmlElement e = doc.CreateElement("CityMap");

            e.SetAttribute("region", region);
            e.SetAttribute("numx", cmap.NumX.ToString());
            e.SetAttribute("numy", cmap.NumY.ToString());

            XmlElement p;

            for (int i = 0; i < cmap.NumX; i++)
                for (int j = 0; j < cmap.NumY; j++)
                    if (cmap.isCity(i, j))
                    {
                        p = pos(doc, "Pos", new Point(i, j));

                        p.AppendChild(city(doc, cmap.get(i, j)));

                        e.AppendChild(p);
                    }

            return e;
        }

        public static CityMap citymap(XmlElement e)
        {
            CityMap cmap = new CityMap(int.Parse(e.GetAttribute("numx")), int.Parse(e.GetAttribute("numy")));

            Point p;

            foreach (XmlElement ce in e.ChildNodes)
                if (ce.Name == "Pos")
                {
                    p = pos(ce);
                    cmap.set(p.X, p.Y, city(ce["City"]));
                }

            return cmap;
        }

        public static Content.Money_info money_info(XmlElement e)
        {
            Content.Money_info mi = new Content.Money_info();

            mi.start = int.Parse(e.GetAttribute("start"));
            mi.perRegion = int.Parse(e.GetAttribute("perregion"));

            return mi;
        }

        public static SpellList spellList(XmlElement e)
        {
            List<Spell> spls = new List<Spell>();

            foreach (XmlElement te in e.ChildNodes)
                spls.Add(spell(te));

            return new SpellList(spls);
        }

        public static XmlElement spellList(XmlDocument doc, SpellList spls)
        {
            XmlElement e = doc.CreateElement("SpellList");

            foreach (Spell sp in spls.toList())
                e.AppendChild(spell(doc, sp));

            return e;
        }

        public static XmlElement spell(XmlDocument doc, Spell sp)
        {
            XmlElement e = doc.CreateElement("Spell");

            e.SetAttribute("name", sp.Name);
            e.SetAttribute("damage", sp.Damage.ToString());
            e.SetAttribute("manacost", sp.ManaCost.ToString());
            e.SetAttribute("minlvl", sp.MinLvl.ToString());
            e.SetAttribute("maxlvl", sp.MaxLvl.ToString());

            return e;
        }

        public static Spell spell(XmlElement e)
        {
            String name = e.GetAttribute("name");
            int damage = int.Parse(e.GetAttribute("damage"));
            int manaCost = int.Parse(e.GetAttribute("manacost"));
            int minLvl = int.Parse(e.GetAttribute("minlvl"));
            int maxLvl = int.Parse(e.GetAttribute("maxlvl"));

            return new Spell(name, damage, manaCost, minLvl, maxLvl);
        }

        public static XmlElement city(XmlDocument doc, City c)
        {
            XmlElement e = doc.CreateElement("City");

            e.SetAttribute("name", c.Name);
            e.SetAttribute("owner", c.Owner);
            e.SetAttribute("side", c.Side.ToString());
            e.SetAttribute("factor", c.EnnemyFactor.ToString());

            return e;
        }

        public static City city(XmlElement e)
        {
            City c = new City(e.GetAttribute("name"));

            c.Owner = e.GetAttribute("owner");
            c.Side = (City.CitySide)Enum.Parse(typeof(City.CitySide), e.GetAttribute("side"));
            c.EnnemyFactor = int.Parse(e.GetAttribute("factor"));

            return c;
        }

        public static List<Character> standbyChar(XmlElement e)
        {
            List<Character> cls = new List<Character>();

            foreach (XmlElement te in e.ChildNodes)
                if (te.Name == "Character")
                    cls.Add(xmlToChar(te));

            return cls;
        }

        public static Army army(XmlElement e)
        {
            Army a = new Army();

            a.Standby = standbyChar(e["StandbyChar"]);

            List<Unit> uls = new List<Unit>();

            foreach (XmlElement te in e.ChildNodes)
                if (te.Name == "Unit")
                    uls.Add(unit(te));

            a.Units = uls;

            a.Money = int.Parse(e.GetAttribute("money"));

            a.Inventory = inventory(e["Inventory"]);

            return a;
        }

        public static Unit unit(XmlElement e)
        {
            Character c = null;
            Point lp = new Point();

            foreach (XmlElement te in e.ChildNodes)
                if (te.Name == "Pos" && te.GetAttribute("leader") == "True")
                {
                    c = xmlToChar(te["Character"]);
                    lp = new Point(int.Parse(te.GetAttribute("x")), int.Parse(te.GetAttribute("y")));
                }

            if (c == null)
                return null;

            Unit u = new Unit(c, lp.X, lp.Y);

            foreach (XmlElement te in e.ChildNodes)
                if (te.Name == "Pos" && te.GetAttribute("leader") == "False")
                {
                    c = xmlToChar(te["Character"]);
                    u.set(int.Parse(te.GetAttribute("x")), int.Parse(te.GetAttribute("y")), c);
                }

            u.Name = e.GetAttribute("name");
            u.Organization = e.GetAttribute("org");

            u.Inventory = inventory(e["Inventory"]);

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

            c.Organization=e.GetAttribute("org");

            if (e.GetAttribute("main") == "main")
                GameState.CurrentState.mainChar = c;

            return c;
        }

        public static XmlElement army(XmlDocument doc, Army a, String org)
        {
            XmlElement e = doc.CreateElement("Army");

            e.SetAttribute("money", a.Money.ToString());

            e.SetAttribute("org", org);
            e.AppendChild(standbyChar(doc, a.Standby));

            foreach (Unit u in a.Units)
                e.AppendChild(unit(doc, u));

            e.AppendChild(inventory(doc, a.Inventory));

            return e;
        }

        public static XmlElement inventory(XmlDocument doc, Inventory inv)
        {
            XmlElement e = doc.CreateElement("Inventory");

            foreach (Item i in inv.Items)
                e.AppendChild(item(doc, i));

            return e;
        }

        public static Inventory inventory(XmlElement e)
        {
            Inventory inv = new Inventory();

            foreach (XmlElement te in e.ChildNodes)
                if (te.Name == "Item")
                    inv.Items.Add(item(te));

            return inv;
        }

        public static XmlElement item(XmlDocument doc, Item i)
        {
            XmlElement e = doc.CreateElement("Item");

            e.SetAttribute("name", i.Name);
            e.SetAttribute("cost", i.Cost.ToString());
            e.SetAttribute("type", i.Type.ToString());

            e.AppendChild(traits(doc, i.Modifications));

            return e;
        }

        public static Item item(XmlElement e)
        {
            return new Item(e.GetAttribute("name"), (Item.Item_Type)Enum.Parse(typeof(Item.Item_Type), e.GetAttribute("type")), int.Parse(e.GetAttribute("cost")),  traits(e["Traits"]));
        }

        public static XmlElement unit(XmlDocument doc, Unit u)
        {
            XmlElement e = doc.CreateElement("Unit");
            XmlElement te;

            e.SetAttribute("name", u.Name);
            e.SetAttribute("org", u.Organization);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (!u.isChar(i, j))
                        continue;

                    te = doc.CreateElement("Pos");
                    te.SetAttribute("x", i.ToString());
                    te.SetAttribute("y", j.ToString());
                    te.SetAttribute("leader", u.isLeader(i, j).ToString());
                    te.AppendChild(charToXml(doc, u.get(i, j)));
                    e.AppendChild(te);
                }

            e.AppendChild(inventory(doc, u.Inventory));

            return e;
        }

        public static XmlElement standbyChar(XmlDocument doc, List<Character> cls)
        {
            XmlElement e = doc.CreateElement("StandbyChar");

            foreach (Character c in cls)
                e.AppendChild(charToXml(doc, c));

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
            int numx = int.Parse(e.GetAttribute("numx"));
            int numy = int.Parse(e.GetAttribute("numy"));

            Fog f = new Fog(numx, numy);

            bool[] ba = Gen.BitPacker.unpack(Gen.hexstr(e.GetAttribute("data")));

            for (int j = 0; j < numy; j++)
                for (int i = 0; i < numx; i++)
                    f.set(i, j, ba[j * numx + i]);

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
            stat.hp = int.Parse(e.GetAttribute("hp"));
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

            t.str = (int)double.Parse(e.GetAttribute("str"));
            t.dex = (int)double.Parse(e.GetAttribute("dex"));
            t.con = (int)double.Parse(e.GetAttribute("con"));
            t.intel = (int)double.Parse(e.GetAttribute("intel"));
            t.wis = (int)double.Parse(e.GetAttribute("wis"));
            t.spd = (int)double.Parse(e.GetAttribute("spd"));

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
}
