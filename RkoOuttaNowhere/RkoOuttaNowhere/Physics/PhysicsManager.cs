using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RkoOuttaNowhere.Gameplay.Units;
using RkoOuttaNowhere.Gameplay;
using RkoOuttaNowhere.Screens;

namespace RkoOuttaNowhere.Physics
{
    /// <summary>
    /// Collision Types Handled by the Physics Manager
    /// </summary>
    [Flags]
    public enum CollisionType
    {
        None = 0,
        AllyProjectilesVSEnemyUnits = 1,
        AllyUnitsVSEnemyUnits = 2,
        EnemyProjectilesVSAllyUnits = 4,
        EnemyProjectilesVSFirewall = 8,
        EnemyUnitsVSFirewall = 16
    }

    public class CollisionJob
    {
        public GameObject firstObject;
        public GameObject secondObject;
        public CollisionType type;

        public CollisionJob (CollisionType ctype, GameObject first, GameObject second)
        {
            firstObject = first;
            secondObject = second;
            type = ctype;
        }
    }

    /// <summary>
    /// Does all the fancy collision detection and handling
    /// </summary>
    public class PhysicsManager
    {
        private static PhysicsManager _instance;
        private List<Projectile> _allyProjectiles;
        private List<Unit> _allyUnits;
        private List<Projectile> _enemyProjectiles;
        private List<Unit> _enemyUnits;
        private GameObject _firewall; //TODO: get some references up in here

        public const double Gravity = 200;

        private Queue<CollisionJob> _collisionQueue;

        public static PhysicsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhysicsManager();
                }
                return _instance;
            }
        }

        public PhysicsManager()
        {
            _allyProjectiles = new List<Projectile>();
            _allyUnits = new List<Unit>();
            _enemyProjectiles = new List<Projectile>();
            _enemyUnits = new List<Unit>();
            _firewall = null;

            _collisionQueue = new Queue<CollisionJob>();
        }

        /// <summary>
        /// Finds all valid collisions on screen, bulky operation #1
        /// </summary>
        public void FindCollisions()
        {
            //Welcome to another episode of Brute Forcing: The Right Way
            //Today we'll be brute forcing collision culling
            //What's that? You're not brute forcing if you're culling?
            //Bullsh**
            //Watch this kiddies.
            float testValue = 0, newValue = 0;
            List<Projectile> allyProjectileRemovalList = new List<Projectile>();
            List<Unit> enemyUnitsRemovalList = new List<Unit>();

            List<Unit> enemyUnitsCloseToAllyProjectile = new List<Unit>();
            foreach(Projectile p in _allyProjectiles)
            {
                if (!p.IsCollidable) continue;

                enemyUnitsCloseToAllyProjectile = _enemyUnits.Where(x => x.IsCollidable && Vector2.DistanceSquared(p.HitBox.Position, x.HitBox.Position) < Math.Max(p.HitBox.RangeThreshold * p.HitBox.RangeThreshold, x.HitBox.RangeThreshold*x.HitBox.RangeThreshold)).ToList();

                newValue = Vector2.DistanceSquared(p.HitBox.Position, _enemyUnits[0].HitBox.Position);

                if(testValue != newValue)
                {
                    testValue = newValue;

                }

                foreach(Unit eu in enemyUnitsCloseToAllyProjectile)
                {
                    if (Intersects(p.HitBox, eu.HitBox))
                    {
                        _collisionQueue.Enqueue(new CollisionJob(CollisionType.AllyProjectilesVSEnemyUnits, p, eu));
                        if (eu.Damage(p.Damage)) enemyUnitsRemovalList.Add(eu);
                        p.OnDestroy();
                        allyProjectileRemovalList.Add(p);
                    }
                }

                enemyUnitsCloseToAllyProjectile.Clear();
            }
            foreach(Projectile p in allyProjectileRemovalList)
            {
                _allyProjectiles.Remove(p);
            }
            foreach(Unit u in enemyUnitsRemovalList)
            {
                _enemyUnits.Remove(u);
            }

            List<Unit> allyUnitsRemovalList = new List<Unit>();
            enemyUnitsRemovalList.Clear();
            List<Unit> enemyUnitsCloseToAllyUnits = new List<Unit>();
            foreach(Unit u in _allyUnits)
            {
                if (!u.IsCollidable) continue;

                enemyUnitsCloseToAllyUnits = _enemyUnits.Where(x => x.IsCollidable && Vector2.DistanceSquared(u.HitBox.Position, x.HitBox.Position) < Math.Max(u.HitBox.RangeThreshold, x.HitBox.RangeThreshold)).ToList();

                foreach(Unit eu in enemyUnitsCloseToAllyUnits)
                {
                    if (Intersects(u.HitBox, eu.HitBox))
                    {
                        //TODO: Unit v. Unit encounters
                    }
                }

                enemyUnitsCloseToAllyUnits.Clear();
            }

            allyUnitsRemovalList.Clear();
            List<Projectile> enemyProjectileRemovalList = new List<Projectile>();

            List<Projectile> enemyProjectilesCloseToAllyUnits = new List<Projectile>();
            foreach (Unit u in _allyUnits)
            {
                if (!u.IsCollidable) continue;

                enemyProjectilesCloseToAllyUnits = _enemyProjectiles.Where(x => x.IsCollidable && Vector2.DistanceSquared(u.HitBox.Position, x.HitBox.Position) < Math.Max(u.HitBox.RangeThreshold, x.HitBox.RangeThreshold)).ToList();

                foreach(Projectile ep in enemyProjectilesCloseToAllyUnits)
                {
                    if (Intersects(u.HitBox, ep.HitBox))
                    {
                        if (u.Damage(ep.Damage)) allyUnitsRemovalList.Add(u);
                        ep.OnDestroy();
                        enemyProjectileRemovalList.Add(ep);
                    }
                }

                enemyProjectilesCloseToAllyUnits.Clear();
            }
            foreach(Unit u in allyUnitsRemovalList)
            {
                _allyUnits.Remove(u);
            }
            foreach(Projectile p in enemyProjectileRemovalList)
            {
                _enemyProjectiles.Remove(p);
            }

            //This will handle the firewall stuff. Since we don't have a 
            //firewall up yet then we'll just box it off at the bottom here
            if (_firewall != null)
            {
                List<Projectile> enemyProjectilesCloseToFirewall = _enemyProjectiles.Where(x => Vector2.DistanceSquared(_firewall.HitBox.Position, x.HitBox.Position) < Math.Max(_firewall.HitBox.RangeThreshold, x.HitBox.RangeThreshold)).ToList();

                foreach(Projectile ep in enemyProjectilesCloseToFirewall)
                {
                    if (Intersects(_firewall.HitBox, ep.HitBox))
                    {
                        //TODO: What to do when enemies are shooting at the firewall (damage it obviously)
                    }
                }

                List<Unit> enemyUnitsCloseToFirewall = _enemyUnits.Where(x => Vector2.DistanceSquared(_firewall.HitBox.Position, x.HitBox.Position) < Math.Max(_firewall.HitBox.RangeThreshold, x.HitBox.RangeThreshold)).ToList();

                foreach(Unit eu in enemyUnitsCloseToFirewall)
                {
                    if (Intersects(_firewall.HitBox, eu.HitBox))
                    {
                        //TODO: Handle enemy units running into the wall
                    }
                }

                enemyProjectilesCloseToFirewall.Clear();
                enemyUnitsCloseToFirewall.Clear();
            }
        }

        private bool Intersects(HitBox hitbox1, HitBox hitbox2)
        {
            if(hitbox1.Type == HitBoxType.Circle)
            {
                if(hitbox2.Type == HitBoxType.Circle)
                {
                    return Intersects_Circle_V_Circle(hitbox1, hitbox2);
                }
                else if(hitbox2.Type == HitBoxType.Rectangle)
                {
                    return Intersects_Circle_V_Rectangle(hitbox1, hitbox2);
                }
            }
            else if(hitbox1.Type == HitBoxType.Rectangle)
            {
                if (hitbox2.Type == HitBoxType.Circle)
                {
                    return Intersects_Circle_V_Rectangle(hitbox2, hitbox1);
                }
                else if (hitbox2.Type == HitBoxType.Rectangle)
                {
                    return Intersects_Rectangle_V_Rectangle(hitbox1, hitbox2);
                }
            }

            return false;
        }

        private bool Intersects_Circle_V_Circle(HitBox circle1, HitBox circle2)
        {
            return circle1.Circle.Intersects(circle2.Circle);
        }

        private bool Intersects_Rectangle_V_Rectangle(HitBox rect1, HitBox rect2)
        {
            return rect1.Rectangle.Intersects(rect2.Rectangle);
        }

        private bool Intersects_Circle_V_Rectangle(HitBox circle, HitBox rect)
        {
            return circle.Circle.Intersects(rect.Rectangle);
        }

        /// <summary>
        /// Processes all collisions that were found, bulky operation #2
        /// </summary>
        public void ProcessCollisions()
        {
            while(_collisionQueue.Count > 0)
            {
                CollisionJob currentJob = _collisionQueue.Dequeue();

                switch(currentJob.type)
                {
                    case CollisionType.AllyProjectilesVSEnemyUnits:
                        break;

                    case CollisionType.AllyUnitsVSEnemyUnits:
                        break;

                    case CollisionType.EnemyProjectilesVSAllyUnits:
                        break;

                    case CollisionType.EnemyProjectilesVSFirewall:
                        break;

                    case CollisionType.EnemyUnitsVSFirewall:
                        break;

                    case CollisionType.None:
                    default:
                        //Do Nothing
                        break;
                }
            }
        }

        /// <summary>
        /// Add a projectile to the collidable projectile list
        /// </summary>
        /// <param name="p">The projectile to add</param>
        public void AddProjectile(Projectile p)
        {
            if(p.IsAlly)
            {
                if (!_allyProjectiles.Contains(p))
                    _allyProjectiles.Add(p);
            }
            else
            {
                if (!_enemyProjectiles.Contains(p))
                    _enemyProjectiles.Add(p);
            }
        }

        /// <summary>
        /// Add a unit to the collidable unit list
        /// </summary>
        /// <param name="u">The unit to add</param>
        public void AddUnit(Unit u)
        {
            if(u.IsAlly)
            {
                if (!_allyUnits.Contains(u))
                    _allyUnits.Add(u);
            }
            else
            {
                if (!_enemyUnits.Contains(u))
                    _enemyUnits.Add(u);
            }
        }

        /// <summary>
        /// Remove a projectile from the collidable projectile list
        /// </summary>
        /// <param name="p">The projectile to remove</param>
        public void RemoveProjectile(Projectile p)
        {
            if(p.IsAlly)
            {
                if (_allyProjectiles.Contains(p))
                    _allyProjectiles.Remove(p);
            }
            else
            {
                if (_enemyProjectiles.Contains(p))
                    _enemyProjectiles.Remove(p);
            }
        }

        /// <summary>
        /// Remove a unit from the collidable unit list
        /// </summary>
        /// <param name="u">The unit to remove</param>
        public void RemoveUnit(Unit u)
        {
            if(u.IsAlly)
            {
                if (_allyUnits.Contains(u))
                    _allyUnits.Remove(u);
            }
            else
            {
                if (_enemyUnits.Contains(u))
                    _enemyUnits.Remove(u);
            }
        }

        //public void SetFirewall(Firewall f) { }

        public void Update(GameTime gameTime)
        {
            List<Projectile> projectileRemovalList = new List<Projectile>();
            foreach(Projectile p in _allyProjectiles)
            {
                if (p.IsActive) p.Update(gameTime);

                if(p.HasGravity)
                {
                    Vector2 tempVel = new Vector2(p.Velocity.X * p.Speed, p.Velocity.Y * p.Speed);
                    tempVel.Y += (float)(Gravity * gameTime.ElapsedGameTime.TotalSeconds);
                    p.Speed = tempVel.Length();
                    tempVel.Normalize();
                    p.Velocity = tempVel;
                }

                if (p.Position.Y > ScreenManager.Instance.Dimensions.Y)
                {
                    p.OnDestroy();
                    projectileRemovalList.Add(p);
                    continue;
                }
            }

            foreach(Projectile p in projectileRemovalList)
            {
                _allyProjectiles.Remove(p);
            }
            projectileRemovalList.Clear();

            foreach(Projectile p in _enemyProjectiles)
            {
                if (p.HasGravity)
                {
                    Vector2 temp = p.HitBox.Position;
                    temp.Y += (float)(Gravity * gameTime.ElapsedGameTime.TotalSeconds);
                    p.HitBox.Position = temp;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Projectile p in _allyProjectiles)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}
