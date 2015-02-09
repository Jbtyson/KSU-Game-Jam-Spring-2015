using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RkoOuttaNowhere.Images;
using RkoOuttaNowhere.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RkoOuttaNowhere.Gameplay.Projectiles
{
     class Fire : Projectile
    {
        public Fire(Vector2 start, Vector2 dest, float dmg)
        {
            _position = start;
            _velocity = dest - start;
            if (_velocity != Vector2.Zero)
                _velocity.Normalize();
            Image = new Image();
        }

        public void LoadContent(bool isAlly = false) 
        {
            Image.Path = "Gameplay/Fire";
            Image.Position = _position;
            Image.LoadContent();
            this.IsAlly = isAlly;
            this.HasGravity = true;
            this.HitBox = new CircularHitBox(_position, Math.Max(Image.SourceRect.Width, Image.SourceRect.Height));
            PhysicsManager.Instance.AddProjectile(this);
        }

        public override void UnloadContent() 
        {
            Image.UnloadContent();
        }

        public override void Update(GameTime gametime) 
        {
            this.HitBox.Position += _velocity * _speed * (float)gametime.ElapsedGameTime.TotalSeconds;
            Image.Position = this.HitBox.Position;
            Image.Update(gametime);            
        }

        public override void Draw(SpriteBatch spritebatch) 
        {
            Image.Draw(spritebatch);
        }
    }
}
