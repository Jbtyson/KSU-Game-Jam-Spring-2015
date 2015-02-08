using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RkoOuttaNowhere.Images;
using RkoOuttaNowhere.Input;
using RkoOuttaNowhere.Screens;
using RkoOuttaNowhere.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RkoOuttaNowhere.Gameplay
{
    public class Projectile : GameObject
    {
        protected Vector2 _destination;
        protected float _speed = 400.0f, _damage;
        protected Upgrades.ammunition ammo;

        public bool IsAlly { get; set; }

        public Projectile()
        {
            _destination = Vector2.Zero;
            this.IsAlly = false;
            _damage = Upgrade.Fire;
        }

        public void LoadContent(bool isAlly = false)
        {
            _image.Path = "Gameplay/" + ammo.ToString();
            _image.Position = _position;
            _image.LoadContent();
            this.IsAlly = isAlly;

            PhysicsManager.Instance.AddProjectile(this);
        }

        public override void UnloadContent()
        {
            _image.UnloadContent();
        }

        public override void Update(GameTime gametime)
        {
            this.HitBox.Position += _velocity * _speed * (float)gametime.ElapsedGameTime.TotalSeconds;
            _image.Position = this.HitBox.Position;
            _image.Update(gametime);
            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            _image.Draw(spritebatch);
        }

        public Vector2 Position { get { return _position; } }
        public float Damage { get { return (Upgrade.DamageBoost * _damage); } set { _damage = value; } }
        public float Speed { get { return _speed; } set { _speed = value; } }
        public Upgrades.ammunition Ammo { get { return ammo; } set { ammo = value; } }

        public override void OnDestroy()
        {
            _isActive = false;
        }

    }
}
