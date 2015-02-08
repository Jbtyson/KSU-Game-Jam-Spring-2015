using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RkoOuttaNowhere.Gameplay
{
    public class Upgrade
    {
        private static float _damageBoost   = 1.0f,
                             _moneyBoost    = 1.0f,
                             _moneyRate     = 0,
                             _healthIncrease= 0,
                             _healthRate    = 0,
                             _gun           = 10,
                             _fire          = 15,
                             _laser         = 20;

        public static float HealthRate
        {
            get { return _healthRate; }
            set { _healthRate = value; }
        }

        public static float HealthIncrease
        {
            get { return _healthIncrease; }
            set { _healthIncrease = value; }
        }

        public static float MoneyRate
        {
            get { return _moneyRate; }
            set { _moneyRate = value; }
        }

        public static float MoneyBoost
        {
            get { return _moneyBoost; }
            set { _moneyBoost = value; }
        }

        public static float DamageBoost
        {
            get { return _damageBoost; }
            set { _damageBoost = value; }
        }

        public static float Gun
        {
            get { return _gun; }
            set { _gun = value; }
        }

        public static float Fire
        {
            get { return _fire; }
            set { _fire = value; }
        }

        public static float Laser
        {
            get { return _laser; }
            set { _laser = value; }
        }
    }
}
