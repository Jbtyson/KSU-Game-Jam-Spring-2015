using System;
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
using RkoOuttaNowhere.Physics;

namespace RkoOuttaNowhere.Screens
{
    public class GameplayScreen : GameScreen
    {
        
        private Level _currentLevel;
        private List<Level> _levels;
        private Firewall _firewall;
        private int _money, _health;

        public GameplayScreen()
            : base()
        {
            _player = new Player();
            _currentLevel = new Level();
            _gui = new GameplayGui();
            _levels = new List<Level>();
            _firewall = new Firewall();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _backgroundImage.Path = "backgrounds/gameplay";
            _backgroundImage.LoadContent();
            _player.LoadContent();
            _firewall.LoadContent();

            // Load the levels
            for (int i = 0; i < RKOGame.NUM_LEVELS; i++)
            {
                Level l = new Level();
                l.LoadContent(i);
                _levels.Add(l);
            }

                // Load the gui
                _gui.LoadContent();
        }

        public void Reload() 
        {
            // Unload and reload our level for replay
            _currentLevel.Reinitialize();
            // Change to our new level
            _currentLevel = _levels[RKOGame.Instance.getCurrentLevel];
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _player.UnloadContent();

            _currentLevel.UnloadContent();
            _gui.UnloadContent();
            _firewall.UnloadContent();
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

            /* old Collision Detection
            foreach (Wave w in _currentLevel.Waves)
                _player.laserHitEnemy(w.Units);
             * */

            PhysicsManager.Instance.Update(gametime);
            PhysicsManager.Instance.FindCollisions();

            // Process units
            //_player.laserHitEnemy(_units);
            _currentLevel.Update(gametime);

            // Update the gui
            _gui.SetTimer(_currentLevel.WaveCountdown);
            _gui.SetWaves(_currentLevel.WavesRemaining);
            _gui.SetMoney(RKOGame.Instance.getCurrency);
            _gui.SetHealth(RKOGame.Instance.getHealth);

            // Update the firewall
            _firewall.Update(gametime);

            if (_currentLevel.Completed)
            {
                if (RKOGame.Instance.getHighestCompletedLevel < _currentLevel.LevelValue)
                    RKOGame.Instance.getHighestCompletedLevel = _currentLevel.LevelValue;
                
                // Change the screens
                if(!ScreenManager.Instance.IsTransitioning)
                    ScreenManager.Instance.ChangeScreens(ScreenType.LevelSelect);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _currentLevel.Draw(spriteBatch);
            _firewall.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            _gui.Draw(spriteBatch);
        }
    }
}
