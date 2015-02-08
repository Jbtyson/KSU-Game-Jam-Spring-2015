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
using RkoOuttaNowhere.Images;

namespace RkoOuttaNowhere.Screens
{
    public class UpgradeScreen : GameScreen
    {

        private Button damageMod, healthMod, moneyMod;

        private Image _tip, _money;

        public UpgradeScreen()
            : base()
        {
            damageMod = new Button();
            healthMod = new Button();
            moneyMod = new Button();
            _tip = new Image();
            _money = new Image();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _backgroundImage.Path = "backgrounds/upgrade";
            _backgroundImage.LoadContent();
            damageMod.LoadContent("ui/upgrades/damage", new Vector2(200.0f, 350.0f), upgradeDamage);
            healthMod.LoadContent("ui/upgrades/health", new Vector2(200.0f, 450.0f), upgradeHealth);
            moneyMod.LoadContent("ui/upgrades/money", new Vector2(200.0f, 550.0f), upgradeMoney);

            _tip.Text = "Press Space to go back";
            _tip.Position = new Vector2(5, 5);
            _tip.Path = "transparent";
            _tip.LoadContent();

            _money.Text = RKOGame.Instance.getCurrency.ToString();
            _money.Position = new Vector2(800, 5);
            _money.Scale = new Vector2(2, 2);
            _money.Path = "transparent";
            _money.LoadContent();
        }

       

        public override void UnloadContent()
        {
            base.UnloadContent();
            damageMod.UnloadContent();
            healthMod.UnloadContent();
            moneyMod.UnloadContent();
            _tip.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.Instance.KeyPressed(Keys.Space))
            {
                ScreenManager.Instance.ChangeScreens(RKOGame.Instance.LastScreen);
            }
            damageMod.Update(gameTime);
            healthMod.Update(gameTime);
            moneyMod.Update(gameTime);
            _tip.Update(gameTime);
            _money.Text = RKOGame.Instance.getCurrency.ToString();
            _money.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            damageMod.Draw(spriteBatch);
            healthMod.Draw(spriteBatch);
            moneyMod.Draw(spriteBatch);
            _tip.Draw(spriteBatch);
            _money.RedrawText(spriteBatch, Color.White);
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
