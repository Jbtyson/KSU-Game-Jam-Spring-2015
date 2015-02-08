using Microsoft.Xna.Framework;
using RkoOuttaNowhere.Gameplay.Projectiles;
using RkoOuttaNowhere.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RkoOuttaNowhere.Gameplay
{
    class ProjectileFactory
    {
        public const int GUN_DAMAGE = 5;
        public const int FIRE_DAMAGE = 10;
        public const int LASER_DAMAGE = 15;

        public static Projectile Shoot(Vector2 _position, int damageMod, Upgrades.ammunition x, bool isAlly = false)
        {
            if (Upgrades.ammunition.Laser == x)
                return CreateLaserShot(_position, damageMod, isAlly);
            else if (Upgrades.ammunition.Fire == x)
                return CreateFireShot(_position, damageMod, isAlly);
            else if (Upgrades.ammunition.Gun == x)
                return CreateGunShot(_position, damageMod, isAlly);
            return null;
        }

        public static Projectile CreateGunShot(Vector2 _position, int damageMod, bool isAlly = false)
        {
            return null;
        }

        public static Projectile CreateFireShot(Vector2 _position, int damageMod, bool isAlly = false)
        {
            Fire f = new Fire(_position, new Vector2(InputManager.Instance.MousePosition.X, InputManager.Instance.MousePosition.Y), FIRE_DAMAGE * Upgrade.DamageBoost);
            f.LoadContent(isAlly);
            return f;
        }

        public static Projectile CreateLaserShot(Vector2 _position, int damageMod, bool isAlly = false)
        {
            Laser l = new Laser(_position, new Vector2(InputManager.Instance.MousePosition.X, InputManager.Instance.MousePosition.Y), LASER_DAMAGE * Upgrade.DamageBoost);
            l.LoadContent(isAlly);
            return l;
        }
    }
}
