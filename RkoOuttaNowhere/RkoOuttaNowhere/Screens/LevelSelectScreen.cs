using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RkoOuttaNowhere.Input;
using RkoOuttaNowhere.Ui;

namespace RkoOuttaNowhere.Screens
{
    public class LevelSelectScreen : GameScreen
    {
        private int _currentWorld,
                    _currentLevel,
                    _nextLevelProgression;

        private bool _guiLoaded;

        public LevelSelectScreen()
            : base()
        {
            _gui = new LevelSelectGui();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _currentLevel = RKOGame.Instance.getCurrentLevel;
            _currentWorld = RKOGame.Instance.getCurrentWorld;

            _backgroundImage.Path = "ui/level_select/world" + (_currentWorld + 1);
            _backgroundImage.LoadContent();

            // Load all of the nodes (buttons)
            List<Point> points = LoadFromFile();
            if (!_guiLoaded)
            {
                _gui.LoadContent();
                _guiLoaded = true;
            }
            _gui.LoadNodes(points, new Action(HandleNodeClick), "ui/level_select/Node" + (_currentWorld + 1));

            _nextLevelProgression = RKOGame.Instance.getHighestCompletedLevel + 1;
            _gui.AnimateButton(_nextLevelProgression % 10);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _gui.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.U))
            {
                RKOGame.Instance.LastScreen = ScreenType.LevelSelect;
                ScreenManager.Instance.ChangeScreens(ScreenType.Upgrade);
            }
            else if (InputManager.Instance.KeyPressed(Keys.F10))
            {
                RKOGame.Instance.LastScreen = ScreenType.LevelSelect;
                ScreenManager.Instance.ChangeScreens(ScreenType.Victory);
            }
            else if (InputManager.Instance.KeyPressed(Keys.F9))
            {
                RKOGame.Instance.AddMoney(1000);
            }

            if (_nextLevelProgression == RKOGame.Instance.getHighestCompletedLevel)
            {
                _nextLevelProgression = RKOGame.Instance.getCurrentLevel + 1;
                if (_nextLevelProgression % 10 == 0)
                {
                    NextWorld();
                    if (_currentWorld == 3)
                    {
                        ScreenManager.Instance.ChangeScreens(ScreenType.Victory);
                    }
                }
                _gui.AnimateButton(_nextLevelProgression % 10);
            }

            _gui.Update(gameTime);
        }

        public void NextWorld()
        {
            RKOGame.Instance.getCurrentWorld++;
            if (RKOGame.Instance.getCurrentWorld == 3)
                ScreenManager.Instance.ChangeFast(ScreenType.Victory);
            else
            {
                _backgroundImage = new Images.Image();
                LoadContent();
            }
            

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            _gui.Draw(spriteBatch);
        }

        public List<Point> LoadFromFile()
        {            
            List<Point> list = new List<Point>();
            string path = "../../../Content/ui/level_select/world" + (_currentWorld + 1) + "_coor.txt";
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] vals = sr.ReadLine().Split(',');
                            list.Add(new Point(int.Parse(vals[0]), int.Parse(vals[1])));
                        }
                    }
                }
            }
            catch(IOException) { }
            return list;
        }

        public void HandleNodeClick()
        {
            if (_gui.NumClicked <= _nextLevelProgression)
            {
                RKOGame.Instance.getCurrentLevel = _gui.NumClicked + 10 * _currentWorld;
                ScreenManager.Instance.ChangeScreens(ScreenType.Gameplay);
            }
        }
    }
}
