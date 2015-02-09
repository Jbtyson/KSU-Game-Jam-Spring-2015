using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RkoOuttaNowhere.Images;

namespace RkoOuttaNowhere.Gameplay
{
    public class Firewall : GameObject
    {
        private Image[] _images;
        private int _currentImg;

        public Firewall()
            : base()
        {
            _images = new Image[4];
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _position = new Vector2(800, 0);

            _images[0] = new Image();
            _images[0].Path = "Gameplay/wall";
            _images[0].Position = _position;
            _images[0].LoadContent();
            _images[1] = new Image();
            _images[1].Path = "Gameplay/wall_75";
            _images[1].Position = _position;
            _images[1].LoadContent();
            _images[2] = new Image();
            _images[2].Path = "Gameplay/wall_50";
            _images[2].Position = _position;
            _images[2].LoadContent();
            _images[3] = new Image();
            _images[3].Path = "Gameplay/wall_25";
            _images[3].Position = _position;
            _images[3].LoadContent();

            Image = _images[0];
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            Image.Update(gametime);

            if (RKOGame.Instance.getHealth <= 25)
            {
                Image = _images[3];
            }
            else if (RKOGame.Instance.getHealth <= 50)
            {
                Image = _images[2];
            }
            else if (RKOGame.Instance.getHealth <= 75)
            {
                Image = _images[1];
            }
            else
            {
                Image = _images[0];
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            Image.Draw(spritebatch);
        }
    }
}
