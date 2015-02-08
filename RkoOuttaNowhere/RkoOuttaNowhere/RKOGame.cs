﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RkoOuttaNowhere.Levels;

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
                    _finalWave, 
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
            //finalWave = 30;

        }

        public int getCurrency { get { return _currency; } set { _currency += value; } }
        public int getHealth { get { return _health; } set { _health = value; } }
        public int getWavesLeft { get { return (_finalWave - _currentWave); } }
        public int getCurrentLevel { get { return _currentLevel; } set { _currentLevel = value; } }
        public int getCurrentWorld { get { return _currentWorld; } set { _currentWorld = value; } }
        public int getHighestCompletedLevel { get { return _highestCompletedLevel; } set { _highestCompletedLevel = value; } }

        /// <summary>
        /// Setup the given level with initial values and get ready to start
        /// </summary>
        /// <param name="newLevel">Level to begin</param>
        public void SetupLevel(Level newLevel)
        {
            //TODO: make sure that everything has the right position
        }

        /// <summary>
        /// Perform cleanup operations after a level is completed or the player dies
        /// </summary>
        public void CleanLevel()
        {
            //TODO: Implement CleanLevel()
        }

        /// <summary>
        /// Effectively the update method of most of the main game features. This
        /// Is where cooldowns will occur and AI's will tick
        /// </summary>
        public void Update()
        {
            
        }

        /// <summary>
        /// Draw the elements on the game screen
        /// </summary>
        public void Draw()
        {

        }

    }
}
