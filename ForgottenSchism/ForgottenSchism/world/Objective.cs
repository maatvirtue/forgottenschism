using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ForgottenSchism.world
{
    public class Objective
    {
        public enum Objective_Type { DEFEAT_ALL, CAPTURE_CITY, DEFEAT_BOSS, DEFEAT_UNIT, DEFEND_CITY };

        Objective_Type type;
        Point p;
        int turn;
        Unit u;
        Character c;

        public Objective()
        {
            //
        }

        public Objective_Type Type
        {
            get { return type; }
        }

        public Point City
        {
            get { return p; }
        }

        public int Turns
        {
            get { return turn; }
        }

        public Unit Unit
        {
            get { return u; }
        }

        public Character Char
        {
            get { return c; }
        }

        public void setDefeatAll()
        {
            type = Objective_Type.DEFEAT_ALL;
        }

        public void setCaptureCity(Point city)
        {
            type = Objective_Type.CAPTURE_CITY;
            p = city;
        }

        public void setDefeatBoss(Character fc)
        {
            type = Objective_Type.DEFEAT_BOSS;
            c = fc;
        }

        public void setDefeatUnit(Unit fu)
        {
            type = Objective_Type.DEFEAT_UNIT;
            u = fu;
        }

        public void setDefendCity(Point city, int fturn)
        {
            type = Objective_Type.DEFEND_CITY;
            p = city;
            turn = fturn;
        }
    }
}
