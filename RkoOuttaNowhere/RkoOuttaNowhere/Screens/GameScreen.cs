// GameScreen.cs
// James Tyson
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RkoOuttaNowhere.Images;
using RkoOuttaNowhere.Input;
using RkoOuttaNowhere.Ui;
using RkoOuttaNowhere.Gameplay;

namespace RkoOuttaNowhere.Screens
{
    /// <summary>
    /// GameScreen represents a base class for all screens
    /// </summary>
    public class GameScreen
    {
        protected ContentManager _content;
        protected Image _backgroundImage;
        protected Gui _gui;
        protected Player _player;

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameScreen()
        {
            _backgroundImage = new Image();
            _player = new Player();
        }

        public virtual void LoadContent()
        {
            _content = ScreenManager.Instance.Content;
        }

        public virtual void UnloadContent()
        {
            _content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();
            _backgroundImage.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _backgroundImage.Draw(spriteBatch);
        }
    }
}

