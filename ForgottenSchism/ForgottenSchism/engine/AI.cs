using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;
using ForgottenSchism.screen;
using ForgottenSchism.control;

namespace ForgottenSchism.engine
{
    class AI: GameComponent
    {
        private enum Region_Steps {FIND, MOVE};
        private enum AI_Type {WORLD, REGION, BATTLE};

        /// <summary>
        /// a unit that has an action to do
        /// </summary>
        private struct ActionUnit
        {
            public Point unit;
            public Point move;

            public ActionUnit(Point u, Point m)
            {
                unit = u;
                move = m;
            }

            public static ActionUnit Null
            {
                get { return new ActionUnit(Point.Zero, Point.Zero); }
            }

            public override bool Equals(object obj)
            {
                if (!(obj is ActionUnit))
                    return false;

                return this==(ActionUnit)obj;
            }

            public static bool operator ==(ActionUnit a, ActionUnit b)
            {
                return (a.unit == b.unit && a.move == b.move);
            }

            public static bool operator !=(ActionUnit a, ActionUnit b)
            {
                return !(a.unit == b.unit && a.move == b.move);
            }
        }

        /// <summary>
        /// Time to wait before next action is showned
        /// </summary>
        static TimeSpan delay = TimeSpan.FromMilliseconds(300);

        /// <summary>
        /// Called onced the AI is done
        /// </summary>
        public EventHandler done;

        /// <summary>
        /// When the last delay is over
        /// </summary>
        TimeSpan delayFinished;

        /// <summary>
        /// Map for showing AI movements
        /// </summary>
        Map map;

        /// <summary>
        /// WorldMap for graphical interaction
        /// </summary>
        WorldMap wmap;

        /// <summary>
        /// if there was a cursor added to the map it was added here
        /// </summary>
        Point acur;

        /// <summary>
        /// Organization curently acting
        /// </summary>
        String iorg;

        /// <summary>
        /// Tilemap to know the terrain
        /// </summary>
        Tilemap tm;

        UnitMap umap;

        CharMap cmap;

        /// <summary>
        /// set to true to activate a delay
        /// </summary>
        bool needDelay;

        /// <summary>
        /// if there is a battle going on
        /// </summary>
        bool inBattle;

        /// <summary>
        /// The algorithm currently running
        /// </summary>
        AI_Type type;

        /// <summary>
        /// Where the AI is in its steps
        /// </summary>
        Region_Steps reg_step;

        /// <summary>
        /// Curent character doing action in Battle AI
        /// </summary>
        Point cc;

        /// <summary>
        /// Step where the battle AI is at
        /// </summary>
        int bat_step;

        /// <summary>
        /// Wether to use UnitMap (true) or CharMap (false)
        /// </summary>
        bool useUnit;

        /// <summary>
        /// Region to pass to Battle screen if needed
        /// </summary>
        Region reg;

        /// <summary>
        /// Battle object to display label on the battle screen for battle AI and to access Unit in battle
        /// </summary>
        Battle bat;

        /// <summary>
        /// true if the AI is active (in the process of doing something)
        /// </summary>
        bool isActive;

        //UnitMap umap, Tilemap tm, String org

        public AI(): base(Game1.Instance)
        {
            delayFinished = new TimeSpan(0);
            needDelay = false;
            isActive = false;
            inBattle=false;
            acur = new Point(-1, -1);
        }

        /// <summary>
        /// If the AI is Active (doing something)
        /// </summary>
        public bool Active
        {
            get { return isActive; }
        }

        /// <summary>
        /// Setup map info
        /// </summary>
        public void set(Map fmap, Tilemap ftm)
        {
            map = fmap;
            tm = ftm;
        }

        /// <summary>
        /// Setup map info
        /// </summary>
        public void set(Map fmap, Tilemap ftm, UnitMap fumap)
        {
            set(fmap, ftm);

            umap = fumap;
        }

        /// <summary>
        /// Setup map info
        /// </summary>
        public void set(Map fmap, Tilemap ftm, CharMap fcmap)
        {
            set(fmap, ftm);

            cmap = fcmap;
        }

        /// <summary>
        /// Called when Battle finishes
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void battle_done(object o, EventArgs e)
        {
            inBattle = false;
            region_resume();
        }

        /// <summary>
        /// Initiate battle AI process
        /// </summary>
        /// <param name="b"></param>
        /// <param name="org"></param>
        public void battle(Battle fbat, String org)
        {
            bat = fbat;
            useUnit = false;
            bat_step = 0;
            cc = new Point(-1, -1);

            type = AI_Type.BATTLE;
            iorg = org;

            needDelay = true;
            isActive = true;
        }

        /// <summary>
        /// Initiate world AI process
        /// </summary>
        /// <param name="wm"></param>
        /// <param name="org"></param>
        public void world(WorldMap wm, String org)
        {
            type = AI_Type.WORLD;
            iorg = org;

            needDelay = true;
            isActive = true;
        }

        /// <summary>
        /// resumes battle AI process
        /// </summary>
        private void battle_resume()
        {
            Character c;

            if (cc == new Point(-1, -1))
            {
                //find an unmoved character

                cc = findUnmoved(iorg);

                if (cc == new Point(-1, -1))
                {
                    isActive = false;

                    if (done != null)
                        done(this, null);

                    return;
                }

                bat_step = 0;
            }
            
            c = cmap.get(cc.X, cc.Y);

            //execute good AI

            if (c is Archer)
            {
                bai_archer();
            }
            else if (c is Caster)
            {
                bai_caster();
            }
            else if (c is Fighter || c is Scout)
            {
                bai_fighter();
            }
            else if (c is Healer)
            {
                bai_healer();
            }

            if (bat_step != 0)
                cmap.remDeadChar(bat.ally);
        }

        /// <summary>
        /// Battle AI for Archer
        /// </summary>
        private void bai_archer()
        {
            Archer c = (Archer)cmap.get(cc.X, cc.Y);

            if (bat_step == 0)
            {
                // check if can do something

                Point p = weakestEnemy();

                if (p == new Point(-1, -1))
                {
                    cc = new Point(-1, -1);
                    return;
                }

                if (reach_archer(p)||nearEnemyArcher()!=new Point(-1, -1))
                {
                    //can attack
                    bat_step = 2;

                    map.focus(cc.X, cc.Y);
                    needDelay = true;
                    return;
                }

                if (c.stats.movement <= 0)
                {
                    //cannot move
                    cc = new Point(-1, -1);

                    return;
                }

                PathFind.TileMap ptm = PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                if (m == new Point(-1, -1) || m == cc)
                {
                    //cannot move
                    cc = new Point(-1, -1);
                    return;
                }
                else
                {
                    //can move
                    bat_step = 1;

                    map.focus(cc.X, cc.Y);
                    needDelay = true;
                    return;
                }
            }
            else if (bat_step == 1)
            {
                //move

                Point p = weakestEnemy();

                PathFind.TileMap ptm = PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                cmap.move(cc.X, cc.Y, m.X, m.Y);
                cc = m;
                map.focus(cc.X, cc.Y);
                needDelay = true;

                c.stats.movement--;
                bat_step = 0;

                return;
            }
            else if (bat_step == 2)
            {
                //attack

                Point p = weakestEnemy();

                if (reach_archer(p))
                {
                    Character t = cmap.get(p.X, p.Y);

                    c.attack(t);

                    acur = p;
                    map.CurLs.Add(acur, Content.Graphics.Instance.Images.gui.cursorRed);

                    needDelay = true;
                    c.stats.movement = 0;
                    cc = new Point(-1, -1);

                    return;
                }

                //otherwise it means it can attack an enemy which is near

                p = nearEnemyArcher();

                if (reach_archer(p))
                {
                    Character t = cmap.get(p.X, p.Y);

                    c.attack(t);

                    acur = p;
                    map.CurLs.Add(acur, Content.Graphics.Instance.Images.gui.cursorRed);

                    needDelay = true;
                    c.stats.movement = 0;
                    cc = new Point(-1, -1);

                    return;
                }
            }
        }

        /// <summary>
        /// Battle AI for Caster
        /// </summary>
        private void bai_caster()
        {
            Caster c = (Caster)cmap.get(cc.X, cc.Y);

            if (bat_step == 0)
            {
                // check if can do something

                Point p = weakestEnemy();

                if (p == new Point(-1, -1))
                {
                    cc = new Point(-1, -1);
                    return;
                }

                if (reach_caster(p)||reach_caster(nearEnemy()))
                {
                    //can attack
                    bat_step = 2;
                    map.focus(cc.X, cc.Y);
                    needDelay = true;

                    return;
                }

                if (c.stats.movement <= 0)
                {
                    //cannot move
                    cc = new Point(-1, -1);

                    return;
                }

                PathFind.TileMap ptm = PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                if (m == new Point(-1, -1) || m == cc)
                {
                    //cannot move
                    cc = new Point(-1, -1);
                    return;
                }
                else
                {
                    //can move
                    bat_step = 1;
                    map.focus(cc.X, cc.Y);
                    needDelay = true;

                    return;
                }
            }
            else if (bat_step == 1)
            {
                //move

                Point p = weakestEnemy();

                PathFind.TileMap ptm = PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                cmap.move(cc.X, cc.Y, m.X, m.Y);
                cc = m;
                map.focus(cc.X, cc.Y);
                needDelay = true;

                c.stats.movement--;
                bat_step = 0;

                return;
            }
            else if (bat_step == 2)
            {
                //attack

                Point p = weakestEnemy();

                if (reach_caster(p))
                {
                    Character t = cmap.get(p.X, p.Y);

                    c.attack(t, c.getCastableSpells().toList()[0]);

                    acur = p;
                    map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorRed);
                    needDelay = true;

                    c.stats.movement=0;
                    cc = new Point(-1, -1);

                    return;
                }

                //otherwise can attack an enemy which is near

                p = nearEnemy();

                if (reach_caster(p))
                {
                    Character t = cmap.get(p.X, p.Y);

                    c.attack(t, c.getCastableSpells().toList()[0]);

                    acur = p;
                    map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorRed);
                    needDelay = true;

                    c.stats.movement = 0;
                    cc = new Point(-1, -1);

                    return;
                }

                return;
            }
        }

        /// <summary>
        /// See if a caster can reach (cast a spell to) the target point p
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool reach_caster(Point p)
        {
            if (Gen.isAdj(p, cc)||Gen.isDiag(p, cc))
                return true;

            return (cc.X == p.X && cc.Y == p.Y - 2) || (cc.X == p.X + 2 && cc.Y == p.Y) || (cc.X == p.X && cc.Y == p.Y + 2) || (cc.X == p.X - 2 && cc.Y == p.Y);
        }


        /// <summary>
        /// See if an archer can reach (attack to) the target point p
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool reach_archer(Point p)
        {
            if (Gen.isDiag(p, cc))
                return true;

            return (cc.X == p.X && cc.Y == p.Y - 2) || (cc.X == p.X + 2 && cc.Y == p.Y) || (cc.X == p.X && cc.Y == p.Y + 2) || (cc.X == p.X - 2 && cc.Y == p.Y);
        }

        /// <summary>
        /// returns the position of an enemy that the archer can attack now
        /// </summary>
        /// <returns></returns>
        private Point nearEnemyArcher()
        {
            Character c;

            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                {
                    c = cmap.get(i, e);

                    if (c != null && c.Organization != iorg && reach_archer(new Point(i, e)))
                        return new Point(i, e);
                }

            return new Point(-1, -1);
        }

        /// <summary>
        /// Battle AI for Fighter and Scout
        /// </summary>
        private void bai_fighter()
        {
            Character c = cmap.get(cc.X, cc.Y);

            if (bat_step == 0)
            {
                // check if can do something

                Point p = weakestEnemy();

                if (p == new Point(-1, -1))
                {
                    cc = new Point(-1, -1);
                    return;
                }

                if (Gen.isAdj(p, cc) || Gen.isAdj(nearEnemy(), cc))
                {
                    //can attack
                    bat_step = 2;
                    map.focus(cc.X, cc.Y);
                    needDelay = true;
                    return;
                }

                if (c.stats.movement <= 0)
                {
                    //cant move

                    cc = new Point(-1, -1);

                    return;
                }

                PathFind.TileMap ptm = PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                if (m == new Point(-1, -1) || m == cc)
                {
                    cc = new Point(-1, -1);
                    return;
                }
                else
                {
                    //can move
                    bat_step = 1;
                    map.focus(cc.X, cc.Y);
                    needDelay = true;
                    return;
                }
            }
            else if (bat_step == 1)
            {
                //move

                Point p = weakestEnemy();

                PathFind.TileMap ptm = PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                cmap.move(cc.X, cc.Y, m.X, m.Y);
                cc = m;
                map.focus(cc.X, cc.Y);
                needDelay = true;

                c.stats.movement--;
                bat_step = 0;

                return;
            }
            else if (bat_step == 2)
            {
                //attack

                Point p = weakestEnemy();

                if (Gen.isAdj(p, cc))
                {
                    Character t = cmap.get(p.X, p.Y);

                    if (c is Fighter)
                        ((Fighter)c).attack(t);
                    else if (c is Scout)
                        ((Scout)c).attack(t);

                    acur = p;
                    map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorRed);
                    needDelay = true;

                    c.stats.movement=0;
                    cc = new Point(-1, -1);

                    return;
                }


                p = nearEnemy();

                if (Gen.isAdj(p, cc))
                {
                    Character t = cmap.get(p.X, p.Y);

                    if (c is Fighter)
                        ((Fighter)c).attack(t);
                    else if (c is Scout)
                        ((Scout)c).attack(t);

                    acur = p;
                    map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorRed);
                    needDelay = true;

                    c.stats.movement = 0;
                    cc = new Point(-1, -1);

                    return;
                }

                return;
            }
        }

        /// <summary>
        /// Returns the position of the closest enemy
        /// </summary>
        /// <returns></returns>
        private Point nearEnemy()
        {
            int dist = int.MaxValue;
            Character c;
            Point ret = new Point(-1, -1);

            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                {
                    c = cmap.get(i, e);

                    if (c != null && c.Organization != iorg && Gen.dist(cc, new Point(i, e))<dist)
                    {
                        dist = Gen.dist(cc, new Point(i, e));
                        ret.X = i;
                        ret.Y = e;
                    }
                }

            return ret;
        }

        /// <summary>
        /// Returns the position of the weakest enemy
        /// </summary>
        /// <returns></returns>
        private Point weakestEnemy()
        {
            int hp = int.MaxValue;
            Character c;
            Point ret = new Point(-1, -1);

            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                {
                    c = cmap.get(i, e);

                    if (c != null && c.Organization != iorg && c.stats.hp < hp)
                    {
                        hp = c.stats.hp;
                        ret.X = i;
                        ret.Y = e;
                    }
                }

            return ret;
        }

        /// <summary>
        /// Battle AI Healer
        /// </summary>
        private void bai_healer()
        {
            Healer c = (Healer)cmap.get(cc.X, cc.Y);

            if (bat_step == 0)
            {
                //check if the healer can do something

                //check if can heal nearby char or himself
                Point p = woundAllyNear(cc);

                if (p != new Point(-1, -1))
                {
                    //can heal
                    bat_step = 2;
                    needDelay = true;

                    map.focus(cc.X, cc.Y);

                    return;
                }

                p = mostWoundAlly();

                if (p == new Point(-1, -1))
                {
                    //cannot heal anyone
                    c.stats.movement = 0;
                    cc = new Point(-1, -1);
                    return;
                }

                if (c.stats.movement <= 0)
                {
                    //cannot move

                    cc = new Point(-1, -1);
                    return;
                }

                PathFind.TileMap ptm=PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                if (m == new Point(-1, -1) || m == cc)
                {
                    c.stats.movement = 0;
                    cc = new Point(-1, -1);
                    return;
                }
                else
                {
                    //can move
                    bat_step = 1;
                    needDelay = true;

                    map.focus(cc.X, cc.Y);

                    return;
                }
            }
            else if (bat_step == 1)
            {
                //move

                Point p = mostWoundAlly();

                PathFind.TileMap ptm = PathFind.TileMap.gen(cmap, tm, iorg, c);
                PathFind.TileMap.free(ref ptm, cc, p);

                Point m = PathFind.pathfind(ptm, cc, p, true);

                cmap.move(cc.X, cc.Y, m.X, m.Y);
                map.focus(m.X, m.Y);

                cc = m;

                needDelay = true;

                c.stats.movement--;
                bat_step = 0;

                return;
            }
            else if (bat_step == 2)
            {
                //heal

                Point p = woundAllyNear(cc);

                if (p != new Point(-1, -1))
                {
                    //heal char

                    Character t = cmap.get(p.X, p.Y);
                    c.heal(t);

                    acur = p;
                    map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorBlue);

                    c.stats.movement = 0;
                    cc = new Point(-1, -1);
                    return;
                }

                needDelay = true;
                c.stats.movement=0;
                cc = new Point(-1, -1);

                return;
            }
        }

        /// <summary>
        /// Finds the lowest ally hp that is not cc (current char)
        /// </summary>
        /// <returns></returns>
        private Point mostWoundAlly()
        {
            int hp = int.MaxValue;
            Character c;
            Point ret=new Point(-1, -1);

            for(int i=0; i<cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                {
                    c = cmap.get(i, e);

                    if (c != null && c.Organization == iorg && new Point(i, e)!=cc && c.stats.hp < hp)
                    {
                        hp = c.stats.hp;
                        ret.X = i;
                        ret.Y = e;
                    }
                }

            return ret;
        }

        /// <summary>
        /// find a wounded ally adjacent to point p (or at point p)
        /// </summary>
        /// <param name="p"></param>
        /// <returns>position of the wounded ally or (-1, -1)</returns>
        private Point woundAllyNear(Point p)
        {
            Character c;

            for(int i=-1; i<=1; i++)
                for (int e = -1; e <= 1; e++)
                {
                    if (p.X + i < 0 || p.X + i >= cmap.NumX || p.Y + e < 0 || p.Y + e >= cmap.NumY)
                        continue;

                    if((i==-1||i==1)&&(e==-1||e==1))
                        continue;

                    c = cmap.get(p.X + i, p.Y + e);

                    if (c != null && c.Organization==iorg && c.stats.hp != c.stats.maxHp)
                        return new Point(p.X + i, p.Y + e);
                }

            return new Point(-1, -1);
        }

        /// <summary>
        /// Initiate region AI process
        /// </summary>
        /// <param name="org"></param>
        public void region(Region freg, String org)
        {
            reg = freg;
            useUnit = true;
            reg_step = Region_Steps.FIND;
            type = AI_Type.REGION;
            iorg = org;

            needDelay = true;
            isActive = true;
        }

        /// <summary>
        /// resumes AI process after delay
        /// </summary>
        private void region_resume()
        {
            if (reg_step == Region_Steps.FIND)
            {
                ActionUnit au=ActionUnit.Null;

                au = findActionUnit(iorg);

                if (au == ActionUnit.Null)
                {
                    isActive = false;

                    if (done != null)
                        done(this, null);

                    return;
                }

                map.focus(au.unit.X, au.unit.Y);

                needDelay = true;

                reg_step = Region_Steps.MOVE;
            }
            else if (reg_step == Region_Steps.MOVE)
            {
                ActionUnit au = findActionUnit(iorg);

                Unit mu=umap.get(au.move.X, au.move.Y);
                Unit eu = umap.get(au.unit.X, au.unit.Y);

                if (mu!=null&&mu.Organization == "main")
                {
                    eu.movement = 0;

                    Objective o = new Objective();
                    o.setDefeatAll();

                    inBattle = true;

                    Battle b = new Battle(mu, eu, reg, o);

                    b.done = battle_done;

                    reg_step = Region_Steps.FIND;

                    StateManager.Instance.goForward(b);
                }
                else
                {
                    umap.move(au.unit.X, au.unit.Y, au.move.X, au.move.Y);
                    umap.update(map);

                    eu.movement--;

                    map.focus(au.move.X, au.move.Y);
                    needDelay = true;
                    reg_step = Region_Steps.FIND;
                }

                umap.remDeadUnit();
            }
        }

        /// <summary>
        /// Finds a unit that has an action to do or an ActionUnit with the 2 points being the same point
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        ActionUnit findActionUnit(String org)
        {
            Point u = new Point(-1, -1);
            Point d = new Point(-1, -1);
            Point m = new Point(-1, -1);
            PathFind.TileMap ptm;

            while (true)
            {
                u = findUnmoved(org);

                if (u == new Point(-1, -1))
                    return ActionUnit.Null;

                d = findNearest(u, org);

                if (Gen.isAdj(u, d))
                    return new ActionUnit(u, d);

                ptm = PathFind.TileMap.gen(umap, tm, umap.get(u.X, u.Y));
                PathFind.TileMap.free(ref ptm, u, d);

                m = PathFind.pathfind(ptm, u, d, true);

                if (m == new Point(-1, -1) || m == u)
                {
                    umap.get(u.X, u.Y).movement = 0;
                    continue;
                }

                return new ActionUnit(u, m);
            }
        }

        /// <summary>
        /// Finds a Unit or Character from organization org that hasnt moved
        /// </summary>
        /// <param name="org"></param>
        /// <returns>Point where the unit or character is</returns>
        private Point findUnmoved(String org)
        {
            if (useUnit)
            {
                Unit u;

                for (int i = 0; i < umap.NumX; i++)
                    for (int e = 0; e < umap.NumY; e++)
                    {
                        u = umap.get(i, e);

                        if (u!=null && u.Organization==org&&u.movement>0)
                            return new Point(i, e);
                    }

                return new Point(-1, -1);
            }
            else
            {
                Character c;

                for (int i = 0; i < cmap.NumX; i++)
                    for (int e = 0; e < cmap.NumY; e++)
                    {
                        c = cmap.get(i, e);

                        if (c != null && c.Organization == org && c.stats.movement > 0)
                            return new Point(i, e);
                    }

                return new Point(-1, -1);
            }
        }

        /// <summary>
        /// Find nearest enemy of the organization org to the point src
        /// </summary>
        /// <param name="src"></param>
        /// <param name="org"></param>
        /// <returns>Point of the nearest enemy to the organization org</returns>
        private Point findNearest(Point src, String org)
        {
            if (useUnit)
            {
                Unit u;
                int ld=int.MaxValue;
                Point cne=new Point(0, 0);
                int tmp;

                for (int i = 0; i < umap.NumX; i++)
                    for (int e = 0; e < umap.NumY; e++)
                    {
                        u = umap.get(i, e);

                        if (u != null && u.Organization=="main")
                        {
                            tmp = Gen.dist(src, new Point(i, e));

                            if (tmp < ld)
                                cne = new Point(i, e);
                        }
                    }

                return cne;
            }
            else
            {
                Character c;
                int ld = int.MaxValue;
                Point cne = new Point(0, 0);
                int tmp;

                for (int i = 0; i < cmap.NumX; i++)
                    for (int e = 0; e < cmap.NumY; e++)
                    {
                        c = cmap.get(i, e);

                        if (c != null && c.Organization == "main")
                        {
                            tmp = Gen.dist(src, new Point(i, e));

                            if (tmp < ld)
                                cne = new Point(i, e);
                        }
                    }

                return cne;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (useUnit)
                umap.update(map);
            else
                cmap.update(map);

            if (inBattle)
                return;

            if (needDelay)
            {
                delayFinished = gameTime.TotalGameTime + delay;
                needDelay = false;
            }

            if (delayFinished > gameTime.TotalGameTime)
                return;
            else
            {
                if (acur != new Point(-1, -1))
                {
                    //a cursor was added and needs to be removed

                    map.CurLs.Remove(acur);
                    acur = new Point(-1, -1);
                }

                if (type == AI_Type.REGION)
                    region_resume();
                else if (type == AI_Type.BATTLE)
                    battle_resume();
            }
        }

        //SUB ALGORITHM STUFF

        private class PathFind
        {
            private struct PointCounter
            {
                public Point p;
                public int c;

                public PointCounter(Point fp, int fc)
                {
                    p = fp;
                    c = fc;
                }

                public PointCounter(int x, int y, int fc)
                {
                    p = new Point(x, y);
                    c = fc;
                }
            }

            public class TileMap
            {
                public enum Tile_Type {NOTHING, BLOCK_UNIT, BLOCK_TERRAIN};

                /// <summary>
                /// Put the source and destination free (for the algorithm to work properly)
                /// </summary>
                /// <param name="tm"></param>
                /// <param name="src"></param>
                /// <param name="dest"></param>
                public static void free(ref TileMap tm, Point src, Point dest)
                {
                    tm.set(src.X, src.Y, Tile_Type.NOTHING);
                    tm.set(dest.X, dest.Y, Tile_Type.NOTHING);
                }

                /// <summary>
                /// Generates a TileMap with the given UnitMap and Unit (and Tilemap and Organization)
                /// </summary>
                /// <param name="umap">The UnitMap to use</param>
                /// <param name="unit">The Unit to be based on</param>
                /// <param name="tm">Tilemap (not the pathfind one)</param>
                /// <param name="org">Organization to base the map from (determins who is ally and who is enemy)</param>
                /// <returns>a PathFind.TileMap</returns>
                public static TileMap gen(UnitMap umap, Tilemap tm, Unit unit)
                {
                    TileMap ptm = new TileMap(tm.NumX, tm.NumY);

                    for(int i=0; i<tm.NumX; i++)
                        for(int e=0; e<tm.NumY; e++)
                            if(!unit.canMove(tm.get(i, e).Type))
                                ptm.set(i, e, Tile_Type.BLOCK_TERRAIN);
                            else if(!umap.canMove(i, e, unit.Organization))
                                ptm.set(i, e, Tile_Type.BLOCK_UNIT);
                            else
                                ptm.set(i, e, Tile_Type.NOTHING);

                    return ptm;
                }

                /// <summary>
                /// Generates a TileMap with the given CharMap and Character (and Tilemap and Organization)
                /// </summary>
                /// <param name="cmap">The CharMap to use</param>
                /// <param name="c">The Character to base the map on</param>
                /// <param name="tm">Tilemap (not the pathfind one)</param>
                /// <param name="org">Organization to base the map from (determins who is ally and who is enemy)</param>
                /// <returns>a PathFind.TileMap</returns>
                public static TileMap gen(CharMap cmap, Tilemap tm, String org, Character c)
                {
                    TileMap ptm = new TileMap(tm.NumX, tm.NumY);

                    for (int i = 0; i < tm.NumX; i++)
                        for (int e = 0; e < tm.NumY; e++)
                            if (!c.canMove(tm.get(i, e).Type))
                                ptm.set(i, e, Tile_Type.BLOCK_TERRAIN);
                            else if (!cmap.canMove(i, e))
                                ptm.set(i, e, Tile_Type.BLOCK_UNIT);
                            else
                                ptm.set(i, e, Tile_Type.NOTHING);

                    return ptm;
                }

                Tile_Type[,] map;

                /// <summary>
                /// Creates a PathFind TileMap that PathFind uses to calculate pathfinding
                /// </summary>
                /// <param name="nx">Number of tile in x</param>
                /// <param name="ny">Number of tile in y</param>
                public TileMap(int nx, int ny)
                {
                    map=new Tile_Type[nx, ny];
                }

                /// <summary>
                /// Set the tile at position [x, y] to type
                /// </summary>
                /// <param name="x">position in x</param>
                /// <param name="y">position in y</param>
                /// <param name="type">tile type</param>
                public void set(int x, int y, Tile_Type type)
                {
                    map[x, y] = type;
                }

                /// <summary>
                /// Check if the poin is in the boundaries of the TileMap
                /// </summary>
                /// <param name="p">Point to check</param>
                /// <returns>if the point is in the boundaries of the TileMap</returns>
                public bool inMap(Point p)
                {
                    return p.X >= 0 && p.Y >= 0 && p.X < map.GetLength(0) && p.Y < map.GetLength(1);
                }

                /// <summary>
                /// Gets the tile type of the tile at position [x, y]
                /// </summary>
                /// <param name="x">position in x</param>
                /// <param name="y">position in y</param>
                /// <returns>tile type of the given position</returns>
                public Tile_Type get(int x, int y)
                {
                    return map[x, y];
                }
            }

            /// <summary>
            /// Finds the path from source to destination point.
            /// </summary>
            /// <param name="tm">TileMap</param>
            /// <param name="src">Source point (the Unit or Character moving)</param>
            /// <param name="dest">Destination point (the target)</param>
            /// <param name="fallBack">Whether or not to use the fallback (try to find a path not considering ally unit)</param>
            /// <returns>A point adjacent to the source which is the next move of the character or unit
            /// in order to arrive to destination. or the source point if no path is found</returns>
            public static Point pathfind(TileMap tm, Point src, Point dest, bool fallBack)
            {
                Dictionary<Point, int> map = new Dictionary<Point, int>();
                Queue<PointCounter> main = new Queue<PointCounter>();
                Queue<PointCounter> temp = new Queue<PointCounter>();
                PointCounter cur;
                PointCounter tcur;

                main.Enqueue(new PointCounter(dest, 0));
                map[dest] = 0;

                int cc;
                bool f = false;

                while (main.Count > 0)
                {
                    cur = main.Dequeue();
                    temp.Clear();

                    if (cur.p == src)
                    {
                        f = true;
                        break;
                    }

                    cc = cur.c + 1;

                    temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y - 1, cc));
                    temp.Enqueue(new PointCounter(cur.p.X + 1, cur.p.Y, cc));
                    temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y + 1, cc));
                    temp.Enqueue(new PointCounter(cur.p.X - 1, cur.p.Y, cc));

                    while (temp.Count > 0)
                    {
                        tcur = temp.Dequeue();

                        if (tcur.p != src)
                        {
                            if (!tm.inMap(tcur.p) || tm.get(tcur.p.X, tcur.p.Y)!=TileMap.Tile_Type.NOTHING)
                                continue;

                            if (map.ContainsKey(tcur.p) && map[tcur.p] <= tcur.c)
                                continue;
                        }

                        map[tcur.p] = tcur.c;
                        main.Enqueue(tcur);
                    }
                }

                if (!f)
                {
                    if (fallBack)
                        return pathfindFallback(tm, src, dest);
                    else
                        return new Point(-1, -1);
                }

                Point ret = src;
                cc = map[src];

                temp.Clear();

                temp.Enqueue(new PointCounter(src.X, src.Y - 1, 0));
                temp.Enqueue(new PointCounter(src.X + 1, src.Y, 0));
                temp.Enqueue(new PointCounter(src.X, src.Y + 1, 0));
                temp.Enqueue(new PointCounter(src.X - 1, src.Y, 0));

                while (temp.Count > 0)
                {
                    tcur = temp.Dequeue();

                    if (map.ContainsKey(tcur.p) && map[tcur.p] < cc)
                    {
                        cc = map[tcur.p];
                        ret = tcur.p;
                    }
                }

                if (ret == dest)
                    return new Point(-1, -1);

                return ret;
            }

            /// <summary>
            /// Finds the path from source to destination point (does not consider ally unit)
            /// </summary>
            /// <param name="tm">TileMap</param>
            /// <param name="src">Source point (the Unit or Character moving)</param>
            /// <param name="dest">Destination point (the target)</param>
            /// <returns>A point adjacent to the source which is the next move of the character or unit
            /// in order to arrive to destination. or the source point if no path is found</returns>
            public static Point pathfindFallback(TileMap tm, Point src, Point dest)
            {
                Dictionary<Point, int> map = new Dictionary<Point, int>();
                Queue<PointCounter> main = new Queue<PointCounter>();
                Queue<PointCounter> temp = new Queue<PointCounter>();
                PointCounter cur;
                PointCounter tcur;

                main.Enqueue(new PointCounter(dest, 0));
                map[dest] = 0;

                int cc;
                bool f = false;

                while (main.Count > 0)
                {
                    cur = main.Dequeue();
                    temp.Clear();

                    if (cur.p == src)
                    {
                        f = true;
                        break;
                    }

                    cc = cur.c + 1;

                    temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y - 1, cc));
                    temp.Enqueue(new PointCounter(cur.p.X + 1, cur.p.Y, cc));
                    temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y + 1, cc));
                    temp.Enqueue(new PointCounter(cur.p.X - 1, cur.p.Y, cc));

                    while (temp.Count > 0)
                    {
                        tcur = temp.Dequeue();

                        if (tcur.p != src)
                        {
                            if (!tm.inMap(tcur.p) || tm.get(tcur.p.X, tcur.p.Y) == TileMap.Tile_Type.BLOCK_TERRAIN)
                                continue;

                            if (map.ContainsKey(tcur.p) && map[tcur.p] <= tcur.c)
                                continue;
                        }

                        map[tcur.p] = tcur.c;
                        main.Enqueue(tcur);
                    }
                }

                if (!f)
                    return new Point(-1, -1);

                Point ret = src;
                cc = map[src];

                temp.Clear();

                temp.Enqueue(new PointCounter(src.X, src.Y - 1, 0));
                temp.Enqueue(new PointCounter(src.X + 1, src.Y, 0));
                temp.Enqueue(new PointCounter(src.X, src.Y + 1, 0));
                temp.Enqueue(new PointCounter(src.X - 1, src.Y, 0));

                while (temp.Count > 0)
                {
                    tcur = temp.Dequeue();

                    if (map.ContainsKey(tcur.p) && map[tcur.p] < cc)
                    {
                        cc = map[tcur.p];
                        ret = tcur.p;
                    }
                }

                if (tm.get(ret.X, ret.Y) != TileMap.Tile_Type.NOTHING||ret==dest)
                    return src;

                return ret;
            }
        }
    }
}
