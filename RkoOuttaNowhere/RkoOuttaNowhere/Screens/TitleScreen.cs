﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RkoOuttaNowhere.Input;
using RkoOuttaNowhere.Ui;

namespace RkoOuttaNowhere.Screens
{
    public class TitleScreen : GameScreen
    {
        public TitleScreen()
            : base()
        {
            _gui = new TitleGui();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            // Load the background image
            _backgroundImage.Path = "backgrounds/tempTitle";
            _backgroundImage.LoadContent();

            _backgroundImage.ActivateEffect("SpriteSheetEffect");
            _backgroundImage.IsActive = true;

            // Load the gui
            _gui.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            // Unload the gui
            _gui.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Update the gui
            _gui.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            // Draw the gui
            _gui.Draw(spriteBatch);
        }

        public static void OnNewGameClick()
        {
            ScreenManager.Instance.ChangeScreens(ScreenType.LevelSelect);
        }
        public static void OnLoadGameClick()
        {
            ScreenManager.Instance.ChangeScreens(ScreenType.LevelSelect);
        }
        public static void OnOptionsClick()
        {
            ScreenManager.Instance.ChangeFast(ScreenType.GameOver);
        }
        public static void OnExitClick()
        {
            TowerDefense.ExitGame();
        }
    }
}
