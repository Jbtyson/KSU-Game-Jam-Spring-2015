﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RkoOuttaNowhere.Input;
using RkoOuttaNowhere.Gameplay;
using RkoOuttaNowhere.Gameplay.Units;
using RkoOuttaNowhere.Levels;
using RkoOuttaNowhere.Ui;

namespace RkoOuttaNowhere.Screens
{
    public class GameplayScreen : GameScreen
    {
        private Player _player;
        private Level _currentLevel;
        private int _money, _health;

        public GameplayScreen()
            : base()
        {
            _player = new Player();
            _currentLevel = new Level();
            _gui = new GameplayGui();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _backgroundImage.Path = "backgrounds/gameplay";
            _backgroundImage.LoadContent();
            _player.LoadContent();

            // Test level
            _currentLevel.LoadContent(2);
            _money = 0;
            _health = 100;

            // Load the gui
            _gui.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _player.UnloadContent();

            _currentLevel.UnloadContent();
            _gui.UnloadContent();
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            // Process input
            if (InputManager.Instance.KeyPressed(Keys.U))
            {
                ScreenManager.Instance.ChangeScreens(ScreenType.Upgrade);
            }
            else if (InputManager.Instance.KeyPressed(Keys.X))
            {
                ScreenManager.Instance.ChangeFast(ScreenType.GameOver);
            }
            _player.Update(gametime);
            //_player.laserHitEnemy(_units);
            _currentLevel.Update(gametime);

            // Update the gui
            _gui.SetTimer(_currentLevel.WaveCountdown);
            _gui.SetWaves(_currentLevel.WavesRemaining);
            _gui.SetMoney(_money++);
            _gui.SetHealth(_health);

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _currentLevel.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            _gui.Draw(spriteBatch);
        }
    }
}
