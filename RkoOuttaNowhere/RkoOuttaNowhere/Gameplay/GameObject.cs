using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RkoOuttaNowhere.Images;
using RkoOuttaNowhere.Screens;
using RkoOuttaNowhere.Physics;

namespace RkoOuttaNowhere.Gameplay
{
    public class GameObject
    {
        protected Vector2 _position { get { return HitBox.Position; } set { HitBox.Position = value; } }
        
        protected Vector2 _velocity, _dimensions;
        protected Image _image;
        protected bool _isVisible, _isActive, _isCollidable, _hasGravity;
        
        public HitBox HitBox { get; set; }

        public Vector2 Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public bool IsCollidable
        {
            get { return _isCollidable; }
            set { _isCollidable = value; }
        }

        public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }

        public bool HasGravity { get { return _hasGravity; } set { _hasGravity = value; } }

        public GameObject()
        {
            _dimensions = Vector2.Zero;
            this.HitBox = new RectangularHitBox(Vector2.Zero, (int)_dimensions.X, (int)_dimensions.Y);
            _position = _velocity = Vector2.Zero;
            _image = new Image();
            _isVisible = _isActive = true;
            _isCollidable = true;
        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {
            _image.UnloadContent();
        }

        public virtual void Update(GameTime gametime)
        {
            _image.Position = _position;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            _image.Draw(spritebatch);
        }

        public virtual void OnDestroy()
        {
            
        }

        public virtual void SetPosition(Vector2 pos)
        {
            _position = pos;
            _image.Position = pos;
            HitBox.Position = pos;
        }

        public Rectangle GetRect()
        {

            Rectangle r = _image.SourceRect;
            r.X = (int)_image.Position.X;
            r.Y = (int)_image.Position.Y;
            return r;
        }
    }
}
