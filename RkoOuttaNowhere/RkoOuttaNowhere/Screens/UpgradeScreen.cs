using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RkoOuttaNowhere.Input;
using System.IO;
using RkoOuttaNowhere.Gameplay;
using RkoOuttaNowhere.Ui;

namespace RkoOuttaNowhere.Screens
{
    public class UpgradeScreen : GameScreen
    {

        private Button damageMod, healthMod, moneyMod;
        public UpgradeScreen()
            : base()
        {
            damageMod = new Button();
            healthMod = new Button();
            moneyMod = new Button();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _backgroundImage.Path = "backgrounds/upgrade";
            _backgroundImage.LoadContent();
            damageMod.LoadContent("ui/upgrades/damage", new Vector2(200.0f, 350.0f), upgradeDamage);
            healthMod.LoadContent("ui/upgrades/health", new Vector2(200.0f, 450.0f), upgradeHealth);
            moneyMod.LoadContent("ui/upgrades/money", new Vector2(200.0f, 550.0f), upgradeMoney);
        }

       

        public override void UnloadContent()
        {
            base.UnloadContent();
            damageMod.UnloadContent();
            healthMod.UnloadContent();
            moneyMod.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.Instance.KeyPressed(Keys.G))
            {
                ScreenManager.Instance.ChangeScreens(ScreenType.Gameplay);
            }
            else if (InputManager.Instance.KeyPressed(Keys.L))
            {
                ScreenManager.Instance.ChangeScreens(ScreenType.LevelSelect);
            }
            damageMod.Update(gameTime);
            healthMod.Update(gameTime);
            moneyMod.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            damageMod.Draw(spriteBatch);
            healthMod.Draw(spriteBatch);
            moneyMod.Draw(spriteBatch);
        }


        private void upgradeDamage(Object o, EventArgs e)
        {
            Upgrade.DamageBoost += .25f;
        }

        private void upgradeHealth(Object o, EventArgs e)
        {
            Upgrade.HealthIncrease += 25;
        }

        private void upgradeMoney(Object o, EventArgs e)
        {
            Upgrade.MoneyBoost += .25f;
        }







    }
}
