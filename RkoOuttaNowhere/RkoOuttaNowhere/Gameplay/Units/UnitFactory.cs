using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RkoOuttaNowhere.Gameplay.Units
{
    public class UnitFactory
    {
        // Health
        public const int WEAK_HEALTH     = 20;
        public const int MEDIUM_HEALTH   = 50;
        public const int STRONG_HEALTH   = 100;
        // Speed
        public const float SLOW_SPEED     = 60;
        public const float MEDIUM_SPEED   = 80;
        public const float FAST_SPEED     = 100;
        // Money
        public const int WEAK_MONEY     = 5;
        public const int MEDIUM_MONEY   = 10;
        public const int STRONG_MONEY   = 20;
        //DPS
        public const int WEAK_DPS = 1;
        public const int MEDIUM_DPS = 2;
        public const int STRONG_DPS = 3;

        public static Unit CreateWeakMelee()
        {
            Unit u = new Unit();
            u.LoadContent("enemies/weakMelee", Vector2.Zero, MEDIUM_SPEED, WEAK_HEALTH, WEAK_MONEY, WEAK_DPS, Behaviour.BasicMove);
            return u;
        }

        public static Unit CreateMediumMelee()
        {
            Unit u = new Unit();
            u.LoadContent("enemies/trojan", Vector2.Zero, MEDIUM_SPEED, MEDIUM_HEALTH, MEDIUM_MONEY, MEDIUM_DPS, Behaviour.BasicMove);
            return u;
        }

        public static Unit CreateStrongMelee()
        {
            Unit u = new Unit();
            u.LoadContent("enemies/heavyMelee", Vector2.Zero, SLOW_SPEED, STRONG_HEALTH, STRONG_MONEY, STRONG_DPS, Behaviour.BasicMove);
            return u;
        }

        public static Unit CreateWeakRanged()
        {
            Unit u = new Unit();
            u.LoadContent("testUnit2", Vector2.Zero, FAST_SPEED, WEAK_HEALTH, WEAK_MONEY, WEAK_DPS, Behaviour.BasicMove);
            return u;
        }

        public static Unit CreateMediumRanged()
        {
            Unit u = new Unit();
            u.LoadContent("testUnit2", Vector2.Zero, MEDIUM_SPEED, MEDIUM_HEALTH, MEDIUM_MONEY, MEDIUM_DPS, Behaviour.BasicMove);
            return u;
        }

        public static Unit CreateStrongRanged()
        {
            Unit u = new Unit();
            u.LoadContent("testUnit2", Vector2.Zero, MEDIUM_SPEED, STRONG_HEALTH, STRONG_MONEY, STRONG_DPS, Behaviour.BasicMove);
            return u;
        }
    }
}
