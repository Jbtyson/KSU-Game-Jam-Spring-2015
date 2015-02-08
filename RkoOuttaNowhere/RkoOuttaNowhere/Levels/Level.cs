// Level.cs
// James Tyson
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RkoOuttaNowhere.Gameplay.Units;

namespace RkoOuttaNowhere.Levels
{
    public class Level
    {
        private const int FIELD_SIZE = 300;
        private const int FIELD_ORIGIN = 400;

        private int _numLanes,
                    _numWaves,
                    _currentWave;

        private float _waveTimer,
                      _elapsedTime,
                      _waveCountdown;

        private bool _levelOver,
                     _changingScrens,
                     _completed;

        public bool Completed
        {
            get { return _completed; }
            set { _completed = value; }
        }

        private List<Wave> _waves;
        public List<Wave> Waves
        {
            get { return _waves; }
            set { _waves = value; }
        }

        private int _levelValue;
        public int LevelValue
        {
            get { return _levelValue; }
            set { _levelValue = value; }
        }
        public float WaveCountdown
        {
            get { return _waveCountdown; }
            set { _waveCountdown = value; }
        }
        public int WavesRemaining
        {
            get { return _numWaves - _currentWave - 1; }
        }

        public Level()
        {
            _waves = new List<Wave>();
        }

        public void LoadContent(int levelValue)
        {
            _levelValue = levelValue;
            _currentWave = -1;
            _waveTimer = 5000;
            // Load the enemies into the unit list
            List<Tuple<string, int>> list = LoadFromFile();
            foreach (Tuple<string, int> val in list)
            {
                string type = val.Item1;
                int num = val.Item2;

                switch (type)
                {
                    case "---":
                        _waves.Add(new Wave(++_currentWave, _numLanes, FIELD_SIZE, FIELD_ORIGIN));
                        break;
                    case "WeakMelee":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateWeakMelee());
                        break;
                    case "Trojan":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateMediumMelee());
                        break;
                    case "HeavyMelee":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateStrongMelee());
                        break;
                    case "WeakRanged":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateWeakRanged());
                        break;
                    case "MediumRanged":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateMediumRanged());
                        break;
                    case "StrongRanged":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateStrongRanged());
                        break;
                    case "WeakAerialMelee":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateWeakAerialMelee());
                        break;
                    case "WeakAerialRanged":
                        for (int i = 0; i < num; i++)
                            _waves[_currentWave].AddUnit(UnitFactory.CreateWeakAerialRanged());
                        break;
                    default:
                        throw new ArgumentException("check spelling of an enemy type");
                }
            }
            _currentWave = -1;

            foreach (Wave w in _waves)
            {
                w.LoadContent();
            }
        }

        public void Reinitialize() 
        {
            _completed = false;
            _waves = new List<Wave>();
            LoadContent(_levelValue);
        }

        public void UnloadContent()
        {
            foreach (Wave w in _waves)
                w.UnloadContent();
        }

        public void Update(GameTime gametime)
        {
            _waveCountdown = _waveTimer - _elapsedTime;
            
            foreach (Wave w in _waves)
                w.Update(gametime);

            // Update the wave timer
            if (_currentWave + 1 != _numWaves)
            {
                _elapsedTime += (float)gametime.ElapsedGameTime.TotalMilliseconds;
                if (_elapsedTime >= 5000)
                {
                    _waves[++_currentWave].Activate();
                    _elapsedTime = 0;
                }
            }

            // Check for the level to be over
            if (_currentWave + 1 == _numWaves)
            {
                _waveCountdown = 0;
                _completed = true;
                foreach (Wave w in _waves) 
                {
                    if (w.Active)
                        _completed = false;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Wave w in _waves)
                w.Draw(spritebatch);
        }

        /// <summary>
        /// Load from the level file and store it as a list of tuples
        /// </summary>
        /// <returns></returns>
        private List<Tuple<string, int>> LoadFromFile()
        {
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            try
            {
                string path = "../../../Levels/Load/Level" + _levelValue + ".txt";
                string test = Directory.GetCurrentDirectory();
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        _numLanes = int.Parse(sr.ReadLine());
                        _numWaves = int.Parse(sr.ReadLine());
                        while (!sr.EndOfStream)
                        {
                            string[] vals = sr.ReadLine().Split(' ');
                            if(vals.Length == 2)
                                list.Add(new Tuple<string, int>(vals[0], int.Parse(vals[1])));
                            else
                                list.Add(new Tuple<string, int>(vals[0], 0));
                        }
                    }
                }
            }
            catch (IOException) { Console.WriteLine("Failed to read from file"); }
            catch (FormatException) { Console.WriteLine("Failed to load some enemies"); }

            return list;
        }
    }
}
