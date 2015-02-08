using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RkoOuttaNowhere.Levels;
using RkoOuttaNowhere.Gameplay;

namespace RkoOuttaNowhere
{
    //Add upgrades here in powers of two
    //1,2,4,8,16,32,64,128,256,512,1024,2048,4096,8192,16384,32768,65536, etc.
    [Flags]
    public enum Upgrades
    {
        None = 0
    }

    public class RKOGame
    {
        public const int NUM_LEVELS = 30;

        private static RKOGame _instance;
        /// <summary>
        /// Singleton class instance
        /// </summary>
        public static RKOGame Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RKOGame();
                }
                return _instance;
            }
        }

        private int _currency, 
                    _health, 
                    _currentWave,
                    _currentWorld,
                    _currentLevel,
                    _highestCompletedLevel;
                    //add upgrades, stats, 

        private Screens.ScreenType _lastScreen;
        public Screens.ScreenType LastScreen
        {
            get { return _lastScreen; }
            set { _lastScreen = value; }
        }
        /// <summary>
        /// constructor for Game class, sets initial values for game object
        /// </summary>
        /// <param name="curr"> starting curency </param>
        private RKOGame()
        {
            _currency = 100;
            _health = 100;
            _currentLevel = 0;
            _currentWave = 0;
            _highestCompletedLevel = -1;
            _currentWorld = 0;
        }

        public int getCurrency { get { return _currency; } set { _currency += value; } }
        public int getHealth { get { return (_health + (int)Upgrade.HealthIncrease); } set { _health = value; } }
        public int getCurrentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
        public int getCurrentWorld { get { return _currentWorld; } set { _currentWorld = value; } }
        public int getHighestCompletedLevel { get { return _highestCompletedLevel; } set { _highestCompletedLevel = value; } }

        public void Reset()
        {
            _instance = null;
        }

        public void AddMoney(int value)
        {
            _currency += (int)Math.Ceiling(value * Upgrade.MoneyBoost);
        }
    

    }
}
