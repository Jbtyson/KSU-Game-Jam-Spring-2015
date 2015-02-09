using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RkoOuttaNowhere.Input;

namespace RkoOuttaNowhere.Screens
{
    public class GameOverScreen : GameScreen
    {
        public GameOverScreen()
            : base()
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
            _backgroundImage.Path = "backgrounds/game_over";
            _backgroundImage.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Space))
            {
                TowerDefense.ExitGame();
            }
            if (InputManager.Instance.KeyPressed(Keys.F10))
            {
                ScreenManager.Instance.ChangeScreens(ScreenType.Victory);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
