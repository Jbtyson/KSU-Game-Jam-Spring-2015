using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RkoOuttaNowhere.Input;
using RkoOuttaNowhere.Physics;

namespace RkoOuttaNowhere.Gameplay.Units
{
    public class Unit : GameObject
    {
        private int _health, _maxHealth, _baseMoney;
        private float _moveSpeed;
        private bool _moving;
        private float _dps;
        private int _attackTimerMax = 1000, _attackTimer = 0; //Timer set to attack every 1 second
        private Behaviour _behaviour;

        public int Health 
        { 
            get { return _health; } 
            set { _health = value; } 
        }

        public bool IsAlly { get; set; }

        public Unit()
            : base()
        {
            _behaviour = Behaviour.BasicMove;
        }

        public void LoadContent(string path, Vector2 position, float movespeed, int maxHealth, int baseMoney, float dps, Behaviour behaviour, bool isally = false)
        {
            base.LoadContent();

            _position = position;
            _image.Path = path;
            _image.Position = position;
            _image.LoadContent();
            _image.ActivateEffect("SpriteSheetEffect");
            _image.IsActive = true;
            _image.SpriteSheetEffect.AmountOfFrames = new Vector2(3, 1);
            _image.SpriteSheetEffect.SwitchFrame = 100;
            
            _dimensions = new Vector2(_image.SourceRect.Width, _image.SourceRect.Height);
            _baseMoney = baseMoney;

            this.HitBox = new RectangularHitBox(position, (int)_dimensions.X, (int)_dimensions.Y);

            _moveSpeed = movespeed;
            _maxHealth = _health = maxHealth;
            _behaviour = behaviour;
            _moving = true;
            _dps = dps;
            this.IsAlly = isally;

            PhysicsManager.Instance.AddUnit(this);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            if (_moving)
            {
                if (_behaviour == Behaviour.BasicMove)
                {
                    Vector2 temp = _position;
                    temp.X += (float)(_moveSpeed * gametime.ElapsedGameTime.TotalSeconds);
                    _position = temp;
                }

                if (_position.X + _image.SourceRect.Width >= 800)
                    _moving = false;

            }
            else if(!_moving)
            {
                if (!_moving)
                {
                    _attackTimer += (int)gametime.ElapsedGameTime.TotalMilliseconds;

                    if (_attackTimer >= _attackTimerMax)
                    {
                        _attackTimer = 0;
                        //attack firewall
                        RKOGame.Instance.getHealth -= (int)_dps;
                    }
                }
            }

            _image.Update(gametime);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }

        public override void OnDestroy() 
        {
            _isActive = false;
            RKOGame.Instance.AddMoney(_baseMoney);
        }

        public bool Damage(float damage)
        {
            this.Health -= (int)damage;
            if (this.Health <= 0)
            {
                this.OnDestroy();
                return true;
            }
            return false;
        }
    }
}
